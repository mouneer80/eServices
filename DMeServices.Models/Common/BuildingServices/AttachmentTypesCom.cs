using AutoMapper;
using DMeServices.DAL;
using DMeServices.Models.BuildingServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMeServices.Models.Common.BuildingServices
{
    public class AttachmentTypesCom
    {

        #region Method :: All AttachmentTypes

        public static List<AttachmentTypes> AllAttachmentTypes()
        {
            using (eServicesEntities db = new eServicesEntities())
            {

                List<BldAttachmentTypes> _BldAttachmentTypes = db.BldAttachmentTypes.OrderByDescending(x => x.Id).ToList();
                List<AttachmentTypes> _AttachmentTypes = Mapper.Map<List<BldAttachmentTypes>, List<AttachmentTypes>>(_BldAttachmentTypes);

                return _AttachmentTypes;
            }

        }
        #endregion







        #region Method :: All Structured Types

        public static List<AttachmentTypes> AllStructuredTypes()
        {
            using (eServicesEntities db = new eServicesEntities())
            {

                List<BldAttachmentTypes> _BldAttachmentTypes = db.BldAttachmentTypes.Where(x => x.Id > 3).AsEnumerable().ToList();
                List<AttachmentTypes> _AttachmentTypes = Mapper.Map<List<BldAttachmentTypes>, List<AttachmentTypes>>(_BldAttachmentTypes);

                return _AttachmentTypes;
            }

        }
        #endregion


        #region Method :: AttachmentTypes By ID

        public static AttachmentTypes TypeByID(int Id)
        {
            using (eServicesEntities db = new eServicesEntities())
            {

                BldAttachmentTypes _BldAttachmentTypes = db.BldAttachmentTypes.Where(x => x.Id == Id).SingleOrDefault();
                AttachmentTypes _AttachmentTypes = Mapper.Map<BldAttachmentTypes, AttachmentTypes>(_BldAttachmentTypes);

                return _AttachmentTypes;
            }

        }
        #endregion


    }
}
