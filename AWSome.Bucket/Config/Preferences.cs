using Amazon;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using Amazon.S3;
using System.Collections.Specialized;
using System.Configuration;

namespace Liberator.AWSome.Bucket.Config
{
    /// <summary>
    /// 
    /// </summary>
    public class Preferences
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
        public static string ProfileName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public static string ProfileLocation { get; set; }

        /// <summary>
        /// Sets the Preferences based on the configuration file
        /// </summary>
        public Preferences()
        {
        }


        /// <summary>
        /// Gets the Amazon S3 Client set in the App.config file
        /// </summary>
        /// <returns>The Amazon S3 Client as configured</returns>
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
