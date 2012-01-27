/**
 *--------------------------------------------------------------------
 *
 * Default Page to display barcodes
 *
 *--------------------------------------------------------------------
 * Revision History
 * v1.0.0	12 apr	2009	Jean-Sébastien Goupil	New version
 *--------------------------------------------------------------------
 * $Id: Default.aspx.cs,v 1.1 2009/05/03 20:44:23 jsgoupil Exp $
 *--------------------------------------------------------------------
 * Copyright (C) Jean-Sebastien Goupil
 * http://www.barcodeasp.com
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Barcode.code;

namespace Barcode
{
    public partial class _Default : Page
    {
        private const string Version = "1.0.0";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                versionLabel.Text = Version;
                imagerow.Visible = false;

                foreach (KeyValuePair<Type, string> kvp in Utility.CodeType)
                {
                    var item = new ListItem(kvp.Value, kvp.Value, true);

                    string valueSelected = Request.Form[general_type.UniqueID];
                    if ((valueSelected == null && item.Value == "Code 39") || (valueSelected == item.Value))
                    {
                        item.Selected = true;
                        configFor.Text = item.Value;
                    }
                    else
                    {
                        item.Selected = false;
                    }

                    general_type.Items.Add(item);
                }

                foreach (KeyValuePair<int, string> kvp in Utility.OutputType)
                {
                    var item = new ListItem(kvp.Value, kvp.Key.ToString(), true);

                    string valueSelected = Request.Form[general_output.UniqueID];
                    if ((valueSelected == null && kvp.Key == 2) || (valueSelected == item.Value))
                    {
                        item.Selected = true;
                    }
                    else
                    {
                        item.Selected = false;
                    }

                    general_output.Items.Add(item);
                }

                foreach (string font in Utility.FontType)
                {
                    var item = new ListItem(font, font, true);

                    string valueSelected = Request.Form[general_font.UniqueID];
                    if ((valueSelected == null && item.Value == "Arial") || (valueSelected == item.Value))
                    {
                        item.Selected = true;
                    }
                    else
                    {
                        item.Selected = false;
                    }

                    general_font.Items.Add(item);
                }

                string valueFont = Request.Form[general_fontsize.UniqueID];
                if (valueFont == null)
                {
                    general_fontsize.Text = "8";
                }

                if (!general_res1.Checked && !general_res2.Checked && !general_res3.Checked)
                {
                    general_res1.Checked = true;
                }

                if (string.IsNullOrEmpty(general_thickness.Text))
                {
                    general_thickness.Text = "30";
                }

                var code39 = new Code39();
                explanation.Controls.Add(new LiteralControl(code39.Explanation));
                DisplayKeys(code39.Keys, code39.SizeKeys);
                data1.Text = code39.SpecificText;
                data2.Controls.Clear();
                data2.Controls.Add(code39.SpecificValue);

                general_type.Attributes["onchange"] = string.Format("document.getElementById('{0}').value='';var obj=document.getElementById('{1}');if(obj)obj.style.display='none';", general_Text.ClientID, barcodeimage.ClientID);
            }

            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "newKey", string.Format(@"function newkey(variable){{document.getElementById('{0}').value += unescape(variable);}}", general_Text.ClientID), true);
        }

        public void GenerateClick(object sender, EventArgs e)
        {
            int scale;
            if (general_res3.Checked)
            {
                scale = 3;
            }
            else if (general_res2.Checked)
            {
                scale = 2;
            }
            else
            {
                scale = 1;
            }
            string text = HttpUtility.UrlEncode(Request.Form[general_Text.UniqueID]);

            code.Barcode temporaryBarcode = FindSelectedBarcode();
            HtmlControl control = temporaryBarcode.SpecificValue;
            data2.Controls.Clear();
            data2.Controls.Add(control);

            string a1 = string.Empty;
            string a2 = string.Empty;
            string a3 = string.Empty;
            Type type = control.GetType();
            if (type == typeof(HtmlInputCheckBox))
            {
                a1 = Request.Form[control.UniqueID];
                if (!string.IsNullOrEmpty(a1) && a1.Equals("1"))
                {
                    ((HtmlInputCheckBox)control).Checked = true;
                }
            }
            else if (type == typeof(HtmlSelect))
            {
                a2 = Request.Form[control.UniqueID];
                if (!string.IsNullOrEmpty(a2))
                {
                    foreach (ListItem item in ((HtmlSelect)control).Items)
                    {
                        item.Selected = item.Value.Equals(a2);
                    }
                }
            }
            else if (type == typeof(HtmlInputText))
            {
                a3 = Request.Form[control.UniqueID];
                if (!string.IsNullOrEmpty(a3))
                {
                    ((HtmlInputText)control).Value = a3;
                }
            }

            if (!string.IsNullOrEmpty(text))
            {
                imagerow.Visible = true;
                barcodeimage.ImageUrl = string.Format("image.aspx?code={0}&o={1}&t={2}&r={3}&text={4}&f1={5}&f2={6}&a1={7}&a2={8}&a3={9}",
                    HttpUtility.UrlEncode(Request.Form[general_type.UniqueID]),
                    Request.Form[general_output.UniqueID],
                    Request.Form[general_thickness.UniqueID],
                    scale,
                    text,
                    HttpUtility.UrlEncode(Request.Form[general_font.UniqueID]),
                    Request.Form[general_fontsize.UniqueID],
                    a1,
                    a2,
                    a3);
            }
            else
            {
                imagerow.Visible = false;
            }
        }

        public void BarcodeTypeChanged(object sender, EventArgs e)
        {
            code.Barcode temporaryBarcode = FindSelectedBarcode();
            explanation.Controls.Add(new LiteralControl(temporaryBarcode.Explanation));

            DisplayKeys(temporaryBarcode.Keys, temporaryBarcode.SizeKeys);

            data1.Text = temporaryBarcode.SpecificText;

            HtmlControl control = temporaryBarcode.SpecificValue;
            data2.Controls.Clear();
            data2.Controls.Add(control);
        }

        private code.Barcode FindSelectedBarcode()
        {
            Type codeType = (from kvp in Utility.CodeType where kvp.Value == general_type.SelectedItem.Value select kvp.Key).FirstOrDefault();

            return (code.Barcode)Activator.CreateInstance(codeType);
        }

        private void DisplayKeys(IEnumerable<KeyCode> listKeys, int size)
        {
            if (listKeys != null)
            {
                var buttons = new StringBuilder();
                foreach (KeyCode key in listKeys)
                {
                    buttons.AppendFormat("<input type=\"button\" style=\"width:{2}px;padding:0px;\" onclick=\"newkey('{0}')\" value=\"{1}\" /> ", key.Code, HttpUtility.HtmlEncode(key.Text), size);
                }
                keys.Text = buttons.ToString();
            }
            else
            {
                keys.Text = "No specific buttons";
            }
        }
    }
}