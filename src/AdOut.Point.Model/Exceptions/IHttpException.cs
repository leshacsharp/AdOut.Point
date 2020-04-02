namespace AdOut.Point.Model.Exceptions
{
    public interface IHttpException
    {
        int HttpStatusCode { get; }
    }
}
