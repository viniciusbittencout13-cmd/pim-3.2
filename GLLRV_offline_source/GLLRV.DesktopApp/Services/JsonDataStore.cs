using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace GLLRV.DesktopApp.Services
{
    public interface IDataStore
    {
        Task<T[]> LoadAsync<T>(string name);
        Task SaveAsync<T>(string name, IEnumerable<T> data);
        string Root { get; }
    }

    public class JsonDataStore : IDataStore
    {
        private readonly string _root;
        public string Root => _root;

        public JsonSerializerOptions Options { get; } = new(JsonSerializerDefaults.Web)
        {
            WriteIndented = true
        };

        public JsonDataStore(string root)
        {
            _root = Path.GetFullPath(root);
            Directory.CreateDirectory(_root);
        }

        public async Task<T[]> LoadAsync<T>(string name)
        {
            var path = Path.Combine(_root, name + ".json");
            if (!File.Exists(path))
                return Array.Empty<T>();

            await using var fs = File.OpenRead(path);
            var data = await JsonSerializer.DeserializeAsync<T[]>(fs, Options);
            return data ?? Array.Empty<T>();
        }

        public async Task SaveAsync<T>(string name, IEnumerable<T> data)
        {
            var path = Path.Combine(_root, name + ".json");
            await using var fs = File.Create(path);
            await JsonSerializer.SerializeAsync(fs, data, Options);
        }
    }
}
