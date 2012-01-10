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
        }

        public string ReceiptNumber { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public string Contact { get; set; }

        public string DonationAmount { get; set; }

        public string DonationAmountinWords { get; set; }

        public string MerchandiseItem { get; set; }

        public string Value { get; set; }

        public int HoursServed { get; set; }

        public IList<DateTime> RecurringDates { get; set; }

        public ModeOfPayment ModeOfPayment { get; set; }

        public User DonationReceiver { get; set; }

        public DateTime OnDateTime { get; set; }

        public ReceiptType ReceiptType { get; set; }

        public string GroupId { get; set; }
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