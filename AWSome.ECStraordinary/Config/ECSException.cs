using Amazon.ECS;
using System;


namespace Liberator.AWSome.ECStraordinary.Config
{
    /// <summary>
    /// 
    /// </summary>
    public class ECSException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public ECSException(string message, AmazonECSException exception)
        {
            Console.WriteLine(message);
            Console.WriteLine("Amazon error code: {0}", string.IsNullOrEmpty(exception.ErrorCode) ? "None" : exception.ErrorCode);
            Console.WriteLine("Exception message: {0}", exception.Message);
        }
    }
}
