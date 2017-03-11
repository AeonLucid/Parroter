using System;

namespace Parroter.Parrot.Exceptions
{
    internal class UnhandledNotificationException : Exception
    {
        public UnhandledNotificationException(string message) : base(message)
        {
        }
    }
}
