﻿using eCom.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace eCom.DataAccess.InMemory
{
    class ProductRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<Product> products;

        public ProductRepository()
        {
            products = cache["products"] as List<Product>;
            if(products == null)
            {
                products = new List<Product>();
            }
        }

        public void Commit()
        {
            cache["products"] = products;
        }

        public void Insert(Product product)
        {
            products.Add(product);
        }

        public void Update(Product product)
        {
            Product productToUpdate = products.Find(x => x.Id == product.Id);
            if(productToUpdate != null) 
            {
                productToUpdate = product;
            }
            else
            {
                throw new Exception("Product not Fount");
            }
        }

        public Product Find(string Id)
        {
            Product product = products.Find(x => x.Id == Id);
            if (product != null)
            {
                return product;
            }
            else
            {
                throw new Exception("Product not Fount");
            }
        }

        public IQueryable<Product> Collection()
        {
            return products.AsQueryable();
        }

        public void Delete(string Id)
        {
            Product productToDelete = products.Find(x => x.Id == Id);
            if (productToDelete != null)
            {
                products.Remove(productToDelete);
            }
            else
            {
                throw new Exception("Product not Fount");
            }
        }
    }
}