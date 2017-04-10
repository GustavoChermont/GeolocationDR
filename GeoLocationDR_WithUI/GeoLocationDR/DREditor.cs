using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OSIsoft.AF;
using OSIsoft.AF.Asset;
using OSIsoft.AF.Time;
using OSIsoft.AF.UnitsOfMeasure;
using OSIsoft.AF.Data;
using System.Runtime.InteropServices;


namespace GeolocationDR
{
    public partial class DREditor : Form
    {

        int attributeType = -1; //Default = none
        Geolocation myDR = new Geolocation();

        public DREditor(Geolocation dr, bool isReadOnly)
        {
            InitializeComponent();

            myDR = dr;

            int pathIndex = dr.Path.IndexOf('|');
            String elemetPath = dr.Path.Substring(0, pathIndex);
            String attributePath = dr.Path.Substring(pathIndex + 1, dr.Path.Length - pathIndex - 1);

            AFElement currElement = dr.Database.Elements[elemetPath];

            //Populates the attribute list in the ListBox. This data reference supports only the first level of child attributes
            foreach (AFAttribute attr in currElement.Attributes)
            {
                if (attributePath != attr.ToString())
                {
                    cbLatitude.Items.Add(attr.ToString());
                    cbLongitude.Items.Add(attr.ToString());
                }
                foreach (AFAttribute subAttr in attr.Attributes)
                {
                    if (attributePath != attr.ToString() + '|' + subAttr.ToString())
                    {
                        cbLatitude.Items.Add(attr.ToString() + '|' + subAttr.ToString());
                        cbLongitude.Items.Add(attr.ToString() + '|' + subAttr.ToString());
                    }
                }
            }
        }

        private void rbAddress_CheckedChanged(object sender, EventArgs e)
        {
            if (rbAddress.Checked)
            {
                cbLongitude.Visible = true;
                cbLatitude.Text = "Latitude Attribute";
                label3.Text = "Returns the formal Address from the selected Latitude and Longitude attributes.";
                attributeType = 0;
            }
        }

        private void rbLatLong_CheckedChanged(object sender, EventArgs e)
        {
            if (rbLatLong.Checked)
            {
                cbLongitude.Visible = false;
                cbLatitude.Text = "Location Attribute";
                label3.Text = "Returns the address, the Latitude and Longitude values from the selected location attribute.";
                attributeType = 1;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (attributeType == 0)
            {
                if (cbLatitude.SelectedIndex != -1) //checks if the ComboBox is still empty
                    myDR.sourceLatitude = cbLatitude.SelectedItem.ToString(); //Sets the sourceLatitude property
                else
                    return;

                if (cbLongitude.SelectedIndex != -1) //checks if the ComboBox is still empty
                    myDR.sourceLongitude = cbLongitude.SelectedItem.ToString(); //Sets the sourceLongitude property
                else
                    return;
            }
            else if (attributeType == 1)
                myDR.sourceAddress = cbLatitude.SelectedItem.ToString(); 

            myDR.targetAttributeType = attributeType;

            this.Close();
            this.Dispose();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        
    }
}
