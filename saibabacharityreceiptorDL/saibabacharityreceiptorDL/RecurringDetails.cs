using System;
using Telerik.OpenAccess;

namespace saibabacharityreceiptorDL
{
    [Persistent]
    public class RecurringDetails
    {
        public DateTime DueDate { get; set; }

        public ModeOfPayment ModeOfPayment { get; set; }

        public string Amount { get; set; }
    }
}