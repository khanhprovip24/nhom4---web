using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BanDongHo.Models.ViewModel;

namespace BanDongHo.Models.Service
{
    interface IRegisterService
    {
       
        bool isExistAccount(string account);

   
        bool isValidPassword(string password);
        void RegisterAccount(RegisterViewModel register);
    }
}