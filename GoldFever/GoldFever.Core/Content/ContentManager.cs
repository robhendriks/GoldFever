using Newtonsoft.Json;
using System;
using System.IO;
using System.Reflection;

namespace GoldFever.Core.Content
{
    public sealed class ContentManager
    {
        #region Properties

        private string _path;

        public string Path
        {
            get { return _path; }
        }

        private IContentSource _source;

        public IContentSource Source
        {
            get { return _source; }
        }

        #endregion


        #region Constructors

        public ContentManager(string path, IContentSource source)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentException("Invalid path supplied.");
            else if (source == null)
                throw new ArgumentNullException("source");

            _path = path;
            _source = source;
        }

        #endregion


        #region Methods

        private Stream GetStream(string fileName)
        {
            var assembly = Source.GetAssembly();
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

        #endregion
    }
}
