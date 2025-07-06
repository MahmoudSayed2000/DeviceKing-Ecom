using AutoMapper;
using Ecom.Core.DTO;
using Ecom.Core.Entites.Product;
using Ecom.Core.Interfaces;
using Ecom.Core.Services;
using Ecom.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly AppDbContext context;
        private readonly IMapper mapper;
        private readonly IImageManagmentService imageManagmentService;
        public ProductRepository(AppDbContext context, IMapper mapper, IImageManagmentService imageManagmentService) : base(context)
        {
            this.context = context;
            this.mapper = mapper;
            this.imageManagmentService = imageManagmentService;
        }

        public async Task<bool> AddAsync(AddProductDTO productDTO)
        {
            if (productDTO == null)
            {
                return false;
            }
            var product = mapper.Map<Product>(productDTO);
            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();

            var ImagePath = await imageManagmentService.AddImagesAsync(productDTO.Photo,productDTO.name);

            var photo = ImagePath.Select(path => new Photo
            {
                ImageName = path,
                productId = product.Id,
            }).ToList();
            await context.Photos.AddRangeAsync(photo);
            await context.SaveChangesAsync();
            return true;
        }


        public async Task<bool> UpdateAsync(UpdateProductDTO updateproductDTO)
        {
            if (updateproductDTO is null)
            {
                return false;
            }
            var FindProduct = await context.Products.Include(x => x.category)
                .Include(x => x.photos)
                .FirstOrDefaultAsync(x => x.Id == updateproductDTO.Id);
            if (FindProduct is null)
            {
                return false;
            }
            mapper.Map(updateproductDTO, FindProduct);
            var FindPhoto = await context.Photos.Where(m => m.productId == updateproductDTO.Id).ToListAsync();

                foreach (var item in FindPhoto)
                {
                     imageManagmentService.DeleteImageAsync(item.ImageName);
                }
                context.Photos.RemoveRange(FindPhoto);
                await context.SaveChangesAsync();

            var ImagePath = await imageManagmentService.AddImagesAsync(updateproductDTO.Photo, updateproductDTO.name);
            var photo = ImagePath.Select(path => new Photo
            {
                ImageName = path,
                productId = FindProduct.Id,
            }).ToList();

            await context.Photos.AddRangeAsync(photo);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Product product)
        {
            var photo = await context.Photos.Where(x => x.productId == product.Id).ToListAsync();

            foreach (var item in photo)
            { 
                imageManagmentService.DeleteImageAsync(item.ImageName);
            }

            context.Products.Remove(product);
            await context.SaveChangesAsync();
            return true;

        }


    }
}
