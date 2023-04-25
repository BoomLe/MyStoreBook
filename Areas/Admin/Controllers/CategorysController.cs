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
using Microsoft.AspNetCore.Authorization;

namespace book.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = ManagerRole.Role_Admin)]
    public class CategorysController : Controller
    {
        private readonly ILogger<CategorysController> _logger;
        private readonly IUnitityOfWork _unitityOfWork;

        public CategorysController(ILogger<CategorysController> logger, IUnitityOfWork db)
        {
            _logger = logger;
            _unitityOfWork = db;
        }

        public IActionResult Index()
        {
            List<Categorys> listCategories = _unitityOfWork.Category.GetAll().ToList();
            return View(listCategories);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Categorys obj)
        {
            if(obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "Tên hiển thị không được trùng với số thứ tự !");
            }
            if(ModelState.IsValid)
            {
              _unitityOfWork.Category.Add(obj);
              _unitityOfWork.Save();
              TempData["success"] = "Bạn đã tạo thành công !";
              return RedirectToAction("Index");
            }
            return View();

        }

        public IActionResult Edit(int? id)
        {
            if(id == null || id==0)
            {
                return NotFound();
            }
            Categorys? categories = _unitityOfWork.Category.Get(p =>p.Id ==id);
            
            if(categories == null)
            {
                return NotFound();
            }
            return View(categories);
        }
        [HttpPost]
        public IActionResult Edit(Categorys obj)
        {
           
            if(ModelState.IsValid)
            {
              _unitityOfWork.Category.Update(obj);
              _unitityOfWork.Save();
              TempData["success"] = "Bạn đã chỉnh sửa thành công !";
              return RedirectToAction("Index");
            }
            return View();

        }

        public IActionResult Delete(int? id)
        {
            if(id == null || id==0)
            {
                return NotFound();
            }
            Categorys? categories = _unitityOfWork.Category.Get(p =>p.Id == id);
            
            if(categories == null)
            {
                return NotFound();
            }
            return View(categories);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Categorys? foundId = _unitityOfWork.Category.Get(p=> p.Id == id);
            if(foundId == null)
            {
                return NotFound();
            }
            _unitityOfWork.Category.Remove(foundId);
             _unitityOfWork.Save();
             TempData["success"] = "Bạn đã xóa thành công !";
              return RedirectToAction("Index");
           
      
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}