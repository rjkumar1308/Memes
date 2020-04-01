using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MemesAPI.Models
{
    public class Memes
    {

        public int ImageId { get; set; }
        public string Url { get; set; }
        public List<String> Tags { get; set; }

        public List<Memes> RemoveDuplicate(DataSet ds)
        {
            var map = new Dictionary<String, List<String>>();
            foreach (DataTable table in ds.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    List<String> ls = new List<string>();
                    foreach (object item in row.ItemArray)
                    {
                        String s = (item == DBNull.Value) ? String.Empty : item.ToString();
                        ls.Add(s);
                    }
                    List<String> l = new List<string>();
                    if (map.ContainsKey(ls[0]))
                    {
                        map[ls[0]].Add(ls[2]);
                    }
                    else
                    {
                        l.Add(ls[1]);
                        l.Add(ls[2]);
                        map.Add(ls[0], l);
                    }
                }
            }
            ds.Clear();
            List<Memes> d = new List<Memes>();
            foreach (KeyValuePair<string, List<String>> entry in map)
            {
                Memes temp = new Memes();
                string t = entry.Value[0];
                List<string> temp_list = new List<string>();
                temp_list = entry.Value;
                temp_list.RemoveAt(0);
                temp.ImageId = int.Parse(entry.Key);
                temp.Url = t;
                temp.Tags = temp_list;
                d.Add(temp);
            }
            map.Clear();
            return d;
        }
        public string ListToString(List<String> tags)
        {
            string items = string.Join(",", tags.Select(item => item.ToString()).ToArray());
            return items;
        }

        public List<String> TagsHelper(DataSet ds)
        {
            List<String> ls = new List<string>();
            foreach (DataTable table in ds.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    foreach (object item in row.ItemArray)
                    {
                        String s = (item == DBNull.Value) ? String.Empty : item.ToString();
                        ls.Add(s);
                    }
                }
            }
            return ls;
        }


    }
}