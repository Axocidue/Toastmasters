using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace RoleTakingSummaryFromAgenda.Utilities
{
    public static class DataFeeder
    {
        public static Dictionary<string, string> GetCheckList(this string file)
        {
            var dict = new Dictionary<string, string>();
            var lines = File.ReadAllLines(file);
            foreach(var line in lines)
            {
                var elements = Regex.Split(line, @"\t");
                dict.Add(elements[0], elements[1]);
            }
            return dict;
        }
    }
}
