using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DMeServices.DAL;

namespace DMeServices.Models.Common
{
    public static class ConsultantOccupationCom
    {
        #region Method :: Get Consultant Occupation By ID
        public static ConsultantOccupations GetConsultantOccupationById (int id)
        {
            using (eServicesEntities db = new eServicesEntities())
            {
                ConsultantOccupations oOccupation = db.ConsultantOccupations.SingleOrDefault(x  => x.OccupationID == id);
                return oOccupation;
            }
        }
        #endregion
        
        #region Method :: Get Consultant Occupation By Arabic Name
        public static ConsultantOccupations GetConsultantOccupationByArabicName(string nameAr)
        {
            using (eServicesEntities db = new eServicesEntities())
            {
                ConsultantOccupations oOccupation = db.ConsultantOccupations.SingleOrDefault(x  => x.OccupationDescArabic == nameAr);
                return oOccupation;
            }
        }
        #endregion
        
        #region Method :: Get Consultant Occupation By English Name
        public static ConsultantOccupations GetConsultantOccupationByEnglishName(string nameEn)
        {
            using (eServicesEntities db = new eServicesEntities())
            {
                ConsultantOccupations oOccupation = db.ConsultantOccupations.SingleOrDefault(x  => x.OccupationDescEnglish == nameEn);
                return oOccupation;
            }
        }
        #endregion
        
        #region Method :: Is Occupassion Valid
        public static bool IsOccupassionValid(string nameAr)
        {
            using (eServicesEntities db = new eServicesEntities())
            {
                return  db.ConsultantOccupations.Count(x => x.OccupationDescArabic == nameAr) > 0 ;
            }
        }
        #endregion
        
    }
}
