using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BanDongHo.Domain.DataContext;

namespace BanDongHo.Areas.Admin.Models
{
    public class PromotionDetailService
    {
        BANDONGHOEntities db;

        public PromotionDetailService()
        {
            db = new BANDONGHOEntities();
        }
        public IEnumerable<CHITIETKM> getAllPromotionDetail()//Lấy tất cả chi tiết khuyến mãi
        {           
            return db.CHITIETKMs;
        }

        public IEnumerable<SANPHAM> getAllProduct()//Lấy tất cả sản phẩm
        {
            return db.SANPHAMs;
        }

        public IEnumerable<KHUYENMAI> getALLPromotion()//Lấy tất cả khuyến mãi
        {
            return db.KHUYENMAIs;
        }

        public CHITIETKM getOnePromotionDetail(string makm, int masp)//Lấy một chi tiết khuyến mãi
        {         
            return db.CHITIETKMs.Find(makm, masp);          
        }

        public bool addPromotionDetail(CHITIETKM ctkm)//Thêm chi tiết khuyến mãi
        {
            try
            {
                db.CHITIETKMs.Add(ctkm);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool updatePromotionDetail(CHITIETKM ctkm)//Cập nhật chi tiết khuyến mãi
        {
            try
            {
                var result = db.CHITIETKMs.Find(ctkm.MAKM, ctkm.MASP);
                if (result != null)
                {                   
                    result.PHANTRAMKM = ctkm.PHANTRAMKM;
                    db.SaveChanges();
                    return true;
                    
                }             
                return false;                     
               
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool deletePromotionDetail(string makm, int masp)//Xóa chi tiết khuyến mãi
        {
            try
            {
                db.CHITIETKMs.Remove(db.CHITIETKMs.Find(makm, masp));
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }


        }


    }
}