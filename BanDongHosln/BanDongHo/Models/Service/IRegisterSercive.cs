using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BanDongHo.Models.ViewModel;
namespace BanDongHo.Models.Service
{
    interface IRegisterSercive
    {
       
        bool isExistAccount(string account);//kiem tra tai khoan da ton tai hay chua

        bool isPasswordAccount(string password);//kiem tra mat khau hop le hay khong


        void RegisterAccount(Register register);//dang ky tai khoan

    }
}
