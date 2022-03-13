using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class ProductWithFilterForCountSpecification : BaseSpecification<Product>
    {
        public ProductWithFilterForCountSpecification(ProductsSpecificationParameters productsSpecificationParameters) : base(x =>
            (string.IsNullOrEmpty(productsSpecificationParameters.Search) || x.ProductName.ToLower().Contains(productsSpecificationParameters.Search)) &&
            (!productsSpecificationParameters.BrandID.HasValue || x.ProductBrandID == productsSpecificationParameters.BrandID) && (!productsSpecificationParameters.TypeID.HasValue || x.ProductTypeID == productsSpecificationParameters.TypeID))
        {

        }
    }
}
