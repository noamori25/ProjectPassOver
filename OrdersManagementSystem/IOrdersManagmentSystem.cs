using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersManagementSystem
{
    interface IOrdersManagmentSystem
    {
        void OpenSystem();
        void CloseSystem();
        void BackToMainMenu();
    }
}
