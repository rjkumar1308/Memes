using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MemesAPI.Models
{
    public class DBManager
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
        Memes m = new Memes();
        public string AddImage(Memes val)
        {
            try
            {

                string temp = m.ListToString(val.Tags);
                SqlCommand com = new SqlCommand("add_tag2", con);
                com.CommandType = System.Data.CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@url", val.Url);
                com.Parameters.AddWithValue("@tags", temp);
                con.Open();
                com.ExecuteNonQuery();
                con.Close();

            }
            catch (Exception e)
            {
                return e.ToString();
            }
            return "Success";
        }

        public List<Memes> GetAllImages()
        {
            SqlCommand com = new SqlCommand("Images_Get", con);
            com.CommandType = System.Data.CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return m.RemoveDuplicate(ds);
        }

        public List<String> GetTagsHelper()
        {
            SqlCommand com = new SqlCommand("GetTagsHelper", con);
            com.CommandType = System.Data.CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return m.TagsHelper(ds);
        }

        public List<Memes> GetImagesWithTags(List<String> val)
        {
            string temp = m.ListToString(val);
            SqlCommand com = new SqlCommand("get_image_with_tag2", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@tag", temp);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return m.RemoveDuplicate(ds);
        }

        //shaklen's code below

        public string update_record(Memes img)
        {
            try
            {
                SqlCommand com = new SqlCommand("Images_Update", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@id", img.ImageId);
                com.Parameters.AddWithValue("@url", img.Url);
                con.Open();
                com.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception e)
            {
                return e.ToString();
            }
            return "Success";

        }

        public string delete_record(int id)
        {
            try
            {
                SqlCommand com = new SqlCommand("Images_Delete", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@id", id);
                con.Open();
                com.ExecuteNonQuery();
                con.Close();

            }
            catch (Exception e)
            {
                return e.ToString();
            }
            return "Success";


        }

        public string update_add_tag(int id, List<string> tag)
        {
            //adding tag 
            try
            {
                string temp = m.ListToString(tag);
                SqlCommand com = new SqlCommand("update_add_tag", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@id", id);
                com.Parameters.AddWithValue("@tags", temp);
                con.Open();
                com.ExecuteNonQuery();
                con.Close();


            }

            catch (Exception e)
            {
                return e.ToString();
            }
            return "Success";
        }

        public string update_delete_tags(int id, List<string> tag)
        {
            //deleting tag from image only 
            try
            {
                string temp = m.ListToString(tag);
                SqlCommand com = new SqlCommand("update_delete_tags", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@id", id);
                com.Parameters.AddWithValue("@tags", temp);
                con.Open();
                com.ExecuteNonQuery();
                con.Close();

            }


            catch (Exception e)
            {
                return e.ToString();
            }
            return "Success";

        }
        public DataRow LoginHelper(string UserName, string Password)
        {
            SqlCommand com = new SqlCommand("LoginHelper", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@UserName", UserName);
            com.Parameters.AddWithValue("@UserPassword", Password);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds);
            foreach (DataTable table in ds.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    return row;
                }
            }
            return null;
        }

        public string SignUpHelper(UserDeatils deatils)
        {
            try
            {
                SqlCommand com = new SqlCommand("SignUpHelper", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@UserName", deatils.UserName);
                com.Parameters.AddWithValue("@Password", deatils.Password);
                com.Parameters.AddWithValue("@Email", deatils.Email);
                com.Parameters.AddWithValue("@Role", deatils.Role);
                con.Open();
                com.ExecuteNonQuery();
                con.Close();
            }

            catch (Exception e)
            {
                return e.ToString();
            }
            return "Success";
        }

        public string ChangePasswordHelper(UserDeatils deatils)
        {
            try
            {
                String temp = String.Empty;
                SqlCommand com = new SqlCommand("ChangePasswordHelper", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@UserName", deatils.UserName);
                com.Parameters.AddWithValue("@NewPassword", deatils.Password);
                com.Parameters.AddWithValue("@OldPassword", deatils.OldPassword);
                SqlDataAdapter da = new SqlDataAdapter(com);
                DataSet ds = new DataSet();
                da.Fill(ds);
                foreach (DataTable table in ds.Tables)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        foreach(Object obj in row.ItemArray)
                        {
                            temp = (obj == DBNull.Value) ? String.Empty : obj.ToString();
                        }
                    }
                }
                if(temp!=deatils.OldPassword)
                {
                    return "Old Password Does Not Match.";
                }
            }

            catch (Exception e)
            {
                return e.ToString();
            }
            return "Successfully Changed The password.";
        }

        public string DeleteAccountHelper(UserDeatils deatils)
        {
            try
            {
                String temp = String.Empty;
                SqlCommand com = new SqlCommand("DeleteAccountHelper", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@UserName", deatils.UserName);
                com.Parameters.AddWithValue("@Password", deatils.Password);
                SqlDataAdapter da = new SqlDataAdapter(com);
                DataSet ds = new DataSet();
                da.Fill(ds);
                foreach (DataTable table in ds.Tables)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        foreach (Object obj in row.ItemArray)
                        {
                            temp = (obj == DBNull.Value) ? String.Empty : obj.ToString();
                        }
                    }
                }
                if (temp != deatils.Password)
                {
                    return "Password Does Not Match.";
                }
            }

            catch (Exception e)
            {
                return e.ToString();
            }
            return "Success";
        }
    }
}