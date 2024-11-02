using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BanDongHo.Domain.DataContext;
using BanDongHo.Models.ViewModel;
using BanDongHo.Models.Models;

namespace BanDongHo.Models.Service
{
    public class CusInfoService
    {
        // Phương thức kiểm tra số lượng sản phẩm
        public static bool CheckNumberProduct(int id, int sl)
        {
            using (var db = new BANDONGHOEntities())
            {
                var product = db.SANPHAMs.FirstOrDefault(s => s.MASP == id);
                if (product != null)
                {
                    return product.SOLUONG >= sl;
                }
                return false;
            }
        }

        // Phương thức thêm một đơn hàng
        public static void AddBill(CusInfoViewModel model, int idKhachHang)
        {
            using (var db = new BANDONGHOEntities())
            {
                var donhang = new DONHANG
                {
                    
                    MAKH = idKhachHang,
                    DIACHIGIAO = model.DiaChiGiao,
                    SDT = model.Sdt,
                    MOTA = model.MoTa,
                    TONGTIEN = model.cart.TotalMoney(),
                    TRANGTHAI = "0",
                    NGAYDAT = DateTime.Now,
                    NGAYGIAO = DateTime.Now.AddDays(7)
                };

                db.DONHANGs.Add(donhang);
                db.SaveChanges();

                // Retrieve the generated MADH value
                int id = donhang.MADH;

                AddListDetailBill(model.cart, id);
            }
        }



        // Phương thức thêm chi tiết đơn hàng
        private static void AddListDetailBill(Cart cart,int id)
        {
            BANDONGHOEntities db = new BANDONGHOEntities();
            foreach (var item in cart.GetList())
            {
                CHITIETDONHANG ct = new CHITIETDONHANG { 
                    MADH = id,
                    MASP = item.Product.MASP,
                    SOLUONG = item.Quantity
                };
                db.CHITIETDONHANGs.Add(ct);
                db.SaveChanges();
            }
            
        }
        // phương thức lấy về mã khách hàng từ mã TK
        
        public static int GetIdCustomer(long IdAccount)
        {
            BANDONGHOEntities db = new BANDONGHOEntities();
            var customer = db.KHACHHANGs.FirstOrDefault(s => s.MATK == IdAccount);
            if (customer != null)
            {
                return customer.MAKH;
            }
            else
            {
                throw new InvalidOperationException("Customer not found.");
            }
        }





    }
}