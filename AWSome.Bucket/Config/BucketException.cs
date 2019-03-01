using Amazon.S3;
using System;

namespace Liberator.AWSome.Bucket.Config
{
    /// <summary>
    /// An exception has been thrown into a bucket
    /// </summary>
    public class BucketException : Exception
    {
        /// <summary>
        /// There has been an exceptional bucket found
        /// </summary>
        /// <param name="message">The message sent by Bucket</param>
        /// <param name="exception">The inner exception that was thrown</param>
        public BucketException(string message, AmazonS3Exception exception)
        {
            Console.WriteLine(message);
            Console.WriteLine("Amazon error code: {0}", string.IsNullOrEmpty(exception.ErrorCode) ? "None" : exception.ErrorCode);
            Console.WriteLine("Exception message: {0}", exception.Message);
        }
    }
}
