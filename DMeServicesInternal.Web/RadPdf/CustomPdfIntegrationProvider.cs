using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Resources;
using System.Text;
using System.Web;
using System.Web.Mvc;
using DMeServices.Models;
using DMeServices.Models.ViewModels.Internal.Permits;
using iTextSharp.text.pdf;
using RadPdf.Data.Document.Common;
using RadPdf.Data.Document.Objects;
using RadPdf.Data.Document.Objects.FormFields;
using RadPdf.Data.Document.Objects.Shapes;
using RadPdf.Integration;

namespace DMeServicesInternal.Web.RadPdf
{
    public class CustomPdfIntegrationProvider : DefaultPdfIntegrationProvider
    {


        public string DocID { get; set; }

        public class PdfIntegrationProvider
        {
        }


        public CustomPdfIntegrationProvider()
            : base()
        {
            //Assign our resources file to be used by the PdfWebControl
            this.PdfWebControlResources = Resources.PdfWebControlResourcesAR.ResourceManager;

        }

        public override void ProcessObjectDataRequest(PdfDataContext context)
        {
            PermitsViewModel model = new PermitsViewModel();
            //Look for form field with the name "test" and see if it is null or has no value 
            var empNo = model.oEmployeeInfo.EMP_NO;
            char[] delimiterChars = { ' ', ',' };
            var key = context.Request.DataKey.Split(delimiterChars, 2)[0].ToString();
            var docid = context.Request.DataKey.Split(delimiterChars, 2)[1].ToString();
            var text = context.Request.DataKey.Split(delimiterChars, 2)[1].ToString();

            switch (key)
            {
                case "dynamic":
                    int imageWidth = 300;
                    int margin = 10;
                    Font font = new Font(FontFamily.GenericMonospace, 10);
                    StringFormat stringFormat = new StringFormat();

                    int stringHeight = MeasureDisplayStringHeight(text, font, imageWidth - 2 * margin, stringFormat);
                    int imageHeight = stringHeight + margin * 2;
                    //Create a dynamic image showing the date (200px by 50px)
                    using (Bitmap bmp = new Bitmap(imageWidth, imageHeight, PixelFormat.Format32bppArgb))
                    {
                        //Create graphics object
                        using (Graphics gr = Graphics.FromImage(bmp))
                        {
                            //Set smoothing mode
                            gr.SmoothingMode = SmoothingMode.AntiAlias;

                            //Get the rect for the bitmap
                            RectangleF rect = gr.VisibleClipBounds;

                            //Create a new brush to draw background with
                            using (Brush br = new SolidBrush(Color.Yellow))
                            {
                                //Draw background
                                gr.FillRectangle(br, rect);
                            }

                            //Create a new brush to draw text with
                            using (Brush br = new SolidBrush(Color.Black))
                            {
                                //Create a new font to draw text with
                                using (Font ft = new Font("Arial", 20.0f, FontStyle.Bold, GraphicsUnit.Pixel))
                                {
                                    //Create string format to draw text with
                                    using (StringFormat sf = new StringFormat())
                                    {
                                        //Set format properties
                                        sf.Alignment = StringAlignment.Center;
                                        sf.LineAlignment = StringAlignment.Center;

                                        //Draw current date to bitmap
                                        gr.DrawString(text, ft, br, rect, sf);
                                    }
                                }
                            }
                        }

                        //Create output strea
                        using (MemoryStream ms = new MemoryStream())
                        {
                            //Save image to stream
                            bmp.Save(ms, ImageFormat.Gif);

                            //Write bytes to the response
                            context.Response.Write(ms.ToArray());
                        }
                    }
                    break;

                case "signature":
                    //Write a file to the response
                    //Alternatively, we could also use the .Write method to write data from almost any source (e.g. database, memory, etc.)
                    context.Response.WriteFile(HttpContext.Current.Server.MapPath(@"~/Images/Signatures/" + empNo.ToString() + ".png"));
                    break;
                case "stamp":
                    //Write a file to the response
                    //Alternatively, we could also use the .Write method to write data from almost any source (e.g. database, memory, etc.)
                    context.Response.WriteFile(HttpContext.Current.Server.MapPath(@"~/Images/Stamps/" + empNo.ToString() + ".png"));
                    break;
                case "note":
                    //Write a file to the response
                    //Alternatively, we could also use the .Write method to write data from almost any source (e.g. database, memory, etc.)
                    context.Response.WriteFile(HttpContext.Current.Server.MapPath(@"~/Images/Notes/" + "note" + docid + ".png"));
                    break;
            }
        }
        public static int MeasureDisplayStringHeight(string text, Font font, int maxWidth, System.Drawing.StringFormat format)
        {
            System.Drawing.Graphics g =
                    Graphics.FromImage(new Bitmap(maxWidth, 1000));
            System.Drawing.RectangleF rect =
                    new System.Drawing.RectangleF(0, 0, maxWidth, 1000);
            System.Drawing.CharacterRange[] ranges =
                    { new System.Drawing.CharacterRange(0, text.Length) };
            System.Drawing.Region[] regions = new System.Drawing.Region[1];

            format.SetMeasurableCharacterRanges(ranges);

            regions = g.MeasureCharacterRanges(text, font, rect, format);
            rect = regions[0].GetBounds(g);

            return (int)(rect.Bottom + 1.0f);
        }

        public override void OnObjectDataAdding(ObjectDataAddingEventArgs e)
        {
            base.OnObjectDataAdding(e);

            //If data is added to an image
            if (e.PdfObjectType == typeof(PdfImageShape))
            {
                //Check image size (if larger than 1 MB)
                if (e.Data.Length > 0x100000)
                {
                    //Cancel object data adding and display a message
                    e.Cancel = true;
                    e.CancelMessage = "Maximum image size is 1 MB.";
                }
            }
            else
            {
                throw new ArgumentException("PdfObjectType unsupported.");
            }
        }




        public override void OnDocumentSaved(DocumentSavedEventArgs e)
        {
            base.OnDocumentSaved(e);
            PermitsViewModel model = new PermitsViewModel();
            //Look for form field with the name "test" and see if it is null or has no value 
            var req = System.Web.HttpContext.Current.Request;
            if (e.Document.DocumentInfo.Title == "PrintPermit")
            {
                DMeServices.Models.Common.BuildingServices.PermitsAttachmentsCom.UpdateAttachmentStream(int.Parse(DocID), e.DocumentData);
                
            }
            else
            {
                DMeServices.Models.Common.BuildingServices.PermitsAttachmentsCom.UpdatePermitsAttachments(model.oEmployeeInfo.NAME, (int)model.oEmployeeInfo.EMP_NO, int.Parse(DocID), e.DocumentData);
                
            }
            return;
        }

        public override void OnDocumentSaving(DocumentSavingEventArgs e)
        {
            base.OnDocumentSaving(e);
            //e.Document.BidiTextMode = (PdfBidiTextMode)2;
            //Encoding.UTF32.GetBytes(((PdfTextShape)e.Document.Pages[0].Objects[0]).Text);
            //var obj = e.Document.Pages[0].Objects[0];
            //var obj1=((PdfTextShape)obj).Text;

            //Encoding.UTF32.GetBytes(obj1);
            DocID = e.Document.DocumentProperties.DocumentFileName;
        }
    }
}