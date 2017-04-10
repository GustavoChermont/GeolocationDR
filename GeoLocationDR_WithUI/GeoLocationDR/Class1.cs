using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OSIsoft.AF;
using OSIsoft.AF.Asset;
using OSIsoft.AF.Time;
using OSIsoft.AF.UnitsOfMeasure;
using OSIsoft.AF.Data;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Web.Script.Serialization;
using System.Net;
using System.IO;
using System.Web;

namespace GeolocationDR
{
    [Guid("58ACC7BE-3A48-4DAE-8D5B-BD5DC1A16BC7"), Serializable(), Description("Geolocation;Convert coordinates")]

    public class Geolocation : AFDataReference
    {
        String latAttribute = String.Empty;
        String longAttribute = String.Empty;
        String addrAttribute = String.Empty;
        int attributeType = -1; //not set
        String currPath = String.Empty;
        AFElement currElement = new AFElement();
        AFAttribute currAttribute = null;

        //Gets or Sets the source latitude attribute
        public String sourceLatitude
        {
            get
            {
                return latAttribute;
            }
            set
            {
                if (latAttribute != value)
                {
                    latAttribute = value;
                    SaveConfigChanges();
                }
            }
        }

        //Gets or Sets the source longitude attribute
        public String sourceLongitude
        {
            get
            {
                return longAttribute;
            }
            set
            {
                if (longAttribute != value)
                {
                    longAttribute = value;
                    SaveConfigChanges();
                }
            }
        }

        //Gets or Sets the source longitude attribute
        public String sourceAddress
        {
            get
            {
                return addrAttribute;
            }
            set
            {
                if (addrAttribute != value)
                {
                    addrAttribute = value;
                    SaveConfigChanges();
                }
            }
        }

        //Gets or Sets the target Attribute type
        public int targetAttributeType
        {
            get
            {
                return attributeType;
            }
            set
            {
                if (attributeType != value)
                {
                    attributeType = value;
                    SaveConfigChanges();
                }
            }
        }

