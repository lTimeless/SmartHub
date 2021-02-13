using System;
using System.Collections.Generic;

namespace SmartHub.BasePlugin.Interfaces
{
    public interface IBuild<T> : IPlugin where T : IBuild<T>
    {
        T InstantiateQuery();
        Tuple<string, Dictionary<string, string>> Build();
    }
}