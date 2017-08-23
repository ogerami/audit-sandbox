using System;
using AuditSandbox.Types;
using Newtonsoft.Json;

namespace AuditSandbox.Models
{
    public class LogModel<TModel>
    {
        public LogModel(TModel obj, 
            RequestType requestType)
        {
            LogId = Guid.NewGuid().ToString();
            RequestType = requestType;
            Object = JsonConvert.SerializeObject(obj);
            CreationTime = DateTime.Now;
        }

        public string LogId { get; set; }
        public RequestType RequestType { get; set; }
        public string Object { get; set; }
        public DateTime CreationTime { get; set; }

        public TModel GetObjectAsModel()
        {
            return JsonConvert.DeserializeObject<TModel>(Object);
        }
    }
}