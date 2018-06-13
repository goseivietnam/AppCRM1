using System;
using System.Collections.Generic;
using System.Text;

namespace AppCRM.Models
{
    public class Document
    {
        public Guid? DocumentID { get; set; }

        public Guid? ObjectID { get; set; }

        public string DocumentName { get; set; }

        public string DocumentType { get; set; }

        public byte[] DocumentData { get; set; }

        public string DocumentDataString { get; set; }

        public string ReferenceForm { get; set; }

        //public DocReferenceFrom ReferenceFrom { get; set; }

        public DateTime? UploadedDate { get; set; }

        public int FileSize { get; set; }

        public string UploadedDateString { get; set; }

        public Guid UserRoleModuleID { get; set; }

        public byte[] Thumbnail { get; set; }

        public string ThumbnailUri { get; set; }
    }
}
