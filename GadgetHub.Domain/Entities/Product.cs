using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace GadgetHub.Domain.Entities
{
	public class Product
	{
		[HiddenInput (DisplayValue = false)]
		public int ProductID { get; set; }

		[Required(ErrorMessage = "Please enter product name")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Please enter product brand")]
		public string Brand { get; set; }

		[Required]
		[Range(0.01, double.MaxValue, ErrorMessage = "Please enter a price")]
		public decimal Price { get; set; }
		
		[DataType(DataType.MultilineText)]
		[Required(ErrorMessage = "Please enter product description")]
		public string Description { get; set; }

		[Required(ErrorMessage = "Please enter product category")]
		public string Category { get; set; }
	}
}
