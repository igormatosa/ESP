using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aucxis.Eprw.Reporting.Dataservice.Models;
using Aucxis.Eprw.Reporting.Dataservice.Services;
using Aucxis.Eprw.Reporting.Webapi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Aucxis.Eprw.Reporting.Webapi.Controllers
{
    [Route("api/reporting")]
    [ApiController]
    public class ReportingController : ControllerBase
    {
        private readonly TestService _testService;

        public ReportingController(TestService testService)
        {
            _testService = testService;
        }

       [HttpGet]
        [Route("gettest")]
        public async Task<List<TestModel>> GetTest()
        {
            var testModels = await _testService.GetAllAsync();
            return testModels;
        }

            [HttpGet]
        public List<ReportModel> Get()
        {
            List<ReportModel> reportData = new List<ReportModel>
            {
                new ReportModel
                {
                    OrderNumber = 100110,
                    DeviceID = "4484-443",
                    Assets = "dAD-2234-3",
                    SSCCNumber = "3333-554-33245-3",
                    StatusValidate = "True",
                    Timestamp = DateTime.Now
                },
                new ReportModel
                {
                    OrderNumber = 100220,
                    DeviceID = "4222-443",
                    Assets = "SAD-2222-3",
                    SSCCNumber = "2222-554-33245-3",
                    StatusValidate = "True",
                    Timestamp = DateTime.Now
                },
                new ReportModel
                {
                    OrderNumber = 100330,
                    DeviceID = "4333-443",
                    Assets = "ZAD-2334-3",
                    SSCCNumber = "8888-554-33245-3",
                    StatusValidate = "True",
                    Timestamp = DateTime.Now
                }
            };
            return reportData;
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public void Post([FromBody] string value)
        {
        }


        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
