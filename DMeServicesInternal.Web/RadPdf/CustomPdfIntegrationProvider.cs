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
using RadPdf.Data.Document.Common;
using RadPdf.Data.Document.Objects;
using RadPdf.Data.Document.Objects.FormFields;
using RadPdf.Data.Document.Objects.Shapes;
using RadPdf.Integration;
using System.Collections.Generic;
//using iTextSharp.text.pdf;

namespace DMeServicesInternal.Web.RadPdf
{
    public class CustomPdfIntegrationProvider : DefaultPdfIntegrationProvider
    {

        public string DocID { get; set; }
        public string PrimID { get; set; }
        List<string> stampStatus = new List<string>();
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
                    Font font = new Font(FontFamily.GenericMonospace, 20);
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
                            //gr.SmoothingMode = SmoothingMode.AntiAlias;
                            gr.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit;
                            //Get the rect for the bitmap
                            RectangleF rect = gr.VisibleClipBounds;

                            //Create a new brush to draw background with
                            using (Brush br = new SolidBrush(Color.FromArgb(0, 255, 255, 255)))
                            {
                                //Draw background
                                gr.FillRectangle(br, rect);
                            }

                            //Create a new brush to draw text with
                            using (Brush br = new SolidBrush(Color.FromArgb(255, 215, 5, 5)))
                            {
                                //Create a new font to draw text with
                                using (Font ft = new Font("Arial", 15.0f, FontStyle.Bold, GraphicsUnit.Point))
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

                        //Create output stream
                        using (MemoryStream ms = new MemoryStream())
                        {
                            //Save image to stream
                            bmp.Save(ms, ImageFormat.Png);

                            //Write bytes to the response
                            context.Response.Write(ms.ToArray());
                        }
                    }
                    break;
                case "dynamicNO":
                    imageWidth = 200;
                    margin = 10;
                    font = new Font(FontFamily.GenericMonospace, 8);
                    stringFormat = new StringFormat();

