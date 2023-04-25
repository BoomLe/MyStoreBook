using System.Security.AccessControl;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Book.Data;
using Book.IRepository;
using Book.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Book.Unitity;
using Book.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Book.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IUnitityOfWork _unitityOfWork;

        

        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductsController(ILogger<ProductsController> logger, IUnitityOfWork db,IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _unitityOfWork = db;
            _webHostEnvironment = webHostEnvironment;
           
        }

        public IActionResult Index()
        {
            List<Products> listProducts = _unitityOfWork.Product.GetAll(includeProperties:"Category").ToList();

            return View(listProducts);
        }

        public IActionResult Upsert(int? id)
        {
          ViewModels productVM = new()
          {
            listCategories = _unitityOfWork.Category.GetAll().Select(
                p => new SelectListItem
                {
                    Text = p.Name,
                    Value = p.Id.ToString()
                }
            ),
            Product = new Products()
          };

          //create
          if(id == null || id == 0)
          {
            return View(productVM);
          }
          //Update
          else
          {
            productVM.Product = _unitityOfWork.Product.Get(p => p.Id == id);
            return View(productVM);
          }
            
            
        }
        [HttpPost]
        public IActionResult Upsert(ViewModels productVM, IFormFile? fileUrl)
        {
         
            if(ModelState.IsValid)
            {
                string pathRoot = _webHostEnvironment.WebRootPath;
              if(fileUrl != null)
               {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(fileUrl.FileName);
                string productPath = Path.Combine(pathRoot , @"images\product");

                if(!string.IsNullOrEmpty(productVM.Product.ImageUrl))
                {
                    string oldFilePath = Path.Combine(pathRoot, productVM.Product.ImageUrl.TrimStart('\\'));
                    if(System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                }

                using (var fileStream = new FileStream (Path.Combine(productPath , fileName),FileMode.Create))
                {
                    fileUrl.CopyTo(fileStream);
                };
                productVM.Product.ImageUrl = @"\images\product\" + fileName;
               }


               if(productVM.Product.Id == 0)
               {
                _unitityOfWork.Product.Add(productVM.Product);
               }
               else
               {
                _unitityOfWork.Product.Update(productVM.Product);
               }
              
              _unitityOfWork.Save();
              TempData["success"] = "Bạn đã tạo thành công !";
              return RedirectToAction("Index");
            }
            
            else
            {
       
           productVM.listCategories = _unitityOfWork.Category.GetAll().Select(
                p => new SelectListItem
                {
                    Text = p.Name,
                    Value = p.Id.ToString()
                }
            );

           
            return View(productVM);
          };
            
            

        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }

        #region APICall
        [HttpGet]
        public IActionResult GetAll(int id)
        {
            List<Products> listProducts = _unitityOfWork.Product.GetAll(includeProperties:"Category").ToList();
            return Json( new {Data = listProducts});
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var productToDelete = _unitityOfWork.Product.Get(p => p.Id == id);
            if(productToDelete == null)
            {
                return Json(new {success = false, message = "Lỗi không thể xóa"});
            }

            var oldFilePath = Path.Combine(_webHostEnvironment.WebRootPath, productToDelete.ImageUrl.TrimStart('\\'));
            if(System.IO.File.Exists(oldFilePath))
            {
                System.IO.File.Delete(oldFilePath);
            }
            _unitityOfWork.Product.Remove(productToDelete);
            _unitityOfWork.Save();
            return Json(new {success=true, message = "Đã xóa thông tin"});

        }
        #endregion



    }

}