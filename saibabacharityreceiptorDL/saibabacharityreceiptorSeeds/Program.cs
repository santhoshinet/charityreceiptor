using System.Collections.Generic;
using System.Linq;
using saibabacharityreceiptorDL;

namespace saibabacharityreceiptorSeeds
{
    internal class Program
    {
        private static void Main()
        {
            //CreateUser("santhoshonet", "santhoshonet@gmail.com");
            CreateUser("santhosh", "santhoshonet@gmail.com");

            /*var scope = saibabacharityreceiptorDL.ObjectScopeProvider1.GetNewObjectScope();

            List<Receipt> receipts = (from c in scope.GetOqlQuery<Receipt>().ExecuteEnumerable()
                                      select c).ToList();

            foreach (Receipt receipt in receipts)
            {
                scope.Transaction.Begin();
                scope.Remove(receipt);
                scope.Transaction.Commit();
            } */

            /*
            List<User> users = (from c in scope.GetOqlQuery<User>().ExecuteEnumerable()
                                select c).ToList();

            List<SignatureImage> signatureImages = (from c in scope.GetOqlQuery<SignatureImage>().ExecuteEnumerable()
                                                    select c).ToList();

            foreach (SignatureImage signatureImage in signatureImages)
            {
                scope.Transaction.Begin();
                scope.Remove(signatureImage);
                scope.Transaction.Commit();
            }

            List<RecurringDetails> recurringDetailses = (from c in scope.GetOqlQuery<RecurringDetails>().ExecuteEnumerable()
                                                         select c).ToList();

            foreach (RecurringDetails recurringDetailse in recurringDetailses)
            {
                scope.Transaction.Begin();
                scope.Remove(recurringDetailse);
                scope.Transaction.Commit();
            }

            List<Receipt> receipts = (from c in scope.GetOqlQuery<Receipt>().ExecuteEnumerable()
                                      select c).ToList();
            foreach (var receipt in receipts)
            {
                scope.Transaction.Begin();
                scope.Remove(receipt);
                scope.Transaction.Commit();
            }
             * */
        }

        private static void CreateUser(string username, string email)
        {
            var scope = ObjectScopeProvider1.GetNewObjectScope();
            List<User> users = (from c in scope.GetOqlQuery<User>().ExecuteEnumerable()
                                where c.Username.ToLower().Trim().Equals(username.Trim().ToLower())
                                select c).ToList();
            if (users.Count == 0)
            {
                scope.Transaction.Begin();
                var user = new User
                {
                    Email = email,
                    Failcount = 0,
                    IsheAdmin = true,
                    IsheDonationReceiver = true,
                    Username = username.Trim().ToLower()
                };
                scope.Add(user);
                scope.Transaction.Commit();
            }
            else
            {
                foreach (var user in users)
                {
                    scope.Transaction.Begin();
                    user.IsheDonationReceiver = true;
                    user.IsheAdmin = true;
                    scope.Add(user);
                    scope.Transaction.Commit();
                }
            }
        }
    }
}