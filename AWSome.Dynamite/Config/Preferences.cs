using Amazon;
using Amazon.DynamoDBv2;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using NUnit.Framework;
using System.Collections.Specialized;
using System.Configuration;

namespace Liberator.AWSome.Dynamite.Config
{
    /// <summary>
    /// Preferences for the library
    /// </summary>
    public static class Preferences
    {
        /// <summary>
        /// Contains the user's AWS credentials
        /// </summary>
        public static AWSCredentials UserAWSCredentials { get; set; }

        /// <summary>
        /// Contains the currently used Region Endpoint
        /// </summary>
        public static RegionEndpoint RegionEndpoint { get; set; }

        /// <summary>
        /// The profile name being used by AWSome
        /// </summary>
        public static string AWSProfileName { get; set; }

        /// <summary>
        /// Whether streaming has been enableds
        /// </summary>
        public static bool StreamEnabled { get; set; }

        /// <summary>
        /// The type of stream view to be used
        /// </summary>
        public static StreamViewType StreamViewType { get; set; }

        /// <summary>
        /// Whether SSE has been enabled
        /// </summary>
        public static bool SSEEnabled { get; set; }

        /// <summary>
        /// The ID of an AWS managed customer master key
        /// </summary>
        public static string KMSMasterKeyId { get; set; }

        /// <summary>
        /// The type of SSE to use
        /// </summary>
        public static SSEType SSEType { get; set; }

        /// <summary>
        /// Sets the Preferences based on the configuration file
        /// </summary>
        static Preferences()
        {
            UserAWSCredentials = GetAWSCredentials();
        }


        /// <summary>
        /// Gets the DynamoDB Client set in the App.config file
        /// </summary>
        /// <returns>The Dynamo DB Client as configured</returns>
        public static IAmazonDynamoDB GetDynamoDbClient()
        {
            return new AmazonDynamoDBClient(UserAWSCredentials, RegionEndpoint);
        }

        /// <summary>
        /// Gets the user's AWS credentials from their credential profile store chain
        /// </summary>
        /// <returns>The AWS credentials for the user</returns>
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
