using System;
using System.ComponentModel.DataAnnotations;

namespace saibabacharityreceiptor.Models
{
    public class SearchModel
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        [Required]
        [Display(Name = "Type of receipt")]
        public string TypeOfReceipt { get; set; }

        [Required]
        [Display(Name = "Records per page")]
        public string Maxrecordsperpage { get; set; }

        [Required]
        [Display(Name = "Page index")]
        public int PageIndex { get; set; }
    }
}