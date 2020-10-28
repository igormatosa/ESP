using System;

namespace Aucxis.Eprw.Website.Models
{
    public class ReportModel
    {
        public int OrderNumber { get; set; }
        public string SSCCNumber { get; set; }
        public DateTime Timestamp  { get; set; }
        public string DeviceID { get; set; }
        public string StatusValidate { get; set; }
        public string Assets { get; set; }
    }
}
