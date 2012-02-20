using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using saibabacharityreceiptorDL;

namespace saibabacharityreceiptor.Controllers
{
    public class SignatureController : Controller
    {
        //
        // GET: /Signature/
        public FileContentResult Signature(string id)
        {
            try
            {
                var scope = ObjectScopeProvider1.GetNewObjectScope();
                List<SignatureImage> files = (from c in scope.GetOqlQuery<SignatureImage>().ExecuteEnumerable()
                                              where c.ID.ToString().Equals(id)
                                              select c).ToList();
                if (files.Count > 0)
                {
                    return File(files[0].Filedata, files[0].MimeType, files[0].Filename);
                }
            }
            catch (Exception)
            {
            }
            return null;
        }

        public static byte[] SignatureImage(string id)
        {
            try
            {
                var scope = ObjectScopeProvider1.GetNewObjectScope();
                List<SignatureImage> files = (from c in scope.GetOqlQuery<SignatureImage>().ExecuteEnumerable()
                                              where c.ID.ToString().Equals(id)
                                              select c).ToList();
                if (files.Count > 0)
                {
                    return files[0].Filedata;
                }
            }
            catch (Exception)
            {
            }
            return null;
        }
    }
}