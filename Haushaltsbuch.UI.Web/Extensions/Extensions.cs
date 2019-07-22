using System;
using System.Collections.Generic;

namespace Haushaltsbuch.UI.Web.Extensions
{
    public static class Extensions
    {
        public static void ForEach<T>(this IEnumerable<T> ie, Action<T> action)
        {
            foreach (T i in ie)
            {
                action(obj: i);
            }
        }
    }
}