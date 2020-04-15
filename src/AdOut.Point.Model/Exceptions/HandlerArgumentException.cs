using System;

namespace AdOut.Point.Model.Exceptions
{
    public class HandlerArgumentException : Exception
    {
        public HandlerArgumentException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
