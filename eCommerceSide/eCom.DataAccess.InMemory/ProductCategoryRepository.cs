using eCom.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace eCom.DataAccess.InMemory
{
    public class ProductCategoryRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<ProductCategory> productsCategoris;

        public ProductCategoryRepository()
        {
            productsCategoris = cache["productsCategoris"] as List<ProductCategory>;
            if (productsCategoris == null)
            {
                productsCategoris = new List<ProductCategory>();
            }
        }

        public void Commit()
        {
            cache["productsCategoris"] = productsCategoris;
        }

        public void Insert(ProductCategory category)
        {
            productsCategoris.Add(category);
        }

        public void Update(ProductCategory category)
        {
            ProductCategory categoryToUpdate = productsCategoris.Find(x => x.Id == category.Id);
            if (categoryToUpdate != null)
            {
                categoryToUpdate = category;
            }
            else
            {
                throw new Exception("category not Fount");
            }
        }

        public ProductCategory Find(string Id)
        {
            ProductCategory category = productsCategoris.Find(x => x.Id == Id);
            if (category != null)
            {
                return category;
            }
            else
            {
                throw new Exception("category not Fount");
            }
        }

        public IQueryable<ProductCategory> Collection()
        {
            return productsCategoris.AsQueryable();
        }

        public void Delete(string Id)
        {
            ProductCategory categoryToDelete = productsCategoris.Find(x => x.Id == Id);
            if (categoryToDelete != null)
            {
                productsCategoris.Remove(categoryToDelete);
            }
            else
            {
                throw new Exception("category not Fount");
            }
        }
    }
}
