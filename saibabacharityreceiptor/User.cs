using System;
using System.Collections.Generic;
using saibabacharityreceiptor.Controllers;
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

        public string FirstName { get; set; }

        public string LastName { get; set; }

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
            RecurringDatas = new List<RecurrenceData>();
        }

        public List<RecurrenceData> RecurringDatas { get; set; }
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