using System;
using System.Text.Json;
using Newtonsoft.Json.Linq;

namespace archolosDotNet.Services;

public static class JsonToItemConverter
{
    public static T Merge<T>(T obj, JsonElement json)
    {
        var propList = json.EnumerateObject();

        foreach (var _prop in propList)
        {
            Console.WriteLine("-");
            var propName = StringService.Capitalize(_prop.Name);
            var prop = obj!.GetType().GetProperty(propName);
            Console.WriteLine("property: " + propName);
            Console.WriteLine("+ " + prop);
            Console.WriteLine(obj!.GetType().GetProperty(propName) == null);
            Console.WriteLine(_prop.Value);

            if (obj!.GetType().GetProperty(propName) != null)
            {
                Console.WriteLine("****" + _prop.Value);
                Console.WriteLine("****" + prop!.GetType().BaseType);

                // var newValue = SimpleDeserialize(prop!.GetType(), _prop.Value);
                // Console.WriteLine("****" + newValue + " ::: " + newValue.GetType());

                // obj!.GetType().GetProperty(propName)?.SetValue(obj, newValue);
            }
            // obj.GetType().GetProperty(propName);
        }

        Console.WriteLine("------");
        Console.WriteLine(obj!.GetType().GetProperty("Price") == null);
        Console.WriteLine(obj!.GetType().GetProperty("Mrice") == null);
        Console.WriteLine(obj!.GetType().GetProperty("Value") == null);
        Console.WriteLine("------");

        foreach (var i in obj.GetType().GetProperties())
        {
            Console.WriteLine(i.GetType());
        }

        return obj;
    }

    public static dynamic SimpleDeserialize(Type type, JsonElement value)
    {
        // var type = example?.GetType();
        Console.WriteLine("### : " + type);

        // if (example == null)
        // {
        //     Console.WriteLine("NULL");
        //     return null;
        // }

        switch (type)
        {
            case Type intType when intType == typeof(int):
                return value.GetInt32();
            case Type decType when decType == typeof(decimal):
                return value.GetDecimal();
            case Type boolType when boolType == typeof(bool):
                return value.GetBoolean();
            case Type strType when strType == typeof(string):
                return value.GetString();
        }
        // є ще проблема якщо Nullable

        Console.WriteLine("Default deserialize - " + type);
        return value.GetString();
        // return value.Deserialize<T>();
    }
}