                    stringHeight = MeasureDisplayStringHeight(text, font, imageWidth - 2 * margin, stringFormat);
                    imageHeight = stringHeight + margin * 2;
                    //Create a dynamic image showing the date (200px by 50px)
                    using (Bitmap bmp = new Bitmap(imageWidth, imageHeight, PixelFormat.Format32bppArgb))
                    {
                        //Create graphics object
                        using (Graphics gr = Graphics.FromImage(bmp))
                        {
                            //Set smoothing mode
                            //gr.SmoothingMode = SmoothingMode.AntiAlias;
                            gr.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit;
                            //Get the rect for the bitmap
                            RectangleF rect = gr.VisibleClipBounds;

                            //Create a new brush to draw background with
                            using (Brush br = new SolidBrush(Color.FromArgb(0, 255, 255, 255)))
                            {
                                //Draw background
                                gr.FillRectangle(br, rect);
                            }

                            //Create a new brush to draw text with
                            using (Brush br = new SolidBrush(Color.FromArgb(255, 0, 69, 224)))
                            {
                                //Create a new font to draw text with
                                using (Font ft = new Font("Arial", 15.0f, FontStyle.Bold, GraphicsUnit.Point))
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

                        //Create output stream
                        using (MemoryStream ms = new MemoryStream())
                        {
                            //Save image to stream
                            bmp.Save(ms, ImageFormat.Png);

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
                case "stamp_r_CivilDefence":
                    //Write a file to the response
                    //Alternatively, we could also use the .Write method to write data from almost any source (e.g. database, memory, etc.)
                    context.Response.WriteFile(HttpContext.Current.Server.MapPath(@"~/Images/Stamps/r_CivilDefence.png"));
                    break;
                case "stamp_r_Permit":
                    //Write a file to the response
                    //Alternatively, we could also use the .Write method to write data from almost any source (e.g. database, memory, etc.)
                    context.Response.WriteFile(HttpContext.Current.Server.MapPath(@"~/Images/Stamps/Maps/" + empNo.ToString() + ".png"));
                    break;
                case "stamp_r_Rifi":
                    //Write a file to the response
                    //Alternatively, we could also use the .Write method to write data from almost any source (e.g. database, memory, etc.)
                    context.Response.WriteFile(HttpContext.Current.Server.MapPath(@"~/Images/Stamps/r_Rifi.png"));
                    break;
                case "stamp_r_SmallPermit":
                    //Write a file to the response
                    //Alternatively, we could also use the .Write method to write data from almost any source (e.g. database, memory, etc.)
                    context.Response.WriteFile(HttpContext.Current.Server.MapPath(@"~/Images/Stamps/r_SmallPermit.png"));
                    break;
                case "stamp_r_SupervisionStamp":
                    //Write a file to the response
                    //Alternatively, we could also use the .Write method to write data from almost any source (e.g. database, memory, etc.)
                    context.Response.WriteFile(HttpContext.Current.Server.MapPath(@"~/Images/Stamps/r_SupervisionStamp.png"));
                    break;
                case "stamp_r_PermitNo":
                    //Write a file to the response
                    //Alternatively, we could also use the .Write method to write data from almost any source (e.g. database, memory, etc.)
                    context.Response.WriteFile(HttpContext.Current.Server.MapPath(@"~/Images/Notes/" + "stamp_r_PermitNo" + docid + ".png"));
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
            string sFilename = string.Empty;
            string PerPath;
            string sPath;

            base.OnDocumentSaved(e);

            sFilename = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + DocID;
            PerPath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Files/EditedFiles/" + e.DocumentID.ToString()));
            sPath = System.IO.Path.Combine(PerPath.ToString());
            string PerUploadPath = string.Format("{0}\\{1}", sPath, sFilename);
            if (!Directory.Exists(PerPath))
            {
                Directory.CreateDirectory(PerPath);
            }

            PermitsViewModel model = new PermitsViewModel();
            //Look for form field with the name "test" and see if it is null or has no value 
            var req = System.Web.HttpContext.Current.Request;
            if (e.Document.DocumentInfo.Title != "" && e.Document.DocumentInfo.Title.Substring(0, 5) == "Print")
            {
                File.WriteAllBytes(PerUploadPath + ".pdf", e.DocumentData);
                DMeServices.Models.Common.BuildingServices.PermitsAttachmentsCom.UpdateAttachmentStream(int.Parse(DocID), e.DocumentData);
            }
            else
            {
                File.WriteAllBytes(PerUploadPath + ".pdf", e.DocumentData);
                if (stampStatus.Count != 0)
                {
                    List<string> stampNames = new List<string>();
                    foreach (var stamp in stampStatus)
                    {
                        switch (stamp)
                        {
                            case "signature":
                                stampNames.Add("توقيع المهندس");
                                break;
                            case "stamp":
                                stampNames.Add("ختم المهندس");
                                break;
                            case "stamp_r_CivilDefence":
                                stampNames.Add("ختم الدفاع المدني");
                                break;
                            case "stamp_r_SupervisionStamp":
                                stampNames.Add("ختم الاشراف الهندسي");
                                break;
                            case "stamp_r_Rifi":
                                stampNames.Add("ختم السكن الريفي");
                                break;
                            case "stamp_r_SmallPermit":
                                stampNames.Add("ختم الاباحة الصغرى");
                                break;
                            case "stamp_r_Permit":
                                stampNames.Add("ختم الاباحة الكبرى");
                                break;
                        }
                    }
                    string stamps = string.Join(",", stampNames);
                    DMeServices.Models.Common.BuildingServices.PermitsAttachmentsCom.UpdatePermitsAttachments(model.oEmployeeInfo.NAME, (int)model.oEmployeeInfo.EMP_NO, int.Parse(DocID), e.DocumentData, 90, stamps);
                }
                else
                {
                    DMeServices.Models.Common.BuildingServices.PermitsAttachmentsCom.UpdatePermitsAttachments(model.oEmployeeInfo.NAME, (int)model.oEmployeeInfo.EMP_NO, int.Parse(DocID), e.DocumentData);
                }
            }
            return;
        }

        public override void OnDocumentSaving(DocumentSavingEventArgs e)
        {
            base.OnDocumentSaving(e);

            foreach (PdfObject obj in e.Document.Pages[0].Objects)
            {
                string type = obj.GetType().Name;
                if (type == "PdfTextShape")
                {
                    try
                    {
                        stampStatus.Add((obj as PdfTextShape).Text);
                    }
                    catch (Exception ex)
                    {
                        string exception = ex.Message;
                        stampStatus.Add(exception);
                    }
                }
            }
            //e.Document.BidiTextMode = (PdfBidiTextMode)2;
            //Encoding.UTF32.GetBytes(((PdfTextShape)e.Document.Pages[0].Objects[0]).Text);
            //var obj = e.Document.Pages[0].Objects[0];
            //var obj1=((PdfTextShape)obj).Text;
            //Encoding.UTF32.GetBytes(obj1);
            DocID = e.Document.DocumentProperties.DocumentFileName;
        }
    }
}