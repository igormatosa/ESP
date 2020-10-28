using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Aucxis.Eprw.Website.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Aucxis.Eprw.Website
{
    [Authorize]
    public class ReportController : Controller
    {

        private readonly ILogger<ReportController> _logger;
        private readonly IConfiguration _Configure;
        private readonly string apiReportingBaseUrl;

        public ReportController(ILogger<ReportController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _Configure = configuration;

            apiReportingBaseUrl = _Configure.GetValue<string>("WebAPIReportingBaseUrl");
        }

        public async Task<IActionResult> StockOverview()
        {

            List<ReportModel> reportList = new List<ReportModel>();
            using (var httpClient = new HttpClient())
            {
                using var response = await httpClient.GetAsync(apiReportingBaseUrl);
                string apiResponse = await response.Content.ReadAsStringAsync();
                reportList = JsonConvert.DeserializeObject<List<ReportModel>>(apiResponse);
            }

            return View(reportList);
        }
    }
}


