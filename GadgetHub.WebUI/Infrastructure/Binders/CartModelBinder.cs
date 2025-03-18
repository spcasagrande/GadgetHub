using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GadgetHub.Domain.Entities;

namespace GadgetHub.WebUI.Infrastructure.Binders
{
	public class CartModelBinder : IModelBinder
	{
		private const string sessionKey = "Cart";

		public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
		{
			Cart cart = null;

			// get the Cart from the session
			if (controllerContext.HttpContext.Session != null)
			{
				cart = (Cart)controllerContext.HttpContext.Session[sessionKey];
			}

			// create the cart if there wasn't one in the session data
			if (cart == null)
			{
				cart = new Cart();
				if (controllerContext.HttpContext.Session != null)
				{
					controllerContext.HttpContext.Session[sessionKey] = cart;
				}
			}

			// return the cart
			return cart;
		}
	}
}