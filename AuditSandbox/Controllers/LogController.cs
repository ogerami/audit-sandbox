using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using AuditSandbox.Models;

namespace AuditSandbox.Controllers
{
    public class LogController : ApiController
    {
        private readonly IList<LogModel<TestModel>> _logModels;

        public LogController(IList<LogModel<TestModel>> logModels)
        {
            _logModels = logModels;
        }

        [HttpGet]
        [Route("api/log")]
        public IHttpActionResult GetAll()
        {
            return Ok(_logModels);
        }
    }
}