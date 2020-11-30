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

    [LocalizedDisplayName("LandSurveys"), MetadataType(typeof(Meta_Suveys))]

    public partial class LandSurvey
    {
        public int SurveyID { get; set; }
        public int WelyahID { get; set; }
        public int RegionID { get; set; }
        public int SquareLetterID { get; set; }
        public string LandNo { get; set; }
        public string ElectricityAccountNo { get; set; }
        public string LandCoordinates { get; set; }
        public Nullable<int> LandUseTypeID { get; set; }
        public Nullable<int> BuildingTypeID { get; set; }
        public Nullable<int> BuildingsCount { get; set; }
        public Nullable<int> FlatsCount { get; set; }
        public Nullable<int> ShopsCount { get; set; }
        public Nullable<int> RoomsCount { get; set; }
        public string Notes { get; set; }
        public string TransactNo { get; set; }
        public Nullable<System.DateTime> UpdatedON { get; set; }
        public Nullable<int> IndustrialSurveyID { get; set; }
        public string UpdatedBy { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedON { get; set; }
        public Nullable<int> CollectiveResidenceType { get; set; }
        public Nullable<long> ConsultantCivilId { get; set; }
        public Nullable<long> OwnerCivilId { get; set; }

        //public BuildingTypes BldBuildingTypes { get; set; }
        //public LandUseTypes BldLandUseTypes { get; set; }
        public Regions BldRegions { get; set; }
        //public SquareLetters BldSquareLetters { get; set; }
        public Welyat BldWelyat { get; set; }
        //public AreaSurvey BldIndustrialAreasSurvey { get; set; }
        public WorkersHousingDetails BldWorkersHousingDetails { get; set; }
    }
}
