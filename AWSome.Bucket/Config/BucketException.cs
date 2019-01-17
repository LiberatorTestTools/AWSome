using Amazon.S3;
using System;

namespace AWSome.Bucket.Config
{
    public class BucketException : Exception
    {
        public BucketException(string message, AmazonS3Exception exception)
        {
            Console.WriteLine(message);
            Console.WriteLine("Amazon error code: {0}", string.IsNullOrEmpty(exception.ErrorCode) ? "None" : exception.ErrorCode);
            Console.WriteLine("Exception message: {0}", exception.Message);
        }
    }
}
