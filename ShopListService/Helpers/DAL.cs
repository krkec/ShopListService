using ShopListService.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ShopListService.Helpers
{
    public class DAL
    {
        public static List<String> _getAllUsers()
        {
            try
            {
                List<String> useri = new List<string>();
                System.Data.SqlClient.SqlConnection con;
                using (con = new SqlConnection(Database.ConnectionString))
                {
                    con.Open();
                    String upit = "select [UserName] from [Users]";
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = upit;
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        useri.Add(dr["UserName"].ToString());
                    }
                    con.Close();
                }
                return useri;
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }
        public static User getUserFromDB(String UserName, String PassWord)
        {
            System.Data.SqlClient.SqlConnection con = new SqlConnection(Database.ConnectionString);
            con.Open();
            String upit1 = String.Format("select * from [Users] where UserName = '{0}'and PassWord='{1}'", UserName,PassWord);
            SqlCommand cmd = new SqlCommand(upit1, con);
            SqlDataReader dr = cmd.ExecuteReader();
            User u = new User();
            dr.Read();
            u.id = (int)dr["ID"];
            u.Uname = dr["UserName"].ToString();
            u.Pass = dr["PassWord"].ToString();
           
            dr.Close();
            con.Close();
            return u;
        }
        public static List<ShopList> _GetAllShopLst(int Uid)
        {
            System.Data.SqlClient.SqlConnection con = new SqlConnection(Database.ConnectionString);
            con.Open();
            if (Uid==0)
            {
                return null;
            }

            String upit1 = String.Format("Select [ShopLists].ID, [ShopLists].ListName,[ShopLists].Store  from ShopLists join UserLists on ShopLists.ID = UserLists.ListID where UserLists.UserID ={0}", Uid);
            
            SqlCommand cmd = new SqlCommand(upit1, con);
            SqlCommand cmd2;
            SqlDataReader dr = cmd.ExecuteReader();
            List<ShopList> slist = new List<ShopList>();
            while (dr.Read())
            {
                ShopList sl = new ShopList();
                int lid = (int)dr["id"]; ;
                sl.id = lid;
                sl.ListName = dr["ListName"].ToString();
                sl.Store = dr["Store"].ToString();
                slist.Add(sl);
            }
            dr.Close();
            foreach (ShopList sll in slist)
            {
                String upit2 = string.Format("select count(ID) as zbroj from Items where ListId ={0};", sll.id);
                cmd = new SqlCommand(upit2, con);
                SqlDataReader dr2 = cmd.ExecuteReader();
                dr2.Read();
                int b = (int) dr2["zbroj"];
                sll.NumberOfItems = b;
                dr2.Close();
            }
            con.Close();
            return slist;
        }

        public static ShopList _getShopListById(int ListId)
        {

            System.Data.SqlClient.SqlConnection con;
            try
            {
                ShopList sl = null;
                using (con = new SqlConnection(Database.ConnectionString))
                {
                    con.Open();
                    String upit1 = String.Format("select * from [ShopLists] where id={0}", ListId);
                    String upit2 = String.Format("select count(*) from [Items] where ListId={0}", ListId);
                    SqlCommand cmd1 = new SqlCommand(upit1, con);
                    SqlDataReader dr1 = cmd1.ExecuteReader();
                    dr1.Read();
                    sl = new ShopList();
                    sl.id = (int)dr1[0];
                    sl.ListName = dr1[1].ToString();
                    sl.Store = dr1[2].ToString();
                    dr1.Close();
                    cmd1 = new SqlCommand(upit2, con);
                    sl.NumberOfItems = (int)cmd1.ExecuteScalar();
                    con.Close();
                }
                
                return sl;

            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }
        public static List<ShopItem> _GetShopItemsByListId(int ListId)
        {
            List<ShopItem> lista;
            System.Data.SqlClient.SqlConnection con;
            try
            {
                using (con = new SqlConnection(Database.ConnectionString))
                {
                    con.Open();
                    //String upit = String.Format("select count(*) from [Items] where ListId={0}", ListId);
                    String upit = String.Format("select * from [Items] where ListId={0}", ListId);
                    SqlCommand cmd1 = new SqlCommand(upit, con);
                    SqlDataReader dr1 = cmd1.ExecuteReader();
                    lista = new List<ShopItem>();
                    while (dr1.Read())
                    {
                        ShopItem sl = new ShopItem();

                        sl.id = (int)dr1["id"];
                        sl.ItemName = dr1["ItemName"].ToString();
                        sl.ItemQty = dr1["ItemQty"].ToString();
                        sl.ListId = (int)dr1["ListId"];
                        sl.Buyed = (int)dr1["Buyed"];
                        lista.Add(sl);
                    }
                    con.Close();
                }

                return lista;

            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }
        public static ShopItem _getShopItemById(int ItemId)
        {

            System.Data.SqlClient.SqlConnection con;
            try
            {
                ShopItem item = null;
                using (con = new SqlConnection(Database.ConnectionString))
                {
                    con.Open();
                    String upit2 = String.Format("select * from [Items] where ID={0}", ItemId);
                    SqlCommand cmd1 = new SqlCommand(upit2, con);
                    SqlDataReader dr1 = cmd1.ExecuteReader();
                    dr1.Read();
                    item = new ShopItem();
                    item.id= (int)dr1[0];
                    item.ItemName = dr1[1].ToString();
                    item.ItemQty = dr1[2].ToString();
                    item.ListId = (int)dr1[3];
                    dr1.Close();
                    con.Close();
                }

                return item;

            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }

        public static Nullable<int> _CreateShopList(ShopList shopList)
        {
            //String upit = "INSERT INTO [krkeec_admin].[ShopLists] ([ListName],[Store])  OUTPUT INSERTED.ID VALUES ('@ListName','@Store')";
            String upit = String.Format("INSERT INTO [krkeec_admin].[ShopLists] ([ListName],[Store])  OUTPUT INSERTED.ID VALUES ('{0}','{1}')", shopList.ListName, shopList.Store);
            Nullable<int> b = null;
            try
            {
                SqlConnection con;
                using (con = new SqlConnection(Database.ConnectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = upit;
                    //cmd.Parameters.Add("@ListName", SqlDbType.NVarChar).Value = shopList.ListName;
                    //cmd.Parameters.Add("@Store", SqlDbType.NChar).Value = shopList.Store;
                    b = (int)cmd.ExecuteScalar();
                    con.Close();
                    return b.Value;
                }

            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }

        public static Nullable<int> _CreateShopItem(ShopItem shopItem)
        {
            String upit = String.Format("INSERT INTO [krkeec_admin].[Items] ([ItemName]  ,[ItemQty],[Buyed] ,[ListId]) VALUES ('{0}','{1}',0,{2})", shopItem.ItemName, shopItem.ItemQty, shopItem.ListId);
            Nullable<int> b = null;
            try
            {
                SqlConnection con;
                using (con = new SqlConnection(Database.ConnectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(upit, con);
                    b = cmd.ExecuteNonQuery();
                    con.Close();
                    return b.Value;
                }

            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }
        public static Nullable<int> _UpdateShopList(ShopList shopList)
        {
            String upit = String.Format("UPDATE [krkeec_admin].[ShopLists] SET [ListName] = {0}, [Store] = {1} where id={2}", shopList.ListName, shopList.Store, shopList.id);
            Nullable<int> b = null;
            try
            {
                SqlConnection con;
                using (con = new SqlConnection(Database.ConnectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(upit, con);
                    b = cmd.ExecuteNonQuery();
                    con.Close();
                    return b.Value;
                }

            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }

        public static Nullable<int> _UpdateShopItem(ShopItem shopItem)
        {
            String upit = String.Format("UPDATE [krkeec_admin].[Items] SET [ItemName] ={0},[ItemQty] = {1} where id={2}", shopItem.ItemName, shopItem.ItemQty, shopItem.id);
            Nullable<int> b = null;
            try
            {
                SqlConnection con;
                using (con = new SqlConnection(Database.ConnectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(upit, con);
                    b = cmd.ExecuteNonQuery();
                    con.Close();
                    return b.Value;
                }

            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }
        public static Nullable<int> _DeleteShopList(int ID)
        {
            String upit = "DELETE [krkeec_admin].[ShopLists] where ID=@ID";
            String upit2 = "DELETE [krkeec_admin].[Items] where ID=@LID";
            Nullable<int> b = null;
            try
            {
                SqlConnection con;
                using (con = new SqlConnection(Database.ConnectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = upit;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = ID;
                    b = cmd.ExecuteNonQuery();
                    cmd.CommandText = upit2;
                    cmd.Parameters.Add("@LID", SqlDbType.Int).Value = ID;
                    b = cmd.ExecuteNonQuery();
                    con.Close();
                    return b.Value;
                }

            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }
        public static Nullable<int> _AuthList(int ListID, int ShareID)
        {
            //String upit = String.Format("INSERT INTO [krkeec_admin].[Items] ([ItemName]  ,[ItemQty] ,[ListId]) VALUES ('{0}','{1}',{2})", shopItem.ItemName, shopItem.ItemQty, shopItem.ListId);
            //String upit = "INSERT INTO[krkeec_admin].[UserLists] [UserID] ,[ListID]) VALUES @UserID, @ListID";
            String upit = String.Format("INSERT INTO[krkeec_admin].[UserLists] ([UserID] ,[ListID]) VALUES ({0}, {1})",ShareID,ListID);
            Nullable<int> b = null;
            try
            {
                SqlConnection con;
                using (con = new SqlConnection(Database.ConnectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = upit;
                    //cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = ListID;
                    //cmd.Parameters.Add("@ListID", SqlDbType.Int).Value = ShareID;
                    b = cmd.ExecuteNonQuery();
                    con.Close();
                    return b.Value;
                }

            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }
        public static Nullable<int> _getIdByEmail(string email)
        {
            //String upit = String.Format("INSERT INTO [krkeec_admin].[Items] ([ItemName]  ,[ItemQty] ,[ListId]) VALUES ('{0}','{1}',{2})", shopItem.ItemName, shopItem.ItemQty, shopItem.ListId);
            String upit = "select ID from Users Where email = '@email";
            Nullable<int> b = null;
            try
            {
                SqlConnection con;
                using (con = new SqlConnection(Database.ConnectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = upit;
                    cmd.Parameters.Add("@email", SqlDbType.Int).Value = email;
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        b = (int)dr["ID"];
                    }
                    con.Close();
                    return b.Value;
                }

            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }
        public static Nullable<int> _getIdByUserName(string UserNeme)
        {
            //String upit = "select ID from Users Where UserName = '@UserName'";
            String upit = String.Format("select ID from Users Where UserName = '{0}'",UserNeme);
            Nullable<int> b = null;
            try
            {
                SqlConnection con;
                using (con = new SqlConnection(Database.ConnectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = upit;
                    b = (int)cmd.ExecuteScalar();
                    //cmd.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = UserNeme;
                    //cmd.Parameters.AddWithValue("@UserName", UserNeme);
                    /*
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            b = (int)dr["ID"];
                            con.Close();
                            return b.Value;
                        }
                        else
                        {
                            return null;
                        }
                    }*/
                    return b;
                }

            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }
    }



    

   
}
