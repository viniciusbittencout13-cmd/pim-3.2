using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using GLLRV.DesktopApp.Models;

namespace GLLRV.DesktopApp.Services
{
    public static class JsonUserStore
    {
        private class UsuarioRoot
        {
            public List<Usuario>? Usuarios { get; set; }
        }

        private static string GetFilePath()
        {
            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            return Path.Combine(baseDir, "data", "usuarios.json");
        }

        public static List<Usuario> LoadAll()
        {
            var path = GetFilePath();
            if (!File.Exists(path))
                return new List<Usuario>();

            var json = File.ReadAllText(path);
            var root = JsonSerializer.Deserialize<UsuarioRoot>(json) ?? new UsuarioRoot();
            return root.Usuarios ?? new List<Usuario>();
        }

        public static void SaveAll(List<Usuario> usuarios)
        {
            var path = GetFilePath();
            var root = new UsuarioRoot { Usuarios = usuarios };
            var json = JsonSerializer.Serialize(root, new JsonSerializerOptions { WriteIndented = true });
            Directory.CreateDirectory(Path.GetDirectoryName(path)!);
            File.WriteAllText(path, json);
        }
    }
}
