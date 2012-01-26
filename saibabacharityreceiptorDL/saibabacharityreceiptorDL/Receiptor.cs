using System;
using System.Collections.Generic;
using Telerik.OpenAccess;

namespace saibabacharityreceiptorDL
{
    [Persistent]
    public class Receipt
    {
        public Receipt()
        {
            RecurringDates = new List<DateTime>();
            DateReceived = DateTime.Now;
            IssuedDate = DateTime.Now;
        }

        public string ReceiptNumber { get; set; }

        public string FirstName { get; set; }

        public string Mi { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string ZipCode { get; set; }

        public string Email { get; set; }

        public string Contact { get; set; }

        public string DonationAmount { get; set; }

        public string DonationAmountinWords { get; set; }

        public string MerchandiseItem { get; set; }

        public string ServiceType { get; set; }

        public string Quantity { get; set; }

        public string FmvValue { get; set; }

        public int HoursServed { get; set; }

        public string RatePerHrOrDay { get; set; }

        public IList<DateTime> RecurringDates { get; set; }

        public ModeOfPayment ModeOfPayment { get; set; }

        public User DonationReceiver { get; set; }

        public DateTime DateReceived { get; set; }

        public ReceiptType ReceiptType { get; set; }

        public string GroupId { get; set; }

        public DateTime IssuedDate { get; set; }
    }

    public enum ModeOfPayment
    {
        Cash,
        Cheque,
        Online,
        Mobile,
        Goods
    }

    public enum ReceiptType
    {
        GeneralReceipt,
        RecurringReceipt,
        MerchandiseReceipt,
        ServicesReceipt
    }
}