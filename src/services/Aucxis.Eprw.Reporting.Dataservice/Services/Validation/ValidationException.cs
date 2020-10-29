using System;
using System.Collections.Generic;
using System.Linq;

namespace Aucxis.Eprw.Reporting.Dataservice.Services.Validation
{
    public class ValidationException : Exception
    {
        public ValidationException(string message)
            : base(message)
        {
           
        }

        
    }
}
