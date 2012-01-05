using System;
using Telerik.OpenAccess;

namespace saibabacharityreceiptorDL
{
    [Persistent]
    public class User
    {
        public User()
        {
            Id = DateTime.Now.ToString("%yy%mm%dd%MM%HH%ss");
            Lasttriedtime = DateTime.Now;
        }

        public string Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public int Failcount { get; set; }

        public DateTime Lasttriedtime { get; set; }

        public bool IsheDonationReceiver { get; set; }

        public bool IsheAdmin { get; set; }
    }
}