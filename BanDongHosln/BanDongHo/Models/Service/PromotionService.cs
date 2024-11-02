using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BanDongHo.Domain.DataContext;

namespace BanDongHo.Models.Service
{
    public class PromotionService
    {
        public static int GetPromotion(int id)//Lấy giá trị phần trăm khuyến mãi của sản phẩm
        {
            int result = 0;
            using (var db = new BANDONGHOEntities())
            {
                // Lấy giá trị phần trăm khuyến mãi đầu tiên tìm thấy cho sản phẩm
                int? promotionValue = (from ct in db.CHITIETKMs
                                       join sp in db.SANPHAMs on ct.MASP equals sp.MASP
                                       where sp.MASP == id
                                       select ct.PHANTRAMKM).FirstOrDefault();

                if (promotionValue.HasValue)
                {
                    result = promotionValue.Value;
                }
            }
            return result;
        }

    }
}