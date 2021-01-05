using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.DAL;
using Task.DAL.Models;

namespace Task.BLL.Abstract
{
    public interface IUserService:IRepository<User>
    {
    }
}
