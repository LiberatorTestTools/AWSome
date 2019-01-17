using Amazon.DataPipeline;
using Amazon.DataPipeline.Model;
using Amazon.Runtime;
using Moq;
using System.Collections.Generic;
using System.Net;
using ParameterValue = Amazon.DataPipeline.Model.ParameterValue;

namespace AWSome.Tests.Piping
{
    /// <summary>
    /// Contains methods that return mock DataPipeline objects for testing purposes
    /// </summary>
    public static class PipelineMoqs
    {
        #region Public Properties

        public static string PipelineId { get; set; }

        public static List<string> PipelineIds { get; set; }

        public static List<PipelineIdName> PipelineIdNames { get; set; }

        public static string UniqueId { get; set; }

        public static string ObjectId { get; set; }

        public static List<string> PropertyIds { get; set; }

        public static string Version { get; set; }

        public static string PipelineName { get; set; }

        public static string Description { get; set; }

        public static List<string> ObjectIds { get; set; }

        public static List<Tag> Tags { get; set; }

        public static List<string> TagKeys { get; set; }

        public static bool CancelActive { get; set; }

        public static bool EvaluateExpressions { get; set; }

        public static string Expression { get; set; }

        public static Query Query { get; set; }

        public static string Sphere { get; set; }

        public static int Limit { get; set; }

        public static List<PipelineDescription> PipelineDescriptions { get; set; }

        public static List<PipelineObject> PipelineObjects { get; set; }

        public static List<ParameterObject> ParameterObjects { get; set; }

        public static List<ParameterValue> ParameterValues { get; set; }

        public static ResponseMetadata ResponseMetadata { get; set; }

        public static HttpStatusCode HttpStatusCode { get; set; }

        public static int ContentLength { get; set; }

        public static bool HasMoreResults { get; set; }

        public static List<ValidationError> ValidationErrors { get; set; }

        public static List<ValidationWarning> ValidationWarnings { get; set; }

        public static string Status { get; set; }

        public static bool Errored { get; set; } 

        #endregion



        /// <summary>
        /// Sets up a DataPipeline client mock for testing
        /// </summary>
        /// <returns>A fully mocked and configured AmazonDataPipelineClient</returns>
        public static AmazonDataPipelineClient AmazonDataPipelineClient()
        {
            Mock<AmazonDataPipelineClient> mockPipelineClient = new Mock<AmazonDataPipelineClient>(MockBehavior.Strict);

            mockPipelineClient.SetupProperty(m => m.Config, null);

            mockPipelineClient.Setup(m => m.ActivatePipeline(ActivatePipelineRequest())).Returns(ActivatePipelineResponse());
            mockPipelineClient.Setup(m => m.CreatePipeline(CreatePipelineRequest())).Returns(CreatePipelineResponse());
            mockPipelineClient.Setup(m => m.DeactivatePipeline(DeactivatePipelineRequest())).Returns(DeactivatePipelineResponse());
            mockPipelineClient.Setup(m => m.DeletePipeline(DeletePipelineRequest())).Returns(DeletePipelineResponse);
            mockPipelineClient.Setup(m => m.DescribeObjects(DescribeObjectsRequest())).Returns(DescribeObjectsResponse);
            mockPipelineClient.Setup(m => m.DescribePipelines(DescribePipelinesRequest())).Returns(DescribePipelinesResponse);
            mockPipelineClient.Setup(m => m.EvaluateExpression(EvaluateExpressionRequest())).Returns(EvaluateExpressionResponse);
            mockPipelineClient.Setup(m => m.GetPipelineDefinition(GetPipelineDefinitionRequest())).Returns(GetPipelineDefinitionResponse);
            mockPipelineClient.Setup(m => m.ListPipelines(ListPipelinesRequest())).Returns(ListPipelinesResponse);
            mockPipelineClient.Setup(m => m.PutPipelineDefinition(PutPipelineDefinitionRequest())).Returns(PutPipelineDefinitionResponse);
            mockPipelineClient.Setup(m => m.QueryObjects(QueryObjectsRequest())).Returns(QueryObjectsResponse);
            mockPipelineClient.Setup(m => m.RemoveTags(RemoveTagsRequest())).Returns(RemoveTagsResponse);
            mockPipelineClient.Setup(m => m.SetStatus(SetStatusRequest())).Returns(SetStatusResponse);
            mockPipelineClient.Setup(m => m.ValidatePipelineDefinition(ValidatePipelineDefinitionRequest())).Returns(ValidatePipelineDefinitionResponse);

            return mockPipelineClient.Object;
        }


