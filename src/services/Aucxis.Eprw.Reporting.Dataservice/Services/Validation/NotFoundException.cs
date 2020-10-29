using System;

namespace Aucxis.Eprw.Reporting.Dataservice.Services.Validation
{
    public class NotFoundException : Exception
    {
        public NotFoundException()
            : base("Object not found")
        {
        }

        public NotFoundException(string message)
           : base(message)
        {
        }
    }
}
