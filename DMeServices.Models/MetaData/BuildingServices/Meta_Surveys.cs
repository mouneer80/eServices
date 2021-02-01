using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMeServices.Models.MetaData.BuildingServices
{
  public class Meta_Suveys
    {

        [Key]

        public int SurveyID { get; set; }
        public int WelyahID { get; set; }
        public int RegionID { get; set; }
        public int SquareLetterID { get; set; }
        public string LandNo { get; set; }
        public int LandUseTypeID { get; set; }
        public Nullable<int> BuildingTypeID { get; set; }
        public Nullable<int> BuildingsCount { get; set; }
        public Nullable<int> FlatsCount { get; set; }
        public Nullable<int> ShopsCount { get; set; }
        public Nullable<int> RoomsCount { get; set; }
        public string Notes { get; set; }
        public string TransactNo { get; set; }
    }
}
