using System;
using System.Linq;
using System.Reflection;

namespace GoldFever.Core.Content
{
    public class NamedContentSource : IContentSource
    {
        private string _name;

        public string Name
        {
            get { return _name; }
        }

        public NamedContentSource(string name)
        {
            _name = name;
        }

        public Assembly GetAssembly()
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .SingleOrDefault(a => a.GetName().Name.Equals(_name));
        }
    }
}
