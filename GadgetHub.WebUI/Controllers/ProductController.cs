using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GadgetHub.Domain.Abstract;

namespace GadgetHub.WebUI.Controllers
{
	public class ProductController : Controller
	{
		private IProductRepository myRepository;

		public ProductController(IProductRepository productRepository)
		{
			this.myRepository = productRepository;
		}

		public ViewResult List()
		{
			return View(myRepository.Products);
		}
	}
}