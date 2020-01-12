using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plymouth_Pub.Models.Cart
{
    [Serializable] 
    public class Cart : IEnumerable<CartItem>
    {
        public Cart()
        {
            this.cartItems = new List<CartItem>();
        }

        private List<CartItem> cartItems;

        public int Count
        {
            get
            {
                return this.cartItems.Count;
            }
        }

        public decimal TotalAmount
        {
            get
            {
                decimal totalAmount = 0.0m;
                foreach (var cartItem in this.cartItems)
                {
                    totalAmount = totalAmount + cartItem.Amount;
                }
                return totalAmount;
            }
        }
        public bool AddProduct(int ProductId)
        {
            var findItem = this.cartItems
                            .Where(s => s.Id == ProductId)
                            .Select(s => s)
                            .FirstOrDefault();

            if (findItem == default(Models.Cart.CartItem))
            {   
                using (Models.PlymouthEntities db = new PlymouthEntities())
                {
                    var product = (from s in db.Products
                                   where s.Id == ProductId
                                   select s).FirstOrDefault();
                    if (product != default(Models.Product))
                    {
                        this.AddProduct(product);
                    }
                }
            }
            else
            {   
                findItem.Quantity += 1;
            }
            return true;
        }

        private bool AddProduct(Product product)
        {
            var cartItem = new Models.Cart.CartItem()
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                DefaultImageURL = product.DefaultImageURL,
                Quantity = 1
            };

            this.cartItems.Add(cartItem);
            return true;
        }

        public bool RemoveProduct(int ProductId)
        {
            var findItem = this.cartItems
                            .Where(s => s.Id == ProductId)
                            .Select(s => s)
                            .FirstOrDefault();

            if (findItem == default(Models.Cart.CartItem))
            {
            }
            else
            {   
                this.cartItems.Remove(findItem);
            }
            return true;
        }

        public bool ClearCart()
        {
            this.cartItems.Clear();
            return true;
        }

        public List<Models.OrderDetail> ToOrderDetailList(int orderId)
        {
            var result = new List<Models.OrderDetail>();
            foreach (var cartItem in this.cartItems)
            {
                result.Add(new Models.OrderDetail()
                {
                    Name = cartItem.Name,
                    Price = cartItem.Price,
                    Quantity = cartItem.Quantity,
                    OrderId = orderId
                });
            }
            return result;
        }

        #region IEnumerator

        IEnumerator<CartItem> IEnumerable<CartItem>.GetEnumerator()
        {
            return this.cartItems.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.cartItems.GetEnumerator();
        }

        #endregion
    }
}