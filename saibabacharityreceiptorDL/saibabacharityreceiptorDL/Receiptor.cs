using Telerik.OpenAccess;

namespace saibabacharityreceiptorDL
{
    [Persistent]
    public class Receiptor
    {
        public Receiptor()
        {
            UserLog = new UserLog();
        }

        public string ReceiptNumber { get; set; }

        public string DateReceived { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public string Contact { get; set; }

        public string DonationAmount { get; set; }

        public string DonationAmountinWords { get; set; }

        public ModeOfPayment ModeOfPayment { get; set; }

        public UserLog UserLog { get; set; }
    }

    public enum ModeOfPayment
    {
        Cash,
        Cheque,
        Online,
        Mobile,
        Goods
    }
}