namespace Web.Business.Model
{
    using System.ComponentModel.DataAnnotations;
    public class DealerModel
    {
        [Display(Name = "DealNumber")]
        public string DealNumber { get; set; }
        [Display(Name = "CustomerName")]
        public string CustomerName { get; set; }
        [Display(Name = "DealershipName")]
        public string DealershipName { get; set; }
        [Display(Name = "Vehicle")]
        public string Vehicle { get; set; }
        [Display(Name = "Price")]
        public string Price { get; set; }
        [Display(Name = "Date")]
        public string Date { get; set; }
        public decimal OriginalPrice { get; set; }
    }
}