﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BanDongHo.Domain.DataContext;

namespace BanDongHo.Models.Service
{
    public class CartService
    {
        public static bool CheckNumberProduct(int id,int soluong)//kiem tra so luong san pham co du de mua hay khong
        {
            BANDONGHOEntities db = new BANDONGHOEntities();
            SANPHAM sp = db.SANPHAMs.Where(s => s.MASP == id).SingleOrDefault();
            return sp.SOLUONG >= soluong;
        }
    }
}