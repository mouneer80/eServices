using DMeServices.Models.Configrations;
using DMeServices.Models.MetaData.BuildingServices;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;


namespace DMeServices.Models.BuildingServices
{
    [LocalizedDisplayName("WorkersHousingDetails"), MetadataType(typeof(Meta_WorkersHousingDetails))]
    public class WorkersHousingDetails
    {
        public int Id { get; set; }
        public int SurveyID { get; set; }
        public string WorkerName { get; set; }
        public int WorkerID { get; set; }
        public Nullable<int> WorkerPhone { get; set; }

        //public virtual LandSurvey BldLandSurvey { get; set; }
    }
}
