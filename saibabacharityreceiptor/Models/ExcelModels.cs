using System.ComponentModel.DataAnnotations;

namespace saibabacharityreceiptor.Models
{
    public class ExcelModels
    {
        [Required]
        [Display(Name = "ExcelFile")]
        public System.Web.HttpPostedFileBase ExcelFile { get; set; }
    }
}