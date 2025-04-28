using AutoMapper;
using Ecom.Core.Interfaces;
using Ecom.Core.Services;
using Ecom.Infrastructure.Data;
using Ecom.Infrastructure.Repositories.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IImageManagmentService _imageManagmentService;
        public ICategoryRepository categoryRepository {get;}
        public IProductRepository productRepository { get; }
        public IPhotoRepository photoRepository { get; }
        public UnitOfWork(AppDbContext context, IMapper mapper, IImageManagmentService imageManagmentService)
        {
            _context = context;
            _mapper = mapper;
            _imageManagmentService = imageManagmentService;
            categoryRepository = new CategoryRepository(_context);
            photoRepository = new PhotoRepository(_context);
            productRepository = new ProductRepository(_context, _mapper, _imageManagmentService);
            
        }
    }
}
