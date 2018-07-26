using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Newtonsoft.Json;

namespace WebWinClient
{
    public static class Storage
    {
        public static async Task WriteStorage<T>(string key, T value)
        {
            string strValue = JsonConvert.SerializeObject(value);
            await JSRuntime.Current.InvokeAsync<bool>("storage.writeStorage", key, strValue);
        }

        public static async Task<T> ReadStorage<T>(string key)
        {
            string strValue = await JSRuntime.Current.InvokeAsync<string>("storage.readStorage", key);
            if (!string.IsNullOrWhiteSpace(strValue))
                return JsonConvert.DeserializeObject<T>(strValue);
            return default;
        }

        public static async Task WriteStorage(string key, string value)
        {
            await JSRuntime.Current.InvokeAsync<bool>($"storage.writeStorage", key, value);
        }

        public static async Task<string> ReadStorage(string key)
        {
            return await JSRuntime.Current.InvokeAsync<string>($"storage.readStorage", key);
        }

        public static async Task RemoveStorage(string key)
        {
            await JSRuntime.Current.InvokeAsync<bool>($"storage.removeStorage", key);
        }
    }
}
