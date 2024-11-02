using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BanDongHo.Domain.DataContext;

namespace BanDongHo.Areas.Admin.Models
{
    public class ProductCategoryService
    {

        BANDONGHOEntities db;

        public ProductCategoryService()
        {
            db = new BANDONGHOEntities();
        }

        public IEnumerable<LOAISANPHAM> getAllProductCategory()//lấy tất cả loại sản phẩm
        {        
            return db.LOAISANPHAMs;
        }

        public bool addProductCategory(LOAISANPHAM lsp)//thêm loại sản phẩm
        {
            try
            {
                db.LOAISANPHAMs.Add(lsp);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool updateProductCategory(LOAISANPHAM lsp)//cập nhật loại sản phẩm
        {
            try
            {
                var result = db.LOAISANPHAMs.Find(lsp.MALOAISP);
                if(result != null)
                {
                    result.TENLOAISP = lsp.TENLOAISP;
                }
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool deleteProductCategory(string malsp)//xóa loại sản phẩm
        {          
                try
                {              
                    string query = "DELETE FROM SANPHAM WHERE MALOAISP = '" + malsp + "'";
                    string query2 = "DELETE FROM LOAISANPHAM WHERE MALOAISP = '" + malsp + "'";
                    db.Database.ExecuteSqlCommand(query);
                    db.Database.ExecuteSqlCommand(query2);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }          

        }

        public string getLastRecord()//lấy mã loại sản phẩm cuối cùng
        {
            string res = "";
            var lastrecord = db.LOAISANPHAMs.OrderByDescending(p => p.MALOAISP).FirstOrDefault();
            if (lastrecord != null)
            {
                res = lastrecord.MALOAISP;
            }

            return res ;
        }

        public LOAISANPHAM getProductCategoryById(string malsp)//lấy loại sản phẩm theo mã
        {
            return db.LOAISANPHAMs.Find(malsp);
        }

    }
}