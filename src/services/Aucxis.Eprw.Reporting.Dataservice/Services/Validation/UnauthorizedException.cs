using System;

namespace Aucxis.Eprw.Reporting.Dataservice.Services.Validation
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException()
            : base("Unauthorized")
        {
        }
    }
}
