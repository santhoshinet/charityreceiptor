using System.Collections.Generic;
using System.Linq;
using saibabacharityreceiptorDL;

namespace saibabacharityreceiptorSeeds
{
    internal class Program
    {
        private static void Main()
        {
            CreateUser("santhoshonet","santhoshonet@gmail.com");
            CreateUser("santhosh", "santhoshonet@gmail.com");
        }

        private static void CreateUser(string username,string email)
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
        }
    }
}