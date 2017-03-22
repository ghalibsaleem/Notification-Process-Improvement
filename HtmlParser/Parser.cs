using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlParser
{
    public class Parser
    {
        public Dictionary<string, string> ParseHtml(string htmlString)
        {

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(htmlString);
            string[,] tableData = new string[20, 2];
            Dictionary<string, string> tableDict = new Dictionary<string, string>();

            foreach (HtmlNode table in doc.DocumentNode.SelectNodes("//table"))
            {
                try
                {
                    int j = 0;
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
                                tableDict.Add(key, temp);
                            }
                            i++;
                        }
                        j++;
                    }
                }
                catch (Exception)
                {


                }

            }
            tableDict.Remove("Modify my alert settings");
            return tableDict;
        }
    }
}
