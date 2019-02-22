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
    public class ItemController : Controller
    {
        #region Ucitavanje svih stvari
        [ActionName("GetAllShopItems")]
        [HttpGet]
        public List<ShopItem> GetAllShopItems(int ListId)
        {
            return DAL._GetShopItemsByListId(ListId);
        }
        [HttpGet("GetAllShopItemsRecords")]
        public JsonResult GetAllShopItemsRecords(int ListId)
        {
            JsonResult jr = Json(DAL._GetShopItemsByListId(ListId));
            Newtonsoft.Json.JsonSerializerSettings jsonSerializerSettings = new Newtonsoft.Json.JsonSerializerSettings();
            jsonSerializerSettings.TypeNameHandling = Newtonsoft.Json.TypeNameHandling.All;
            return Json(DAL._GetShopItemsByListId(ListId), jsonSerializerSettings);
        }
        #endregion

        #region Kreiranje nove liste

        [ActionName("CreateItem")]
        [HttpPost]
        public IActionResult CreateItem(String ItemName, String ItemQty, int ListId)
        {
            ShopItem item;
            try
            {
                if (ItemName.Length > 0 & ItemQty.Length > 0 & ListId > 0)
                {
                    item = new ShopItem();
                    item.ItemName = ItemName;
                    item.ItemQty = ItemQty;
                    item.ListId = ListId;
                    item.Buyed = 0;

                    var k = DAL._CreateShopItem(item);
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
        [ActionName("DeleteItem")]
        [HttpPost]
        public IActionResult DeleteItem(int ShopItemID)
        {
            try
            {
                if (ShopItemID > 0)
                {
                    var k = DAL._DeleteShopList(ShopItemID);
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
                    return BadRequest(String.Format("ID={0} is not valid!", Convert.ToInt32(ShopItemID)));
                }
            }
            catch (Exception ex)
            {

                return BadRequest(String.Format("Exception occured. Message: {0}", ex.Message));
            }
        }
        #endregion
    }
}
