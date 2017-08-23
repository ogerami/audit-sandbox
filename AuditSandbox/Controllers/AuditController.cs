using System;
using System.Diagnostics;
using System.Web.Http;
using AuditSandbox.Models;
using AuditSandbox.Service;

namespace AuditSandbox.Controllers
{
    public class AuditController : ApiController
    {
        private readonly IAuditService<TestModel> _auditService;

        public AuditController(IAuditService<TestModel> auditService)
        {
            _auditService = auditService;
            _auditService.DeleteEventHandler += (sender, model) =>
            {
                Debug.WriteLine($"Deleted item: {model.Id}");
            };
        }

        [HttpGet]
        [Route("api/audit/{auditDateTime:DateTime}")]
        public IHttpActionResult Get(DateTime auditDateTime)
        {
            var testAudit = _auditService.Load(auditDateTime);

            return Ok(testAudit);
        }
    }
}