using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Deployed.Helper
{
    public static class DeployedHelper
    {
        public static T ConvertDictionaryTo<T>(dynamic properties) where T : new()
        {
            //list of meta to dictionary
            var dynamicObject = new ExpandoObject() as IDictionary<string, object>;
            foreach (var property in properties)
            {
                dynamicObject.Add(property.MetaKey, property.MetaValue);
            }

            Type type = typeof(T);
            T ret = new T();

            //dictionary to model
            foreach (var keyValue in dynamicObject)
            {
                type.GetProperty(keyValue.Key).SetValue(ret, keyValue.Value, null);
            }

            return ret;
        }

        public static IDictionary<string, string?> ConvertModelToDictionary<T>(T model)
        {
            //model to dictionary
            return model.GetType()
                        .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                        .ToDictionary(prop => prop.Name, prop => (string)prop.GetValue(model, null));
        }
    }
}
