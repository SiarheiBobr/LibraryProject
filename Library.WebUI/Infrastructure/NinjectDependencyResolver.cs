using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : Microsoft.AspNet.SignalR.DefaultDependencyResolver,
    System.Web.Http.Dependencies.IDependencyResolver
    {
        public readonly IKernel Kernel;

        public NinjectDependencyResolver(string moduleFilePattern)
            : base()
        {
            Kernel = new StandardKernel();
            Kernel.Load(moduleFilePattern);

        }
        public override object GetService(Type serviceType)
        {
            var service = Kernel.TryGet(serviceType) ?? base.GetService(serviceType);
            return service;
        }

        public override IEnumerable<object> GetServices(Type serviceType)
        {
            IEnumerable<object> services = Kernel.GetAll(serviceType).ToList();
            if (services.Count() == 0)
            {
                services = base.GetServices(serviceType) ?? services;
            }
            return services;
        }

        public System.Web.Http.Dependencies.IDependencyScope BeginScope()
        {
            return this;
        }

    }
}