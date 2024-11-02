using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BanDongHo.Domain.DataContext;

namespace BanDongHo.Models.Models
{
    public class CartItem//model giỏ hàng
    {
        public SANPHAM Product { get; set; }
        public int Quantity { get; set; }
        public int Promotion { get; set; }
    }
}