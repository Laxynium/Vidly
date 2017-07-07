using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject;
using Ninject.Syntax;
using Ninject.Web.Common;
using Vidly.Domain.Abstract;
using Vidly.Infrastructure.Concrete;
using IDependencyResolver = System.Web.Mvc.IDependencyResolver;

namespace Vidly.Infrastructure
{
    public class NinjectDependencyResolver:IDependencyResolver
    {
        private IKernel _kernel;

        public NinjectDependencyResolver(IKernel kernel)
        {
            _kernel = kernel;
            AddBindings();
        }
        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }

        void AddBindings()
        {
            _kernel.Bind<ICustomerRepository>().To<EFCustomerRepository>();
            _kernel.Bind<IMembershipTypeRepository>().To<EFMembershipTypeRepository>();

            _kernel.Bind<IMovieRepository>().To<EFMovieRepository>();
            _kernel.Bind<IGenreRepository>().To<EFGenreRepository>();

            _kernel.Bind<IRentalRepository>().To<EFRentalRepository>();
        }
    }
}