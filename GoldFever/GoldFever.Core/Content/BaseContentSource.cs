using System;
using System.Reflection;

namespace GoldFever.Core.Content
{
    public class BaseContentSource : IContentSource
    {
        public Assembly GetAssembly()
        {
            return Assembly.GetEntryAssembly();
        }
    }
}
