using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OSIsoft.AF;
using OSIsoft.AF.Asset;
using OSIsoft.AF.Time;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Web.Script.Serialization;
using System.Net;
using System.IO;

namespace GeolocationDR
{
    [Guid("58ACC7BE-3A48-4DAE-8D5B-BD5DC1A16BC7"), Serializable(), Description("Geolocation;Convert coordinates")]

    public class Geolocation : AFDataReference
    {
        String latAttribute = String.Empty;
        String longAttribute = String.Empty;
        String addrAttribute = String.Empty;

        //Gets or Sets the configstring
        public override string ConfigString
        {
            get
            {
                StringBuilder sb = new StringBuilder();

                if (String.IsNullOrEmpty(latAttribute) || String.IsNullOrEmpty(longAttribute))//verifies if the variables were already set
                    sb.AppendFormat("Coordinates Not Set;");
                else
                {
                    sb.AppendFormat("{0}={1};", "Latitude", latAttribute); //Shows the Latitude attribute name
                    sb.AppendFormat("{0}={1};", "Longitude", longAttribute); //Shows the Longitude attribute name
                }
                sb.AppendFormat("{0}={1};", "OutType", "Address"); //Adds the output type to the connection string
                return sb.ToString();
            }

            set //called by hitting the Settings button or typing the Config String directly
            {
                if (value != null) //value contains the configuration string from PSE
                {
                    var tokens = value.Split(';'); //gets each term before the ';' character

                    foreach (var token in tokens)
                    {
                        var keyvalue = token.Split('=');

                        switch (keyvalue[0].ToLower())
                        {
                            case "latitude": //sets config for lattude attribute
                                latAttribute = keyvalue[1];
                                break;
                            case "longitude": //sets config for longitude attribute
                                longAttribute = keyvalue[1];
                                break;
                        }
                    }
                }
                SaveConfigChanges(); //makes the changes persistent
            }
        }

        //Gets the input Attributes
        public override AFAttributeList GetInputs(object context)
        {
            AFAttributeList inputs = new AFAttributeList();

            if (!String.IsNullOrEmpty(latAttribute))
            {
                inputs.Add(this.GetAttribute(latAttribute)); //Adds the Latitude Attribute to the input attribute colletion
            }

            if (!String.IsNullOrEmpty(longAttribute))
            {
                inputs.Add(this.GetAttribute(longAttribute)); //Adds the Longitude Attribute to the input attribute colletion
            }

            return inputs;
        }

        public override AFValue GetValue(object context, object timeContext, AFAttributeList inputAttributes, AFValues inputValues)
        {
            AFValue value = new AFValue();
            AFValue result = new AFValue();

            if (inputAttributes.Count == 2) //Checks if both Latitude and Longitude attributes are set
            {
                value = GetAddressByCoordinates(inputValues[0].ToString(), inputValues[1].ToString()); //Gets data from Geocoding API

                result.Timestamp = inputValues[0].Timestamp; //Sets the attribute timestamp
                result.Value = value.Value; //Sets the attribute value
            }
            else //returns bad if the inputs are not set
            {
                result.Status = AFValueStatus.Bad; 
                result.Value = AFSystemStateCode.BadInput;
            }
            return result;
        }

        public AFValue GetAddressByCoordinates(String latitude, String longitude)
        {
            AFValue address = new AFValue();

            string url = "https://maps.googleapis.com/maps/api/geocode/json?latlng=" + latitude + "," + longitude + "&key=AIzaSyBsvOTS7S6oN9ia6jXPb2Mz9C-gTcCChKg";

            Dictionary<string, dynamic> result = LoadJson(url);//Handles the JSON response from url

            try
            {
                String textResult = result["results"][0]["formatted_address"];
                address.Value = textResult;
            }
            catch
            {
                address.Status = AFValueStatus.Bad;
                address.Value = AFSystemStateCode.BadInput;
            }
            return address;
        }

        static public Dictionary<string, dynamic> LoadJson(String url)
        {
            WebRequest request = WebRequest.Create(url);//Creates a request to the server            
            WebResponse webResponse = request.GetResponse();//Gets the web response            
            Stream dataStream = webResponse.GetResponseStream();//Gets a stream with the JSON response            
            StreamReader reader = new StreamReader(dataStream);//Gets a StreamReader with the JSON response            
            String text = reader.ReadToEnd();//creates a string with the JSON text

            var jss = new JavaScriptSerializer();//Deserializes the JSON string
            var result = jss.Deserialize<Dictionary<string, dynamic>>(text);

            return result;
        }
    }
}

