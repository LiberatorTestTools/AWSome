using Amazon;
using Amazon.DataPipeline;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using NUnit.Framework;

namespace Liberator.AWSome.Piping.Config
{
    public class Preferences
    {
        public static string AWSProfileName { get; set; }
        public static RegionEndpoint RegionEndpoint { get; set; }
        public static AWSCredentials UserAWSCredentials { get; set; }
        public static AmazonDataPipelineConfig PipelineConfig { get; set; }
        public static string PipelineId { get; set; }

        public string GetPipelineId()
        {
            return PipelineId;
        }

        public static int PipelinePollingDelay { get; set; }
        public static int PipelineMaxTime { get; set; }

        public AmazonDataPipelineConfig GetPipelineConfig()
        {
            return new AmazonDataPipelineConfig()
            {
                RegionEndpoint = RegionEndpoint.EUWest1
            };

        }

        public int GetPipelineMaxTime()
        {
            return PipelineMaxTime;
        }

        public int GetPipelinePollingDelay()
        {
            return PipelinePollingDelay;
        }

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
        /// <returns>The Pipeline Client as configured</returns>
        public static AmazonDataPipelineClient GetPipelineClient(RegionEndpoint regionEndpoint)
        {
            return new AmazonDataPipelineClient(UserAWSCredentials, regionEndpoint);
        }

        /// <summary>
        /// Gets the Pipeline Client set in the App.config file
        /// </summary>
        /// <returns>The Kinesis Pipeline as configured</returns>
        public static AmazonDataPipelineClient GetPipelineClient(AmazonDataPipelineConfig amazonDataPipelineConfig)
        {
            return new AmazonDataPipelineClient(UserAWSCredentials, amazonDataPipelineConfig);
        }

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
