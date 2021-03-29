using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace DMeServices.Models.Configrations
{
    class LocalizedDisplayNameAttribute : DisplayNameAttribute
    {
        public LocalizedDisplayNameAttribute(string resourceId) : base(GetMessageFromResource(resourceId)) { }

        private static string GetMessageFromResource(string resourceId)
        {
            ResourceManager resourceManager = new ResourceManager(typeof(Resources.DisplayName));
            return resourceManager.GetString(resourceId);
        }


    }     
}
