using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopListService.Model
{
    [JsonObject(MemberSerialization.OptIn)]
    public class ShopItem
    {
        [JsonProperty]
        public int id { get; set; }
        [JsonProperty]
        public String ItemName { get; set; }
        [JsonProperty]
        public string ItemQty { get; set; }
        [JsonProperty]
        public int ListId { get; set; }
        [JsonProperty]
        public int Buyed { get; set; }

        public ShopItem()
        {

        }
        [JsonConstructor]
        public ShopItem(int _id, String _ItemName, string _ItemQty, int _ListId, int _Buyed)
        {
            id = _id;
            ItemName = _ItemName;
            ItemQty = _ItemQty;
            ListId = _ListId;
            Buyed = _Buyed;
        }
    }


}
