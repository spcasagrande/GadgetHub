using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GadgetHub.Domain.Abstract;
using GadgetHub.WebUI.Models;

namespace GadgetHub.WebUI.Controllers
{
	public class ProductController : Controller
	{
		private IProductRepository myRepository;

		public ProductController(IProductRepository productRepository)
		{
			this.myRepository = productRepository;
		}

		public int PageSize = 3;
		public ViewResult List(string category, int page = 1)
		{
			ProductListViewModel model = new ProductListViewModel
			{
				Products = myRepository.Products
				.Where(p => category == null || p.Category == category)
				.OrderBy(p => p.ProductID)
				.Skip((page - 1) * PageSize)
				.Take(PageSize),

				PagingInfo = new PagingInfo
				{
					CurrentPage = page,
					ItemsPerPage = PageSize,
					TotalItems = category == null ?
									myRepository.Products.Count() :
									myRepository.Products.Where(x => x.Category == category).Count()
				},
				CurrentCategory = category
			};

			return View(model);
		}
	}
}