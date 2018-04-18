using Library.Domain.Abstract;
using Library.Domain.Concrete;
using Library.WebUI.Hubs;
using Library.WebUI.Infrastructure;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNet.SignalR.Infrastructure;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Web.Common;
using Ninject.Web.Common.WebHost;
using System;
using System.Web;


[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Library.WebUI.NinjectWebCommon), "Start")]
namespace Library.WebUI
{
    
    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

            // THIS LINE DOES IT!!! Set our Ninject-based SignalRDependencyResolver as the SignalR resolver
            GlobalHost.DependencyResolver = new NinjectSignalRDependencyResolver(kernel);

            RegisterServices(kernel);
            return kernel;
        }

        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IBookRepository>().To<EFBookRepository>();
            kernel.Bind<IBookingRepository>().To<EFBookingRepository>();
            kernel.Bind<ICommentRepository>().To<EFCommentRepository>();

            kernel.Bind(typeof(IHubConnectionContext<dynamic>)).ToMethod(context =>
                GlobalHost.DependencyResolver.Resolve<IConnectionManager>().GetHubContext<BoardHub>().Clients
                ).WhenInjectedInto<ICommentRepository>();

            kernel.Bind(typeof(IHubConnectionContext<dynamic>)).ToMethod(context =>
                GlobalHost.DependencyResolver.Resolve<IConnectionManager>().GetHubContext<BookingHub>().Clients
                ).WhenInjectedInto<IBookingRepository>();


            ////kernel.Bind<ICommentRepository>()
            //    ////    .To<Microsoft.AspNet.SignalR.Comment.StockTicker>()  // Bind to StockTicker.
            //    ////    .InSingletonScope();  // Make it a singleton object.

            //    //kernel.Bind(typeof(IHubConnectionContext<dynamic>)).ToMethod(context =>
            //    //        resolver.Resolve<IConnectionManager>().GetHubContext<BoardHub>().Clients
            //    //            ).WhenInjectedInto<ICommentRepository>();
        }
    }
}