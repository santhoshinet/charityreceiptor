using Telerik.OpenAccess;

namespace saibabacharityreceiptorDL
{
    [Persistent]
    public class Receiptor
    {
        public Receiptor()
        {
            DonationReceiver = new DonationReceivers();
        }

        public string ReceiptNumber { get; set; }

        public string DateReceived { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public string Contact { get; set; }

        public string DonationAmount { get; set; }

        public string DonationAmountinWords { get; set; }

        public string ModeOfPayment { get; set; }

        public DonationReceivers DonationReceiver { get; set; }
    }
}