        /// <summary>
        /// Sets up a mock pipeline activation request
        /// </summary>
        /// <returns>A mock pipeline activation request</returns>
        public static ActivatePipelineRequest ActivatePipelineRequest()
        {
            Mock<ActivatePipelineRequest> mockPipelineRequest = new Mock<ActivatePipelineRequest>(MockBehavior.Strict);
            mockPipelineRequest.SetupProperty(m => m.PipelineId, PipelineId);
            mockPipelineRequest.SetupProperty(m => m.ParameterValues, ParameterValues);
            return mockPipelineRequest.Object;
        }


        /// <summary>
        /// Sets up a mock pipeline activation response
        /// </summary>
        /// <returns>A mock pipeline activation response</returns>
        public static ActivatePipelineResponse ActivatePipelineResponse()
        {
            Mock<ActivatePipelineResponse> mockPipelineResponse = new Mock<ActivatePipelineResponse>(MockBehavior.Strict);
            mockPipelineResponse.SetupProperty(m => m.HttpStatusCode, HttpStatusCode);
            mockPipelineResponse.SetupProperty(m => m.ContentLength, ContentLength);
            mockPipelineResponse.SetupProperty(m => m.ResponseMetadata, ResponseMetadata);
            return mockPipelineResponse.Object;
        }


        public static CreatePipelineRequest CreatePipelineRequest()
        {
            Mock<CreatePipelineRequest> mockPipelineRequest = new Mock<CreatePipelineRequest>(MockBehavior.Strict);
            mockPipelineRequest.SetupProperty(m => m.UniqueId, UniqueId);
            mockPipelineRequest.SetupProperty(m => m.Name, PipelineName);
            mockPipelineRequest.SetupProperty(m => m.Description, Description);
            mockPipelineRequest.SetupProperty(m => m.Tags, Tags);
            return mockPipelineRequest.Object;
        }

        public static CreatePipelineResponse CreatePipelineResponse()
        {
            Mock<CreatePipelineResponse> mockPipelineResponse = new Mock<CreatePipelineResponse>(MockBehavior.Strict);
            mockPipelineResponse.SetupProperty(m => m.PipelineId, PipelineId);
            mockPipelineResponse.SetupProperty(m => m.HttpStatusCode, HttpStatusCode);
            mockPipelineResponse.SetupProperty(m => m.ContentLength, ContentLength);
            mockPipelineResponse.SetupProperty(m => m.ResponseMetadata, ResponseMetadata);
            return mockPipelineResponse.Object;
        }

        public static DeactivatePipelineRequest DeactivatePipelineRequest()
        {
            Mock<DeactivatePipelineRequest> mockPipelineRequest = new Mock<DeactivatePipelineRequest>(MockBehavior.Strict);
            mockPipelineRequest.SetupProperty(m => m.PipelineId, PipelineId);
            mockPipelineRequest.SetupProperty(m => m.CancelActive, CancelActive);
            return mockPipelineRequest.Object;
        }

        public static DeactivatePipelineResponse DeactivatePipelineResponse()
        {
            Mock<DeactivatePipelineResponse> mockPipelineResponse = new Mock<DeactivatePipelineResponse>(MockBehavior.Strict);
            mockPipelineResponse.SetupProperty(m => m.HttpStatusCode, HttpStatusCode);
            mockPipelineResponse.SetupProperty(m => m.ContentLength, ContentLength);
            mockPipelineResponse.SetupProperty(m => m.ResponseMetadata, ResponseMetadata);
            return mockPipelineResponse.Object;
        }

        public static DeletePipelineRequest DeletePipelineRequest()
        {
            Mock<DeletePipelineRequest> mockPipelineRequest = new Mock<DeletePipelineRequest>(MockBehavior.Strict);
            mockPipelineRequest.SetupProperty(m => m.PipelineId, PipelineId);
            return mockPipelineRequest.Object;
        }

