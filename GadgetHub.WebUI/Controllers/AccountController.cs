using GadgetHub.WebUI.Infrastructure.Abstract;
using GadgetHub.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GadgetHub.WebUI.Controllers
{
	public class AccountController : Controller
	{
		IAuthProvider authProvider;

		public AccountController(IAuthProvider auth)
		{
			authProvider = auth; ;
		}

		public ViewResult Login()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Login(LoginViewModel model, string returnUrl)
		{
			if (ModelState.IsValid)
			{
				// proceed with admin dashboard
				if (authProvider.Authenticate(model.UserName, model.Password))
				{
					return Redirect(returnUrl ?? Url.Action("Index", "Admin"));
				}

				// username or password wrong
				else
				{
					ModelState.AddModelError("", "Incorrect username or password");
					return View();
				}
			}
			else
			{
				return View();
			}
		}
	}
}