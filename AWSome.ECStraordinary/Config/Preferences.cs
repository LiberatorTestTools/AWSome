using Amazon;
using Amazon.ECS;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;


namespace Liberator.AWSome.ECStraordinary.Config
{
    /// <summary>
    /// The preferences for the Elastic Container Service
    /// </summary>
    public class Preferences
    {
        /// <summary>
        /// The credentials for the current user
        /// </summary>
        public static AWSCredentials UserAWSCredentials { get; set; }

        /// <summary>
        /// The region for the Elastic Container Service
        /// </summary>
        public static RegionEndpoint RegionEndpoint { get; set; }

        /// <summary>
        /// The name for
        /// </summary>
        public static string ProfileName { get; set; }

        /// <summary>
        /// The location for the current profile
        /// </summary>
        public static string ProfileLocation { get; set; }

        /// <summary>
        /// Sets the Preferences based on the configuration file
        /// </summary>
        public Preferences()
        {
        }


        /// <summary>
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