        //Gets or Sets the configstring
        public override string ConfigString 
        {
            get
            {
                StringBuilder sb = new StringBuilder();

                GetCurrentObjectPaths(); //Gets the current element and attribute

                //Converts the integer code to the return Type names
                switch (attributeType)
                {
                    case 0://output to address
                        if (String.IsNullOrEmpty(latAttribute) || String.IsNullOrEmpty(longAttribute))//verifies if the variables were already set
                            sb.AppendFormat("Coordinates Not Set");
                        else
                        {
                            sb.AppendFormat("{0}={1};", "Latitude", latAttribute);//Shows the Latitude attribute name
                            sb.AppendFormat("{0}={1};", "Longitude", longAttribute);//Shows the Longitude attribute name
                        }
                        sb.AppendFormat("{0}={1};", "OutType", "Address");//Adds the output type to the connection string
                        break;
                    case 1://output to Lat and Long
                        sb.AppendFormat("{0}={1};", "Address", addrAttribute);
                        sb.AppendFormat("{0}={1};", "OutType", "Latitude_Longitude");
                        break;
                    default:
                        sb.AppendFormat("parameters not set");
                        break;
                }
                return sb.ToString();
            }

            set //called by hitting the Settings button or typing the Config String directly
            {
                GetCurrentObjectPaths(); //Gets the current element and attribute

                if (value != null)
                {

                    var tokens = value.Split(';');//value contains the configuration string from PSE

                    foreach (var token in tokens)//gets each term before the ';' character
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
                            case "address": //sets config for longitude attribute
                                addrAttribute = keyvalue[1];
                                break;
                            case "outtype":
                                switch (keyvalue[1].ToLower()) //Sets config for the type attribute
                                {
                                    case "address": 
                                        attributeType = 0;
                                        break;
                                    case "latitude_longitude":
                                        attributeType = 1;
                                        break;
                                }
                                break;
                        }
                    }
                }
                SaveConfigChanges();//makes the changes persistent
            }
        }

        //
        public override AFAttributeList GetInputs(object context)
        {
            AFAttributeList inputs = new AFAttributeList();

            if (!String.IsNullOrEmpty(latAttribute))
            {
                inputs.Add(this.GetAttribute(latAttribute));//Adds the Latitude Attribute to the input attribute colletion
            }

            if (!String.IsNullOrEmpty(longAttribute))
            {
                inputs.Add(this.GetAttribute(longAttribute));//Adds the Longitude Attribute to the input attribute colletion
            }

            if (!String.IsNullOrEmpty(addrAttribute))
            {
                inputs.Add(this.GetAttribute(addrAttribute));//Adds the Address Attribute to the input attribute colletion
            }
            return inputs;
        }

        public override AFValue GetValue(object context, object timeContext, AFAttributeList inputAttributes, AFValues inputValues)
        {
            AFValue value = new AFValue();
            AFValue result = new AFValue();
             
            if (inputAttributes.Count != 0) //Checks if the attributes are set
            {
                switch (attributeType) //verifies the output type
                {
                    case 0:
                        value = GetAddressByCoordinates(inputValues[0].ToString() , inputValues[1].ToString());//Gets data from Geocoding API
                        break;
                    case 1:
                        value = GetAddressByAddress(inputValues[0].ToString());//Gets data from Geocoding API

                        CreateAttributes("Latitude", "Longitude");
                        try
                        {
                            currAttribute.Attributes["Latitude"].SetValue(GetLatitudeByAddress(value.Value.ToString()));
                            currAttribute.Attributes["Longitude"].SetValue(GetLongitudeByAddress(value.Value.ToString()));
                        }
                        catch
                        {

                        }
                        break;
                }
                result.Timestamp = inputValues[0].Timestamp;//Sets the attribute timestamp
                result.Value = value.Value;//Sets the attribute value
            }
            else//returns bad if the inputs are not set
            {
                result.Status = AFValueStatus.Bad;
                result.Value = AFSystemStateCode.BadInput;
            }
            return result;
        }

        public AFValue GetLatitudeByAddress(String address)
        {
            AFValue latitude = new AFValue();

            //this free key allows us to do only 2500 requests per day / 50 requests per second
            string url = "https://maps.googleapis.com/maps/api/geocode/json?address=" + address + "&key=AIzaSyBsvOTS7S6oN9ia6jXPb2Mz9C-gTcCChKg";

            //Handles the JSON response from url
            Dictionary<string, dynamic> result = LoadJson(url);//Handles the JSON response from url

            try
            {
                string textResult = result["results"][0]["geometry"]["location"]["lat"].ToString();
                latitude.Value = textResult;
            }
            catch
            {
                latitude.Status = AFValueStatus.Bad;
                latitude.Value = AFSystemStateCode.BadInput;
            }

            return latitude;
        }

        public AFValue GetAddressByAddress(String address)
        {
            AFValue latitude = new AFValue();

            //this free key allows us to do only 2500 requests per day / 50 requests per second
            string url = "https://maps.googleapis.com/maps/api/geocode/json?address=" + address + "&key=AIzaSyBsvOTS7S6oN9ia6jXPb2Mz9C-gTcCChKg";

            //Handles the JSON response from url
            Dictionary<string, dynamic> result = LoadJson(url);//Handles the JSON response from url

            try
            {
                String textResult = result["results"][0]["formatted_address"].ToString();
                latitude.Value = textResult;
            }
            catch
            {
                latitude.Status = AFValueStatus.Bad;
                latitude.Value = AFSystemStateCode.BadInput;
            }

            return latitude;
        }

        public AFValue GetLongitudeByAddress(String address)
        {
            AFValue longitude = new AFValue();

            //this free key allows us to do only 2500 requests per day / 50 requests per second
            string url = "https://maps.googleapis.com/maps/api/geocode/json?address=" + address + "&key=AIzaSyBsvOTS7S6oN9ia6jXPb2Mz9C-gTcCChKg";

            //Handles the JSON response from url
            Dictionary<string, dynamic> result = LoadJson(url);//Handles the JSON response from url

            try
            {
                string textResult = result["results"][0]["geometry"]["location"]["lng"].ToString();
                longitude.Value = textResult;
            }
            catch
            {
                longitude.Status = AFValueStatus.Bad;
                longitude.Value = AFSystemStateCode.BadInput;
            }

            return longitude;
        }

        public AFValue GetAddressByCoordinates(String latitude, String longitude)
        {
            AFValue address = new AFValue();

            string url = "https://maps.googleapis.com/maps/api/geocode/json?latlng=" + latitude + "," + longitude + "&key=AIzaSyBsvOTS7S6oN9ia6jXPb2Mz9C-gTcCChKg";

            //Handles the JSON response from url
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

        public override Type EditorType
        {
            get
            {
                return typeof(DREditor);
            }
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

        public bool CreateAttributes(string attrName1, string attrName2)
        {
            try
            {  
                AFAttribute latAttr = currAttribute.Attributes.Add(attrName1);
                latAttr.IsConfigurationItem = false;
                
                AFAttribute longAttr = currAttribute.Attributes.Add(attrName2);
                longAttr.IsConfigurationItem = false;

                SaveConfigChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public void GetCurrentObjectPaths()
        {
            int pathIndex = Path.IndexOf('|');
            String elemetPath = Path.Substring(0, pathIndex);
            String attributePath = Path.Substring(pathIndex + 1, Path.Length - pathIndex - 1);

            currElement = Database.Elements[elemetPath];
            currAttribute = currElement.Attributes[attributePath];
        }

    }
}