        public static DeletePipelineResponse DeletePipelineResponse()
        {
            Mock<DeletePipelineResponse> mockPipelineResponse = new Mock<DeletePipelineResponse>(MockBehavior.Strict);
            mockPipelineResponse.SetupProperty(m => m.HttpStatusCode, HttpStatusCode);
            mockPipelineResponse.SetupProperty(m => m.ContentLength, ContentLength);
            mockPipelineResponse.SetupProperty(m => m.ResponseMetadata, ResponseMetadata);
            return mockPipelineResponse.Object;
        }

        public static DescribeObjectsRequest DescribeObjectsRequest()
        {
            Mock<DescribeObjectsRequest> mockPipelineResponse = new Mock<DescribeObjectsRequest>(MockBehavior.Strict);
            mockPipelineResponse.SetupProperty(m => m.PipelineId, PipelineId);
            mockPipelineResponse.SetupProperty(m => m.ObjectIds, ObjectIds);
            mockPipelineResponse.SetupProperty(m => m.EvaluateExpressions, EvaluateExpressions);
            return mockPipelineResponse.Object;
        }

        public static DescribeObjectsResponse DescribeObjectsResponse()
        {
            Mock<DescribeObjectsResponse> mockPipelineResponse = new Mock<DescribeObjectsResponse>(MockBehavior.Strict);
            mockPipelineResponse.SetupProperty(m => m.HttpStatusCode, HttpStatusCode);
            mockPipelineResponse.SetupProperty(m => m.ContentLength, ContentLength);
            mockPipelineResponse.SetupProperty(m => m.HasMoreResults, HasMoreResults);
            mockPipelineResponse.SetupProperty(m => m.Marker, "");
            mockPipelineResponse.SetupProperty(m => m.PipelineObjects, PipelineObjects);
            mockPipelineResponse.SetupProperty(m => m.ResponseMetadata, ResponseMetadata);
            return mockPipelineResponse.Object;
        }

        public static DescribePipelinesRequest DescribePipelinesRequest()
        {
            Mock<DescribePipelinesRequest> mockPipelineResponse = new Mock<DescribePipelinesRequest>(MockBehavior.Strict);
            mockPipelineResponse.SetupProperty(m => m.PipelineIds, PipelineIds);
            return mockPipelineResponse.Object;
        }

        public static DescribePipelinesResponse DescribePipelinesResponse()
        {
            Mock<DescribePipelinesResponse> mockPipelineResponse = new Mock<DescribePipelinesResponse>(MockBehavior.Strict);
            mockPipelineResponse.SetupProperty(m => m.ContentLength, ContentLength);
            mockPipelineResponse.SetupProperty(m => m.HttpStatusCode, HttpStatusCode);
            mockPipelineResponse.SetupProperty(m => m.ResponseMetadata, ResponseMetadata);
            mockPipelineResponse.SetupProperty(m => m.PipelineDescriptionList, PipelineDescriptions);
            return mockPipelineResponse.Object;
        }

        public static EvaluateExpressionRequest EvaluateExpressionRequest()
        {
            Mock<EvaluateExpressionRequest> mockPipelineResponse = new Mock<EvaluateExpressionRequest>(MockBehavior.Strict);
            mockPipelineResponse.SetupProperty(m => m.PipelineId, PipelineId);
            mockPipelineResponse.SetupProperty(m => m.ObjectId, ObjectId);
            mockPipelineResponse.SetupProperty(m => m.Expression, Expression);
            return mockPipelineResponse.Object;
        }

        public static EvaluateExpressionResponse EvaluateExpressionResponse()
        {
            Mock<EvaluateExpressionResponse> mockPipelineResponse = new Mock<EvaluateExpressionResponse>(MockBehavior.Strict);
            mockPipelineResponse.SetupProperty(m => m.ContentLength, ContentLength);
            mockPipelineResponse.SetupProperty(m => m.HttpStatusCode, HttpStatusCode);
            mockPipelineResponse.SetupProperty(m => m.ResponseMetadata, ResponseMetadata);
            mockPipelineResponse.SetupProperty(m => m.EvaluatedExpression, Expression);
            return mockPipelineResponse.Object;
        }

        public static GetPipelineDefinitionRequest GetPipelineDefinitionRequest()
        {
            Mock<GetPipelineDefinitionRequest> mockPipelineResponse = new Mock<GetPipelineDefinitionRequest>(MockBehavior.Strict);
            mockPipelineResponse.SetupProperty(m => m.PipelineId, PipelineId);
            mockPipelineResponse.SetupProperty(m => m.Version, Version);
            return mockPipelineResponse.Object;
        }

