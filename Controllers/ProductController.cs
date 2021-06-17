using AutoMapper;
using E_Shopper.Models.Services.Interfaces;
using E_Shopper.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shopper.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProduct _productRepo;
        private readonly IMapper _mapper;
        public ProductController(IProduct productRepo, IMapper mapper)
        {
            _productRepo = productRepo;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var dbProduct = await _productRepo.SellApprovedProducts();
            var product = _mapper.Map<IEnumerable<ApprovedProductToSale>>(dbProduct);
            return View(product);
        }
    }
}
