using AutoMapper;
using Mango.Services.ProductAPI.DbContext;
using Mango.Services.ProductAPI.Models;
using Mango.Services.ProductAPI.Models.DtoS;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace Mango.Services.ProductAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private IMapper _mapper;

        public ProductRepository(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
         
        }
        public async Task<ProductDto> CreateUpdateProduct(ProductDto productDto)
        {
            var product = _mapper.Map<ProductDto, Product>(productDto);

            if(product.ProductId > 0)
            {
                _dbContext.Products.Update(product);
            }
            else
            {
                _dbContext.Products.Add(product);
            }

            _dbContext.SaveChanges();

            return _mapper.Map<Product, ProductDto>(product);
        }

        public async Task<bool> DeleteProduct(int productId)
        {
            try
            {
                var product = await _dbContext.Products.Where(x => x.ProductId == productId).FirstOrDefaultAsync();

                if(product is null)
                {
                    return false;
                }

                _dbContext.Products.Remove(product);
                _dbContext.SaveChanges();

                return true;
            }
            catch(Exception ex) 
            {
                return false;
            }
        }

        public async Task<ProductDto> GetProductById(int productID)
        {
            var products = await _dbContext.Products.Where(x => x.ProductId == productID).FirstOrDefaultAsync();
            return _mapper.Map<ProductDto>(products);
        }

        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            List<Product> products = await _dbContext.Products.ToListAsync();
            return _mapper.Map<List<ProductDto>>(products);
        }
    }
}
