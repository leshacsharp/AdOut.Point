using System;

namespace AdOut.Point.Common.Helpers
{
    public static class FileHelper
    {
        public static string GetRandomFileName()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
