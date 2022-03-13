using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class ProductWithTypesAndBrandsSpecification : BaseSpecification<Product>
    {
        public ProductWithTypesAndBrandsSpecification(ProductsSpecificationParameters productsSpecificationParameters) : base(x => (string.IsNullOrEmpty(productsSpecificationParameters.Search) || x.ProductName.ToLower().Contains(productsSpecificationParameters.Search)) &&
            (!productsSpecificationParameters.BrandID.HasValue || x.ProductBrandID == productsSpecificationParameters.BrandID) && (!productsSpecificationParameters.TypeID.HasValue || x.ProductTypeID == productsSpecificationParameters.TypeID))
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
            AddOrderBy(x => x.ProductName);
            ApplyPaging(productsSpecificationParameters.PageSize * (productsSpecificationParameters.PageIndex - 1), productsSpecificationParameters.PageSize);

            if (!string.IsNullOrEmpty(productsSpecificationParameters.Sort))
            {
                switch (productsSpecificationParameters.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        AddOrderBy(p => p.Price);
                        break;
                    default:
                        AddOrderBy(n => n.ProductName);
                        break;
                }
            }
        }

        public ProductWithTypesAndBrandsSpecification(int ID) : base(x => x.Id == ID)
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
        }

    }
}
