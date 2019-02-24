using Amazon.S3;
using System;

namespace Liberator.AWSome.Bucket.Config
{
    /// <summary>
    /// 
    /// </summary>
    public class BucketException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public BucketException(string message, AmazonS3Exception exception)
        {
            Console.WriteLine(message);
            Console.WriteLine("Amazon error code: {0}", string.IsNullOrEmpty(exception.ErrorCode) ? "None" : exception.ErrorCode);
            Console.WriteLine("Exception message: {0}", exception.Message);
        }
    }
}
