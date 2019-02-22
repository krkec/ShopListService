using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShopListService.Helpers;
using ShopListService.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShopListService.Controllers
{
    [Route("api/[action]")]
    public class UserController : Controller
    {
        //get Shoping list by ID
        // GET: api/<controller>
        [ActionName("GetUid")]
        [HttpGet]
        public User getUid(String Uname, String Pass)
        {
            User u = DAL.getUserFromDB(Uname, Pass);
            return DAL.getUserFromDB(Uname, Pass);
        }
        [HttpGet("GetShopingListByIdRecord")]

        public JsonResult GetShopingListByIdRecord(String Uname, String Pass)
        {
            Newtonsoft.Json.JsonSerializerSettings jsonSerializerSettings = new Newtonsoft.Json.JsonSerializerSettings();
            jsonSerializerSettings.TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Auto;
            return Json(DAL.getUserFromDB(Uname, Pass), jsonSerializerSettings);
        }
        //get all users
        // GET: api/<controller>
        [ActionName("GetAllUsers")]
        [HttpGet]
        public List<String> GetAllUsers()
        {
            return DAL._getAllUsers();
        }
        [HttpGet("GetAllUsersRecord")]

        public JsonResult GetAllUsersRecord()
        {
            Newtonsoft.Json.JsonSerializerSettings jsonSerializerSettings = new Newtonsoft.Json.JsonSerializerSettings();
            jsonSerializerSettings.TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Auto;
            return Json(DAL._getAllUsers(), jsonSerializerSettings);
        }
    }
}
