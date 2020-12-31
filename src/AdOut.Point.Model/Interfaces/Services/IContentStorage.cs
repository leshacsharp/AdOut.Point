﻿using System.IO;
using System.Threading.Tasks;

namespace AdOut.Point.Model.Interfaces.Services
{
    public interface IContentStorage
    {
        Task CreateObjectAsync(Stream content, string filePath);
        Task DeleteObjectAsync(string filePath);
        Task<Stream> GetObjectAsync(string filePath);
    }
}
