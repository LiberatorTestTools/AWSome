using Amazon.DataPipeline;
using Amazon.DataPipeline.Model;
using Amazon.Runtime;
using AWSome.Piping.Config;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace AWSome.Piping.Client
{
    public class DataPipeline
    {
        public AmazonDataPipelineClient PipelineClient { get; set; }

        public AWSCredentials UserAWSCredentials = Preferences.UserAWSCredentials;

        public AmazonDataPipelineConfig PipelineConfig = Preferences.PipelineConfig;

        public int PipelinePollingDelay = Preferences.PipelinePollingDelay;

        public int PipelineMaxTime = Preferences.PipelineMaxTime;

        public string PipelineId = Preferences.PipelineId;

        public DataPipeline(AmazonDataPipelineConfig pipelineConfig)
        {

        }

        public AmazonWebServiceResponse ActivatePipeline(ActivatePipelineRequest activatePipelineRequest, [Optional]List<Tag> tags)
        {

            using (AmazonDataPipelineClient amazonDataPipelineClient = new AmazonDataPipelineClient(UserAWSCredentials, PipelineConfig))
            {
                if (tags != null)
                {
                    amazonDataPipelineClient.AddTags(activatePipelineRequest.PipelineId, tags);
                }
                return amazonDataPipelineClient.ActivatePipeline(activatePipelineRequest);
            };
        }

        public AmazonWebServiceResponse ActivatePipelineAsync(ActivatePipelineRequest activatePipelineRequest, [Optional]List<Tag> tags)
        {
            using (AmazonDataPipelineClient amazonDataPipelineClient = new AmazonDataPipelineClient(UserAWSCredentials, PipelineConfig))
            {

                DateTime dateTime = DateTime.Now;
                if (tags != null)
                {
                    Task<AddTagsResponse> addTagsResponse = amazonDataPipelineClient.AddTagsAsync(activatePipelineRequest.PipelineId, tags);
                    addTagsResponse.Wait();
                }

                Task<ActivatePipelineResponse> activatePipelineResponse = amazonDataPipelineClient.ActivatePipelineAsync(activatePipelineRequest);

                activatePipelineResponse.Wait();

                /*
                QueryObjectsRequest queryObjectsRequest = new QueryObjectsRequest()
                {
                    PipelineId = PipelineId,
                    Sphere = "INSTANCE"
                };

                QueryObjectsResponse queryObjectsResponse = amazonDataPipelineClient.QueryObjects(queryObjectsRequest);

                var taskId = queryObjectsResponse.Ids[0];

                ReportTaskProgressRequest reportTaskProgressRequest = new ReportTaskProgressRequest()
                {
                    TaskId = taskId
                };

                ReportTaskProgressResponse reportTaskProgressResponse = amazonDataPipelineClient.ReportTaskProgress(reportTaskProgressRequest);

                while (DateTime.Now <= dateTime.AddMinutes(PipelineMaxTime))
                {
                    activatePipelineResponse.Wait(PipelinePollingDelay);
                }
                */

                //TODO: Replace thread.sleep with a poll for the instance status "FINISHED"
                Thread.Sleep(540000);

                return activatePipelineResponse.Result;
            };
        }

        public AmazonWebServiceResponse CreatePipeline(CreatePipelineRequest createPipelineRequest, [Optional]List<Tag> tags)
        {
            using (AmazonDataPipelineClient amazonDataPipelineClient = new AmazonDataPipelineClient(Preferences.UserAWSCredentials, Preferences.PipelineConfig))
            {
                createPipelineRequest.Tags = tags;
                return amazonDataPipelineClient.CreatePipeline(createPipelineRequest);
            };
        }

        public AmazonWebServiceResponse CreatePipelineAsync(CreatePipelineRequest createPipelineRequest, [Optional]List<Tag> tags)
        {
            using (AmazonDataPipelineClient amazonDataPipelineClient = new AmazonDataPipelineClient(Preferences.UserAWSCredentials, Preferences.PipelineConfig))
            {
                createPipelineRequest.Tags = tags ?? null;
                Task<CreatePipelineResponse> createPipelineResponse = amazonDataPipelineClient.CreatePipelineAsync(createPipelineRequest);
                createPipelineResponse.Wait();
                return createPipelineResponse.Result;
            };
        }

        public AmazonWebServiceResponse DeactivatePipeline(DeactivatePipelineRequest deactivatePipelineRequest)
        {
            using (AmazonDataPipelineClient amazonDataPipelineClient = new AmazonDataPipelineClient(Preferences.UserAWSCredentials, Preferences.PipelineConfig))
            {
                return amazonDataPipelineClient.DeactivatePipeline(deactivatePipelineRequest);
            };
        }

        public AmazonWebServiceResponse DeactivatePipelineAsync(DeactivatePipelineRequest deactivatePipelineRequest)
        {
            using (AmazonDataPipelineClient amazonDataPipelineClient = new AmazonDataPipelineClient(Preferences.UserAWSCredentials, Preferences.PipelineConfig))
            {
                DateTime dateTime = DateTime.Now;
                Task<DeactivatePipelineResponse> deactivatePipelineResponse = amazonDataPipelineClient.DeactivatePipelineAsync(deactivatePipelineRequest);

                while (DateTime.Now <= dateTime.AddMinutes(Preferences.PipelineMaxTime) && deactivatePipelineResponse.Status != System.Threading.Tasks.TaskStatus.RanToCompletion)
                {
                    deactivatePipelineResponse.Wait(Preferences.PipelinePollingDelay);
                }

                return deactivatePipelineResponse.Result;
            };
        }

    }
}
