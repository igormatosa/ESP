using System.Collections.Generic;

namespace Aucxis.Eprw.Reporting.Dataservice.Services.Validation
{
    public class ValidationError
    {
        public string ErrorMessage { get; set; }
        public List<string> PropertyNames { get; set; }
    }
}
