using Amazon;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using Amazon.S3;
using System.Collections.Specialized;
using System.Configuration;

namespace AWSome.Bucket.Config
{
    public class Preferences
    {
        public static NameValueCollection appSettings = ConfigurationManager.AppSettings;
        public static AWSCredentials UserAWSCredentials { get; set; }
        public static RegionEndpoint RegionEndpoint { get; set; }

        public static string ProfileName { get; set; }
        public static string ProfileLocation { get; set; }

        /// <summary>
        /// Sets the Preferences based on the configuration file
        /// </summary>
        public Preferences()
        {
        }


        // <summary>
        // Gets the DynamoDB Client set in the App.config file
        // </summary>
        // <returns>The Dynamo DB Client as configured</returns>
        public static IAmazonS3 GetAmazonS3Client()
        {
            UserAWSCredentials = GetAWSCredentials();
            return new AmazonS3Client(UserAWSCredentials, RegionEndpoint);
        }

        /// <summary>
        /// Gets the credentials from the credential store chane
        /// </summary>
        /// <returns>An object representing the credentials</returns>
        public static AWSCredentials GetAWSCredentials()
        {
            CredentialProfileStoreChain profileStoreChain = new CredentialProfileStoreChain(ProfileLocation);
            if (!profileStoreChain.TryGetAWSCredentials(ProfileName, out AWSCredentials awsCredentials))
            {
                return null;
            }
            return awsCredentials;
        }
    }
}
