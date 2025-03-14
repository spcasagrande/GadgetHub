using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GadgetHub.Domain.Entities
{
	public class CartLine
	{
		public Product Product { get; set; }
		public int Quantity { get; set; }
	}

	public class Cart
	{
		// Initiliazing the cart
		private List<CartLine> lineCollection = new List<CartLine>();

		// Getter for CartLine
		public IEnumerable<CartLine> Lines
		{
			get { return lineCollection; }
		}

		// Add item
		public void AddItem(Product myProduct, int myQuantity)
		{
			CartLine line = lineCollection
							.Where(p => p.Product.ProductID == myProduct.ProductID)
							.FirstOrDefault();

			if (line == null)
			{
				lineCollection.Add(new CartLine
				{
					Product = myProduct,
					Quantity = myQuantity
				});
			}
			else
			{
				line.Quantity += myQuantity;
			}
		}

		// Remove item
		public void RemoveLine(Product myProduct)
		{
			lineCollection.RemoveAll(x => x.Product.ProductID == myProduct.ProductID);
		}

		// Compute total cost
		public decimal ComputeTotalValue()
		{
			return lineCollection.Sum(x => x.Product.Price * x.Quantity);
		}

		// Emptying the cart
		public void Clear()
		{
			lineCollection.Clear();
		}
	}
}
