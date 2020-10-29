using System;
using System.Collections.Generic;
using System.Linq;

namespace Aucxis.Eprw.Reporting.Dataservice.Services.Validation
{
    public class InvalidModelException : Exception
    {
        public InvalidModelException(IEnumerable<ValidationError> errors = null)
            : base("Invalid data")
        {
            Errors = errors ?? Enumerable.Empty<ValidationError>();
        }

        public IEnumerable<ValidationError> Errors { get; }
    }
}
