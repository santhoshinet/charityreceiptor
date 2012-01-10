using System;
using System.Collections.Generic;
using saibabacharityreceiptorDL;

namespace saibabacharityreceiptor
{
    public class LocalUser
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public bool IsheDonationReceiver { get; set; }

        public bool IsheAdmin { get; set; }
    }

    public class BaseReceipt
    {
        public string ReceiptNumber { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public string Contact { get; set; }

        public DateTime OnDateTime { get; set; }

        public string DonationReceiverName { get; set; }
    }

    public class LocalRegularReceipt : BaseReceipt
    {
        public string DonationAmount { get; set; }

        public string DonationAmountinWords { get; set; }

        public ModeOfPayment ModeOfPayment { get; set; }
    }

    public class LocalRecurrenceReceipt : BaseReceipt
    {
        public LocalRecurrenceReceipt()
        {
            RecurringDates = new List<DateTime>();
        }

        public IList<DateTime> RecurringDates { get; set; }

        public string DonationAmount { get; set; }

        public string DonationAmountinWords { get; set; }

        public ModeOfPayment ModeOfPayment { get; set; }
    }

    public class LocalMerchandiseReceipt : BaseReceipt
    {
        public string MerchandiseItem { get; set; }

        public string Value { get; set; }
    }

    public class LocalServicesReceipt : BaseReceipt
    {
        public int HoursServed { get; set; }
    }
}