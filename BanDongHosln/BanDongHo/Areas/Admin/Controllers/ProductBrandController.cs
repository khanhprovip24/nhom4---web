using BanDongHo.Areas.Admin.Models;
using BanDongHo.Common;
using BanDongHo.Domain.DataContext;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BanDongHo.Areas.Admin.Controllers
{
    public class ProductBrandController : Controller
    {
        ProductBrandService productBrandService = new ProductBrandService();
        // GET: Admin/ProductBrand
        public ActionResult Index()// hiển thị danh sách thương hiệu
        {
            var userSession = (UserLogin)Session[CommonConstands.ADMIN_SESSION];
            if (userSession == null)
            {
                return Redirect("~/Admin/Login/Login");
            }
            
            return View(productBrandService.getAllProductBrand());
        }

        [HttpGet]
        public ActionResult Create()// hiển thị form thêm mới thương hiệu
        {
            ProductBrandViewModel th = new ProductBrandViewModel();
            return View(th);
        }

        [HttpPost]
        public ActionResult Create(ProductBrandViewModel th)// thêm mới thương hiệu
        {
            ViewBag.message = "";
            if (ModelState.IsValid)
            {
                THUONGHIEU thuonghieu = new THUONGHIEU();
                thuonghieu.TENTH = th.TENTH;
                thuonghieu.HINHTH = th.HINHTH;

                if (productBrandService.addProductBrand(thuonghieu))
                {
                    ViewBag.message = "Thêm mới thương hiệu thành công";
                }
            }
            return View(th);
        }

        [HttpGet]
        public ActionResult Update(int math)//tạo phương thức chỉnh sửa thông tin của hàng
        {
            THUONGHIEU thuonghieu = productBrandService.getProductBrandById(math);
            ProductBrandViewModel th = new ProductBrandViewModel();
            th.MATH = math;
            th.TENTH = thuonghieu.TENTH;
            th.HINHTH = thuonghieu.HINHTH;
            ViewBag.MATH = thuonghieu.HINHTH;
            return View(th);
        }

        [HttpPost]
        public ActionResult Update( ProductBrandViewModel th)// cập nhật thông tin thương hiệu
        {
            ViewBag.message = "";
            if (ModelState.IsValid)
            {
                THUONGHIEU thuonghieu = new THUONGHIEU();
                thuonghieu.MATH = th.MATH;
                thuonghieu.TENTH = th.TENTH;
                thuonghieu.HINHTH = th.HINHTH;
                if (productBrandService.updateProductBrand(thuonghieu))
                {
                    ViewBag.message = "Chỉnh sửa thương hiệu thành công";
                }
            }
           
            return View(th);
        }

        public ActionResult Delete(int math)// xóa 1 hàng
        {
            return Json(new { result = productBrandService.deleteProductBrand(math)});


        }

        [HttpPost]
        public JsonResult PhotoCreate(string tenhinh)// tạo phương thức upload hình ảnh
        {    
            for (int i = 0; i < Request.Files.Count; i++)
            {
                var file = Request.Files[i];

                if (file != null)
                {
                   
                    var path = Path.Combine(Server.MapPath("~/images/HINHTH/"), tenhinh);
                   
                    file.SaveAs(path);
                    return Json(new { result = true });
                }
                else
                {
                    return Json(new { result = false });
                }
              
            }
            return Json(new {result = true });
        }

    }
}