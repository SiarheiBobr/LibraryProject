using System;
using System.Web.Mvc;
using System.Web.Routing;
using Ninject;
using Library.Domain.Entities;
using Library.Domain.Abstract;
using System.Collections.Generic;
using System.Linq;
using Moq;
using Library.Domain.Concrete;
using System.Configuration;
//using Library.WebUI.Infrastructure.Abstract;
//using Library.WebUI.Infrastructure.Concrete;

namespace Library.WebUI.Infrastructure
{

    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel ninjectKernel;

        public NinjectControllerFactory()
        {
            ninjectKernel = new StandardKernel();
            AddBindings();
        }

        protected override IController GetControllerInstance(RequestContext
            requestContext, Type controllerType)
        {

            return controllerType == null
                ? null
                : (IController)ninjectKernel.Get(controllerType);
        }

        private void AddBindings()
        {
            ninjectKernel.Bind<IBookRepository>().To<EFBookRepository>();
            ninjectKernel.Bind<IBookingRepository>().To<EFBookingRepository>();
            ninjectKernel.Bind<ICommentRepository>().To<EFCommentRepository>();

            //Mock<IBookRepository> mock = new Mock<IBookRepository>();
            //mock.Setup(m => m.Books).Returns(new List<Book> {
            //    new Book { Title = "book1", Year = 2011 },
            //    new Book { Title = "book3", Year = 2000 },
            //    new Book { Title = "book3", Year = 1999 }
            //  }.AsQueryable());

            //ninjectKernel.Bind<IBookRepository>().ToConstant(mock.Object);
        }

    }

}
