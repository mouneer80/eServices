
using System;


namespace DMeServices.Models.BuildingServices
{
    public class SupervisionServicesTypes
    {
        public int ID { get; set; }
        public string ServiceNameAR { get; set; }
        public string ServiceNameEn { get; set; }
        public Nullable<int> ServiceStatus { get; set; }
        public string CssClassName { get; set; }
    }
}