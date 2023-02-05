using System.Data;
using System.Reflection;

namespace CRM.Models
{
    public static class Extensions
    {
        public static List<T> ToList<T>(this DataSet ds) where T : new()
        {
            var dt = ds.Tables[0];
            var list = dt.ToList<T>();
            return list;
        }
        public static List<T> ToList<T>(this DataTable table) where T : new()
        {
            IList<PropertyInfo> properties = typeof(T).GetProperties().ToList();
            List<T> result = new List<T>();

            foreach (var row in table.Rows)
            {
                try
                {
                    var item = CreateItemFromRow<T>((DataRow)row, properties);
                    result.Add(item);
                }
                catch (Exception ex)
                {

                }
            }

            return result;
        }

        public static List<T> ToList<T>(this DataView view) where T : new()
        {
            var properties = typeof(T).GetProperties().Where(p => p.SetMethod != null).ToList();
            List<T> result = new List<T>();

            var list = view.Cast<DataRowView>();

            var r = list.Select(r => CreateItemFromRow<T>(r, properties)).ToList();
            return r;

        }

        private static T CreateItemFromRow<T>(DataRowView row, IList<PropertyInfo> properties) where T : new()
        {
            T item = new T();
            foreach (var property in properties)
            {
                try
                {
                    if (property.PropertyType == typeof(System.DayOfWeek))
                    {
                        DayOfWeek day = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), row[property.Name].ToString());
                        property.SetValue(item, day, null);
                    }
                    else
                    {
                        var value = row[property.Name];

                        if (value == DBNull.Value)
                            property.SetValue(item, null, null);
                        else
                            property.SetValue(item, value, null);
                    }
                }
                catch (Exception ex)
                {
                    property.SetValue(item, null, null);
                }
            }
            return item;
        }

        private static T CreateItemFromRow<T>(DataRow row, IList<PropertyInfo> properties) where T : new()
        {
            T item = new T();
            foreach (var property in properties)
            {
                var hasProperty = true;
                try
                {
                    var x = row[property.Name];
                }
                catch (Exception ex)
                {
                    hasProperty = false;
                }

                if (hasProperty == false)
                {
                    continue;
                }

                if (property.PropertyType == typeof(System.DayOfWeek))
                {
                    DayOfWeek day = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), row[property.Name].ToString());
                    property.SetValue(item, day, null);
                }
                else
                {
                    if (row[property.Name] == DBNull.Value)
                        property.SetValue(item, null, null);
                    else
                        property.SetValue(item, row[property.Name], null);
                }
            }
            return item;
        }

        public static T ToObject<T>(this DataRow row) where T : new()
        {
            T item = new T();
            var properties = typeof(T).GetProperties();
            foreach (var property in properties)
            {
                var hasProperty = true;
                try
                {
                    var x = row[property.Name];
                }
                catch (Exception ex)
                {
                    hasProperty = false;
                }

                if (hasProperty == false)
                {
                    continue;
                }

                if (property.PropertyType == typeof(System.DayOfWeek))
                {
                    DayOfWeek day = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), row[property.Name].ToString());
                    property.SetValue(item, day, null);
                }
                else
                {
                    if (row[property.Name] == DBNull.Value)
                        property.SetValue(item, null, null);
                    else
                        property.SetValue(item, row[property.Name], null);
                }
            }
            return item;
        }
    }
}