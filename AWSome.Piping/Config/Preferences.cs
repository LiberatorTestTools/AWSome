using Amazon;
using Amazon.DataPipeline;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using NUnit.Framework;
using System;
using System.Configuration;
using System.IO;
using System.Reflection;

namespace Liberator.AWSome.Piping.Config
{
    public class Preferences
    {
        static Configuration configFile = ConfigurationManager.OpenExeConfiguration(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/AWSome.Piping.dll");
        static KeyValueConfigurationCollection appSettings = configFile.AppSettings.Settings;

        public static string AWSProfileName { get; set; }
        public static RegionEndpoint RegionEndpoint { get; set; }
        public static AWSCredentials UserAWSCredentials { get; set; }
        public static AmazonDataPipelineConfig PipelineConfig { get; set; }
        public static string PipelineId { get; set; }

        public string GetPipelineId()
        {
            return PipelineId;
        }

        public static int PipelinePollingDelay { get; set; }
        public static int PipelineMaxTime { get; set; }

        public AmazonDataPipelineConfig GetPipelineConfig()
        {
            return new AmazonDataPipelineConfig()
            {
                RegionEndpoint = RegionEndpoint.EUWest1
            };

        }

        public int GetPipelineMaxTime()
        {
            return PipelineMaxTime;
        }

        public int GetPipelinePollingDelay()
        {
            return PipelinePollingDelay;
        }


        /// <summary>
        /// Sets the Preferences based on the configuration file
        /// </summary>
        static Preferences()
        {
            RegionEndpoint = RegionEndpoint.GetBySystemName(appSettings["RegionEndpoint"].Value);
            AWSProfileName = appSettings["AWSProfileName"].Value;
            PipelineMaxTime = Convert.ToInt32(appSettings["PipelineMaxTime"].Value);
            PipelinePollingDelay = Convert.ToInt32(appSettings["PipelinePollingDelay"].Value);
            UserAWSCredentials = GetAWSCredentials();
            PipelineId = appSettings["PipelineId"].Value;
        }

        /// <summary>
        /// Gets the Pipeline Client set in the App.config file
        /// </summary>
        /// <returns>The Pipeline Client as configured</returns>
        public static AmazonDataPipelineClient GetPipelineClient()
        {
            return new AmazonDataPipelineClient(UserAWSCredentials, RegionEndpoint.EUWest1);
        }

        /// <summary>
        /// Gets the Pipeline Client set in the App.config file
        /// </summary>
        /// <returns>The Pipeline Client as configured</returns>
        public static AmazonDataPipelineClient GetPipelineClient(RegionEndpoint regionEndpoint)
        {
            return new AmazonDataPipelineClient(UserAWSCredentials, regionEndpoint);
        }

        /// <summary>
        /// Gets the Pipeline Client set in the App.config file
        /// </summary>
        /// <returns>The Kinesis Pipeline as configured</returns>
        public static AmazonDataPipelineClient GetPipelineClient(AmazonDataPipelineConfig amazonDataPipelineConfig)
        {
            return new AmazonDataPipelineClient(UserAWSCredentials, amazonDataPipelineConfig);
        }

        public static AWSCredentials GetAWSCredentials()
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
