using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Models;
using Models.Attributes;
using System.Reflection;

namespace HtmlParser
{
    public class Parser
    {
        public SIT2_Item ParseHtml(string htmlString)
        {

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(htmlString);

            foreach (HtmlNode table in doc.DocumentNode.SelectNodes("//table"))
            {
                
                try
                {
                    SIT2_Item item = new SIT2_Item();
                    int counter = 0;
                    HtmlNode tbody = table.SelectNodes("tbody").First();
                    foreach (HtmlNode row in tbody.SelectNodes("tr"))
                    {
                        int i = 0;
                        string key = null;
                        foreach (var cell in row.SelectNodes("td"))
                        {
                            string temp = null;
                            if (i < 2)
                            {
                                temp = cell.InnerText;
                                while (temp.Contains("\r\n"))
                                {
                                    temp = temp.Replace("\r\n", "");
                                }
                                while (temp.Contains(@"&nbsp;"))
                                {
                                    temp = temp.Replace(@"&nbsp;", " ");
                                }
                            }
                            if (i == 0)
                            {
                                key = temp;
                            }
                            if (i == 1)
                            {
                                PropertyInfo info = typeof(SIT2_Item)
                                        .GetProperties()
                                        .Where(p =>
                                                (p.GetCustomAttribute(typeof(NameAttribute), false) as NameAttribute).Name == key.Replace(":","")).FirstOrDefault<PropertyInfo>();
                                if(info != null)
                                {
                                    info.SetValue(item, temp);
                                    counter++;
                                }
                                    
                            }
                            i++;
                        }
                    }
                    if(counter > 27)
                    {
                        
                        return item;
                    }
                }
                catch (Exception)
                {
                }

            }
            return null;
        }

        private SIT2_Item ListToSitItem()
        {
            List<PropertyInfo> result =
                typeof(SIT2_Item)
                .GetProperties()
                .Where(
                    p =>
                        p.GetCustomAttributes(typeof(NameAttribute),false )
                        .Where(ca => ((NameAttribute)ca).Name == "Project")
                        .Any()
                    )
                .ToList();
            SIT2_Item item = new SIT2_Item();
            item.GetType().GetProperty("Project").SetValue(item,"asd");
            var s = item.GetType().GetProperties();
            return null;
        }
    }
}
