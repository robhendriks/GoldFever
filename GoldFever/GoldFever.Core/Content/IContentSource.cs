using System;
using System.Reflection;

namespace GoldFever.Core.Content
{
    public interface IContentSource
    {
        Assembly GetAssembly();
    }
}
