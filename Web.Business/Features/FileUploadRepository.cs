using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Business.Model;

namespace Web.Business.Features
{
    public class FileUploadRepository : IFileUploadRepository
    {
        public FileUploadModel UploadFile(HttpPostedFileBase postedFile)
        {
            var fileUploadModel = new FileUploadModel();
            if (postedFile.ContentLength > 0)
            {
                var dealerList = ProcessCSVFile(postedFile);
                if (dealerList.Any())
                {
                    var mostSoldVehicle = dealerList.GroupBy(x => x.Vehicle).
                                                Select(y => new
                                                {
                                                    TotalPrice = y.Sum(p => p.OriginalPrice),
                                                    VehicleName = y.Key
                                                }).ToList().OrderByDescending(x => x.TotalPrice).FirstOrDefault().VehicleName;
                    fileUploadModel.DealerList = dealerList;
                    fileUploadModel.MostSoldVehicle = mostSoldVehicle;
                }
            }
            
            return fileUploadModel;
        }

        private List<DealerModel> ProcessCSVFile(HttpPostedFileBase postedFile)
        {
            var dealerList = new List<DealerModel>();
            //read data from input stream
            var count = 0;
            using (var parser = new TextFieldParser(postedFile.InputStream, System.Text.Encoding.Default))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                parser.HasFieldsEnclosedInQuotes = true;
                while (!parser.EndOfData)
                {
                    var fields = parser.ReadFields();
                     if (count > 0)
                    {
                        dealerList.Add(new DealerModel
                        {
                            DealNumber = fields[0],
                            CustomerName = fields[1],
                            DealershipName = fields[2],
                            Vehicle = fields[3],
                            Price = String.Format("CAD {0:C}", Convert.ToDecimal(fields[4]).ToString("C")),
                            OriginalPrice = Convert.ToDecimal(fields[4]),
                            Date = fields[5]
                        });
                    }
                    count++;
                }
                parser.Close();

            }
            return dealerList;
        }

    }
}
