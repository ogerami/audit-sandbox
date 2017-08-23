using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AuditSandbox.Models;
using AuditSandbox.Types;

namespace AuditSandbox.Controllers
{
    public class TestController : ApiController
    {
        private readonly IList<TestModel> _testModels;
        private readonly IList<LogModel<TestModel>> _logModels;

        public TestController(IList<TestModel> testModels,
            IList<LogModel<TestModel>> logModels)
        {
            _testModels = testModels;
            _logModels = logModels;
        }

        [HttpGet]
        [Route("api/test")]
        public IHttpActionResult Get()
        {
           return Ok(_testModels);
        }

        [HttpPost]
        [Route("api/test")]
        public IHttpActionResult Post(TestModel testModel)
        {
            _testModels.Add(testModel);
            var logModel = new LogModel<TestModel>(testModel, RequestType.Add)
            {
                CreationTime = DateTime.Now.AddDays(testModel.Id*-1)
            };
            _logModels.Add(logModel);
            
            return Ok(testModel);
        }

        [HttpDelete]
        [Route("api/test/{testId:int}")]
        public IHttpActionResult Delete(int testId)
        {
            var testModel = _testModels.SingleOrDefault(model => model.Id == testId);
            if(testModel == null)
                throw new NullReferenceException(nameof(testModel));

            _testModels.Remove(testModel);
            var logModel = new LogModel<TestModel>(testModel, RequestType.Delete);
            _logModels.Add(logModel);

            return Ok();
        }
    }
}