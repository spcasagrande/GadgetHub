using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GadgetHub.Domain.Abstract;
using GadgetHub.Domain.Entities;
using GadgetHub.WebUI.Models;

namespace GadgetHub.WebUI.Controllers
{
	public class CartController : Controller
	{
		private IProductRepository repository;

		public CartController(IProductRepository repo)
		{
			repository = repo;
		}

		private Cart GetCart()
		{
			Cart cart = (Cart)Session["Cart"];

			if (cart == null)
			{
				cart = new Cart();
				Session["Cart"] = cart;
			}

			return cart;
		}

		// Add to cart
		public RedirectToRouteResult AddToCart(Cart cart, int productID, string returnUrl)
		{
			Product product = repository.Products.FirstOrDefault(p => p.ProductID == productID);

			if (product != null)
			{
				GetCart().AddItem(product, 1);
			}

			return RedirectToAction("Index", new { returnUrl });
		}

		// Remove from cart
		public RedirectToRouteResult RemoveFromCart(int productID, string returnUrl)
		{
			Product product = repository.Products.FirstOrDefault(p => p.ProductID == productID);

			if (product != null)
			{
				GetCart().RemoveLine(product);
			}

			return RedirectToAction("Index", new { returnUrl });
		}

		public ViewResult Index(string returnUrl)
		{
			return View(new CartIndexViewModel
			{
				Cart = GetCart(),
				ReturnUrl = returnUrl
			});
		}
	}
}