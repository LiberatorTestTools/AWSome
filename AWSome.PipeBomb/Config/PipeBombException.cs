using Amazon.DataPipeline;
using System;

namespace Liberator.AWSome.PipeBomb.Config
{
    /// <summary>
    /// An AWSome pipe bomb has exploded
    /// </summary>
    public class PipeBombException : Exception
    {
        /// <summary>
        /// PipeBomb has blown up.
        /// </summary>
        /// <param name="message">What was shouted when PipeBomb exploded.</param>
        /// <param name="exception">The exception that caused PipeBomb to blow up.</param>
        public PipeBombException(string message, AmazonDataPipelineException exception)
        {
            Console.WriteLine(message);
            Console.WriteLine("Amazon error code: {0}", string.IsNullOrEmpty(exception.ErrorCode) ? "None" : exception.ErrorCode);
            Console.WriteLine("Exception message: {0}", exception.Message);
        }
    }
}
