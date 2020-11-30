using DMeServices.Models.Configrations;
using DMeServices.Models.MetaData.BuildingServices;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMeServices.Models.BuildingServices
{

    [LocalizedDisplayName("AreaSurveys"), MetadataType(typeof(Meta_AreaSuveys))]

    public partial class AreaSurvey
    {
        public int IndustrialSurveyID { get; set; }
        public int WelayahID { get; set; }
        public int RegionID { get; set; }
        public int SquareLetterID { get; set; }
        public string RegionArName { get; set; }
        public string RegionEnName { get; set; }
        public Nullable<int> UseTypeID { get; set; }
        public string LocationCoordinates { get; set; }
        public int BuildingsCount { get; set; }
        public int FlatsCount { get; set; }
        public int IndustrialShopsCount { get; set; }
        public int CommercialShopsCount { get; set; }
        public string TransactNo { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedON { get; set; }
        public Nullable<int> BuildingsTypeID { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedON { get; set; }

        public BuildingTypes BldBuildingTypes { get; set; }
        public LandUseTypes BldLandUseTypes { get; set; }
        public Regions BldRegions { get; set; }
        public SquareLetters BldSquareLetters { get; set; }
        public Welyat BldWelyat { get; set; }
        //public virtual ICollection<LandSurvey> BldLandSurvey { get; set; }
    }
}
