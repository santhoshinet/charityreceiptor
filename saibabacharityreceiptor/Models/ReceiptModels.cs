using System.ComponentModel.DataAnnotations;

namespace saibabacharityreceiptor.Models
{
    public class ReceiptModels
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "ReceiptNumber")]
        public string ReceiptNumber { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "ReceiptReceived")]
        public string DateReceived { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Contact")]
        public string Contact { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Donation_Amount")]
        public string DonationAmount { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Donation_Amount_inWords")]
        public string DonationAmountinWords { get; set; }
    }
}