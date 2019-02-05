using Amazon.DataPipeline;
using System;

namespace AWSome.Piping.Config
{
    public class PipingException : Exception
    {
        public PipingException(string message, AmazonDataPipelineException exception)
        {
            Console.WriteLine(message);
            Console.WriteLine("Amazon error code: {0}", string.IsNullOrEmpty(exception.ErrorCode) ? "None" : exception.ErrorCode);
            Console.WriteLine("Exception message: {0}", exception.Message);
        }
    }
}
