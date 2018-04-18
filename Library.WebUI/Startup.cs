using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using Library.Domain.Abstract;
using Library.WebUI.Hubs;
using Library.WebUI.Infrastructure;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNet.SignalR.Infrastructure;
using Microsoft.Owin;
using Ninject;
using Ninject.Web.Common;
using Owin;

[assembly: OwinStartup(typeof(Library.WebUI.Startup))]

namespace Library.WebUI
{
    public class Startup
    {

        public void Configuration(IAppBuilder app)
        {


            //HttpConfiguration config = new HttpConfiguration();
            //WebApiConfig.Register(config);
            //app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            //NinjectWebCommon.Start();
            //config.DependencyResolver = new NinjectDependencyResolver(NinjectWebCommon.bootstrapper.Kernel);
            //app.UseWebApi(config);
            
            app.MapSignalR();
        }

        //public static class NinjectWebCommon
        //{
        //    private static bool _isStarted;

        //    internal static readonly Bootstrapper bootstrapper = new Bootstrapper();

        //    /// <summary>
        //    /// Starts the application
        //    /// </summary>
        //    public static void Start()
        //    {
        //        // When creating OWIN TestService instances during unit tests
        //        // Start() method might be called several times
        //        // This check ensures that Ninject kernel is initialized only once per process
        //        if (_isStarted)
        //            return;

        //        _isStarted = true;

        //        bootstrapper.Initialize(CreateKernel);
        //    }

        //    /// <summary>
        //    /// Creates the kernel that will manage your application.
        //    /// </summary>
        //    /// <returns>The created kernel.</returns>
        //    internal static IKernel CreateKernel()
        //    {
        //        var kernel = new StandardKernel();
        //        RegisterServices(kernel);
        //        return kernel;
        //    }

        //    /// <summary>
        //    /// Load your modules or register your services here!
        //    /// </summary>
        //    /// <param name="kernel">The kernel.</param>
        //    private static void RegisterServices(IKernel kernel)
        //    {
        //        // DI for SignalR
        //        GlobalHost.DependencyResolver = new Microsoft.AspNet.SignalR.Ninject.NinjectDependencyResolver(kernel);
        //        // DI for MVC
        //        DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));

        //        // Binding code here
        //        kernel.Bind<ICommentRepository>().ToSelf().InSingletonScope();
        //        kernel.Bind(typeof(IHubConnectionContext<dynamic>)).ToMethod(context =>
        //            GlobalHost.DependencyResolver.Resolve<IConnectionManager>().GetHubContext<BoardHub>().Clients
        //            ).WhenInjectedInto<ICommentRepository>();
        //    }
        //}

        //public void Configuration(IAppBuilder app)
        //{
        //    var dependencyResolver = new NinjectDependencyResolver("*.dll");

        //    var httpConfiguration = new HttpConfiguration();
        //    httpConfiguration.DependencyResolver = dependencyResolver;
        //    app.UseWebApi(httpConfiguration);

        //    var hubConfig = new HubConfiguration { Resolver = dependencyResolver };
        //    app.MapSignalR(hubConfig);
        //}




        //public void Configuration(IAppBuilder app)
        //{
        //    // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888

        //    //var kernel = new StandardKernel();
        //    //var resolver = new NinjectSignalRDependencyResolver(kernel);

        //    ////kernel.Bind<ICommentRepository>()
        //    ////    .To<Microsoft.AspNet.SignalR.Comment.StockTicker>()  // Bind to StockTicker.
        //    ////    .InSingletonScope();  // Make it a singleton object.

        //    //kernel.Bind(typeof(IHubConnectionContext<dynamic>)).ToMethod(context =>
        //    //        resolver.Resolve<IConnectionManager>().GetHubContext<BoardHub>().Clients
        //    //            ).WhenInjectedInto<ICommentRepository>();

        //    //var config = new HubConfiguration();
        //    //config.Resolver = resolver;
        //    app.MapSignalR();
        //    //Microsoft.AspNet.SignalR.StockTicker.Startup.ConfigureSignalR(app, config);
        //}
        //public void Configuration(IAppBuilder app)
        //{
        //    var unityHubActivator = new MvcHubActivator();

        //    GlobalHost.DependencyResolver.Register(
        //        typeof(IHubActivator),
        //        () => unityHubActivator);

        //    app.MapSignalR();
        //}

        //public class MvcHubActivator : IHubActivator
        //{
        //    public IHub Create(HubDescriptor descriptor)
        //    {
        //        return (IHub)DependencyResolver.Current
        //            .GetService(descriptor.HubType);
        //    }
        //}
    }
}
