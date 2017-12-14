using System;

namespace Plugins.Utils.Extensions
{
    public static class ActionExtensions
    {
        public static void SafeInvoke<T>(this Action<T> action, T param)
        {
            if(action!=null)
                action.Invoke(param);
        }
    }
}