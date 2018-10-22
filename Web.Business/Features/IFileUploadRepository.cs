using System.Collections.Generic;
using System.Web;
using Web.Business.Model;

namespace Web.Business.Features
{
    public interface IFileUploadRepository
    {
        FileUploadModel UploadFile(HttpPostedFileBase files);
    }
}
