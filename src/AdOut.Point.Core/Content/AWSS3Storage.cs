using AdOut.Point.Common.Helpers;
using AdOut.Point.Model.Interfaces.Content;
using AdOut.Point.Model.Settings;
using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Threading.Tasks;

namespace AdOut.Point.Core.Content
{
    public class AWSS3Storage : IContentStorage
    {
        private readonly IAmazonS3 _awsClient;
        private readonly AWSS3Config _awsConfig;

        public AWSS3Storage(IOptions<AWSS3Config> awsConfig)
        {
            _awsConfig = awsConfig.Value;

            var awsCredentials = new BasicAWSCredentials(_awsConfig.AccessKey, _awsConfig.SecretKey);
            var regionEndpoint = RegionEndpoint.GetBySystemName(_awsConfig.RegionEndpointName);

            _awsClient = new AmazonS3Client(awsCredentials, regionEndpoint);
        }

        public string GenerateFilePath(string filePath)
        {
            if (filePath == null)
            {
                throw new ArgumentNullException(filePath);
            }

            var fileExtension = Path.GetExtension(filePath);
            var fileName = FileHelper.GetRandomFileName();
            var fullPath = $"{fileName}{fileExtension}";

            return fullPath;
        }

        public Task CreateObjectAsync(Stream content, string filePath)
        {
            if (content == null)
            {
                throw new ArgumentNullException(nameof(content));
            }

            if (filePath == null)
            {
                throw new ArgumentNullException(filePath);
            }

            var putRequest = new PutObjectRequest
            {
                BucketName = _awsConfig.BucketName,
                Key = filePath,
                InputStream = content
            };

            //todo: add logging
            return _awsClient.PutObjectAsync(putRequest);
        }

        public Task DeleteObjectAsync(string filePath)
        {
            if (filePath == null)
            {
                throw new ArgumentNullException(filePath);
            }

            var deleteRequest = new DeleteObjectRequest
            {
                BucketName = _awsConfig.BucketName,
                Key = filePath,
            };

            //todo: add logging
            return _awsClient.DeleteObjectAsync(deleteRequest);
        }

        public async Task<Stream> GetObjectAsync(string filePath)
        {
            if (filePath == null)
            {
                throw new ArgumentNullException(filePath);
            }

            var getRequest = new GetObjectRequest
            {
                BucketName = _awsConfig.BucketName,
                Key = filePath,
            };

            //todo: add logging
            var response = await _awsClient.GetObjectAsync(getRequest);
            return response.ResponseStream;
        }
    }
}
