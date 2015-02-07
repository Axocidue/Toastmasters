using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoleTakingSummaryFromAgenda.Utilities
{
    public static class ReflectionHelper
    {
        public static string ReplaceUnderscoreWithSpace(this string raw)
        {
            return raw.Replace("_", " ");
        }

        public static string ToStringWithPropertyValuesByTab(this object o, Type t)
        {
            if (o == null) return "";

            var properties = t.GetProperties();
            var sb = new StringBuilder();
            foreach (var property in properties)
            {
                sb.Append(property.GetValue(o).ToString());
                sb.Append("\t");
            }
            return sb.Remove(sb.Length - 1, 1).ToString();
        }

        public static string ToStringWithPropertyNamesByTab(this Type t)
        {
            var properties = t.GetProperties();
            var sb = new StringBuilder();
            foreach (var property in properties)
            {
                sb.Append(property.Name.ReplaceUnderscoreWithSpace());
                sb.Append("\t");
            }
            return sb.Remove(sb.Length - 1, 1).ToString();
        }

        public static string NewLine(this object o)
        {
            return "\r\n";
        }

        public static string ToStringWithPropertyNamesAndValuesByTabInLines(
            this IEnumerable<object> objects, Type t)
        {
            if (objects == null || objects.Count() == 0) return "";

            var sb = new StringBuilder();

            sb.Append(t.ToStringWithPropertyNamesByTab());
            sb.Append("\r\n");

            foreach(var _object in objects)
            {
                sb.Append(_object.ToStringWithPropertyValuesByTab(t));
                sb.Append("\r\n");
            }

            return sb.Remove(sb.Length - 2, 2).ToString();

        }


        public static string Dump<T>(this IEnumerable<T> collection, 
            Func<T, bool> selector, Func<T, string> headerFormattor, Func<T, string> itemFormattor)
        {

            var objects = selector == null ? collection : from item in collection where selector(item) select item;

            if (objects == null || objects.Count() == 0) return "";

            var type = typeof(T);
            var sb = new StringBuilder();            

            if(headerFormattor != null) sb.Append(headerFormattor(objects.FirstOrDefault()));
            else sb.AppendLine(type.ToStringWithPropertyNamesByTab());               

            if(itemFormattor != null) foreach (var _object in objects) sb.AppendLine(itemFormattor(_object));
            else foreach (var _object in objects) sb.AppendLine(_object.ToStringWithPropertyValuesByTab(type));            

            return sb.ToString();
            
        }

        public static string Dump<T1, T2>(
            this IEnumerable<T1> sourceCollection,
            Func<IEnumerable<T1>, IEnumerable<T2>> selector = null,
            Func<T2, string> headerFormattor = null,
            Func<T2, string> itemFormattor = null)
        {
            if (sourceCollection == null || sourceCollection.Count() == 0) return "";

        }
    }
}