        public static GetPipelineDefinitionResponse GetPipelineDefinitionResponse()
        {
            Mock<GetPipelineDefinitionResponse> mockPipelineResponse = new Mock<GetPipelineDefinitionResponse>(MockBehavior.Strict);
            mockPipelineResponse.SetupProperty(m => m.ContentLength, ContentLength);
            mockPipelineResponse.SetupProperty(m => m.HttpStatusCode, HttpStatusCode);
            mockPipelineResponse.SetupProperty(m => m.PipelineObjects, PipelineObjects);
            mockPipelineResponse.SetupProperty(m => m.ParameterObjects, ParameterObjects);
            mockPipelineResponse.SetupProperty(m => m.ParameterValues, ParameterValues);
            mockPipelineResponse.SetupProperty(m => m.ResponseMetadata, ResponseMetadata);
            return mockPipelineResponse.Object;
        }

        public static ListPipelinesRequest ListPipelinesRequest()
        {
            Mock<ListPipelinesRequest> mockPipelineResponse = new Mock<ListPipelinesRequest>(MockBehavior.Strict);
            mockPipelineResponse.SetupProperty(m => m.Marker, "");
            return mockPipelineResponse.Object;
        }

        public static ListPipelinesResponse ListPipelinesResponse()
        {
            Mock<ListPipelinesResponse> mockPipelineResponse = new Mock<ListPipelinesResponse>(MockBehavior.Strict);
            mockPipelineResponse.SetupProperty(m => m.ContentLength, ContentLength);
            mockPipelineResponse.SetupProperty(m => m.HasMoreResults, HasMoreResults);
            mockPipelineResponse.SetupProperty(m => m.Marker, "");
            mockPipelineResponse.SetupProperty(m => m.HttpStatusCode, HttpStatusCode);
            mockPipelineResponse.SetupProperty(m => m.PipelineIdList, PipelineIdNames);
            mockPipelineResponse.SetupProperty(m => m.ResponseMetadata, ResponseMetadata);
            return mockPipelineResponse.Object;
        }

        public static PutPipelineDefinitionRequest PutPipelineDefinitionRequest()
        {
            Mock<PutPipelineDefinitionRequest> mockPipelineResponse = new Mock<PutPipelineDefinitionRequest>(MockBehavior.Strict);
            mockPipelineResponse.SetupProperty(m => m.PipelineId, PipelineId);
            mockPipelineResponse.SetupProperty(m => m.PipelineObjects, PipelineObjects);
            mockPipelineResponse.SetupProperty(m => m.ParameterObjects, ParameterObjects);
            mockPipelineResponse.SetupProperty(m => m.ParameterValues, ParameterValues);
            return mockPipelineResponse.Object;
        }

        public static PutPipelineDefinitionResponse PutPipelineDefinitionResponse()
        {
            Mock<PutPipelineDefinitionResponse> mockPipelineResponse = new Mock<PutPipelineDefinitionResponse>(MockBehavior.Strict);
            mockPipelineResponse.SetupProperty(m => m.ContentLength, ContentLength);
            mockPipelineResponse.SetupProperty(m => m.Errored, Errored);
            mockPipelineResponse.SetupProperty(m => m.HttpStatusCode, HttpStatusCode);
            mockPipelineResponse.SetupProperty(m => m.ResponseMetadata, ResponseMetadata);
            mockPipelineResponse.SetupProperty(m => m.ValidationErrors, ValidationErrors);
            mockPipelineResponse.SetupProperty(m => m.ValidationWarnings, ValidationWarnings);
            return mockPipelineResponse.Object;
        }

        public static QueryObjectsRequest QueryObjectsRequest()
        {
            Mock<QueryObjectsRequest> mockPipelineResponse = new Mock<QueryObjectsRequest>(MockBehavior.Strict);
            mockPipelineResponse.SetupProperty(m => m.PipelineId, "MockPipeline");
            mockPipelineResponse.SetupProperty(m => m.Query, Query);
            mockPipelineResponse.SetupProperty(m => m.Sphere, Sphere);
            mockPipelineResponse.SetupProperty(m => m.Limit, Limit);
            mockPipelineResponse.SetupProperty(m => m.Marker, "");
            return mockPipelineResponse.Object;
        }

