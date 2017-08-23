using System;
using System.Collections.Generic;
using System.Linq;
using AuditSandbox.Models;
using AuditSandbox.Types;

namespace AuditSandbox.Service
{
    public interface IAuditService<TModel> where TModel : ITestModel
    {
        event EventHandler<TModel> DeleteEventHandler;
        IList<TModel> Load(DateTime auditDateTime);
    }

    public class AuditService<TModel> : IAuditService<TModel>
        where TModel : ITestModel
    {
        private readonly IList<LogModel<TModel>> _logModels;
        private readonly IList<TModel> _models;

        public event EventHandler<TModel> DeleteEventHandler;

        public AuditService(IList<LogModel<TModel>> logModels)
        {
            _logModels = logModels;
            _models = new List<TModel>();
        }

        public IList<TModel> Load(DateTime auditDateTime)
        {
            foreach (var logModel in _logModels)
            {
                if (logModel.RequestType == RequestType.Add)
                {
                    _models.Add(logModel.GetObjectAsModel());
                }
                else if (logModel.RequestType == RequestType.Delete)
                {
                    var objectAsModel = logModel.GetObjectAsModel();
                    var log = _models.Single(model => model.Id == objectAsModel.Id);
                    _models.Remove(log);
                    OnDelete(objectAsModel);
                }
            }

            return _models;
        }

        protected virtual void OnDelete(TModel model)
        {
            var handler = DeleteEventHandler;
            handler?.Invoke(this, model);
        }
    }
}