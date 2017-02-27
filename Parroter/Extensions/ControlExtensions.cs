using System.ComponentModel;
using System.Windows.Forms;

namespace Parroter.Extensions
{
    public static class ControlExtensions
    {
        public delegate void InvokeIfRequiredDelegate<T>(T obj) where T : ISynchronizeInvoke;

        public static void InvokeIfRequired(this ISynchronizeInvoke obj, MethodInvoker action)
        {
            if (obj.InvokeRequired)
            {
                var args = new object[0];
                obj.Invoke(action, args);
            }
            else
            {
                action();
            }
        }
    }
}
