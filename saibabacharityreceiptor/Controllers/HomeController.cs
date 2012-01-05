using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using saibabacharityreceiptorDL;

namespace saibabacharityreceiptor.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("LogOn", "Account");
            var modeofpayments = new List<string> { "Cash", "Cheque", "Online", "Mobile", "Goods" };
            ViewData["modeOfPayment"] = modeofpayments;
            var scope = ObjectScopeProvider1.ObjectScope();
            var receivers = (from c in scope.GetOqlQuery<User>().ExecuteEnumerable()
                             where c.IsheDonationReceiver.Equals(true)
                             select c).ToList();
            var donationReceivers = receivers.Select(receiver => receiver.Username).ToList();
            ViewData["donationReceivers"] = donationReceivers;
            return View();
        }
    }
}