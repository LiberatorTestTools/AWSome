using Amazon.ECS;
using System;


namespace AWSome.ECStraordinary.Config
{
    public class ECSException : Exception
    {
        public ECSException(string message, AmazonECSException exception)
        {
            Console.WriteLine(message);
            Console.WriteLine("Amazon error code: {0}", string.IsNullOrEmpty(exception.ErrorCode) ? "None" : exception.ErrorCode);
            Console.WriteLine("Exception message: {0}", exception.Message);
        }
    }
}
