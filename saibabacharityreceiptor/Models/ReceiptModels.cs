using System;
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
        [Display(Name = "FirstName")]
        public string FirstName { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "MI")]
        public string Mi { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "LastName")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Address2")]
        public string Address2 { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "City")]
        public string City { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "State")]
        public string State { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "ZipCode")]
        public string ZipCode { get; set; }

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
        public DateTime DateReceived { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "IssuedDate")]
        public DateTime IssuedDate { get; set; }
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
        public string[] RecurrenceDates { get; set; }
    }

    public class MerchandiseReceipt : BasicInfo
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Merchandise Item")]
        public string MerchandiseItem { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Quantity")]
        public string Quanity { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Value")]
        public string Value { get; set; }
    }

    public class ServicesReceipt : BasicInfo
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Service Type")]
        public string ServiceType { get; set; }

        [Required]
        [DataType(DataType.Duration)]
        [Display(Name = "Hours Served")]
        public int HoursServed { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Rate per Hr / Day")]
        public int RateperHour { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "FMv Value")]
        public int FmvValue { get; set; }
    }
}