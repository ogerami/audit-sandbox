using System.Collections.Generic;
using System.Web.Http;
using AuditSandbox.Models;
using AuditSandbox.Service;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;

namespace AuditSandbox.Ioc
{
    public static class SimpleInjector
    {
        public static void Initialize(HttpConfiguration httpConfiguration)
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            container.Register<IList<TestModel>>(() => new List<TestModel>(), Lifestyle.Singleton);
            container.Register<IList<LogModel<TestModel>>>(() => new List<LogModel<TestModel>>(), Lifestyle.Singleton);

            container.Register<IAuditService<TestModel>, AuditService<TestModel>>();
            container.Verify();

            httpConfiguration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
        }
    }
}