        public static QueryObjectsResponse QueryObjectsResponse()
        {
            Mock<QueryObjectsResponse> mockPipelineResponse = new Mock<QueryObjectsResponse>(MockBehavior.Strict);
            mockPipelineResponse.SetupProperty(m => m.ContentLength, ContentLength);
            mockPipelineResponse.SetupProperty(m => m.HasMoreResults, HasMoreResults);
            mockPipelineResponse.SetupProperty(m => m.HttpStatusCode, HttpStatusCode);
            mockPipelineResponse.SetupProperty(m => m.Ids, PropertyIds);
            mockPipelineResponse.SetupProperty(m => m.Marker, "");
            mockPipelineResponse.SetupProperty(m => m.ResponseMetadata, ResponseMetadata);
            return mockPipelineResponse.Object;
        }

        public static RemoveTagsRequest RemoveTagsRequest()
        {
            Mock<RemoveTagsRequest> mockPipelineResponse = new Mock<RemoveTagsRequest>(MockBehavior.Strict);
            mockPipelineResponse.SetupProperty(m => m.PipelineId, PipelineId);
            mockPipelineResponse.SetupProperty(m => m.TagKeys, TagKeys);
            return mockPipelineResponse.Object;
        }

        public static RemoveTagsResponse RemoveTagsResponse()
        {
            Mock<RemoveTagsResponse> mockPipelineResponse = new Mock<RemoveTagsResponse>(MockBehavior.Strict);
            mockPipelineResponse.SetupProperty(m => m.ContentLength, ContentLength);
            mockPipelineResponse.SetupProperty(m => m.HttpStatusCode, HttpStatusCode);
            mockPipelineResponse.SetupProperty(m => m.ResponseMetadata, ResponseMetadata);
            return mockPipelineResponse.Object;
        }

        public static SetStatusRequest SetStatusRequest()
        {
            Mock<SetStatusRequest> mockPipelineResponse = new Mock<SetStatusRequest>(MockBehavior.Strict);
            mockPipelineResponse.SetupProperty(m => m.PipelineId, PipelineId);
            mockPipelineResponse.SetupProperty(m => m.ObjectIds, ObjectIds);
            mockPipelineResponse.SetupProperty(m => m.Status, Status);
            return mockPipelineResponse.Object;
        }

        public static SetStatusResponse SetStatusResponse()
        {
            Mock<SetStatusResponse> mockPipelineResponse = new Mock<SetStatusResponse>(MockBehavior.Strict);
            mockPipelineResponse.SetupProperty(m => m.ContentLength, ContentLength);
            mockPipelineResponse.SetupProperty(m => m.HttpStatusCode, HttpStatusCode);
            mockPipelineResponse.SetupProperty(m => m.ResponseMetadata, ResponseMetadata);
            return mockPipelineResponse.Object;
        }

        public static ValidatePipelineDefinitionRequest ValidatePipelineDefinitionRequest()
        {
            Mock<ValidatePipelineDefinitionRequest> mockPipelineResponse = new Mock<ValidatePipelineDefinitionRequest>(MockBehavior.Strict);
            mockPipelineResponse.SetupProperty(m => m.PipelineId, PipelineId);
            mockPipelineResponse.SetupProperty(m => m.PipelineObjects, PipelineObjects);
            mockPipelineResponse.SetupProperty(m => m.ParameterObjects, ParameterObjects);
            mockPipelineResponse.SetupProperty(m => m.ParameterValues, ParameterValues);
            return mockPipelineResponse.Object;
        }

        public static ValidatePipelineDefinitionResponse ValidatePipelineDefinitionResponse()
        {
            Mock<ValidatePipelineDefinitionResponse> mockPipelineResponse = new Mock<ValidatePipelineDefinitionResponse>(MockBehavior.Strict);
            mockPipelineResponse.SetupProperty(m => m.ContentLength, ContentLength);
            mockPipelineResponse.SetupProperty(m => m.Errored, Errored);
            mockPipelineResponse.SetupProperty(m => m.HttpStatusCode, HttpStatusCode);
            mockPipelineResponse.SetupProperty(m => m.ResponseMetadata, ResponseMetadata);
            mockPipelineResponse.SetupProperty(m => m.ValidationErrors, ValidationErrors);
            mockPipelineResponse.SetupProperty(m => m.ValidationWarnings, ValidationWarnings);
            return mockPipelineResponse.Object;
        }


    }
}
