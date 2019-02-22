using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopListService.Model
{
    [JsonObject(MemberSerialization.OptIn)]
    public class User
    {
        [JsonProperty]
        public int id { get; set; }
        [JsonProperty]
        public String Uname { get; set; }
        [JsonProperty]
        public string Pass { get; set; }
     
        public User()
        {

        }
        public User(int _id, String _Uname, string _Pass)
        {
            id = _id;
            Uname = _Uname;
            Pass = _Pass;
        }
    }
}
