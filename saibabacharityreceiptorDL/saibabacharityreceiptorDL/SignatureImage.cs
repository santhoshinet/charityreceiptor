using System;
using Telerik.OpenAccess;

namespace saibabacharityreceiptorDL
{
    [Persistent]
    public class SignatureImage
    {
        public byte[] Filedata { get; set; }

        public string Filename { get; set; }

        public string MimeType { get; set; }

        public Guid ID { get; set; }
    }
}