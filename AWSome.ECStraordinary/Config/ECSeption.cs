using Amazon.ECS;
using System;


namespace Liberator.AWSome.ECStraordinary.Config
{
    /// <summary>
    /// An ECStraordinary exception has been thrown
    /// </summary>
    public class ECSeption : Exception
    {
        /// <summary>
        /// An ECStraordinary exception has been thrown
        /// </summary>
        /// <param name="message">The message for the ECSeption</param>
        /// <param name="exception">The underlying AmazonECSException</param>
        public ECSeption(string message, AmazonECSException exception)
        {
            Console.WriteLine(message);
            Console.WriteLine("Amazon error code: {0}", string.IsNullOrEmpty(exception.ErrorCode) ? "None" : exception.ErrorCode);
            Console.WriteLine("Exception message: {0}", exception.Message);
        }
    }
}
