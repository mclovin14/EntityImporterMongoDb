using System;
using System.Collections.Generic;
using System.Text;

namespace EntityImporterMongoDb.Utils
{
    public struct PropertyName
    {
        public static readonly byte[] ID = Encoding.UTF8.GetBytes("id");
        public static readonly byte[] NAME = Encoding.UTF8.GetBytes("id");
        public static readonly byte[] DOCUMENT = Encoding.UTF8.GetBytes("document");
        public static readonly byte[] DOCUMENTNUMBER = Encoding.UTF8.GetBytes("number");
        public static readonly byte[] DOCUMENTTYPE = Encoding.UTF8.GetBytes("type");
    }
}
