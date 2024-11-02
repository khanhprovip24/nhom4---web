using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BanDongHo.Common
{
    [Serializable]
    public class UserLogin// Lưu trữ thông tin người dùng
    {
        public long UserID { set; get; }
        public string UserName { set; get; }
    }
}