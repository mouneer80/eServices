using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMeServices.Models.MetaData.BuildingServices
{
  public class Meta_WorkersHousingDetails
    {

        [Key]

        public int Id { get; set; }
        public int SurveyID { get; set; }
        public string WorkerName { get; set; }
        public int WorkerID { get; set; }
        public Nullable<int> WorkerPhone { get; set; }
    }
}
