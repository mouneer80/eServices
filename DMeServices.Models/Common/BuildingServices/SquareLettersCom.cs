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
    public class SquareLettersCom
    {

        #region Method :: All SquareLetters

        public static List<SquareLetters> AllSquareLetters()
        {
            using (eServicesEntities db = new eServicesEntities())
            {

                List<BldSquareLetters> _BldSquareLetters = db.BldSquareLetters.OrderBy(x => x.ID).ToList();
                List<SquareLetters> _SquareLetters = Mapper.Map<List<BldSquareLetters>, List<SquareLetters>>(_BldSquareLetters);

                return _SquareLetters;
            }

        }
        #endregion







        #region Method :: All Structured SquareLetters

        public static List<SquareLetters> AllStructuredSquareLetters()
        {
            using (eServicesEntities db = new eServicesEntities())
            {

                List<BldSquareLetters> _BldSquareLetters = db.BldSquareLetters.Where(x => x.ID > 3).AsEnumerable().ToList();
                List<SquareLetters> _SquareLetters = Mapper.Map<List<BldSquareLetters>, List<SquareLetters>>(_BldSquareLetters);

                return _SquareLetters;
            }

        }
        #endregion


        #region Method :: SquareLetters By ID

        public static SquareLetters SquareLetterByID(int Id)
        {
            using (eServicesEntities db = new eServicesEntities())
            {

                BldSquareLetters _BldSquareLetters = db.BldSquareLetters.Where(x => x.ID == Id).SingleOrDefault();
                SquareLetters _SquareLetters = Mapper.Map<BldSquareLetters, SquareLetters>(_BldSquareLetters);

                return _SquareLetters;
            }

        }
        #endregion


    }
}
