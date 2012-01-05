using System;
using Telerik.OpenAccess;

namespace saibabacharityreceiptorDL
{
    [Persistent]
    public class UserLog
    {
        public User DonationReceiver { get; set; }

        public DateTime OnDateTime { get; set; }

        public string TransactionType { get; set; }
    }
}