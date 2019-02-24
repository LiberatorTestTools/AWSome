using Amazon;
using Amazon.Kinesis;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using NUnit.Framework;
using System.Collections.Specialized;
using System.Configuration;

namespace Liberator.AWSome.Messages.Config
{
    public class Preferences
    {
        public NameValueCollection appSettings = ConfigurationManager.AppSettings;

        public static string AWSProfileName { get; set; }
        public static string KinesisStreamName { get; set; }
        public static RegionEndpoint RegionEndpoint { get; set; }
        public static AWSCredentials UserAWSCredentials { get; private set; }

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
            AWSProfileName = appSettings.Get("AWSProfileName");
            KinesisStreamName = appSettings.Get("KinesisStreamName");
            RegionEndpoint = RegionEndpoint.GetBySystemName(appSettings.Get("RegionEndpoint"));
            UserAWSCredentials = GetAWSCredentials();
        }

        /// <summary>
        /// Gets the Kinesis Client set in the App.config file
        /// </summary>
        /// <returns>The Kinesis Client as configured</returns>
        public AmazonKinesisClient GetKinesisClient()
        {
            return new AmazonKinesisClient(UserAWSCredentials, RegionEndpoint);
        }

        /// <summary>
        /// Gets the Kinesis Client set in the App.config file
        /// </summary>
        /// <returns>The Kinesis Client as configured</returns>
        public AmazonKinesisClient GetKinesisClient(RegionEndpoint regionEndpoint)
        {
            return new AmazonKinesisClient(UserAWSCredentials, regionEndpoint);
        }

        /// <summary>
        /// Gets the Kinesis Client set in the App.config file
        /// </summary>
        /// <returns>The Kinesis Client as configured</returns>
        public AmazonKinesisClient GetKinesisClient(AmazonKinesisConfig amazonKinesisConfig)
        {
            return new AmazonKinesisClient(UserAWSCredentials, amazonKinesisConfig);
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
