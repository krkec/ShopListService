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
    public class ListController : Controller
    {
        #region Ucitavanje svih lista
        [ActionName("GetAllShopLst")]
        [HttpGet]
        public List<ShopList> getAllLists(int Uid)
        {
            return DAL._GetAllShopLst(Uid);
            //return Ok(DAL._GetAllShopLst());
        }
        [HttpGet("getAllListsRecords")]
        public JsonResult getAllListsRecords(int Uid)
        {
            JsonResult jr = Json(DAL._GetAllShopLst(Uid));
            Newtonsoft.Json.JsonSerializerSettings jsonSerializerSettings = new Newtonsoft.Json.JsonSerializerSettings();
            jsonSerializerSettings.TypeNameHandling = Newtonsoft.Json.TypeNameHandling.All;
            return Json(DAL._GetAllShopLst(Uid), jsonSerializerSettings);
        }
        #endregion

        #region Kreiranje nove liste

            [ActionName("CreateList")]
            [HttpPost]
            public IActionResult CreateListPost(String ListName, String Store, int Uid)
            {
                ShopList list;
                try
                {
                    if (ListName.Length > 0 & Uid > 0)
                    {
                        list = new ShopList();
                        list.ListName = ListName;
                        list.Store = Store;
                        list.NumberOfItems = 0;
                        int id = DAL._CreateShopList(list).Value;
                        //list.id = id;
                        var k = DAL._AuthList(id,Uid);
                    if (k != null)
                        {
                            return Ok(id);
                        }
                        else
                        {
                            return BadRequest("Could not insert record in database");
                        }
                    }
                    else
                    {
                        return BadRequest("Input parameters are not valid");
                    }
                }
                catch (Exception ex)
                {

                    return BadRequest(String.Format("Exception occured. Message: {0}", ex.Message));
                }
            }
        #endregion

        #region Brisanje lista
        [ActionName("DeleteList")]
        [HttpPost]
        public IActionResult DeleteList(int ShopListID)
        {
            try
            {
                if (ShopListID > 0)
                {
                    var  k = DAL._DeleteShopList(ShopListID);
                    if (k != null)
                    {
                        return Ok(String.Format("{0} record was deleted!", Convert.ToInt32(k)));
                    }
                    else
                    {
                        return BadRequest("Could not delete record in database");
                    }
                }
                else
                {
                    return BadRequest(String.Format("ID={0} is not valid!", Convert.ToInt32(ShopListID)));
                }
            }
            catch (Exception ex)
            {

                return BadRequest(String.Format("Exception occured. Message: {0}", ex.Message));
            }
        }
        #endregion

        #region SHeranje liste

        [ActionName("ShareList")]
        [HttpPost]
        public IActionResult ShareList(int ShopListID, String UserName)
        {
            ShopList list;
            try
            {
                if (UserName.Length > 0 & ShopListID > 0)
                {
                    var u = DAL._getIdByUserName(UserName);
                    if (u != null)
                    {
                        var k = DAL._AuthList(ShopListID, (int)u);
                        if (k != null)
                        {
                            return Ok(String.Format("{0} record was created!", Convert.ToInt32(k)));
                        }
                        else
                        {
                            return BadRequest("Could not insert record in database");
                        }
                    }
                    else
                    {
                        return BadRequest("Could not find user by given UserName");
                    }
                    
                }
                else
                {
                    return BadRequest("Input parameters are not valid");
                }
            }
            catch (Exception ex)
            {

                return BadRequest(String.Format("Exception occured. Message: {0}", ex.Message));
            }
        }
        #endregion


        #region Helpers



        #endregion

    }
}
