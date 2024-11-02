using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BanDongHo.Models.ViewModel;
using BanDongHo.Domain.DataContext;
using System.Text.RegularExpressions;
using BanDongHo.Common;

namespace BanDongHo.Models.Service
{
    public class RegisterService : IRegisterService
    {
        public bool isExistAccount(string account)//kiem tra tai khoan da ton tai hay chua
        {
            // get tk => tk.TENDN == account
            // if tk == null return true ? falsea
            BANDONGHOEntities db = new BANDONGHOEntities();
            TAIKHOAN taikhoan = (from tk in db.TAIKHOANs
                                 where tk.TENDN.Equals(account)
                                 select tk).SingleOrDefault();
            if (taikhoan != null)
            {
                return true;
            }
            return false;
        }

       
        public bool isValidPassword(string password)//kiem tra mat khau hop le hay khong
        {
            return Regex.IsMatch(password, @"\w");
        }

        public void RegisterAccount(RegisterViewModel register)//dang ky tai khoan
        {
            int makh;
            BANDONGHOEntities db = new BANDONGHOEntities();

            // Lấy mã khách hàng lớn nhất để tạo mã khách hàng mới
            KHACHHANG kh = (from KH in db.KHACHHANGs
                            orderby KH.MAKH descending
                            select KH).FirstOrDefault();
            if (kh == null)
            {
                makh = 1;
            }
            else
            {
                int numberKH = kh.MAKH;
                numberKH++;
                makh = numberKH;
            }

            // Tạo và thêm mới tài khoản
            TAIKHOAN account = new TAIKHOAN
            {
                TENDN = register.Account,
                MATKHAU = Encryptor.MD5Hash(register.Password),
                MALOAITK = "LK00002",
                NGAYDANGKY = DateTime.Now,
                TRANGTHAI = true
            };
            db.TAIKHOANs.Add(account);
            db.SaveChanges(); // Lưu tài khoản để nhận được MATK

            // Dùng MATK vừa được tạo để liên kết với khách hàng mới
            KHACHHANG newCustomer = new KHACHHANG
            {
                MAKH = makh,
                MATK = account.MATK,  // Liên kết MATK với tài khoản vừa tạo
                TENKH = register.FirstName + " " + register.LastName,
                DIACHI = register.Address,
                SDT = register.Phone,
                EMAIL = register.Email
            };
            db.KHACHHANGs.Add(newCustomer);
            db.SaveChanges();
        }
    }
}