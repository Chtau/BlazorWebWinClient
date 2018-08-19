using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Blazor.Browser.Interop;
using Newtonsoft.Json;

namespace WebWinClient
{
    public static class Storage
    {
        public static void WriteStorage<T>(string key, T value)
        {
            string strValue = JsonConvert.SerializeObject(value);
            RegisteredFunction.Invoke<bool>($"writeStorage", key, strValue);
        }

        public static T ReadStorage<T>(string key)
        {
            string strValue = RegisteredFunction.Invoke<string>($"readStorage", key);
            return JsonConvert.DeserializeObject<T>(strValue);
        }

        public static void WriteStorage(string key, string value)
        {
            RegisteredFunction.Invoke<bool>($"writeStorage", key, value);
        }

        public static string ReadStorage(string key)
        {
            return RegisteredFunction.Invoke<string>($"readStorage", key);
        }

        public static void RemoveStorage(string key)
        {
            RegisteredFunction.Invoke<bool>($"removeStorage", key);
        }
    }
}
