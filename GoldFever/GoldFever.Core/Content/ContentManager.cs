using Newtonsoft.Json;
using System;
using System.IO;
using System.Reflection;

namespace GoldFever.Core.Content
{
    public sealed class ContentManager
    {
        private string _path;

        public string Path
        {
            get { return _path; }
        }

        public ContentManager(string path)
        {
            if (String.IsNullOrEmpty(path))
                throw new ArgumentException("Invalid path supplied.");

            _path = path;
        }

        private Stream GetStream(string fileName)
        {
            var assembly = Assembly.GetEntryAssembly();
            return assembly.GetManifestResourceStream(String.Join(".", _path, fileName));
        }

        private T LoadObject<T>(Stream stream)
        {
            var serializer = new JsonSerializer();

            using (var streamReader = new StreamReader(stream))
            using (var jsonReader = new JsonTextReader(streamReader))
            {
                return serializer.Deserialize<T>(jsonReader);
            }
        }

        public T LoadObject<T>(string fileName)
        {
            try
            {
                return LoadObject<T>(GetStream(fileName));
            }
            catch(ContentLoadException ex)
            {
                throw ex;
            }
            catch(Exception ex)
            {
                throw new ContentLoadException(fileName, ex);
            }
        }
    }
}
