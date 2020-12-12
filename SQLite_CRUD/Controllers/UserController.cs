using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess;


namespace SQLite_CRUD.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<User> Get()
        {
            SQLiteDataAccess db = new SQLiteDataAccess();

            db.InsertUser();
            List<User> users = new List<User>();
            users = db.GetUser();
            return users;
        }
    }
}
