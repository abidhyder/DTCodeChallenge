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
            //var fileName = files[0].FileName;
            //var contentlength = files[0].ContentLength;
            //var postedFile = files[0];
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
            var cnt = 0;
            using (var parser = new TextFieldParser(postedFile.InputStream, System.Text.Encoding.Default))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                parser.HasFieldsEnclosedInQuotes = true;
                while (!parser.EndOfData)
                {
                    var fields = parser.ReadFields();
                     if (cnt > 0)
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
                    cnt++;
                }
                parser.Close();

            }
            return dealerList;
        }

        public List<DealerModel> UploadFile1(HttpPostedFileBase postedFile)
        {
            //get file
            var dealerList = new List<DealerModel>();

            //var postedFile = Request.Files[0];
            //var postedFile = files[0];
            if (postedFile.ContentLength > 0)
            {
                //read data from input stream
                using (var csvReader = new System.IO.StreamReader(postedFile.InputStream))
                {
                    string inputLine = "";
                    //read each line
                    while ((inputLine = csvReader.ReadLine()) != null)
                    {
                        string text = inputLine;
                        var csv = string.Join(",", text);
                        //get lines values
                        string[] values = inputLine.Split(new char[] { ',' });

                        for (int x = 0; x < values.Length; x++)
                        {
                            //do something with each line and split value
                            dealerList.Add(new DealerModel
                            {
                                DealershipName = values[0],
                                DealNumber = values[1],
                                CustomerName = values[2]

                            });
                        }
                    }

                    csvReader.Close();
                }
            }
            return dealerList;
        }
    }
}
