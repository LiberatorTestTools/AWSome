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
        /// 
        /// </summary>
        public static NameValueCollection appSettings = ConfigurationManager.AppSettings;

        /// <summary>
        /// 
        /// </summary>
        public static AWSCredentials UserAWSCredentials { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public static RegionEndpoint RegionEndpoint { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public static string AWSProfileName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public static bool StreamEnabled { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public static StreamViewType StreamViewType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public static bool SSEEnabled { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public static string KMSMasterKeyId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public static SSEType SSEType { get; set; }

        /// <summary>
        /// Sets the Preferences based on the configuration file
        /// </summary>
        static Preferences()
        {
            bool.TryParse(appSettings.Get("StreamEnabled"), out bool streamEnabled);
            bool.TryParse(appSettings.Get("SseEnabled"), out bool sseEnabled);
             
            RegionEndpoint = RegionEndpoint.GetBySystemName(appSettings.Get("RegionEndpoint"));

            AWSProfileName = appSettings.Get("AWSProfileName");

            StreamEnabled = streamEnabled;
            StreamViewType = StreamViewType.FindValue(appSettings.Get("StreamViewType"));

            SSEEnabled = sseEnabled;
            KMSMasterKeyId = appSettings.Get("KMSMasterKeyId");
            SSEType = SSEType.FindValue(appSettings.Get("SSEType"));

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
