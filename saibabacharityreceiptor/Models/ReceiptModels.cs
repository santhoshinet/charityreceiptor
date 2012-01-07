using System.ComponentModel.DataAnnotations;

namespace saibabacharityreceiptor.Models
{
    public class BasicInfo
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "ReceiptNumber")]
        public string ReceiptNumber { get; set; }

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
        [DataType(DataType.Date)]
        [Display(Name = "ReceiptReceived")]
        public string DateReceived { get; set; }
    }

    public class RegularReceiptModels : BasicInfo
    {
        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Donation_Amount")]
        public string DonationAmount { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Donation_Amount_inWords")]
        public string DonationAmountinWords { get; set; }
    }

    public class MerchandiseReceipt : BasicInfo
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Merchandise Item")]
        public string MerchandiseItem { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Value")]
        public string Value { get; set; }
    }

    public class ServicesReceipt : BasicInfo
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Merchandise Item")]
        public string MerchandiseItem { get; set; }

        [Required]
        [DataType(DataType.Duration)]
        [Display(Name = "Hours Served")]
        public int HoursServed { get; set; }
    }

    public class RecurringReceipt : BasicInfo
    {
        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Donation_Amount")]
        public string DonationAmount { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Donation_Amount_inWords")]
        public string DonationAmountinWords { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "RecurrenceDates")]
        public string RecurrenceDates { get; set; }
    }
}