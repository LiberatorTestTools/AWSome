using Amazon;
using Amazon.DataPipeline;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using NUnit.Framework;

namespace Liberator.AWSome.PipeBomb.Config
{
    /// <summary>
    /// Preferences for AWSome PipeBomb
    /// </summary>
    public class Preferences
    {
        /// <summary>
        /// The profile name to use
        /// </summary>
        public static string AWSProfileName { get; set; }

        /// <summary>
        /// The Region Endpoint for the pipeline
        /// </summary>
        public static RegionEndpoint RegionEndpoint { get; set; }

        /// <summary>
        /// The AWS Credentials for the test account
        /// </summary>
        public static AWSCredentials UserAWSCredentials { get; set; }

        /// <summary>
        /// The configuration for the data pipeline
        /// </summary>
        public static AmazonDataPipelineConfig PipelineConfig { get; set; }

        /// <summary>
        /// The Id of the pipeline
        /// </summary>
        public static string PipelineId { get; set; }

        /// <summary>
        /// The polling delay for the pipeline
        /// </summary>
        public static int PipelinePollingDelay { get; set; }

        /// <summary>
        /// The maximum time to hold the pipeline open
        /// </summary>
        public static int PipelineMaxTime { get; set; }

        /// <summary>
        /// Gets the Pipeline Client set in the App.config file
        /// </summary>
        /// <returns>The Pipeline Client as configured</returns>
        public static AmazonDataPipelineClient GetPipelineClient()
        {
            return new AmazonDataPipelineClient(UserAWSCredentials, RegionEndpoint.EUWest1);
        }

        /// <summary>
        /// Gets the Pipeline Client set in the App.config file
        /// </summary>
        /// <param name="regionEndpoint">The region endpoint for the pipeline</param>
        /// <returns>The Pipeline Client as configured</returns>
        public static AmazonDataPipelineClient GetPipelineClient(RegionEndpoint regionEndpoint)
        {
            return new AmazonDataPipelineClient(UserAWSCredentials, regionEndpoint);
        }

        /// <summary>
        /// Gets the Pipeline Client set in the App.config file
        /// </summary>
        /// <param name="amazonDataPipelineConfig"></param>
        /// <returns>The Kinesis Pipeline as configured</returns>
        public static AmazonDataPipelineClient GetPipelineClient(AmazonDataPipelineConfig amazonDataPipelineConfig)
        {
            return new AmazonDataPipelineClient(UserAWSCredentials, amazonDataPipelineConfig);
        }

        /// <summary>
        /// Gets the user's AWS Credentials from their Credential Profile Store Chain
        /// </summary>
        /// <returns>The AWS Credentials</returns>
        public static AWSCredentials GetAWSCredentials()
        {
            CredentialProfileStoreChain profileStoreChain = new CredentialProfileStoreChain();
            if (!profileStoreChain.TryGetAWSCredentials(AWSProfileName, out AWSCredentials awsCredentials))
            {
                Assert.Fail("Could not get AWS credentials");
            }
            return awsCredentials;
        }
    }
}
