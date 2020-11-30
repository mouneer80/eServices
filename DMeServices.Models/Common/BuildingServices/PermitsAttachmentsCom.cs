using AutoMapper;
using DMeServices.DAL;
using DMeServices.Models.BuildingServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMeServices.Models.Common.BuildingServices
{
    public class PermitsAttachmentsCom
    {



        #region Method :: List Attachments By Permits ID

        public static List<PermitsAttachments> AttachmentsByPermitsID(long BldPermitId)
        {

            string sWebSiteURL = System.Configuration.ConfigurationManager.AppSettings["WebSiteURL"].ToString();

            using (eServicesEntities db = new eServicesEntities())
            {

                List<BldPermitsAttachments> _BldPermitsAttachments = db.BldPermitsAttachments.Where(x => x.BldPermitId == BldPermitId).OrderByDescending(x => x.Id).ToList();
                List<PermitsAttachments> _PermitsAttachments = Mapper.Map<List<BldPermitsAttachments>, List<PermitsAttachments>>(_BldPermitsAttachments);
                foreach (var item in _PermitsAttachments)
                {

                    //FileStream readStream;
                    //string msg = null;

                    //    readStream = new FileStream("c:\\csharp.net-informations.dat", FileMode.Open);
                    //    BinaryReader readBinary = new BinaryReader(readStream);
                    //    msg = readBinary.ReadString();

                    //    readStream.Close();




                    switch (item.AttachmentTypeId)
                    {
                        case 1:
                        case 2:
                        case 3:
                            item.AttachmentUrl = sWebSiteURL + "/Files/AttachedFiles/Personal/" + item.AttachmentName;
                            break;
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                        case 8:
                            item.AttachmentUrl = sWebSiteURL + "/Files/AttachedFiles/StructuralFiles/" + item.AttachmentName;
                            break;

                    }




                }
                return _PermitsAttachments;
            }

        }
        #endregion


        #region Method :: List Attachments By Permits ID

        public static List<PermitsAttachments> MapsAttachByPermitsID(long BldPermitId)
        {

            string sWebSiteURL = System.Configuration.ConfigurationManager.AppSettings["WebSiteURL"].ToString();

            using (eServicesEntities db = new eServicesEntities())
            {

                List<BldPermitsAttachments> _BldPermitsAttachments = db.BldPermitsAttachments.Where(x => x.BldPermitId == BldPermitId && x.AttachmentTypeId > 3).OrderByDescending(x => x.Id).ToList();
                List<PermitsAttachments> _PermitsAttachments = Mapper.Map<List<BldPermitsAttachments>, List<PermitsAttachments>>(_BldPermitsAttachments);

                return _PermitsAttachments;
            }

        }
        #endregion


        #region Method ::List  Attachments Details By Attachments ID

        public static List<PermitsAttachmentDetails> AttachmentsDetailsByAttachmentsID(int AttachID)
        {
            using (eServicesEntities db = new eServicesEntities())
            {
                List<BldPermitsAttachmentDetails> _BldPermitsAttachmentDetails = db.BldPermitsAttachmentDetails.Where(x => x.BldPermitAttId == AttachID).OrderByDescending(x => x.Id).ToList();
                List<PermitsAttachmentDetails> _PermitsAttachmentDetails = Mapper.Map<List<BldPermitsAttachmentDetails>, List<PermitsAttachmentDetails>>(_BldPermitsAttachmentDetails);
                return _PermitsAttachmentDetails;
            }

        }
        #endregion







        #region Method ::  Attachments By ID

        public static PermitsAttachments AttachmentsByID(int Id)
        {



            using (eServicesEntities db = new eServicesEntities())
            {

                BldPermitsAttachments _BldPermitsAttachment = db.BldPermitsAttachments.Where(x => x.Id == Id).SingleOrDefault();
                PermitsAttachments _PermitsAttachment = Mapper.Map<BldPermitsAttachments, PermitsAttachments>(_BldPermitsAttachment);
                return _PermitsAttachment;
            }

        }
        #endregion



        #region Method ::  Attachments Details By ID

        public static PermitsAttachmentDetails AttachmentsDetailsByID(int Id)
        {



            using (eServicesEntities db = new eServicesEntities())
            {

                BldPermitsAttachmentDetails _BldPermitsAttachmentDetails = db.BldPermitsAttachmentDetails.Where(x => x.Id == Id).SingleOrDefault();
                PermitsAttachmentDetails _PermitsAttachmentDetails = Mapper.Map<BldPermitsAttachmentDetails, PermitsAttachmentDetails>(_BldPermitsAttachmentDetails);
                return _PermitsAttachmentDetails;
            }

        }
        #endregion



        #region Method ::  Attachments Details By ID

        public static PermitsAttachmentDetails AttachmentDetailsByID(int Id)
        {



            using (eServicesEntities db = new eServicesEntities())
            {

                BldPermitsAttachmentDetails _BldPermitsAttachmentDetails = db.BldPermitsAttachmentDetails.Where(x => x.Id == Id).SingleOrDefault();
                PermitsAttachmentDetails _PermitsAttachmentDetails = Mapper.Map<BldPermitsAttachmentDetails, PermitsAttachmentDetails>(_BldPermitsAttachmentDetails);
                return _PermitsAttachmentDetails;
            }

        }
        #endregion








        #region Method ::   Update Permits Attachments 

        public static void UpdatePermitsAttachments(string EmpName, int EmpID, int Id, byte[] DocumentData)
        {

            using (eServicesEntities db = new eServicesEntities())
            {

                BldPermitsAttachments _BldPermitsAttachment = db.BldPermitsAttachments.Where(x => x.Id == Id).SingleOrDefault();
                if (_BldPermitsAttachment == null)
                {
                    return;
                }

                //_BldPermitsAttachment.AttachmentStream = DocumentData;
                //_BldPermitsAttachment.UpdatedBy = EmpName;
                //_BldPermitsAttachment.UpdatedOn = DateTime.Now;

                BldPermitsAttachmentDetails AttachmentDetails = new BldPermitsAttachmentDetails();
                AttachmentDetails.BldPermitAttId = _BldPermitsAttachment.Id;
                AttachmentDetails.AttachmentStream = DocumentData;
                AttachmentDetails.AttachmentContentType = _BldPermitsAttachment.AttachmentContentType;
                AttachmentDetails.AttachmentName = _BldPermitsAttachment.AttachmentName;
                AttachmentDetails.AttachmentPath = _BldPermitsAttachment.AttachmentPath;
                AttachmentDetails.AttachmentTypeId = _BldPermitsAttachment.AttachmentTypeId;
                AttachmentDetails.Description = _BldPermitsAttachment.Description;
                AttachmentDetails.InsertDate = DateTime.Now;
                AttachmentDetails.DmEngineerNo = EmpID;
                AttachmentDetails.CreatedBy = EmpName;
                AttachmentDetails.CreatedOn = DateTime.Now;
                db.BldPermitsAttachmentDetails.Add(AttachmentDetails);
                db.SaveChanges();
            }

        }
        #endregion



        #region Method ::  Consultant Update Permits Attachments 

        public static void UpdateConsultantAttachments(string ConsultName, int CivilID, int Id, byte[] DocumentData)
        {

            using (eServicesEntities db = new eServicesEntities())
            {

                BldPermitsAttachmentDetails _BldPermitsAttachmentDetails = db.BldPermitsAttachmentDetails.Where(x => x.Id == Id).SingleOrDefault();
                if (_BldPermitsAttachmentDetails == null)
                {
                    return;
                }

                //_BldPermitsAttachment.AttachmentStream = DocumentData;
                //_BldPermitsAttachment.UpdatedBy = EmpName;
                //_BldPermitsAttachment.UpdatedOn = DateTime.Now;

                BldPermitsAttachmentDetails AttachmentDetails = new BldPermitsAttachmentDetails();
                AttachmentDetails.BldPermitAttId = _BldPermitsAttachmentDetails.BldPermitAttId;
                AttachmentDetails.AttachmentStream = DocumentData;
                AttachmentDetails.AttachmentContentType = _BldPermitsAttachmentDetails.AttachmentContentType;
                AttachmentDetails.AttachmentName = _BldPermitsAttachmentDetails.AttachmentName;
                AttachmentDetails.AttachmentPath = _BldPermitsAttachmentDetails.AttachmentPath;
                AttachmentDetails.AttachmentTypeId = _BldPermitsAttachmentDetails.AttachmentTypeId;
                AttachmentDetails.Description = _BldPermitsAttachmentDetails.Description;
                AttachmentDetails.InsertDate = DateTime.Now;
                AttachmentDetails.ConsultantCivilId = CivilID;
                AttachmentDetails.CreatedBy = ConsultName;
                AttachmentDetails.CreatedOn = DateTime.Now;
                db.BldPermitsAttachmentDetails.Add(AttachmentDetails);
                db.SaveChanges();
            }

        }
        #endregion



    }
}
