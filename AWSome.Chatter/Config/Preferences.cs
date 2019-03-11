using Amazon;
using Amazon.Kinesis;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using NUnit.Framework;

namespace Liberator.AWSome.Chatter.Config
{
    /// <summary>
    /// Preferences for Chatter
    /// </summary>
    public class Preferences
    {
        /// <summary>
        /// The name of the AWS profile
        /// </summary>
        public static string AWSProfileName { get; set; }

        /// <summary>
        /// The name of the kinesis stream
        /// </summary>
        public static string KinesisStreamName { get; set; }

        /// <summary>
        /// The endpoint to be used for the stream
        /// </summary>
        public static RegionEndpoint RegionEndpoint { get; set; }

        /// <summary>
        /// The AWS credentials for the user
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
        /// Gets the user's AWS Credentials
        /// </summary>
        /// <returns>The AWS Credentials for the user</returns>
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
