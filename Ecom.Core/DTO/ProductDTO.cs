using Ecom.Core.Entites.Product;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Core.DTO
{
    public record ProductDTO
    {
        public string name { get; set; }
        public string description { get; set; }
        public decimal price { get; set; }
        public virtual List<PhotoDTO> photos { get; set; }
        public string CategoryName { get; set; }
    }
    public record PhotoDTO
    {
        public string ImageName { get; set; }
        public int productId { get; set; }
    }
    public record AddProductDTO
    {
        public string name { get; set; }
        public string description { get; set; }
        public decimal NewPrice { get; set; }
        public decimal OldPrice { get; set; }
        public int CategoryId { get; set; }
        public IFormFileCollection Photo { get; set; }
    }
    public record UpdateProductDTO : AddProductDTO
    {
        public int Id { get; set; }
    }
}
