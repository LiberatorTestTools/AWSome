using Amazon;
using Amazon.DynamoDBv2;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using NUnit.Framework;
using System.Collections.Specialized;
using System.Configuration;

namespace AWSome.Dynamite.Config
{
    public class Preferences
    {
        public NameValueCollection appSettings = ConfigurationManager.AppSettings;

        public static AWSCredentials UserAWSCredentials { get; set; }
        public static RegionEndpoint RegionEndpoint { get; set; }

        public static string AWSProfileName { get; set; }

        public static bool StreamEnabled { get; set; }
        public static StreamViewType StreamViewType { get; set; }

        public static bool SSEEnabled { get; set; }
        public static string KMSMasterKeyId { get; set; }
        public static SSEType SSEType { get; set; }

        private static Preferences instance;
        /// <summary>
        /// Gets the Preferences singleton, populated from the app.config file.
        /// </summary>
        public static Preferences GetInstance()
        {
            if (instance == null)
            {
                instance = new Preferences();
            }

            return instance;
        }

        /// <summary>
        /// Sets the Preferences based on the configuration file
        /// </summary>
        private Preferences()
        {
            bool.TryParse(appSettings.Get("StreamEnabled"), out bool streamEnabled);
            bool.TryParse(appSettings.Get(""), out bool sseEnabled);

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
        public IAmazonDynamoDB GetDynamoDbClient()
        {
            return new AmazonDynamoDBClient(UserAWSCredentials, RegionEndpoint);
        }

        private AWSCredentials GetAWSCredentials()
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
