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
		private IOrderProcessor orderProcessor;

		public CartController(IProductRepository repo, IOrderProcessor proc)
		{
			repository = repo;
			orderProcessor = proc;
		}

		// Add to cart
		public RedirectToRouteResult AddToCart(Cart cart, int productID, string returnUrl)
		{
			Product product = repository.Products.FirstOrDefault(p => p.ProductID == productID);

			if (product != null)
			{
				cart.AddItem(product, 1);
			}

			return RedirectToAction("Index", new { returnUrl });
		}

		// Remove from cart
		public RedirectToRouteResult RemoveFromCart(Cart cart, int productID, string returnUrl)
		{
			Product product = repository.Products.FirstOrDefault(p => p.ProductID == productID);

			if (product != null)
			{
				cart.RemoveLine(product);
			}

			return RedirectToAction("Index", new { returnUrl });
		}

		public ViewResult Index(Cart cart, string returnUrl)
		{
			return View(new CartIndexViewModel
			{
				Cart = cart,
				ReturnUrl = returnUrl
			});
		}

		public PartialViewResult Summary(Cart cart)
		{
			return PartialView(cart);
		}

		public ViewResult Checkout()
		{
			return View(new ShippingDetails());
		}

		[HttpPost]
		public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails)
		{
			// empty cart check
			if (cart.Lines.Count() == 0)
			{
				ModelState.AddModelError("", "Sorry, your cart is empty!");
			}

			// check for errors in ModelState
			if (ModelState.IsValid)
			{
				orderProcessor.ProcessOrder(cart, shippingDetails);
				cart.Clear();
				return View("Completed", shippingDetails);
			}
			else
			{
				return View(shippingDetails);
			}
		}
	}
}