using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Barcode;

namespace saibabacharityreceiptor.BarCode
{
    public abstract class Barcode
    {
        public abstract string Title
        {
            get;
        }

        public abstract string Description
        {
            get;
        }

        public abstract Type Code
        {
            get;
        }

        public abstract KeyCode[] Keys
        {
            get;
        }

        public abstract string Explanation
        {
            get;
        }

        public virtual int SizeKeys
        {
            get
            {
                return 25;
            }
        }

        public virtual string SpecificText
        {
            get
            {
                return "Specific Config";
            }
        }

        public virtual HtmlControl SpecificValue
        {
            get
            {
                var control = new HtmlGenericControl("span");
                control.Controls.Add(new LiteralControl("None"));
                return control;
            }
        }
    }
}