﻿using BanDongHo.Domain.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BanDongHo.Areas.Admin.Models
{
    public class ProductService
    {
        BANDONGHOEntities db;
        public ProductService()
        {
            db = new BANDONGHOEntities();
        }
       
        public IEnumerable<SANPHAM> getAllProduct()//Lấy tất cả sản phẩm
        {          
            return db.SANPHAMs;
        }

        public int getTotalRecord()//Lấy tổng số sản phẩm
        {         
            return (from sp in db.SANPHAMs orderby sp.MASP descending select sp).Count();
        }

        public SANPHAM getProductById(int masp)//Lấy sản phẩm theo mã sản phẩm
        {          
            return db.SANPHAMs.Find(masp);
        }

        public bool addProduct(SANPHAM sp)//Thêm sản phẩm
        {          
            try
            {
                db.SANPHAMs.Add(sp);
                db.SaveChanges();
                return true;
            }
            catch(Exception)
            {
                return false;
            }
            
        }

        public bool updateProduct(SANPHAM sp)//Cập nhật sản phẩm
        {          
            try
            {
                var result = db.SANPHAMs.Find(sp.MASP);
                if(result != null)
                {
                    result.TENSP = sp.TENSP;
                    result.SOLUONG = sp.SOLUONG;
                    result.MATH = sp.MATH;
                    result.MOTA = sp.MOTA;
                    result.DONGIA = sp.DONGIA;
                    result.MALOAISP = sp.MALOAISP;
                    result.HINHLON = sp.HINHLON;
                    result.HINHNHO = sp.HINHNHO;
                    result.DANHGIA = sp.DANHGIA;
                }
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool deleteProduct(int masp)//Xóa sản phẩm
        {        
            try {
                string query = "DELETE FROM CHITIETKM WHERE MASP = '" + masp + "'";
                string query2 = "DELETE FROM CHITIETDONHANG WHERE MASP = '" + masp + "'";
                string query3 = "DELETE FROM SANPHAM WHERE MASP = '" + masp + "'";
               
                db.Database.ExecuteSqlCommand(query);
                db.Database.ExecuteSqlCommand(query2);
                db.Database.ExecuteSqlCommand(query3);
                return true;
            }
            catch(Exception)
            {
                return false;
            }
            

        }

     
        public IEnumerable<SANPHAM> loadProduct(int pageIndex, int pageSize)//Phân trang sản phẩm theo số trang
        {
            IEnumerable<SANPHAM> ListProduct = null;
                  
            ListProduct = (from sp in db.SANPHAMs orderby sp.MASP descending select sp).Skip((pageIndex - 1) * pageSize).Take(pageSize);

            return ListProduct;
        }

        public IEnumerable<LOAISANPHAM> getLoaiSanPham()//Lấy loại sản phẩm
        {           
            return db.LOAISANPHAMs;
        }

        public IEnumerable<THUONGHIEU> getThuongHieu()//Lấy thương hiệu
        {            
            return db.THUONGHIEUx;
        }
    }
}