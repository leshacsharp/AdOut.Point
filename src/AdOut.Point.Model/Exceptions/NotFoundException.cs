using System;

namespace AdOut.Point.Model.Exceptions
{
    public class NotFoundException : Exception, IHttpException
    {
        public NotFoundException(string message) : base(message)
        {
        }

        public int HttpStatusCode => Constants.HttpStatusCodes.Status404NotFound;
    }
}
