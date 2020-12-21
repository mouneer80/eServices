using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DMeServices.DAL;
using DMeServices.Models.BuildingServices;
using DMeServices.Models.HealthServices;

namespace DMeServices.Models.Configrations
{
 public   class MappingConfig
    {


        public static void RegisterMaps()
        {
            Mapper.Initialize(config =>
            {

                config.CreateMap<BldPermits, BuildingPermits>();
                config.CreateMap<Employees, Employee>();
                config.CreateMap<LookupTypes, LookupType>();
                config.CreateMap<BldPermitsAttachmentDetails, PermitsAttachmentDetails>();
                config.CreateMap<BldPermitsAttachments, PermitsAttachments>();
                config.CreateMap<Users, User>();
                config.CreateMap<MociData, HMociData>();
                config.CreateMap<HsContractAttachments, ContractAttachments>();
                config.CreateMap<HsPermitActivity, PermitActivity>();
                config.CreateMap<HsPermitAdvertisement, PermitAdvertisement>();
                config.CreateMap<HsPermitDuration, PermitDuration>();
                config.CreateMap<HsPermits, HPermits>();
                config.CreateMap<HsPermitSubActivity, PermitSubActivity>();
                config.CreateMap<HsPermitType, PermitType>();
                config.CreateMap<EmployeeGroups, EmployeeGroup>();
                config.CreateMap<BldWorkersHousingDetails, WorkersHousingDetails>();
                config.CreateMap<BuildingControls, BldControlServices>();
                config.CreateMap<BldControlServicesTypes, ControlServicesTypes>();





                //Reverse

                config.CreateMap<BuildingPermits, BldPermits>();
                config.CreateMap<Employee, Employees>();
                config.CreateMap<LookupType, LookupTypes>();
                config.CreateMap<PermitsAttachmentDetails, BldPermitsAttachmentDetails>();
                config.CreateMap<PermitsAttachments, BldPermitsAttachments>();
                config.CreateMap<User, Users>();
                config.CreateMap<HMociData, MociData>();
                config.CreateMap<ContractAttachments, HsContractAttachments>();
                config.CreateMap<PermitActivity, HsPermitActivity>();
                config.CreateMap<PermitAdvertisement, HsPermitAdvertisement>();
                config.CreateMap<PermitDuration, HsPermitDuration>();
                config.CreateMap<HPermits, HsPermits>();
                config.CreateMap<PermitSubActivity, HsPermitSubActivity>();
                config.CreateMap<PermitType, HsPermitType>();
                config.CreateMap<EmployeeGroup, EmployeeGroups>();
                config.CreateMap<WorkersHousingDetails, BldWorkersHousingDetails>();
                config.CreateMap<BuildingControls, BldControlServices>();
                config.CreateMap<ControlServicesTypes, BldControlServicesTypes>();

            });


        }

    }
}
