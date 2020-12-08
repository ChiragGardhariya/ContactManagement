using ContactAPI.Manager;
using Ninject;
using Ninject.Extensions.ChildKernel;
using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;

namespace ContactAPI.App_Start
{
	public class NinjectResolver : IDependencyResolver
	{
		private IKernel kernel;

		public NinjectResolver() : this(new StandardKernel())
		{
		}

		public NinjectResolver(IKernel ninjectKernel, bool scope = false)
		{
			kernel = ninjectKernel;
			if (!scope)
			{
				AddBindings(kernel);
			}
		}
		public IDependencyScope BeginScope()
		{
			return new NinjectResolver(AddRequestBindings(new ChildKernel(kernel)), true);
		}

		public void Dispose()
		{
			throw new NotImplementedException();
		}

		public object GetService(Type serviceType)
		{
			return kernel.TryGet(serviceType);
		}

		public IEnumerable<object> GetServices(Type serviceType)
		{
			return kernel.GetAll(serviceType);
		}

		private void AddBindings(IKernel kernel)
		{
			// singleton and transient bindings go here
		}

		private IKernel AddRequestBindings(IKernel kernel)
		{
			kernel.Bind<IContact>().To<RepoContact>().InSingletonScope();
			return kernel;
		}
	}
}