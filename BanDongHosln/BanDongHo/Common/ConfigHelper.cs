﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Threading.Tasks;

namespace BanDongHo.Common
{
    public class ConfigHelper
    {
        public static string GetByKey(string key)//Lấy thông tin từ file config
        {
            return ConfigurationManager.AppSettings[key].ToString();
        }
    }
}