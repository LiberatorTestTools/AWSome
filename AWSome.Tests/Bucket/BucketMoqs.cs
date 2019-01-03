using Amazon.S3;
using Amazon.S3.Model;
using Amazon.Runtime;
using Moq;
using System.Collections.Generic;
using System.Net;
using ParameterValue = Amazon.DataPipeline.Model.ParameterValue;

namespace AWSome.Tests.Bucket
{
    public static class BucketMoqs
    {
        public static IClientConfig ClientConfig { get; set; }
               
        public static string BucketName { get; set; }
               
        public static string Key { get; set; }
               
        public static RequestPayer RequestPayer { get; set; }
               
        public static string UploadId { get; set; }
               
        public static List<PartETag> PartETags { get; set; }
               
        public static string ETag { get; set; }
               
        public static long ContentLength { get; set; }
               
        public static HttpStatusCode HttpStatusCode { get; set; }
               
        public static RequestCharged RequestCharged { get; set; }
               
        public static ResponseMetadata ResponseMetadata { get; set; }
               
        public static Expiration Expiration { get; set; }
             
        

        public static AmazonS3Client AmazonS3Client()
        {
            Mock<AmazonS3Client> mockS3Client = new Mock<AmazonS3Client>(MockBehavior.Strict);
            mockS3Client.SetupProperty(m => m.Config, ClientConfig);

            mockS3Client.Setup(m => m.AbortMultipartUpload(AbortMultipartUploadRequest())).Returns(AbortMultipartUploadResponse);
            mockS3Client.Setup(m => m.CompleteMultipartUpload(CompleteMultipartUploadRequest())).Returns(CompleteMultipartUploadResponse);

            return mockS3Client.Object;
        }

        private static AbortMultipartUploadRequest AbortMultipartUploadRequest()
        {
            Mock<AbortMultipartUploadRequest> mockS3Request = new Mock<AbortMultipartUploadRequest>(MockBehavior.Strict);
            mockS3Request.SetupProperty(m => m.BucketName, BucketName);
            mockS3Request.SetupProperty(m => m.Key, Key);
            mockS3Request.SetupProperty(m => m.RequestPayer, RequestPayer);
            mockS3Request.SetupProperty(m => m.UploadId, UploadId);
            return mockS3Request.Object;
        }

        private static AbortMultipartUploadResponse AbortMultipartUploadResponse()
        {
            Mock<AbortMultipartUploadResponse> mockS3Request = new Mock<AbortMultipartUploadResponse>(MockBehavior.Strict);
            mockS3Request.SetupProperty(m => m.ContentLength, ContentLength);
            mockS3Request.SetupProperty(m => m.HttpStatusCode, HttpStatusCode);
            mockS3Request.SetupProperty(m => m.RequestCharged, RequestCharged);
            mockS3Request.SetupProperty(m => m.ResponseMetadata, ResponseMetadata);
            return mockS3Request.Object;
        }

        private static CompleteMultipartUploadRequest CompleteMultipartUploadRequest()
        {
            Mock<CompleteMultipartUploadRequest> mockS3Request = new Mock<CompleteMultipartUploadRequest>(MockBehavior.Strict);
            mockS3Request.SetupProperty(m => m.BucketName, BucketName);
            mockS3Request.SetupProperty(m => m.Key, Key);
            mockS3Request.SetupProperty(m => m.PartETags, PartETags);
            mockS3Request.SetupProperty(m => m.RequestPayer, RequestPayer);
            mockS3Request.SetupProperty(m => m.UploadId, UploadId);
            return mockS3Request.Object;
        }

        private static CompleteMultipartUploadResponse CompleteMultipartUploadResponse()
        {
            Mock<CompleteMultipartUploadResponse> mockS3Request = new Mock<CompleteMultipartUploadResponse>(MockBehavior.Strict);
            mockS3Request.SetupProperty(m => m.BucketName, BucketName);
            mockS3Request.SetupProperty(m => m.ContentLength, ContentLength);
            mockS3Request.SetupProperty(m => m.ETag, ETag);
            mockS3Request.SetupProperty(m => m.Expiration, Expiration);
            mockS3Request.SetupProperty(m => m.HttpStatusCode, HttpStatusCode);
            mockS3Request.SetupProperty(m => m.Key, Key);
            return mockS3Request.Object;
        }

        private static CopyObjectRequest CopyObjectRequest()
        {
            Mock<CopyObjectRequest> mockS3Request = new Mock<CopyObjectRequest>(MockBehavior.Strict);

            return mockS3Request.Object;
        }

        private static CopyObjectResponse CopyObjectResponse()
        {
            Mock<CopyObjectResponse> mockS3Request = new Mock<CopyObjectResponse>(MockBehavior.Strict);

            return mockS3Request.Object;
        }
    }
}
