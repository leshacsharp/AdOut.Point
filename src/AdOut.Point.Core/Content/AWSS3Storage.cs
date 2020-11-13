﻿using AdOut.Point.Model.Interfaces.Content;
using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using System;
using System.IO;
using System.Threading.Tasks;

namespace AdOut.Point.Core.Content
{
    public class AWSS3Storage : IContentStorage
    {
        private readonly IAmazonS3 _awsClient;
        private readonly string _bucketName;

        public AWSS3Storage(IAmazonS3 awsClient, string bucketName)
        {
            _awsClient = awsClient;
            _bucketName = bucketName;
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
                BucketName = _bucketName,
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
                BucketName = _bucketName,
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
                BucketName = _bucketName,
                Key = filePath,
            };

            //todo: add logging
            var response = await _awsClient.GetObjectAsync(getRequest);
            return response.ResponseStream;
        }
    }
}
