using Amazon.S3;
using Amazon.S3.Model;
using Amazon.Runtime;
using Moq;
using System.Collections.Generic;
using System.Net;

namespace AWSome.Tests.Bucket
{
    public static class BucketMoqs
    {
        public static IClientConfig ClientConfig { get; set; }
               
        public static string BucketName { get; set; }
               
        public static string Key { get; set; }
               
        public static RequestPayer RequestPayer { get; set; }
               
        public static string UploadId { get; set; }

        public static string VersionId { get; set; }

        public static string SourceVersionId { get; set; }

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

            mockS3Client.Setup(m => m.CopyObject(CopyObjectRequest())).Returns(CopyObjectResponse);

            return mockS3Client.Object;
        }

        private static CopyObjectRequest CopyObjectRequest()
        {
            Mock<CopyObjectRequest> mockS3Request = new Mock<CopyObjectRequest>(MockBehavior.Strict);
            mockS3Request.SetupProperty(m => m.CannedACL, null);
            mockS3Request.SetupProperty(m => m.SourceBucket, null);
            mockS3Request.SetupProperty(m => m.SourceKey, null);
            mockS3Request.SetupProperty(m => m.SourceVersionId, null);
            mockS3Request.SetupProperty(m => m.DestinationBucket, null);
            mockS3Request.SetupProperty(m => m.DestinationKey, null);
            mockS3Request.SetupProperty(m => m.Grants, null);
            mockS3Request.SetupProperty(m => m.RequestPayer, RequestPayer);
            mockS3Request.SetupProperty(m => m.StorageClass, null);
            return mockS3Request.Object;
        }

        private static CopyObjectResponse CopyObjectResponse()
        {
            Mock<CopyObjectResponse> mockS3Request = new Mock<CopyObjectResponse>(MockBehavior.Strict);
            mockS3Request.SetupProperty(m => m.ContentLength, ContentLength);
            mockS3Request.SetupProperty(m => m.HttpStatusCode, HttpStatusCode);
            mockS3Request.SetupProperty(m => m.ResponseMetadata, ResponseMetadata);
            mockS3Request.SetupProperty(m => m.SourceVersionId, SourceVersionId);
            return mockS3Request.Object;
        }

        private static DeleteBucketRequest DeleteBucketRequest()
        {
            Mock<DeleteBucketRequest> mockS3Request = new Mock<DeleteBucketRequest>(MockBehavior.Strict);
            mockS3Request.SetupProperty(m => m.BucketName, BucketName);
            mockS3Request.SetupProperty(m => m.BucketRegion, null);
            return mockS3Request.Object;
        }

        private static DeleteBucketResponse DeleteBucketResponse()
        {
            Mock<DeleteBucketResponse> mockS3Request = new Mock<DeleteBucketResponse>(MockBehavior.Strict);
            mockS3Request.SetupProperty(m => m.ContentLength, ContentLength);
            mockS3Request.SetupProperty(m => m.HttpStatusCode, HttpStatusCode);
            mockS3Request.SetupProperty(m => m.ResponseMetadata, ResponseMetadata);
            return mockS3Request.Object;
        }

        private static PutBucketRequest PutBucketRequest()
        {
            Mock<PutBucketRequest> mockS3Requests = new Mock<PutBucketRequest>(MockBehavior.Strict);
            mockS3Requests.SetupProperty(m => m.BucketName, BucketName);
            mockS3Requests.SetupProperty(m => m.BucketRegion, null);
            mockS3Requests.SetupProperty(m => m.CannedACL, null);
            mockS3Requests.SetupProperty(m => m.Grants, null);
            mockS3Requests.SetupProperty(m => m.ObjectLockEnabledForBucket, false);
            mockS3Requests.SetupProperty(m => m.UseClientRegion, true);
            return mockS3Requests.Object;
        }

        private static PutBucketResponse PutBucketResponse()
        {
            Mock<PutBucketResponse> mockS3Requests = new Mock<PutBucketResponse>(MockBehavior.Strict);
            mockS3Requests.SetupProperty(m => m.ContentLength, ContentLength);
            mockS3Requests.SetupProperty(m => m.HttpStatusCode, HttpStatusCode);
            mockS3Requests.SetupProperty(m => m.ResponseMetadata, ResponseMetadata);
            return mockS3Requests.Object;
        }

        private static PutObjectRequest PutObjectRequest()
        {
            Mock<PutObjectRequest> mockS3Request = new Mock<PutObjectRequest>(MockBehavior.Strict);
            mockS3Request.SetupProperty(m => m.BucketName, BucketName);
            mockS3Request.SetupProperty(m => m.CannedACL, null);
            mockS3Request.SetupProperty(m => m.ContentBody, null);
            mockS3Request.SetupProperty(m => m.FilePath, null);
            mockS3Request.SetupProperty(m => m.Grants, null);
            mockS3Request.SetupProperty(m => m.InputStream, null);
            mockS3Request.SetupProperty(m => m.Key, Key);
            mockS3Request.SetupProperty(m => m.MD5Digest, null);
            mockS3Request.SetupProperty(m => m.RequestPayer, RequestPayer);
            mockS3Request.SetupProperty(m => m.StorageClass, null);
            return mockS3Request.Object;
        }

        private static PutObjectResponse PutObjectResponse()
        {
            Mock<PutObjectResponse> mockS3Request = new Mock<PutObjectResponse>(MockBehavior.Strict);
            mockS3Request.SetupProperty(m => m.ContentLength, ContentLength);
            mockS3Request.SetupProperty(m => m.HttpStatusCode, HttpStatusCode);
            mockS3Request.SetupProperty(m => m.ResponseMetadata, ResponseMetadata);
            mockS3Request.SetupProperty(m => m.VersionId, VersionId);
            return mockS3Request.Object;
        }
    }
}
