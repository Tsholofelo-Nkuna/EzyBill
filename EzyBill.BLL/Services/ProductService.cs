using EzyBill.BLL.Interfaces;
using EzyBill.BLL.Models.DataTranserObjects;
using EzyBill.DAL.Entities;
using EzyBill.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzyBill.BLL.Services
{
    public class ProductService: IProductService
    {
        private readonly ProductRepository _productRepository;
        private readonly ICurrentUserContext<string> _currentUserContext;
        public ProductService(ProductRepository productRepository, ICurrentUserContext<string> currentUserContext)
        {
            _productRepository = productRepository;
            _currentUserContext = currentUserContext;
        }
        public IEnumerable<ProductDto> Add(List<ProductDto> productLineItemDto)
        {
            var inserts = productLineItemDto.Select(x => new ProductEntity {
               Name = x.Name,
               Description = x.Description,
               Price = x.Price,
            }).ToList();
           this._productRepository.Insert(inserts, this._currentUserContext.CurrentUserId);
           this._productRepository.SaveChanges();
          return inserts.Select(x => new ProductDto { 
              Description = x.Description,
              Price = x.Price,
              Name = x.Name,
              Id = x.Id
          });
        }

       
    }
}
