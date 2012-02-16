using System;
using System.ComponentModel.DataAnnotations;

namespace saibabacharityreceiptor.Models
{
    public class SearchModel
    {
        [DataType(DataType.Text)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Receipt ID")]
        public string ReceiptId { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Type of receipt")]
        public string TypeOfReceipt { get; set; }

        [Display(Name = "Records per page")]
        public string Maxrecordsperpage { get; set; }

        [Display(Name = "Page index")]
        public int PageIndex { get; set; }
    }
}