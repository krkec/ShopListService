using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopListService.Model
{
    [JsonObject(MemberSerialization.OptIn)]
    public class ShopList
    {
        [JsonProperty]
        public int id { get; set; }
        [JsonProperty]
        public String ListName { get; set; }
        [JsonProperty]
        public string Store { get; set; }
        [JsonProperty]
        public int NumberOfItems { get; set; }

        public ShopList()
        {

        }
        public ShopList(int _id, String _ListName, string _Store, int _NumberOfItems)
        {
            id = _id;
            ListName = _ListName;
            Store = _Store;
            NumberOfItems = _NumberOfItems;
        }
    }
}