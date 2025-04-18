﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GadgetHub.Domain.Abstract;
using GadgetHub.Domain.Entities;

namespace GadgetHub.WebUI.Controllers
{
	[Authorize]
    public class AdminController : Controller
    {
        private IProductRepository repository;

        public AdminController(IProductRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index()
        {
            return View(repository.Products);
        }

        public ViewResult Edit(int productID)
        {
            Product product = repository.Products.FirstOrDefault(p => p.ProductID == productID);

            return View(product);
        }

		[HttpPost]
		public ActionResult Edit(Product product, HttpPostedFileBase image = null)
		{
			if (ModelState.IsValid)
			{
				if (image != null)
				{
					product.ImageMimeType = image.ContentType;
					product.ImageData = new byte[image.ContentLength];
					image.InputStream.Read(product.ImageData, 0, image.ContentLength);
				}

				repository.SaveProduct(product);
				TempData["message"] = string.Format("{0} has been saved", product.Name);
				return RedirectToAction("Index");
			}
			else
			{
				return View(product);
			}
		}

        public ViewResult Create()
        {
            return View("Edit", new Product());
        }

		public ActionResult Delete(int productID)
		{
			Product deletedProduct = repository.DeleteProduct(productID);

			if (deletedProduct != null)
			{
				TempData["message"] = string.Format("{0} has been deleted", deletedProduct.Name);
			}

			return RedirectToAction("Index");
		}
	}
}