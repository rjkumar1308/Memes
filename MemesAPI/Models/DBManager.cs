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
    }
}