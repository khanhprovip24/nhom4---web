using BanDongHo.Areas.Admin.Models;
using BanDongHo.Common;
using BanDongHo.Domain.DataContext;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Security.Permissions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BanDongHo.Areas.Admin.Controllers
{
   
    public class ProductController : Controller
    {
        //tạo dữ liệu hỗ trợ cho việc thêm mới sản phẩm
        ProductService productService = new ProductService();
        // GET: Admin/Product

        public ActionResult Index(int? page)// hiển thị trang web
        {
            var userSession = (UserLogin)Session[CommonConstands.ADMIN_SESSION];
            if (userSession == null)
            {
                return Redirect("~/Admin/Login/Login");
            }

            var pager = new Pager(productService.getTotalRecord(), page);
            var viewModel = new ProductPagerViewModel
            {
                Products = productService.loadProduct(pager.CurrentPage, pager.PageSize),
                Pager = pager
            };

            return View(viewModel);
        }

        public ActionResult Product(int? page)// hiển thị danh sách sản phẩm theo trang
        {
            var pager = new Pager(productService.getTotalRecord(), page);
            var viewModel = new ProductPagerViewModel
            {
                Products = productService.loadProduct(pager.CurrentPage, pager.PageSize),
                Pager = pager
            };

            return View(viewModel);

        }

        public ActionResult Create()// hiển thị form thêm mới sản phẩm
        {
            var userSession = (UserLogin)Session[CommonConstands.ADMIN_SESSION];
            if (userSession == null)
            {
                return Redirect("~/Admin/Login/Login");
            }
            ProductViewModel productviewmodel = new ProductViewModel();
            ViewBag.ThuongHieu = productService.getThuongHieu();
            ViewBag.LoaiSanPham = productService.getLoaiSanPham();
            return View(productviewmodel);
        }


        public ActionResult Detail(int masp)// hiển thị chi tiết sản phẩm
        {
            var userSession = (UserLogin)Session[CommonConstands.ADMIN_SESSION];
            if (userSession == null)
            {
                return Redirect("~/Admin/Login/Login");
            }
            return View(productService.getProductById(masp));
        }

        [HttpPost]
        public ActionResult Create(ProductViewModel sanpham)// thêm mới sản phẩm
        {
            ViewBag.message = "";
            ViewBag.ThuongHieu = productService.getThuongHieu();
            ViewBag.LoaiSanPham = productService.getLoaiSanPham();
            if (ModelState.IsValid)
            {
                SANPHAM sp = new SANPHAM();
                sp.TENSP = sanpham.TENSP;
                sp.SOLUONG = sanpham.SOLUONG;
                sp.MATH = sanpham.MATH;
                sp.MOTA = sanpham.MOTA;
                sp.DANHGIA = sanpham.DANHGIA;
                sp.DONGIA = sanpham.DONGIA;
                sp.MALOAISP = sanpham.MALOAISP;
                sp.HINHLON = sanpham.HINHLON;
                sp.HINHNHO = sanpham.HINHLON.Split('.')[0];
                if (productService.addProduct(sp))
                {
                    ViewBag.message = "Thêm mới sản phẩm thành công";
                    sanpham = new ProductViewModel();
                    return View(sanpham);
                }
                return View(sanpham);

            }
            
            return View(sanpham);
           
        }

        [HttpGet]
        public ActionResult Update(int masp)//tạo phương thức chỉnh sửa thông tin của hàng
        {
            ViewBag.ThuongHieu = productService.getThuongHieu();
            ViewBag.LoaiSanPham = productService.getLoaiSanPham();

            ProductViewModel sp = new ProductViewModel();
            var sanpham = productService.getProductById(masp);
            sp.MASP = sanpham.MASP;
            sp.TENSP = sanpham.TENSP;
            sp.SOLUONG = (int) sanpham.SOLUONG;
            sp.MATH = (int) sanpham.MATH;
            sp.MOTA = sanpham.MOTA;
            sp.DANHGIA = sanpham.DANHGIA;
            sp.DONGIA = (double)sanpham.DONGIA;
            sp.MALOAISP = sanpham.MALOAISP;
            sp.HINHLON = sanpham.HINHLON;
            sp.HINHNHO = sanpham.HINHNHO;

            return View(sp);
        }

        [HttpPost]
        public ActionResult Update(ProductViewModel sanpham)// cập nhật thông tin sản phẩm
        {
            ViewBag.ThuongHieu = productService.getThuongHieu();
            ViewBag.LoaiSanPham = productService.getLoaiSanPham();
            ViewBag.message = "";
            if (ModelState.IsValid)
            {
                SANPHAM sp = new SANPHAM();
                sp.MASP = sanpham.MASP;
                sp.TENSP = sanpham.TENSP;
                sp.SOLUONG = (int)sanpham.SOLUONG;
                sp.MATH = (int) sanpham.MATH;
                sp.MOTA = sanpham.MOTA;
                sp.DANHGIA = sanpham.DANHGIA;
                sp.DONGIA = (double)sanpham.DONGIA;
                sp.MALOAISP = sanpham.MALOAISP;
                sp.HINHLON = sanpham.HINHLON;
                sp.HINHNHO = sanpham.HINHNHO;

                if (productService.updateProduct(sp))
                {
                    ViewBag.message = "Sửa sản phẩm thành công";
                }
                
                return View(sanpham);

            }
            else
            {
                return View(sanpham);
            }         
        }

        public ActionResult Delete(int masp)//xóa sản phẩm
        {
            return Json(new { result = productService.deleteProduct(masp) });
        }

        [HttpPost]
        public JsonResult PhotoCreate(string tenhinh)// tạo phương thức upload hình ảnh
        {
            for (int i = 0; i < Request.Files.Count; i++)
            {
                var file = Request.Files[i];

                if (file != null)
                {

                    var path = Path.Combine(Server.MapPath("~/images/HINHLON/"), tenhinh);

                    file.SaveAs(path);
                    return Json(new { result = true });
                }
                else
                {
                    return Json(new { result = false });
                }

            }
            return Json(new { result = true });
        }

        [HttpPost]
        public JsonResult Photo(string hinhlon, string hinhnho)// tạo phương thức upload hình ảnh
        {
            for (int i = 0; i < Request.Files.Count; i++)
            {
                var file = Request.Files[i];
                var FolderUploadDir = Server.MapPath("~/images/HINHNHO/" + hinhlon);
           
                if (file != null)
                {
                    if (!Directory.Exists(FolderUploadDir))
                    {
                        Directory.CreateDirectory(FolderUploadDir);
                    }
                    var path = Path.Combine(Server.MapPath("~/images/HINHNHO/" + hinhlon + "/"), hinhnho);
                    
                    file.SaveAs(path);
                    return Json(new { result = true });
                }
                else
                {
                    return Json(new { result = false });
                }

            }
            return Json(new { result = true });
        }

        [HttpPost]
        public JsonResult PhotoUpdate(string hinhlon, string hinhnho)// tạo phương thức upload hình ảnh
        {
            for (int i = 0; i < Request.Files.Count; i++)
            {
                var file = Request.Files[i];

                if (file != null)
                {
                                
                    var path = Path.Combine(Server.MapPath("~/images/HINHNHO/" + hinhlon + "/"), hinhnho);
                    file.SaveAs(path);
                    return Json(new { result = true });
                }
                else
                {
                    return Json(new { result = false });
                }

            }
            return Json(new { result = true });
        }
    

    }
}