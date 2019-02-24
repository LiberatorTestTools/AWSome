using Amazon;
using Amazon.Kinesis;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using NUnit.Framework;

namespace Liberator.AWSome.Messages.Config
{
    /// <summary>
    /// 
    /// </summary>
    public class Preferences
    {

        /// <summary>
        /// 
        /// </summary>
        public static string AWSProfileName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public static string KinesisStreamName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public static RegionEndpoint RegionEndpoint { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public static AWSCredentials UserAWSCredentials { get; private set; }

        /// <summary>
        /// Gets the Kinesis Client set in the App.config file
        /// </summary>
        /// <returns>The Kinesis Client as configured</returns>
        public static AmazonKinesisClient GetKinesisClient()
        {
            return new AmazonKinesisClient(UserAWSCredentials, RegionEndpoint);
        }

        /// <summary>
        /// Gets the Kinesis Client set in the App.config file
        /// </summary>
        /// <returns>The Kinesis Client as configured</returns>
        public static AmazonKinesisClient GetKinesisClient(RegionEndpoint regionEndpoint)
        {
            return new AmazonKinesisClient(UserAWSCredentials, regionEndpoint);
        }

        /// <summary>
        /// Gets the Kinesis Client set in the App.config file
        /// </summary>
        /// <returns>The Kinesis Client as configured</returns>
        public static AmazonKinesisClient GetKinesisClient(AmazonKinesisConfig amazonKinesisConfig)
        {
            return new AmazonKinesisClient(UserAWSCredentials, amazonKinesisConfig);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static AWSCredentials GetAWSCredentials()
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
