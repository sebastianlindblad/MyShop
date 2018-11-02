using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using MyShop.Core.Models;

namespace MyShop.DataAccess.InMemory
{
    public class ProductRepository
    {
        // create a memory cache for storing items internally in VS/C#
        ObjectCache cache = MemoryCache.Default;
        List<Product> products;

        // create a list of products
        public ProductRepository()
        {
            products = cache["products"] as List<Product>;
            if (products == null) {
                products = new List<Product>();
            }
        }

        // commit item(s) to the cache and list
        public void Commit()
        {
            cache["products"] = products;
        }

        // add a product to the list
        public void Insert(Product p)
        {
            products.Add(p);
        }

        // update a product in the list
        public void Update(Product product)
        {
            Product productToUpdate = products.Find(p => p.Id == product.Id);

            if(productToUpdate != null)
            {
                productToUpdate = product;
            }
            else
            {
                throw new Exception("Product not found");
            }
        }

        // find a product in the list
        public Product Find(string Id)
        {
            Product product = products.Find(p => p.Id == Id);

            if (product != null)
            {
                return product;
            }
            else
            {
                throw new Exception("Product not found");
            }
        }

        // returns a list of products that can be queried
        public IQueryable<Product> Collection()
        {
            return products.AsQueryable();
        }

        // delete a product from the list
        public void Delete (string Id)
        {
            Product productToDelete = products.Find(p => p.Id == Id);

            if (productToDelete != null)
            {
                products.Remove(productToDelete);
            }
            else
            {
                throw new Exception("Product not found");
            }
        }
    }
}
