using System;

namespace AppCRM.Models
{
    public class ContactDocument
    {
        public ContactDocument() { }

        public Guid? ContactDocumentID { get; set; }
        public Guid? DocumentVersionID { get; set; }
        public Guid? ContactID { get; set; }
        public Guid? DocumentGroupID { get; set; }
        public bool Accepted { get; set; }
        public Guid? StatusID { get; set; }
        public Guid? VacancyID { get; set; }
        public string ContactTemplate { get; set; }
        public string UploadedDocument { get; set; }
        public string VerificationCode { get; set; }
        public Boolean VerificationCodeRequired { get; set; }
        public Byte[] Signature { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string SignatureString { get; set; }
        //public string VerificationCode { get; set; }
        public string DocumentName { get; set; }
        public string Template { get; set; }
        public bool SignatureRequired { get; set; }
        //public bool VerificationCodeRequired { get; set; }
        public bool AcceptanceRequired { get; set; }
        public bool UploadRequired { get; set; }
    }
}
