using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Ninject;
using Moq;
using GadgetHub.Domain.Abstract;
using GadgetHub.Domain.Entities;
using GadgetHub.Domain.Concrete;
using System.Configuration;
using GadgetHub.WebUI.Infrastructure.Abstract;
using GadgetHub.WebUI.Infrastructure.Concrete;

namespace GadgetHub.WebUI.Infrastructure
{
	public class NinjectDependencyResolver : IDependencyResolver
	{
		private IKernel mykernel;

		public NinjectDependencyResolver(IKernel kernelParam)
		{
			mykernel = kernelParam;
			AddBindings();
		}

		public object GetService(Type myserviceType)
		{
			return mykernel.TryGet(myserviceType);
		}

		public IEnumerable<object> GetServices(Type myserviceType)
		{
			return mykernel.GetAll(myserviceType);
		}

		private void AddBindings()
		{
			/*
			Mock<IProductRepository> myMock = new Mock<IProductRepository>();
			myMock.Setup(m => m.Products).Returns(new List<Product> {
				new Product{ Name = "Laptop", Brand = "ASUS", Price = 600, Description = "A portable computer", Category = "Computers" },
				new Product{ Name = "IPhone", Brand = "Apple", Price = 1500, Description = "A smartphone", Category = "Phone" },
				new Product{ Name = "TV", Brand = "LG", Price = 400, Description = "A smart TV that can access the internet", Category = "Entertainment" },
			});
			mykernel.Bind<IProductRepository>().ToConstant(myMock.Object);
			*/

			mykernel.Bind<IProductRepository>().To<EFProductRepository>();

			EmailSettings emailSettings = new EmailSettings
			{
				WriteAsFile = bool.Parse(System.Configuration.ConfigurationManager.AppSettings["Email.WriteAsFile"] ?? "false")
			};

			mykernel.Bind<IOrderProcessor>().To<EmailOrderProcessor>().WithConstructorArgument("settings", emailSettings);

			mykernel.Bind<IAuthProvider>().To<FormsAuthProvider>();
		}
	}
}