using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BanDongHo.Domain.DataContext;

namespace BanDongHo.Areas.Admin.Models
{
    public class PromotionService
    {
        BANDONGHOEntities db;

        public PromotionService()
        {
            db = new BANDONGHOEntities();
        }

        public IEnumerable<KHUYENMAI> getAllPromotion()//Lấy tất cả khuyến mãi
        {
            db = new BANDONGHOEntities();
            return db.KHUYENMAIs;
        }

        public bool addPromotion(KHUYENMAI km)//Thêm khuyến mãi
        {
            try
            {
                db.KHUYENMAIs.Add(km);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool updatePromotion(KHUYENMAI km)//Cập nhật khuyến mãi
        {
            try
            {
                var result = db.KHUYENMAIs.Find(km.MAKM);
                if (result != null)
                {
                    result.TENKM = km.TENKM;
                    result.NGAYBD = km.NGAYBD;
                    result.NGAYKT = km.NGAYKT;
                }
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool deletePromotion(string makm)//Xóa khuyến mãi
        {
            try
            {
                string query = "DELETE FROM CHITIETKM WHERE MAKM = '" + makm + "'";
                string query2 = "DELETE FROM KHUYENMAI WHERE MAKM = '" + makm + "'";
                db.Database.ExecuteSqlCommand(query);
                db.Database.ExecuteSqlCommand(query2);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public string getLastRecord()//Lấy mã khuyến mãi cuối cùng
        {
            string res = "";
            var lastrecord = db.KHUYENMAIs.OrderByDescending(p => p.MAKM).FirstOrDefault();
            if (lastrecord != null)
            {
                res = lastrecord.MAKM;
            }

            return res;
        }

        public KHUYENMAI getPromotionById(string makm)//Lấy khuyến mãi theo mã khuyến mãi
        {
            return db.KHUYENMAIs.Find(makm);
        }
    }
}