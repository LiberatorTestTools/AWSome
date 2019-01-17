using Amazon.DataPipeline;
using Amazon.DataPipeline.Model;
using Amazon.Runtime;
using AWSome.Piping.Config;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace AWSome.Piping.Client
{
    /// <summary>
    /// The client class for handling Amazon Data Pipelines
    /// </summary>
    public class DataPipeline
    {
        /// <summary>
        /// The Amazon Data Pipeline Client being used within the client
        /// </summary>
        public AmazonDataPipelineClient PipelineClient { get; set; }

        /// <summary>
        /// The credentials being used for communication with the server
        /// </summary>
        public AWSCredentials UserAWSCredentials { get; set; }

        /// <summary>
        /// The Data Pipeline configuration to use when communicating with the server
        /// </summary>
        public AmazonDataPipelineConfig PipelineConfig { get; set; }

        /// <summary>
        /// The delay between polling events
        /// </summary>
        public int PipelinePollingDelay { get; set; }

        /// <summary>
        /// The maximum time to keep the connection to the pipeline open
        /// </summary>
        public int PipelineMaxTime { get; set; }

        /// <summary>
        /// The Id for the pipeline being used
        /// </summary>
        public string PipelineId { get; set; }

        /// <summary>
        /// Parameterless constructor for the Data Pipeline client
        /// </summary>
        public DataPipeline()
        {
            UserAWSCredentials = Preferences.UserAWSCredentials;
            PipelineConfig = Preferences.PipelineConfig;
            PipelinePollingDelay = Preferences.PipelinePollingDelay;
            PipelineMaxTime = Preferences.PipelineMaxTime;
            PipelineId = Preferences.PipelineId;
        }


        /// <summary>
        /// Creates a new Data Pipeline with specific configuration parameters.
        /// </summary>
        /// <param name="pipelineConfig">The configuration settings for the Pipeline</param>
        public DataPipeline(AmazonDataPipelineConfig pipelineConfig)
        {
            UserAWSCredentials = Preferences.UserAWSCredentials;
            PipelineConfig = pipelineConfig;
            PipelinePollingDelay = Preferences.PipelinePollingDelay;
            PipelineMaxTime = Preferences.PipelineMaxTime;
            PipelineId = Preferences.PipelineId;
        }


        /// <summary>
        /// Creates a new Data Pipeline with a given name and specific configuration parameters.
        /// </summary>
        /// <param name="pipelineConfig">The configuration settings for the Pipeline</param>
        /// <param name="pipelineId">The Id of the pipeline used in the client</param>
        public DataPipeline(AmazonDataPipelineConfig pipelineConfig, string pipelineId)
        {
            UserAWSCredentials = Preferences.UserAWSCredentials;
            PipelineConfig = pipelineConfig;
            PipelinePollingDelay = Preferences.PipelinePollingDelay;
            PipelineMaxTime = Preferences.PipelineMaxTime;
            PipelineId = pipelineId;
        }


        /// <summary>
        /// Activates a data pipeline
        /// </summary>
        /// <param name="activatePipelineRequest">The request object detailing the pipeline to activate</param>
        /// <param name="tags">[Optional] Any tags to add to the pipeline</param>
        /// <returns>The response returned from the server</returns>
        public AmazonWebServiceResponse ActivatePipeline(ActivatePipelineRequest activatePipelineRequest, [Optional]List<Tag> tags)
        {
            try
            {
                using (AmazonDataPipelineClient amazonDataPipelineClient = Preferences.GetPipelineClient())
                {
                    if (tags != null)
                    {
                        amazonDataPipelineClient.AddTags(activatePipelineRequest.PipelineId, tags);
                    }
                    return amazonDataPipelineClient.ActivatePipeline(activatePipelineRequest);
                };
            }
            catch (AmazonDataPipelineException e)
            {
                throw new PipingException("Unable to activate the pipeline.", e);
            }
        }


        /// <summary>
        /// Activates a data pipeline
        /// </summary>
        /// <param name="pipelineId">The Id of the pipeline to be used</param>
        /// <param name="tags">[Optional] Any tags to add to the pipeline</param>
        /// <param name="parameterValues">[Optional] Parameters to pass to the pipeline on activation</param>
        /// <returns>The response returned from the server</returns>
        public AmazonWebServiceResponse ActivatePipeline(string pipelineId, [Optional, DefaultParameterValue(null)]List<Tag> tags,
            [Optional, DefaultParameterValue(null)]List<Amazon.DataPipeline.Model.ParameterValue> parameterValues)
        {
            try
            {
                ActivatePipelineRequest activatePipelineRequest = null;

                using (AmazonDataPipelineClient amazonDataPipelineClient = Preferences.GetPipelineClient())
                {
                    if (tags != null)
                    {
                        amazonDataPipelineClient.AddTags(pipelineId, tags);
                    }

                    activatePipelineRequest = PopulateActivateRequest(pipelineId, parameterValues);

                    return amazonDataPipelineClient.ActivatePipeline(activatePipelineRequest);
                };
            }
            catch (AmazonDataPipelineException e)
            {
                throw new PipingException("Unable to activate the pipeline.", e);
            }
        }


        /// <summary>
        /// Asynchronously activates a data pipeline
        /// </summary>
        /// <param name="activatePipelineRequest">The request object detailing the pipeline to activate</param>
        /// <param name="tags">[Optional] Any tags to add to the pipeline</param>
        /// <returns>The response returned from the server</returns>
        public AmazonWebServiceResponse ActivatePipelineAsync(ActivatePipelineRequest activatePipelineRequest, [Optional]List<Tag> tags)
        {
            try
            {
                Task<ActivatePipelineResponse> activatePipelineResponse = null;

                using (AmazonDataPipelineClient amazonDataPipelineClient = Preferences.GetPipelineClient())
                {
                    if (tags != null)
                    {
                        Task<AddTagsResponse> addTagsResponse = amazonDataPipelineClient.AddTagsAsync(activatePipelineRequest.PipelineId, tags);
                        addTagsResponse.Wait();
                    }

                    activatePipelineResponse = amazonDataPipelineClient.ActivatePipelineAsync(activatePipelineRequest);
                    activatePipelineResponse.Wait();
                    return activatePipelineResponse.Result;
                };
            }
            catch (AmazonDataPipelineException e)
            {
                throw new PipingException("Unable to activate the requested pipeline.", e);
            }
        }


        /// <summary>
        /// Asynchronously activates a data pipeline
        /// </summary>
        /// <param name="pipelineId"></param>
        /// <param name="tags">[Optional] Any tags to add to the pipeline</param>
        /// <param name="parameterValues">[Optional] Parameters to pass to the pipeline on activation</param>
        /// <returns>The response returned from the server</returns>
        public AmazonWebServiceResponse ActivatePipelineAsync(string pipelineId, [Optional, DefaultParameterValue(null)]List<Tag> tags,
            [Optional, DefaultParameterValue(null)]List<Amazon.DataPipeline.Model.ParameterValue> parameterValues)
        {
            try
            {
                ActivatePipelineRequest activatePipelineRequest = null;
                Task<ActivatePipelineResponse> activatePipelineResponse = null;

                using (AmazonDataPipelineClient amazonDataPipelineClient = Preferences.GetPipelineClient())
                {
                    if (tags != null)
                    {
                        Task<AddTagsResponse> addTagsResponse = amazonDataPipelineClient.AddTagsAsync(pipelineId, tags);
                        addTagsResponse.Wait();
                    }

                    activatePipelineRequest = PopulateActivateRequest(pipelineId, parameterValues);
                    activatePipelineResponse = amazonDataPipelineClient.ActivatePipelineAsync(activatePipelineRequest);
                    activatePipelineResponse.Wait();
                    return activatePipelineResponse.Result;
                };
            }
            catch (AmazonDataPipelineException e)
            {
                throw new PipingException("Unable to activate the requested pipeline.", e);
            }
        }


        /// <summary>
        /// Creates a data pipeline
        /// </summary>
        /// <param name="createPipelineRequest">The request object detailing the pipeline to create</param>
        /// <param name="tags">Any tags to add to the pipeline</param>
        /// <returns>The response returned from the server</returns>
        public AmazonWebServiceResponse CreatePipeline(CreatePipelineRequest createPipelineRequest, [Optional, DefaultParameterValue(null)]List<Tag> tags)
        {
            try
            {
                using (AmazonDataPipelineClient amazonDataPipelineClient = Preferences.GetPipelineClient())
                {
                    if (tags != null)
                    {
                        createPipelineRequest.Tags = tags;
                    }
                    return amazonDataPipelineClient.CreatePipeline(createPipelineRequest);
                };
            }
            catch (AmazonDataPipelineException e)
            {
                throw new PipingException("Unable to create the requested pipeline.", e);
            }
        }


        /// <summary>
        /// Creates a data pipeline
        /// </summary>
        /// <param name="uniqueId">Unique identifier set by the user</param>
        /// <param name="name">The name of the pipeline</param>
        /// <param name="description">A description of the pipeline</param>
        /// <param name="tags">[Optional] Any tags to add to the pipeline</param>
        /// <returns>The response returned from the server</returns>
        public AmazonWebServiceResponse CreatePipeline(string uniqueId, string name, string description, [Optional]List<Tag> tags)
        {
            try
            {
                CreatePipelineRequest createPipelineRequest = null;

                using (AmazonDataPipelineClient amazonDataPipelineClient = Preferences.GetPipelineClient())
                {
                    createPipelineRequest = PopulateCreateRequest(uniqueId, name, description, tags);
                    return amazonDataPipelineClient.CreatePipeline(createPipelineRequest);
                };
            }
            catch (AmazonDataPipelineException e)
            {
                throw new PipingException("Unable to create the requested pipeline.", e);
            }
        }


        /// <summary>
        /// Asynchronously creates a data pipeline
        /// </summary>
        /// <param name="createPipelineRequest">The request object detailing the pipeline to create</param>
        /// <param name="tags">Any tags to add to the pipeline</param>
        /// <returns>The response returned from the server</returns>
        public AmazonWebServiceResponse CreatePipelineAsync(CreatePipelineRequest createPipelineRequest, [Optional]List<Tag> tags)
        {
            try
            {
                using (AmazonDataPipelineClient amazonDataPipelineClient = Preferences.GetPipelineClient())
                {
                    createPipelineRequest.Tags = tags ?? null;
                    Task<CreatePipelineResponse> createPipelineResponse = amazonDataPipelineClient.CreatePipelineAsync(createPipelineRequest);
                    createPipelineResponse.Wait();
                    return createPipelineResponse.Result;
                };
            }
            catch (AmazonDataPipelineException e)
            {
                throw new PipingException("Unable to create the requested pipeline.", e);
            }
        }

        /// <summary>
        /// Asynchronously creates a data pipeline
        /// </summary>
        /// <param name="uniqueId">Unique identifier set by the user</param>
        /// <param name="name">The name of the pipeline</param>
        /// <param name="description">A description of the pipeline</param>
        /// <param name="tags">[Optional] Any tags to add to the pipeline</param>
        /// <returns></returns>
        public AmazonWebServiceResponse CreatePipelineAsync(string uniqueId, string name, string description, [Optional]List<Tag> tags)
        {
            try
            {
                CreatePipelineRequest createPipelineRequest = PopulateCreateRequest(uniqueId, name, description, tags);

                using (AmazonDataPipelineClient amazonDataPipelineClient = Preferences.GetPipelineClient())
                {
                    Task<CreatePipelineResponse> createPipelineResponse = amazonDataPipelineClient.CreatePipelineAsync(createPipelineRequest);
                    createPipelineResponse.Wait();
                    return createPipelineResponse.Result;
                };
            }
            catch (AmazonDataPipelineException e)
            {
                throw new PipingException("Unable to create the requested pipeline.", e);
            }
        }


        /// <summary>
        /// Deactivates a data pipeline
        /// </summary>
        /// <param name="deactivatePipelineRequest">The request object detailing the pipeline to deactivate</param>
        /// <returns>The response returned from the server</returns>
        public AmazonWebServiceResponse DeactivatePipeline(DeactivatePipelineRequest deactivatePipelineRequest)
        {
            try
            {
                using (AmazonDataPipelineClient amazonDataPipelineClient = Preferences.GetPipelineClient())
                {
                    return amazonDataPipelineClient.DeactivatePipeline(deactivatePipelineRequest);
                };
            }
            catch (AmazonDataPipelineException e)
            {
                throw new PipingException("Unable to deactivate the requested pipeline.", e);
            }
        }


        /// <summary>
        /// Deactivates a data pipeline
        /// </summary>
        /// <param name="pipelineId">The Id of the pipeline to be used</param>
        /// <param name="cancelActive">Whether to cancel any currently running objects</param>
        /// <returns>The response returned from the server</returns>
        public AmazonWebServiceResponse DeactivatePipeline(string pipelineId, bool cancelActive)
        {
            try
            {
                DeactivatePipelineRequest deactivatePipelineRequest = PopulateDeactivateRequest(pipelineId, cancelActive);

                using (AmazonDataPipelineClient amazonDataPipelineClient = Preferences.GetPipelineClient())
                {
                    return amazonDataPipelineClient.DeactivatePipeline(deactivatePipelineRequest);
                };
            }
            catch (AmazonDataPipelineException e)
            {
                throw new PipingException("Unable to deactivate the requested pipeline.", e);
            }
        }


        /// <summary>
        /// Asynchronously deactivates a data pipeline
        /// </summary>
        /// <param name="deactivatePipelineRequest">The request object detailing the pipeline to deactivate</param>
        /// <returns>The response returned from the server</returns>
        public AmazonWebServiceResponse DeactivatePipelineAsync(DeactivatePipelineRequest deactivatePipelineRequest)
        {
            try
            {
                using (AmazonDataPipelineClient amazonDataPipelineClient = Preferences.GetPipelineClient())
                {
                    Task<DeactivatePipelineResponse> deactivatePipelineResponse = amazonDataPipelineClient.DeactivatePipelineAsync(deactivatePipelineRequest);
                    deactivatePipelineResponse.Wait();
                    return deactivatePipelineResponse.Result;
                };
            }
            catch (AmazonDataPipelineException e)
            {
                throw new PipingException("Unable to deactivate the requested pipeline.", e);
            }
        }


        /// <summary>
        /// Asynchronously deactivates a data pipeline
        /// </summary>
        /// <param name="pipelineId">The Id of the pipeline to be used</param>
        /// <param name="cancelActive">Whether to cancel any currently running objects</param>
        /// <returns>The response returned from the server</returns>
        public AmazonWebServiceResponse DeactivatePipelineAsync(string pipelineId, bool cancelActive)
        {
            try
            {
                using (AmazonDataPipelineClient amazonDataPipelineClient = Preferences.GetPipelineClient())
                {
                    DeactivatePipelineRequest deactivatePipelineRequest = PopulateDeactivateRequest(pipelineId, cancelActive);
                    Task<DeactivatePipelineResponse> deactivatePipelineResponse = amazonDataPipelineClient.DeactivatePipelineAsync(deactivatePipelineRequest);
                    deactivatePipelineResponse.Wait();
                    return deactivatePipelineResponse.Result;
                };
            }
            catch (AmazonDataPipelineException e)
            {
                throw new PipingException("Unable to deactivate the requested pipeline.", e);
            }
        }


        /// <summary>
        /// Deletes a data pipeline.
        /// </summary>
        /// <param name="deletePipelineRequest">The request object detailing the pipeline to delete.</param>
        /// <returns>The response returned from the server.</returns>
        public AmazonWebServiceResponse DeletePipeline(DeletePipelineRequest deletePipelineRequest)
        {
            try
            {
                using (AmazonDataPipelineClient amazonDataPipelineClient = Preferences.GetPipelineClient())
                {
                    return amazonDataPipelineClient.DeletePipeline(deletePipelineRequest);
                };
            }
            catch (AmazonDataPipelineException e)
            {

                throw new PipingException("Unable to delete the requested pipeline.", e);
            }
        }


        /// <summary>
        /// Deletes a data pipeline.
        /// </summary>
        /// <param name="pipelineId">The Id of the pipeline to delete.</param>
        /// <returns>The response returned from the server.</returns>
        public AmazonWebServiceResponse DeletePipeline(string pipelineId)
        {
            try
            {
                using (AmazonDataPipelineClient amazonDataPipelineClient = Preferences.GetPipelineClient())
                {
                    DeletePipelineRequest deletePipelineRequest = PopulateDeleteRequest(pipelineId);
                    return amazonDataPipelineClient.DeletePipeline(deletePipelineRequest);
                };
            }
            catch (AmazonDataPipelineException e)
            {

                throw new PipingException("Unable to delete the requested pipeline.", e);
            }
        }


        /// <summary>
        /// Asynchronously deletes a data pipeline.
        /// </summary>
        /// <param name="deletePipelineRequest">The request object detailing the pipeline to delete.</param>
        /// <returns>The response returned from the server.</returns>
        public AmazonWebServiceResponse DeletePipelineAsync(DeletePipelineRequest deletePipelineRequest)
        {
            try
            {
                using (AmazonDataPipelineClient amazonDataPipelineClient = Preferences.GetPipelineClient())
                {
                    Task<DeletePipelineResponse> deletePipelineResponse = amazonDataPipelineClient.DeletePipelineAsync(deletePipelineRequest);
                    deletePipelineResponse.Wait();
                    return deletePipelineResponse.Result;
                };
            }
            catch (AmazonDataPipelineException e)
            {
                throw new PipingException("Unable to delete the requested pipeline.", e);
            }
        }


        /// <summary>
        /// Asynchronously deletes a data pipeline.
        /// </summary>
        /// <param name="pipelineId">The Id of the pipeline to delete.</param>
        /// <returns>The response returned from the server.</returns>
        public AmazonWebServiceResponse DeletePipelineAsync(string pipelineId)
        {
            try
            {
                using (AmazonDataPipelineClient amazonDataPipelineClient = Preferences.GetPipelineClient())
                {
                    DeletePipelineRequest deletePipelineRequest = PopulateDeleteRequest(pipelineId);
                    Task<DeletePipelineResponse> deletePipelineResponse = amazonDataPipelineClient.DeletePipelineAsync(deletePipelineRequest);
                    deletePipelineResponse.Wait();
                    return deletePipelineResponse.Result;
                };
            }
            catch (AmazonDataPipelineException e)
            {
                throw new PipingException("Unable to delete the requested pipeline.", e);
            }
        }


        /// <summary>
        /// Gets the object definitions for objects found using the request.
        /// </summary>
        /// <param name="describeObjectsRequest">The request detailing the objects associated with a pipeline to describe.</param>
        /// <returns>The response returned from the server.</returns>
        public AmazonWebServiceResponse DescribeObjects(DescribeObjectsRequest describeObjectsRequest)
        {
            try
            {
                using (AmazonDataPipelineClient amazonDataPipelineClient = Preferences.GetPipelineClient())
                {
                    return amazonDataPipelineClient.DescribeObjects(describeObjectsRequest);
                };
            }
            catch (AmazonDataPipelineException e)
            {
                throw new PipingException("Unable to describe the requested objects on the pipeline.", e);
                throw;
            }
        }


        /// <summary>
        /// Gets the object definitions for objects found using the request.
        /// </summary>
        /// <param name="pipelineId">The Id of the pipeline to use in the request.</param>
        /// <param name="objectIds">The list of object Ids to find</param>
        /// <param name="evaluateExpressions">Whether to evaluate any expressions on the objects</param>
        /// <returns>The response returned from the server.</returns>
        public AmazonWebServiceResponse DescribeObjects(string pipelineId, List<string> objectIds, [DefaultParameterValue(true)]bool evaluateExpressions)
        {
            try
            {
                DescribeObjectsRequest describeObjectsRequest = PopulateDescribeObjectsRequest(pipelineId, objectIds, evaluateExpressions);

                using (AmazonDataPipelineClient amazonDataPipelineClient = Preferences.GetPipelineClient())
                {
                    return amazonDataPipelineClient.DescribeObjects(describeObjectsRequest);
                };
            }
            catch (AmazonDataPipelineException e)
            {
                throw new PipingException("Unable to describe the requested objects on the pipeline.", e);
                throw;
            }
        }


        /// <summary>
        /// Asynchronously gets the object definitions for objects found using the request.
        /// </summary>
        /// <param name="describeObjectsRequest">The request detailing the objects associated with a pipeline to describe.</param>
        /// <returns>The response returned from the server.</returns>
        public AmazonWebServiceResponse DescribeObjectsAsync(DescribeObjectsRequest describeObjectsRequest)
        {
            try
            {
                using (AmazonDataPipelineClient amazonDataPipelineClient = Preferences.GetPipelineClient())
                {
                    Task<DescribeObjectsResponse> describeObjectsResponse = amazonDataPipelineClient.DescribeObjectsAsync(describeObjectsRequest);
                    describeObjectsResponse.Wait();
                    return describeObjectsResponse.Result;
                };
            }
            catch (AmazonDataPipelineException e)
            {
                throw new PipingException("Unable to describe the requested objects on the pipeline.", e);
            }
        }


        /// <summary>
        /// Asynchronously gets the object definitions for objects found using the request.
        /// </summary>
        /// <param name="pipelineId">The Id of the pipeline to use in the request.</param>
        /// <param name="objectIds">The list of object Ids to find</param>
        /// <param name="evaluateExpressions">Whether to evaluate any expressions on the objects</param>
        /// <returns>The response returned from the server.</returns>
        public AmazonWebServiceResponse DescribeObjectsAsync(string pipelineId, List<string> objectIds, [DefaultParameterValue(true)]bool evaluateExpressions)
        {
            try
            {
                DescribeObjectsRequest describeObjectsRequest = PopulateDescribeObjectsRequest(pipelineId, objectIds, evaluateExpressions);

                using (AmazonDataPipelineClient amazonDataPipelineClient = Preferences.GetPipelineClient())
                {
                    Task<DescribeObjectsResponse> describeObjectsResponse = amazonDataPipelineClient.DescribeObjectsAsync(describeObjectsRequest);
                    describeObjectsResponse.Wait();
                    return describeObjectsResponse.Result;
                };
            }
            catch (AmazonDataPipelineException e)
            {
                throw new PipingException("Unable to describe the requested objects on the pipeline.", e);
            }
        }


        /// <summary>
        /// Gets the details of one or more pipelines.
        /// </summary>
        /// <param name="describePipelinesRequest">The request object detailing the pipeline to describe.</param>
        /// <returns>The response returned from the server.</returns>
        public AmazonWebServiceResponse DescribePipelines(DescribePipelinesRequest describePipelinesRequest)
        {
            try
            {
                using (AmazonDataPipelineClient amazonDataPipelineClient = Preferences.GetPipelineClient())
                {
                    return amazonDataPipelineClient.DescribePipelines(describePipelinesRequest);
                };
            }
            catch (AmazonDataPipelineException e)
            {
                throw new PipingException("Unable to describe the requested pipeline.", e);
            }
        }


        /// <summary>
        /// Gets the details of one or more pipelines.
        /// </summary>
        /// <param name="pipelineIds">The Id of the pipeline to use in the request.</param>
        /// <returns>The response returned from the server.</returns>
        public AmazonWebServiceResponse DescribePipelines(List<string> pipelineIds)
        {
            try
            {
                DescribePipelinesRequest describePipelinesRequest = PopulateDescribePipelinesRequest(pipelineIds);

                using (AmazonDataPipelineClient amazonDataPipelineClient = Preferences.GetPipelineClient())
                {
                    return amazonDataPipelineClient.DescribePipelines(describePipelinesRequest);
                };
            }
            catch (AmazonDataPipelineException e)
            {
                throw new PipingException("Unable to describe the requested pipeline.", e);
            }
        }


        /// <summary>
        /// Asynchronously gets the details of one or more pipelines.
        /// </summary>
        /// <param name="describePipelinesRequest">The request object detailing the pipeline to describe.</param>
        /// <returns>The response returned from the server.</returns>
        public AmazonWebServiceResponse DescribePipelinesAsync(DescribePipelinesRequest describePipelinesRequest)
        {
            try
            {
                using (AmazonDataPipelineClient amazonDataPipelineClient = Preferences.GetPipelineClient())
                {
                    Task<DescribePipelinesResponse> describePipelinesResponse = amazonDataPipelineClient.DescribePipelinesAsync(describePipelinesRequest);
                    describePipelinesResponse.Wait();
                    return describePipelinesResponse.Result;
                };
            }
            catch (AmazonDataPipelineException e)
            {

                throw new PipingException("Unable to describe the requested pipeline.", e);
            }
        }


        /// <summary>
        /// Asynchronously gets the details of one or more pipelines.
        /// </summary>
        /// <param name="pipelineIds">The Id of the pipeline to use in the request.</param>
        /// <returns>The response returned from the server.</returns>
        public AmazonWebServiceResponse DescribePipelinesAsync(List<string> pipelineIds)
        {
            try
            {
                DescribePipelinesRequest describePipelinesRequest = PopulateDescribePipelinesRequest(pipelineIds);

                using (AmazonDataPipelineClient amazonDataPipelineClient = Preferences.GetPipelineClient())
                {
                    Task<DescribePipelinesResponse> describePipelinesResponse = amazonDataPipelineClient.DescribePipelinesAsync(describePipelinesRequest);
                    describePipelinesResponse.Wait();
                    return describePipelinesResponse.Result;
                };
            }
            catch (AmazonDataPipelineException e)
            {

                throw new PipingException("Unable to describe the requested pipeline.", e);
            }
        }


        /// <summary>
        /// Evaluates a string in the context of the specified object.
        /// </summary>
        /// <param name="evaluateExpressionRequest">The request object detailing the expression to evaluate.</param>
        /// <returns>The response returned from the server.</returns>
        public AmazonWebServiceResponse EvaluateExpression(EvaluateExpressionRequest evaluateExpressionRequest)
        {
            try
            {
                using (AmazonDataPipelineClient amazonDataPipelineClient = Preferences.GetPipelineClient())
                {
                    return amazonDataPipelineClient.EvaluateExpression(evaluateExpressionRequest);
                };
            }
            catch (AmazonDataPipelineException e)
            {
                throw new PipingException("Unable to evaluate the expression in the context of the specified object.", e);
            }
        }


        /// <summary>
        /// Evaluates a string in the context of the specified object.
        /// </summary>
        /// <param name="pipelineId">The Id of the pipeline to use in the request.</param>
        /// <param name="objectId">The Id of the object to use in the request.</param>
        /// <param name="expression">The expression to evaluate</param>
        /// <returns>The response returned from the server.</returns>
        public AmazonWebServiceResponse EvaluateExpression(string pipelineId, string objectId, string expression)
        {
            try
            {
                EvaluateExpressionRequest evaluateExpressionRequest = PopulateExpressionRequest(pipelineId, objectId, expression);

                using (AmazonDataPipelineClient amazonDataPipelineClient = Preferences.GetPipelineClient())
                {
                    return amazonDataPipelineClient.EvaluateExpression(evaluateExpressionRequest);
                };
            }
            catch (AmazonDataPipelineException e)
            {
                throw new PipingException("Unable to evaluate the expression in the context of the specified object.", e);
            }
        }


        /// <summary>
        /// Asynchronously gevaluates a string in the context of the specified object
        /// </summary>
        /// <param name="evaluateExpressionRequest">The request object detailing the expression to evaluate.</param>
        /// <returns>The response returned from the server.</returns>
        public AmazonWebServiceResponse EvaluateExpressionAsync(EvaluateExpressionRequest evaluateExpressionRequest)
        {
            try
            {
                using (AmazonDataPipelineClient amazonDataPipelineClient = Preferences.GetPipelineClient())
                {
                    Task<EvaluateExpressionResponse> evaluateExpressionResponse = amazonDataPipelineClient.EvaluateExpressionAsync(evaluateExpressionRequest);
                    evaluateExpressionResponse.Wait();
                    return evaluateExpressionResponse.Result;
                };
            }
            catch (AmazonDataPipelineException e)
            {
                throw new PipingException("Unable to evaluate the expression in the context of the specified object.", e);
            }
        }


        /// <summary>
        /// Asynchronously gevaluates a string in the context of the specified object
        /// </summary>
        /// <param name="pipelineId">The Id of the pipeline to use in the request.</param>
        /// <param name="objectId">The Id of the object to use in the request.</param>
        /// <param name="expression">The expression to evaluate</param>
        /// <returns>The response returned from the server.</returns>
        public AmazonWebServiceResponse EvaluateExpressionAsync(string pipelineId, string objectId, string expression)
        {
            try
            {
                EvaluateExpressionRequest evaluateExpressionRequest = PopulateExpressionRequest(pipelineId, objectId, expression);

                using (AmazonDataPipelineClient amazonDataPipelineClient = Preferences.GetPipelineClient())
                {
                    Task<EvaluateExpressionResponse> evaluateExpressionResponse = amazonDataPipelineClient.EvaluateExpressionAsync(evaluateExpressionRequest);
                    evaluateExpressionResponse.Wait();
                    return evaluateExpressionResponse.Result;
                };
            }
            catch (AmazonDataPipelineException e)
            {
                throw new PipingException("Unable to evaluate the expression in the context of the specified object.", e);
            }
        }


        /// <summary>
        /// Gets the definition of the specified pipeline
        /// </summary>
        /// <param name="getPipelineDefinitionRequest">The request object detailing the pipeline to describe.</param>
        /// <returns>The response returned from the server.</returns>
        public AmazonWebServiceResponse GetPipelineDefinition(GetPipelineDefinitionRequest getPipelineDefinitionRequest)
        {
            try
            {
                using (AmazonDataPipelineClient amazonDataPipelineClient = Preferences.GetPipelineClient())
                {
                    return amazonDataPipelineClient.GetPipelineDefinition(getPipelineDefinitionRequest);
                };
            }
            catch (AmazonDataPipelineException e)
            {
                throw new PipingException("Unable to retrieve the definition of the requested pipeline", e);
            }
        }


        /// <summary>
        /// Gets the definition of the specified pipeline
        /// </summary>
        /// <param name="pipelineId">The Id of the pipeline to use in the request.</param>
        /// <param name="version">The version of the pipeline to use</param>
        /// <returns>The response returned from the server.</returns>
        public AmazonWebServiceResponse GetPipelineDefinition(string pipelineId, string version)
        {
            try
            {
                GetPipelineDefinitionRequest getPipelineDefinitionRequest = PopulateGetPipelineDefinitionRequest(pipelineId, version);

                using (AmazonDataPipelineClient amazonDataPipelineClient = Preferences.GetPipelineClient())
                {
                    return amazonDataPipelineClient.GetPipelineDefinition(getPipelineDefinitionRequest);
                };
            }
            catch (AmazonDataPipelineException e)
            {
                throw new PipingException("Unable to retrieve the definition of the requested pipeline", e);
            }
        }


        /// <summary>
        /// Asynchronously gets the definition of the specified pipeline
        /// </summary>
        /// <param name="getPipelineDefinitionRequest">The request object detailing the pipeline to describe.</param>
        /// <returns>The response returned from the server.</returns>
        public AmazonWebServiceResponse GetPipelineDefinitionAsync(GetPipelineDefinitionRequest getPipelineDefinitionRequest)
        {
            try
            {
                using (AmazonDataPipelineClient amazonDataPipelineClient = Preferences.GetPipelineClient())
                {
                    Task<GetPipelineDefinitionResponse> getPipelineDefinitionResponse = amazonDataPipelineClient.GetPipelineDefinitionAsync(getPipelineDefinitionRequest);
                    getPipelineDefinitionResponse.Wait();
                    return getPipelineDefinitionResponse.Result;
                };
            }
            catch (AmazonDataPipelineException e)
            {
                throw new PipingException("Unable to retrieve the definition of the requested pipeline.", e);
            }
        }


        /// <summary>
        /// Asynchronously gets the definition of the specified pipeline
        /// </summary>
        /// <param name="pipelineId">The Id of the pipeline to use in the request.</param>
        /// <param name="version">The version of the pipeline to use</param>
        /// <returns>The response returned from the server.</returns>
        public AmazonWebServiceResponse GetPipelineDefinitionAsync(string pipelineId, string version)
        {
            try
            {
                GetPipelineDefinitionRequest getPipelineDefinitionRequest = PopulateGetPipelineDefinitionRequest(pipelineId, version);

                using (AmazonDataPipelineClient amazonDataPipelineClient = Preferences.GetPipelineClient())
                {
                    Task<GetPipelineDefinitionResponse> getPipelineDefinitionResponse = amazonDataPipelineClient.GetPipelineDefinitionAsync(getPipelineDefinitionRequest);
                    getPipelineDefinitionResponse.Wait();
                    return getPipelineDefinitionResponse.Result;
                };
            }
            catch (AmazonDataPipelineException e)
            {
                throw new PipingException("Unable to retrieve the definition of the requested pipeline.", e);
            }
        }


        /// <summary>
        /// Gets the pipeline identifiers for all pipelines that the user has access to.
        /// </summary>
        /// <param name="listPipelinesRequest">The request object.</param>
        /// <returns>The response returned from the server.</returns>
        public AmazonWebServiceResponse ListPipelines(ListPipelinesRequest listPipelinesRequest)
        {
            try
            {
                using (AmazonDataPipelineClient amazonDataPipelineClient = Preferences.GetPipelineClient())
                {
                    return amazonDataPipelineClient.ListPipelines(listPipelinesRequest);
                };
            }
            catch (AmazonDataPipelineException e)
            {
                throw new PipingException("Unable to retrieve a list of pipelines.", e);
            }
        }


        /// <summary>
        /// Asynchronously gets the pipeline identifiers for all pipelines that the user has access to.
        /// </summary>
        /// <param name="listPipelinesRequest">The request object.</param>
        /// <returns>The response returned from the server.</returns>
        public AmazonWebServiceResponse ListPipelinesAsync(ListPipelinesRequest listPipelinesRequest)
        {
            try
            {
                using (AmazonDataPipelineClient amazonDataPipelineClient = Preferences.GetPipelineClient())
                {
                    Task<ListPipelinesResponse> listPipelinesResponse = amazonDataPipelineClient.ListPipelinesAsync(listPipelinesRequest);
                    listPipelinesResponse.Wait();
                    return listPipelinesResponse.Result;
                };
            }
            catch (AmazonDataPipelineException e)
            {
                throw new PipingException("Unable to retrieve a list of pipelines.", e);
            }
        }


        /// <summary>
        /// Adds tasks, schedules, and preconditions to the specified pipeline.
        /// </summary>
        /// <param name="putPipelineDefinitionRequest">The pipeline definition request</param>
        /// <returns>The response returned from the server.</returns>
        public AmazonWebServiceResponse PutPipelineDefinition(PutPipelineDefinitionRequest putPipelineDefinitionRequest)
        {
            try
            {
                using (AmazonDataPipelineClient amazonDataPipelineClient = Preferences.GetPipelineClient())
                {
                    return amazonDataPipelineClient.PutPipelineDefinition(putPipelineDefinitionRequest);
                };
            }
            catch (AmazonDataPipelineException e)
            {
                throw new PipingException("Unable to update the pipline definition.", e);
            }
        }


        /// <summary>
        /// Adds tasks, schedules, and preconditions to the specified pipeline.
        /// </summary>
        /// <param name="pipelineId">The Id of the pipeline.</param>
        /// <param name="pipelineObjects">The objects that define the pipeline.</param>
        /// <param name="parameterObjects">The parameter objects used with the pipeline.</param>
        /// <param name="parameterValues">The parameter values used with the pipeline.</param>
        /// <returns>The response returned from the server.</returns>
        public AmazonWebServiceResponse PutPipelineDefinition(string pipelineId, List<PipelineObject> pipelineObjects,
            List<ParameterObject> parameterObjects, List<Amazon.DataPipeline.Model.ParameterValue> parameterValues)
        {
            try
            {
                PutPipelineDefinitionRequest putPipelineDefinitionRequest = PopulatePutPipelineDefinitionRequest(pipelineId, pipelineObjects, parameterObjects, parameterValues);

                using (AmazonDataPipelineClient amazonDataPipelineClient = Preferences.GetPipelineClient())
                {
                    return amazonDataPipelineClient.PutPipelineDefinition(putPipelineDefinitionRequest);
                };
            }
            catch (AmazonDataPipelineException e)
            {
                throw new PipingException("Unable to update the pipline definition.", e);
            }
        }


        /// <summary>
        /// Asynchronously adds tasks, schedules, and preconditions to the specified pipeline.
        /// </summary>
        /// <param name="putPipelineDefinitionRequest">The pipeline definition request</param>
        /// <returns>The response returned from the server.</returns>
        public AmazonWebServiceResponse PutPipelineDefinitionAsync(PutPipelineDefinitionRequest putPipelineDefinitionRequest)
        {
            try
            {
                using (AmazonDataPipelineClient amazonDataPipelineClient = Preferences.GetPipelineClient())
                {
                    Task<PutPipelineDefinitionResponse> putPipelineDefinitionResponse = amazonDataPipelineClient.PutPipelineDefinitionAsync(putPipelineDefinitionRequest);
                    putPipelineDefinitionResponse.Wait();
                    return putPipelineDefinitionResponse.Result;
                };
            }
            catch (AmazonDataPipelineException e)
            {
                throw new PipingException("Unable to update the pipeline definition.", e);
            }
        }


        /// <summary>
        /// Asynchronously adds tasks, schedules, and preconditions to the specified pipeline.
        /// </summary>
        /// <param name="pipelineId">The Id of the pipeline.</param>
        /// <param name="pipelineObjects">The objects that define the pipeline.</param>
        /// <param name="parameterObjects">The parameter objects used with the pipeline.</param>
        /// <param name="parameterValues">The parameter values used with the pipeline.</param>
        /// <returns>The response returned from the server.</returns>
        public AmazonWebServiceResponse PutPipelineDefinitionAsync(string pipelineId, List<PipelineObject> pipelineObjects,
            List<ParameterObject> parameterObjects, List<Amazon.DataPipeline.Model.ParameterValue> parameterValues)
        {
            try
            {
                PutPipelineDefinitionRequest putPipelineDefinitionRequest = PopulatePutPipelineDefinitionRequest(pipelineId, pipelineObjects, parameterObjects, parameterValues);

                using (AmazonDataPipelineClient amazonDataPipelineClient = Preferences.GetPipelineClient())
                {
                    Task<PutPipelineDefinitionResponse> putPipelineDefinitionResponse = amazonDataPipelineClient.PutPipelineDefinitionAsync(putPipelineDefinitionRequest);
                    putPipelineDefinitionResponse.Wait();
                    return putPipelineDefinitionResponse.Result;
                };
            }
            catch (AmazonDataPipelineException e)
            {
                throw new PipingException("Unable to update the pipeline definition.", e);
            }
        }


        /// <summary>
        /// Queries the specified pipeline for the names of objects that match the specified criteria.
        /// </summary>
        /// <param name="queryObjectsRequest">The request object detailing the parameters for the query.</param>
        /// <returns>The response returned from the server.</returns>
        public AmazonWebServiceResponse QueryObjects(QueryObjectsRequest queryObjectsRequest)
        {
            try
            {
                using (AmazonDataPipelineClient amazonDataPipelineClient = Preferences.GetPipelineClient())
                {
                    return amazonDataPipelineClient.QueryObjects(queryObjectsRequest);
                };
            }
            catch (AmazonDataPipelineException e)
            {
                throw new PipingException("Unable to return query results.", e);
            }
        }


        /// <summary>
        /// Queries the specified pipeline for the names of objects that match the specified criteria.
        /// </summary>
        /// <param name="pipelineId">The Id of the pipeline.</param>
        /// <param name="sphere">Whether the query applies to components, instances or attempts</param>
        /// <param name="query">The query that defines the objects to be returned</param>
        /// <param name="limit">The maximum number of records to be returned</param>
        /// <returns>The response returned from the server.</returns>
        public AmazonWebServiceResponse QueryObjects(string pipelineId, string sphere, Query query, int limit)
        {
            try
            {
                QueryObjectsRequest queryObjectsRequest = PopulateQueryObjectsRequest(pipelineId, sphere, query, limit);

                using (AmazonDataPipelineClient amazonDataPipelineClient = Preferences.GetPipelineClient())
                {
                    return amazonDataPipelineClient.QueryObjects(queryObjectsRequest);
                };
            }
            catch (AmazonDataPipelineException e)
            {
                throw new PipingException("Unable to return query results.", e);
            }
        }


        /// <summary>
        /// Asynchronously queries the specified pipeline for the names of objects that match the specified criteria.
        /// </summary>
        /// <param name="queryObjectsRequest">The request object detailing the parameters for the query.</param>
        /// <returns>The response returned from the server.</returns>
        public AmazonWebServiceResponse QueryObjectsAsync(QueryObjectsRequest queryObjectsRequest)
        {
            try
            {
                using (AmazonDataPipelineClient amazonDataPipelineClient = Preferences.GetPipelineClient())
                {
                    Task<QueryObjectsResponse> queryObjectsResponse = amazonDataPipelineClient.QueryObjectsAsync(queryObjectsRequest);
                    queryObjectsResponse.Wait();
                    return queryObjectsResponse.Result;
                };
            }
            catch (AmazonDataPipelineException e)
            {
                throw new PipingException("Unable to return query results.", e);
            }
        }


        /// <summary>
        /// Asynchronously queries the specified pipeline for the names of objects that match the specified criteria
        /// </summary>
        /// <param name="pipelineId">The Id of the pipeline</param>
        /// <param name="sphere">Whether the query applies to components, instances or attempts</param>
        /// <param name="query">The query that defines the objects to be returned</param>
        /// <param name="limit">The maximum number of records to be returned</param>
        /// <returns>The response returned from the server.</returns>
        public AmazonWebServiceResponse QueryObjectsAsync(string pipelineId, string sphere, Query query, int limit)
        {
            try
            {
                QueryObjectsRequest queryObjectsRequest = PopulateQueryObjectsRequest(pipelineId, sphere, query, limit);

                using (AmazonDataPipelineClient amazonDataPipelineClient = Preferences.GetPipelineClient())
                {
                    Task<QueryObjectsResponse> queryObjectsResponse = amazonDataPipelineClient.QueryObjectsAsync(queryObjectsRequest);
                    queryObjectsResponse.Wait();
                    return queryObjectsResponse.Result;
                };
            }
            catch (AmazonDataPipelineException e)
            {
                throw new PipingException("Unable to return query results.", e);
            }
        }


        /// <summary>
        /// Removes existing tags from the specified pipeline.
        /// </summary>
        /// <param name="removeTagsRequest">The request object detailing the tags to remove.</param>
        /// <returns>The response returned from the server.</returns>
        public AmazonWebServiceResponse RemoveTags(RemoveTagsRequest removeTagsRequest)
        {
            try
            {
                using (AmazonDataPipelineClient amazonDataPipelineClient = Preferences.GetPipelineClient())
                {
                    return amazonDataPipelineClient.RemoveTags(removeTagsRequest);
                };
            }
            catch (AmazonDataPipelineException e)
            {
                throw new PipingException("Unable to remove the tags from the specified pipeline.", e);
            }
        }


        /// <summary>
        /// Removes existing tags from the specified pipeline.
        /// </summary>
        /// <param name="pipelineId">The Id of the pipeline</param>
        /// <param name="tagKeys">The keys for the tags to be removed</param>
        /// <returns>The response returned from the server.</returns>
        public AmazonWebServiceResponse RemoveTags(string pipelineId, List<string> tagKeys)
        {
            try
            {
                RemoveTagsRequest removeTagsRequest = PopulateRemoveTagsRequest(pipelineId, tagKeys);

                using (AmazonDataPipelineClient amazonDataPipelineClient = Preferences.GetPipelineClient())
                {
                    return amazonDataPipelineClient.RemoveTags(removeTagsRequest);
                };
            }
            catch (AmazonDataPipelineException e)
            {
                throw new PipingException("Unable to remove the tags from the specified pipeline.", e);
            }
        }


        /// <summary>
        /// Asynchronously removes existing tags from the specified pipeline.
        /// </summary>
        /// <param name="removeTagsRequest">The request object detailing the tags to remove.</param>
        /// <returns>The response returned from the server.</returns>
        public AmazonWebServiceResponse RemoveTagsAsync(RemoveTagsRequest removeTagsRequest)
        {
            try
            {
                using (AmazonDataPipelineClient amazonDataPipelineClient = Preferences.GetPipelineClient())
                {
                    Task<RemoveTagsResponse> removeTagsResponse = amazonDataPipelineClient.RemoveTagsAsync(removeTagsRequest);
                    removeTagsResponse.Wait();
                    return removeTagsResponse.Result;
                };
            }
            catch (AmazonDataPipelineException e)
            {
                throw new PipingException("Unable to remove the tags from the specified pipeline.", e);
            }
        }


        /// <summary>
        /// Asynchronously removes existing tags from the specified pipeline.
        /// </summary>
        /// <param name="pipelineId">The Id of the pipeline</param>
        /// <param name="tagKeys">The keys for the tags to be removed</param>
        /// <returns>The response returned from the server.</returns>
        public AmazonWebServiceResponse RemoveTagsAsync(string pipelineId, List<string> tagKeys)
        {
            try
            {
                RemoveTagsRequest removeTagsRequest = PopulateRemoveTagsRequest(pipelineId, tagKeys);

                using (AmazonDataPipelineClient amazonDataPipelineClient = Preferences.GetPipelineClient())
                {
                    Task<RemoveTagsResponse> removeTagsResponse = amazonDataPipelineClient.RemoveTagsAsync(removeTagsRequest);
                    removeTagsResponse.Wait();
                    return removeTagsResponse.Result;
                };
            }
            catch (AmazonDataPipelineException e)
            {
                throw new PipingException("Unable to remove the tags from the specified pipeline.", e);
            }
        }


        /// <summary>
        /// Requests that the status of a specified pipeline object is updated.
        /// </summary>
        /// <param name="setStatusRequest">The request detailing the updated status.</param>
        /// <returns>The response returned from the server.</returns>
        public AmazonWebServiceResponse SetStatus(SetStatusRequest setStatusRequest)
        {
            try
            {
                using (AmazonDataPipelineClient amazonDataPipelineClient = Preferences.GetPipelineClient())
                {
                    return amazonDataPipelineClient.SetStatus(setStatusRequest);
                };
            }
            catch (AmazonDataPipelineException e)
            {
                throw new PipingException("Unable to set the status of the pipeline object.", e);
            }
        }


        /// <summary>
        /// Requests that the status of a specified pipeline object is updated.
        /// </summary>
        /// <param name="pipelineId">The id of the pipeline that contains the objects</param>
        /// <param name="objectIds">The Id of the objects whose status is to change</param>
        /// <param name="status">The status to be used for the objects</param>
        /// <returns>The response returned from the server.</returns>
        public AmazonWebServiceResponse SetStatus(string pipelineId, List<string> objectIds, string status)
        {
            SetStatusRequest setStatusRequest = PopulateStatusRequest(pipelineId, objectIds, status);

            try
            {
                using (AmazonDataPipelineClient amazonDataPipelineClient = Preferences.GetPipelineClient())
                {
                    return amazonDataPipelineClient.SetStatus(setStatusRequest);
                };
            }
            catch (AmazonDataPipelineException e)
            {
                throw new PipingException("Unable to set the status of the pipeline object.", e);
            }
        }


        /// <summary>
        /// Asynchronously requests that the status of a specified pipeline object is updated.
        /// </summary>
        /// <param name="setStatusRequest">The request detailing the updated status.</param>
        /// <returns>The response returned from the server.</returns>
        public AmazonWebServiceResponse SetStatusAsync(SetStatusRequest setStatusRequest)
        {
            try
            {
                using (AmazonDataPipelineClient amazonDataPipelineClient = Preferences.GetPipelineClient())
                {
                    Task<SetStatusResponse> setStatusResponse = amazonDataPipelineClient.SetStatusAsync(setStatusRequest);
                    setStatusResponse.Wait();
                    return setStatusResponse.Result;
                };
            }
            catch (AmazonDataPipelineException e)
            {
                throw new PipingException("Unable to set the status of the pipeline object.", e);
            }
        }


        /// <summary>
        /// Asynchronously requests that the status of a specified pipeline object is updated.
        /// </summary>
        /// <param name="pipelineId">The id of the pipeline that contains the objects</param>
        /// <param name="objectIds">The Id of the objects whose status is to change</param>
        /// <param name="status">The status to be used for the objects</param>
        /// <returns>The response returned from the server.</returns>
        public AmazonWebServiceResponse SetStatusAsync(string pipelineId, List<string> objectIds, string status)
        {
            try
            {
                SetStatusRequest setStatusRequest = PopulateStatusRequest(pipelineId, objectIds, status);

                using (AmazonDataPipelineClient amazonDataPipelineClient = Preferences.GetPipelineClient())
                {
                    Task<SetStatusResponse> setStatusResponse = amazonDataPipelineClient.SetStatusAsync(setStatusRequest);
                    setStatusResponse.Wait();
                    return setStatusResponse.Result;
                };
            }
            catch (AmazonDataPipelineException e)
            {
                throw new PipingException("Unable to set the status of the pipeline object.", e);
            }
        }


        /// <summary>
        /// Validates the specified Pipeline definition.
        /// </summary>
        /// <param name="validatePipelineDefinitionRequest">The request detailing the pipeline definition to be validated.</param>
        /// <returns>The response returned from the server.</returns>
        public AmazonWebServiceResponse ValidatePipelineDefinition(ValidatePipelineDefinitionRequest validatePipelineDefinitionRequest)
        {
            try
            {
                using (AmazonDataPipelineClient amazonDataPipelineClient = Preferences.GetPipelineClient())
                {
                    return amazonDataPipelineClient.ValidatePipelineDefinition(validatePipelineDefinitionRequest);
                };
            }
            catch (AmazonDataPipelineException e)
            {
                throw new PipingException("Unable to validate the pipeline definition.", e);
            }
        }


        /// <summary>
        /// Validates the specified Pipeline definition.
        /// </summary>
        /// <param name="pipelineId">The Id of the pipeline.</param>
        /// <param name="pipelineObjects">The objects that define the pipeline.</param>
        /// <param name="parameterObjects">The parameter objects used with the pipeline.</param>
        /// <param name="parameterValues">The parameter values used with the pipeline.</param>
        /// <returns>The response returned from the server.</returns>
        public AmazonWebServiceResponse ValidatePipelineDefinition(string pipelineId, List<PipelineObject> pipelineObjects,
            List<ParameterObject> parameterObjects, List<Amazon.DataPipeline.Model.ParameterValue> parameterValues)
        {
            try
            {
                ValidatePipelineDefinitionRequest validatePipelineDefinitionRequest = PopulateValidatePipelineDefinitionRequest(pipelineId, pipelineObjects, parameterObjects, parameterValues);

                using (AmazonDataPipelineClient amazonDataPipelineClient = Preferences.GetPipelineClient())
                {
                    return amazonDataPipelineClient.ValidatePipelineDefinition(validatePipelineDefinitionRequest);
                };
            }
            catch (AmazonDataPipelineException e)
            {
                throw new PipingException("Unable to validate the pipeline definition.", e);
            }
        }


        /// <summary>
        /// Validates the specified Pipeline definition.
        /// </summary>
        /// <param name="validatePipelineDefinitionRequest">The request detailing the pipeline definition to be validated.</param>
        /// <returns>The response returned from the server.</returns>
        public AmazonWebServiceResponse ValidatePipelineDefinitionAsync(ValidatePipelineDefinitionRequest validatePipelineDefinitionRequest)
        {
            try
            {
                using (AmazonDataPipelineClient amazonDataPipelineClient = Preferences.GetPipelineClient())
                {
                    Task<ValidatePipelineDefinitionResponse> validatePipelineDefinitionResponse = amazonDataPipelineClient.ValidatePipelineDefinitionAsync(validatePipelineDefinitionRequest);
                    validatePipelineDefinitionResponse.Wait();
                    return validatePipelineDefinitionResponse.Result;
                };
            }
            catch (AmazonDataPipelineException e)
            {
                throw new PipingException("Unable to validate the pipeline definition.", e);
            }
        }


        /// <summary>
        /// Validates the specified Pipeline definition.
        /// </summary>
        /// <param name="pipelineId">The Id of the pipeline.</param>
        /// <param name="pipelineObjects">The objects that define the pipeline.</param>
        /// <param name="parameterObjects">The parameter objects used with the pipeline.</param>
        /// <param name="parameterValues">The parameter values used with the pipeline.</param>
        /// <returns>The response returned from the server.</returns>
        public AmazonWebServiceResponse ValidatePipelineDefinitionAsync(string pipelineId, List<PipelineObject> pipelineObjects,
            List<ParameterObject> parameterObjects, List<Amazon.DataPipeline.Model.ParameterValue> parameterValues)
        {
            try
            {
                ValidatePipelineDefinitionRequest validatePipelineDefinitionRequest = PopulateValidatePipelineDefinitionRequest(pipelineId, pipelineObjects, parameterObjects, parameterValues);

                using (AmazonDataPipelineClient amazonDataPipelineClient = Preferences.GetPipelineClient())
                {
                    Task<ValidatePipelineDefinitionResponse> validatePipelineDefinitionResponse = amazonDataPipelineClient.ValidatePipelineDefinitionAsync(validatePipelineDefinitionRequest);
                    validatePipelineDefinitionResponse.Wait();
                    return validatePipelineDefinitionResponse.Result;
                };
            }
            catch (AmazonDataPipelineException e)
            {
                throw new PipingException("Unable to validate the pipeline definition.", e);
            }
        }


        #region Private Methods


        /// <summary>
        /// Creates and populates an ActivatePipelineRequest
        /// </summary>
        /// <param name="pipelineId">The Id of the pipeline to be used</param>
        /// <param name="parameterValues">Parameters to pass to the pipeline on activation</param>
        /// <returns>A request to be used when activating data pipelines</returns>
        private static ActivatePipelineRequest PopulateActivateRequest(string pipelineId,
            [Optional, DefaultParameterValue(null)]List<Amazon.DataPipeline.Model.ParameterValue> parameterValues)
        {
            return new ActivatePipelineRequest
            {
                PipelineId = pipelineId,
                ParameterValues = parameterValues ?? null
            };
        }


        /// <summary>
        /// Creates and populates a CreatePipelineRequest
        /// </summary>
        /// <param name="uniqueId">Unique identifier set by the user</param>
        /// <param name="name">The name of the pipeline</param>
        /// <param name="description">A description of the pipeline</param>
        /// <param name="tags">[Optional] Any tags to add to the pipeline</param>
        /// <returns>A request to be used when creating data pipelines</returns>
        private static CreatePipelineRequest PopulateCreateRequest(string uniqueId, string name, string description, [Optional, DefaultParameterValue(null)]List<Tag> tags)
        {
            return new CreatePipelineRequest
            {
                UniqueId = uniqueId ?? null,
                Name = name ?? null,
                Description = description ?? null,
                Tags = tags ?? null
            };
        }


        /// <summary>
        /// Creates and populates a DeactivatePipelineRequest
        /// </summary>
        /// <param name="pipelineId">The Id of the pipeline to be used</param>
        /// <param name="cancelActive">Whether to cancel any currently running objects</param>
        /// <returns>A request to be used when deactivating data pipelines</returns>
        private static DeactivatePipelineRequest PopulateDeactivateRequest(string pipelineId, [DefaultParameterValue(true)]bool cancelActive)
        {
            return new DeactivatePipelineRequest
            {
                PipelineId = pipelineId ?? null,
                CancelActive = cancelActive
            };
        }


        /// <summary>
        /// Creates and populates a DeletePipelineRequest object
        /// </summary>
        /// <param name="pipelineId">The Id of the pipeline.</param>
        /// <returns>A request to be used when deleting data pipelines</returns>
        private static DeletePipelineRequest PopulateDeleteRequest(string pipelineId)
        {
            return new DeletePipelineRequest
            {
                PipelineId = pipelineId ?? null
            };
        }


        /// <summary>
        /// Creates and populates a DescribeObjectsRequest object
        /// </summary>
        /// <param name="pipelineId">The Id of the pipeline.</param>
        /// <param name="objectIds">The Id of the object.</param>
        /// <param name="evaluateExpressions">Whether any expressions in the object should be evaluated</param>
        /// <returns>A request to be used when retrieving metadata about one or more pipline objects</returns>
        private static DescribeObjectsRequest PopulateDescribeObjectsRequest(string pipelineId, List<string> objectIds, 
            [Optional, DefaultParameterValue(true)]bool evaluateExpressions)
        {
            return new DescribeObjectsRequest
            {
                PipelineId = pipelineId ?? null,
                ObjectIds = objectIds ?? null,
                EvaluateExpressions = evaluateExpressions
            };
        }


        /// <summary>
        /// Creates and populates a DescribePipelinesRequest object
        /// </summary>
        /// <param name="pipelineIds">The Ids of the pipelines whose definitions are required.</param>
        /// <returns>A request to be used when retrieving metadata about one or more piplines</returns>
        private static DescribePipelinesRequest PopulateDescribePipelinesRequest(List<string> pipelineIds)
        {
            return new DescribePipelinesRequest
            {
                PipelineIds = pipelineIds ?? null
            };
        }


        /// <summary>
        /// Creates and populates a EvaluateExpressionRequest object
        /// </summary>
        /// <param name="pipelineId">The Id of the pipeline.</param>
        /// <param name="objectId">The Id of the object.</param>
        /// <param name="expression">The expression to evaluate</param>
        /// <returns>A request to be used when evaluating strings in the context of an object</returns>
        public static EvaluateExpressionRequest PopulateExpressionRequest(string pipelineId, string objectId, string expression)
        {
            return new EvaluateExpressionRequest
            {
                PipelineId = pipelineId ?? null,
                ObjectId = objectId ?? null,
                Expression = expression ?? null
            };
        }


        /// <summary>
        /// Creates and populates a GetPipelineDefinitionRequest object
        /// </summary>
        /// <param name="pipelineId">The Id of the pipeline.</param>
        /// <param name="version">The version of the pipeline definition</param>
        /// <returns>A request to be used when getting pipeline definitions</returns>
        public static GetPipelineDefinitionRequest PopulateGetPipelineDefinitionRequest(string pipelineId, string version)
        {
            return new GetPipelineDefinitionRequest
            {
                PipelineId = pipelineId ?? null,
                Version = version ?? null
            };
        }


        /// <summary>
        /// Creates and populates a PutPipelineDefinitionRequest object
        /// </summary>
        /// <param name="pipelineId">The Id of the pipeline.</param>
        /// <param name="pipelineObjects">The objects that define the pipeline.</param>
        /// <param name="parameterObjects">The parameter objects used with the pipeline.</param>
        /// <param name="parameterValues">The parameter values used with the pipeline.</param>
        /// <returns>A request to be used when sending definitions to the pipeline</returns>
        public static PutPipelineDefinitionRequest PopulatePutPipelineDefinitionRequest(string pipelineId, List<PipelineObject> pipelineObjects,
            List<ParameterObject> parameterObjects, List<Amazon.DataPipeline.Model.ParameterValue> parameterValues)
        {
            return new PutPipelineDefinitionRequest
            {
                PipelineId = pipelineId ?? null,
                PipelineObjects = pipelineObjects ?? null,
                ParameterObjects = parameterObjects,
                ParameterValues = parameterValues
            };
        }


        /// <summary>
        /// Creates and populates a QueryObjectsRequest object
        /// </summary>
        /// <param name="pipelineId">The Id of the pipeline</param>
        /// <param name="sphere">Whether the query applies to components, instances or attempts</param>
        /// <param name="query">The query that defines the objects to be returned</param>
        /// <param name="limit">The maximum number of records to be returned</param>
        /// <returns>A request to be used when querying objects on a pipeline</returns>
        public static QueryObjectsRequest PopulateQueryObjectsRequest(string pipelineId, string sphere, Query query, [DefaultParameterValue(100)]int limit)
        {
            return new QueryObjectsRequest
            {
                PipelineId = pipelineId ?? null,
                Limit = limit,
                Sphere = sphere ?? null,
                Query = query
            };
        }


        /// <summary>
        /// Creates and populates a RemoveTagsRequest object
        /// </summary>
        /// <param name="pipelineId">The Id of the pipeline</param>
        /// <param name="tagKeys">The keys of the tags to remove</param>
        /// <returns>A request to be used when removing tags from a pipeline</returns>
        public static RemoveTagsRequest PopulateRemoveTagsRequest(string pipelineId, List<string> tagKeys)
        {
            return new RemoveTagsRequest
            {
                PipelineId = pipelineId ?? null,
                TagKeys = tagKeys ?? null
            };
        }


        /// <summary>
        /// Creates and populates a SetStatusRequest object
        /// </summary>
        /// <param name="pipelineId">The id of the pipeline that contains the objects</param>
        /// <param name="objectIds">The Id of the objects whose status is to change</param>
        /// <param name="status">The status to be used for the objects</param>
        /// <returns>A request to be used when setting status requests to pipeline objects</returns>
        public static SetStatusRequest PopulateStatusRequest(string pipelineId, List<string> objectIds, string status)
        {
            return new SetStatusRequest
            {
                PipelineId = pipelineId ?? null,
                ObjectIds = objectIds ?? null,
                Status = status ?? null
            };
        }


        /// <summary>
        /// Creates and populates a ValidatePipelineDefinitionRequest object
        /// </summary>
        /// <param name="pipelineId">The Id of the pipeline.</param>
        /// <param name="pipelineObjects">The objects that define the pipeline.</param>
        /// <param name="parameterObjects">The parameter objects used with the pipeline.</param>
        /// <param name="parameterValues">The parameter values used with the pipeline.</param>
        /// <returns>A request to be used when validating pata pipeline definitions</returns>
        public static ValidatePipelineDefinitionRequest PopulateValidatePipelineDefinitionRequest(string pipelineId, List<PipelineObject> pipelineObjects,
            List<ParameterObject> parameterObjects, List<Amazon.DataPipeline.Model.ParameterValue> parameterValues)
        {
            return new ValidatePipelineDefinitionRequest
            {
                PipelineId = pipelineId ?? null,
                PipelineObjects = pipelineObjects ?? null,
                ParameterObjects = parameterObjects ?? null,
                ParameterValues = parameterValues ?? null
            };
        }



#if DEBUG

        /// <summary>
        /// Currently experimental - polling the data pipeline for tasks is apparently non-trivial.
        /// </summary>
        /// <param name="amazonDataPipelineClient">The pipeline client being used for the connection.</param>
        /// <param name="activatePipelineResponse">The response from the server.</param>
        /// <returns>The completed activation task.</returns>
        private Task<AmazonWebServiceResponse> WaitForActivation(AmazonDataPipelineClient amazonDataPipelineClient, Task<AmazonWebServiceResponse> activatePipelineResponse)
        {
            //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            //The below is part of an experimental approach to the polling, left here for reference
            //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            DateTime dateTime = DateTime.Now;

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

            return activatePipelineResponse;
        }

#endif

        #endregion

    }
}
