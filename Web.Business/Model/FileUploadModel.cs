using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Business.Model
{
    public class FileUploadModel
    {
        public FileUploadModel()
        {
            DealerList = new List<DealerModel>();
            MostSoldVehicle = string.Empty;
        }
        public List<DealerModel> DealerList { get; set; }
        public string MostSoldVehicle { get; set; }

    }
}
