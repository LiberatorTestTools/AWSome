using Amazon;
using Amazon.ECS;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using System.Collections.Specialized;
using System.Configuration;


namespace AWSome.ECStraordinary.Config
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

        /// Gets the ECS Client set in the App.config file
        /// </summary>
        /// <returns>The ECS Client as configured</returns>
        public static AmazonECSClient GetECSClient()
        {
            UserAWSCredentials = GetAWSCredentials();
            return new AmazonECSClient(UserAWSCredentials, RegionEndpoint);
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



        /// <summary>
        /// Gets the ECS configuration
        /// </summary>
        /// <returns>The ECS configuration</returns>
        public static AmazonECSConfig GetECSConfig()
        {
            return new AmazonECSConfig()
            {
                RegionEndpoint = RegionEndpoint
            };
        }
    }
}
