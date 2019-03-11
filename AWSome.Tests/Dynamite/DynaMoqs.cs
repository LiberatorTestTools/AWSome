using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.Runtime;
using Moq;
using System.Collections.Generic;
using System.Net;

namespace Liberator.AWSome.Tests.Dynamite
{
    public static class DynaMoqs
    {
        public static IClientConfig Config { get; set; }

        public static string TableName { get; set; }

        public static StreamSpecification StreamSpecification { get; set; }

        public static SSESpecification SSESpecification { get; set; }

        public static ProvisionedThroughput ProvisionedThroughput { get; set; }

        public static List<LocalSecondaryIndex> LocalSecondaryIndexes { get; set; }

        public static List<KeySchemaElement> KeySchema { get; set; }

        public static List<GlobalSecondaryIndex> GlobalSecondaryIndexes { get; set; }

        public static BillingMode BillingMode { get; set; }

        public static List<AttributeDefinition> AttributeDefinitions { get; set; }

        public static long ContentLength { get; set; }

        public static HttpStatusCode HttpStatusCode { get; set; }

        public static ResponseMetadata ResponseMetadata { get; set; }

        public static TableDescription TableDescription { get; set; }

        public static Dictionary<string, KeysAndAttributes> RequestItems { get; set; }

        public static ReturnConsumedCapacity ReturnConsumedCapacity { get; set; }

        public static List<ConsumedCapacity> ConsumedCapacityList { get; set; }

        public static ConsumedCapacity ConsumedCapacity { get; set; }

        public static Dictionary<string, List<Dictionary<string, AttributeValue>>> Responses { get; set; }

        public static Dictionary<string, KeysAndAttributes> UnprocessedKeys { get; set; }

        public static Dictionary<string, List<WriteRequest>> BatchRequestItems { get; set; }

        public static ReturnItemCollectionMetrics ReturnItemCollectionMetrics { get; set; }

        public static ConditionalOperator ConditionalOperator { get; set; }

        public static string ConditionExpression { get; set; }

        public static Dictionary<string, ExpectedAttributeValue> Expected { get; set; }

        public static Dictionary<string, string> ExpressionAttributeNames { get; set; }

        public static Dictionary<string, AttributeValue> ExpressionAttributeValues { get; set; }

        public static Dictionary<string, AttributeValue> Key { get; set; }

        public static ReturnValue ReturnValues { get; set; }

        public static Dictionary<string, AttributeValue> Attributes { get; set; }

        public static ItemCollectionMetrics ItemCollectionMetrics { get; set; }

        public static List<string> AttributesToGet { get; set; }

        public static bool ConsistentRead { get; set; }

        public static string ProjectionExpression { get; set; }

        public static bool IsItemSet { get; set; }

        public static Dictionary<string, AttributeValue> Item { get; set; }

        public static int Limit { get; set; }

        public static string ShardIterator { get; set; }

        public static string NextShardIterator { get; set; }

        public static List<Record> Records { get; set; }

        public static string SequenceNumber { get; set; }

        public static string ShardId { get; set; }

        public static ShardIteratorType ShardIteratorType { get; set; }

        public static string StreamArn { get; set; }

        public static string ExclusiveStartTableName { get; set; }

        public static List<string> TableNames { get; set; }

        public static string LastEvaluatedTableName { get; set; }

        public static Dictionary<string, List<WriteRequest>> UnprocessedItems { get; set; }

        public static Dictionary<string, AttributeValue> ExclusiveStartKey { get; set; }

        public static string FilterExpression { get; set; }

        public static string IndexName { get; set; }

        public static string KeyConditionExpression { get; set; }

        public static Dictionary<string, Condition> KeyConditions { get; set; }

        public static Dictionary<string, Condition> QueryFilter { get; set; }

        public static bool ScanIndexForward { get; set; }

        public static Select Select { get; set; }

        public static int Count { get; set; }

        public static List<Dictionary<string, AttributeValue>> Items { get; set; }

        public static Dictionary<string, AttributeValue> LastEvaluatedKey { get; set; }

        public static int ScannedCount { get; set; }
        public static int Segment { get; private set; }
        public static int TotalSegments { get; private set; }
        public static Dictionary<string, Condition> ScanFilter { get; private set; }
        public static Dictionary<string, AttributeValueUpdate> AttributeUpdates { get; private set; }
        public static string UpdateExpression { get; private set; }
        public static List<GlobalSecondaryIndexUpdate> GlobalSecondaryIndexUpdates { get; private set; }

        public static IAmazonDynamoDB AmazonDynamoDB()
        {
            Mock<AmazonDynamoDBClient> amazonDynamoDB = new Mock<AmazonDynamoDBClient>(MockBehavior.Strict);
            amazonDynamoDB.SetupProperty(m => m.Config, Config);

            amazonDynamoDB.Setup(m => m.BatchGetItem(BatchGetItemRequest())).Returns(BatchGetItemResponse);
            amazonDynamoDB.Setup(m => m.BatchWriteItem(BatchWriteItemRequest())).Returns(BatchWriteItemResponse);
            amazonDynamoDB.Setup(m => m.CreateTable(CreateTableRequest())).Returns(CreateTableResponse);
            amazonDynamoDB.Setup(m => m.DeleteItem(DeleteItemRequest())).Returns(DeleteItemResponse);
            amazonDynamoDB.Setup(m => m.DeleteTable(DeleteTableRequest())).Returns(DeleteTableResponse);
            amazonDynamoDB.Setup(m => m.DescribeTable(DescribeTableRequest())).Returns(DescribeTableResponse);
            amazonDynamoDB.Setup(m => m.GetItem(GetItemRequest())).Returns(GetItemResponse);
            amazonDynamoDB.Setup(m => m.ListTables(ListTablesRequest())).Returns(ListTablesResponse);
            amazonDynamoDB.Setup(m => m.PutItem(PutItemRequest())).Returns(PutItemResponse);
            amazonDynamoDB.Setup(m => m.Query(QueryRequest())).Returns(QueryResponse);
            amazonDynamoDB.Setup(m => m.Scan(ScanRequest())).Returns(ScanResponse);

            return amazonDynamoDB.Object;
        }

        private static CreateTableRequest CreateTableRequest()
        {
            Mock<CreateTableRequest> mockDynamoRequest = new Mock<CreateTableRequest>(MockBehavior.Strict);
            mockDynamoRequest.SetupProperty(m => m.AttributeDefinitions, AttributeDefinitions);
            mockDynamoRequest.SetupProperty(m => m.BillingMode, BillingMode);
            mockDynamoRequest.SetupProperty(m => m.GlobalSecondaryIndexes, GlobalSecondaryIndexes);
            mockDynamoRequest.SetupProperty(m => m.KeySchema, KeySchema);
            mockDynamoRequest.SetupProperty(m => m.LocalSecondaryIndexes, LocalSecondaryIndexes);
            mockDynamoRequest.SetupProperty(m => m.ProvisionedThroughput, ProvisionedThroughput);
            mockDynamoRequest.SetupProperty(m => m.SSESpecification, SSESpecification);
            mockDynamoRequest.SetupProperty(m => m.StreamSpecification, StreamSpecification);
            mockDynamoRequest.SetupProperty(m => m.TableName, TableName);
            return mockDynamoRequest.Object;
        }

        private static CreateTableResponse CreateTableResponse()
        {
            Mock<CreateTableResponse> mockDynamoRequest = new Mock<CreateTableResponse>(MockBehavior.Strict);
            mockDynamoRequest.SetupProperty(m => m.ContentLength, ContentLength);
            mockDynamoRequest.SetupProperty(m => m.HttpStatusCode, HttpStatusCode);
            mockDynamoRequest.SetupProperty(m => m.ResponseMetadata, ResponseMetadata);
            mockDynamoRequest.SetupProperty(m => m.TableDescription, TableDescription);
            return mockDynamoRequest.Object;
        }

        public static BatchGetItemRequest BatchGetItemRequest()
        {
            Mock<BatchGetItemRequest> mockDynamoRequest = new Mock<BatchGetItemRequest>(MockBehavior.Strict);
            mockDynamoRequest.SetupProperty(m => m.RequestItems, RequestItems);
            mockDynamoRequest.SetupProperty(m => m.ReturnConsumedCapacity, ReturnConsumedCapacity);
            return mockDynamoRequest.Object;
        }

        public static BatchGetItemResponse BatchGetItemResponse()
        {
            Mock<BatchGetItemResponse> mockDynamoRequest = new Mock<BatchGetItemResponse>(MockBehavior.Strict);
            mockDynamoRequest.SetupProperty(m => m.ConsumedCapacity, ConsumedCapacityList);
            mockDynamoRequest.SetupProperty(m => m.ContentLength, ContentLength);
            mockDynamoRequest.SetupProperty(m => m.HttpStatusCode, HttpStatusCode);
            mockDynamoRequest.SetupProperty(m => m.ResponseMetadata, ResponseMetadata);
            mockDynamoRequest.SetupProperty(m => m.Responses, Responses);
            mockDynamoRequest.SetupProperty(m => m.UnprocessedKeys, UnprocessedKeys);
            return mockDynamoRequest.Object;
        }

        public static BatchWriteItemRequest BatchWriteItemRequest()
        {
            Mock<BatchWriteItemRequest> mockDynamoRequest = new Mock<BatchWriteItemRequest>(MockBehavior.Strict);
            mockDynamoRequest.SetupProperty(m => m.RequestItems, BatchRequestItems);
            mockDynamoRequest.SetupProperty(m => m.ReturnConsumedCapacity, ReturnConsumedCapacity);
            mockDynamoRequest.SetupProperty(m => m.ReturnItemCollectionMetrics, ReturnItemCollectionMetrics);
            return mockDynamoRequest.Object;
        }

        public static BatchWriteItemResponse BatchWriteItemResponse()
        {
            Mock<BatchWriteItemResponse> mockDynamoRequest = new Mock<BatchWriteItemResponse>(MockBehavior.Strict);
            mockDynamoRequest.SetupProperty(m => m.ConsumedCapacity, ConsumedCapacityList);
            mockDynamoRequest.SetupProperty(m => m.ContentLength, ContentLength);
            mockDynamoRequest.SetupProperty(m => m.HttpStatusCode, HttpStatusCode);
            mockDynamoRequest.SetupProperty(m => m.ResponseMetadata, ResponseMetadata);
            mockDynamoRequest.SetupProperty(m => m.UnprocessedItems, UnprocessedItems);
            return mockDynamoRequest.Object;
        }

        public static DeleteItemRequest DeleteItemRequest()
        {
            Mock<DeleteItemRequest> mockDynamoRequest = new Mock<DeleteItemRequest>(MockBehavior.Strict);
            mockDynamoRequest.SetupProperty(m => m.ConditionalOperator, ConditionalOperator);
            mockDynamoRequest.SetupProperty(m => m.ConditionExpression, ConditionExpression);
            mockDynamoRequest.SetupProperty(m => m.Expected, Expected);
            mockDynamoRequest.SetupProperty(m => m.ExpressionAttributeNames, ExpressionAttributeNames);
            mockDynamoRequest.SetupProperty(m => m.ExpressionAttributeValues, ExpressionAttributeValues);
            mockDynamoRequest.SetupProperty(m => m.Key, Key);
            mockDynamoRequest.SetupProperty(m => m.ReturnConsumedCapacity, ReturnConsumedCapacity);
            mockDynamoRequest.SetupProperty(m => m.ReturnItemCollectionMetrics, ReturnItemCollectionMetrics);
            mockDynamoRequest.SetupProperty(m => m.ReturnValues, ReturnValues);
            mockDynamoRequest.SetupProperty(m => m.TableName, TableName);
            return mockDynamoRequest.Object;
        }

        public static DeleteItemResponse DeleteItemResponse()
        {
            Mock<DeleteItemResponse> mockDynamoRequest = new Mock<DeleteItemResponse>(MockBehavior.Strict);
            mockDynamoRequest.SetupProperty(m => m.Attributes, Attributes);
            mockDynamoRequest.SetupProperty(m => m.ConsumedCapacity, ConsumedCapacity);
            mockDynamoRequest.SetupProperty(m => m.ContentLength, ContentLength);
            mockDynamoRequest.SetupProperty(m => m.HttpStatusCode, HttpStatusCode);
            mockDynamoRequest.SetupProperty(m => m.ItemCollectionMetrics, ItemCollectionMetrics);
            mockDynamoRequest.SetupProperty(m => m.ResponseMetadata, ResponseMetadata);
            return mockDynamoRequest.Object;
        }

        public static DeleteTableRequest DeleteTableRequest()
        {
            Mock<DeleteTableRequest> mockDynamoRequest = new Mock<DeleteTableRequest>(MockBehavior.Strict);
            mockDynamoRequest.SetupProperty(m => m.TableName, TableName);
            return mockDynamoRequest.Object;
        }

        public static DeleteTableResponse DeleteTableResponse()
        {
            Mock<DeleteTableResponse> mockDynamoRequest = new Mock<DeleteTableResponse>(MockBehavior.Strict);
            mockDynamoRequest.SetupProperty(m => m.ContentLength, ContentLength);
            mockDynamoRequest.SetupProperty(m => m.HttpStatusCode, HttpStatusCode);
            mockDynamoRequest.SetupProperty(m => m.ResponseMetadata, ResponseMetadata);
            mockDynamoRequest.SetupProperty(m => m.TableDescription, TableDescription);
            return mockDynamoRequest.Object;
        }

        public static DescribeTableRequest DescribeTableRequest()
        {
            Mock<DescribeTableRequest> mockDynamoRequest = new Mock<DescribeTableRequest>(MockBehavior.Strict);
            mockDynamoRequest.SetupProperty(m => m.TableName, TableName);
            return mockDynamoRequest.Object;
        }

        public static DescribeTableResponse DescribeTableResponse()
        {
            Mock<DescribeTableResponse> mockDynamoRequest = new Mock<DescribeTableResponse>(MockBehavior.Strict);
            mockDynamoRequest.SetupProperty(m => m.ContentLength, ContentLength);
            mockDynamoRequest.SetupProperty(m => m.HttpStatusCode, HttpStatusCode);
            mockDynamoRequest.SetupProperty(m => m.ResponseMetadata, ResponseMetadata);
            mockDynamoRequest.SetupProperty(m => m.Table, TableDescription);
            return mockDynamoRequest.Object;
        }

        public static GetItemRequest GetItemRequest()
        {
            Mock<GetItemRequest> mockDynamoRequest = new Mock<GetItemRequest>(MockBehavior.Strict);
            mockDynamoRequest.SetupProperty(m => m.AttributesToGet, AttributesToGet);
            mockDynamoRequest.SetupProperty(m => m.ConsistentRead, ConsistentRead);
            mockDynamoRequest.SetupProperty(m => m.ExpressionAttributeNames, ExpressionAttributeNames);
            mockDynamoRequest.SetupProperty(m => m.Key, Key);
            mockDynamoRequest.SetupProperty(m => m.ProjectionExpression, ProjectionExpression);
            mockDynamoRequest.SetupProperty(m => m.ReturnConsumedCapacity, ReturnConsumedCapacity);
            mockDynamoRequest.SetupProperty(m => m.TableName, TableName);
            return mockDynamoRequest.Object;
        }

        public static GetItemResponse GetItemResponse()
        {
            Mock<GetItemResponse> mockDynamoRequest = new Mock<GetItemResponse>(MockBehavior.Strict);
            mockDynamoRequest.SetupProperty(m => m.ConsumedCapacity, ConsumedCapacity);
            mockDynamoRequest.SetupProperty(m => m.ContentLength, ContentLength);
            mockDynamoRequest.SetupProperty(m => m.HttpStatusCode, HttpStatusCode);
            mockDynamoRequest.SetupProperty(m => m.IsItemSet, IsItemSet);
            mockDynamoRequest.SetupProperty(m => m.Item, Item);
            mockDynamoRequest.SetupProperty(m => m.ResponseMetadata, ResponseMetadata);
            return mockDynamoRequest.Object;
        }

        public static GetRecordsRequest GetRecordsRequest()
        {
            Mock<GetRecordsRequest> mockDynamoRequest = new Mock<GetRecordsRequest>(MockBehavior.Strict);
            mockDynamoRequest.SetupProperty(m => m.Limit, Limit);
            mockDynamoRequest.SetupProperty(m => m.ShardIterator, ShardIterator);
            return mockDynamoRequest.Object;
        }

        public static GetRecordsResponse GetRecordsResponse()
        {
            Mock<GetRecordsResponse> mockDynamoRequest = new Mock<GetRecordsResponse>(MockBehavior.Strict);
            mockDynamoRequest.SetupProperty(m => m.ContentLength, ContentLength);
            mockDynamoRequest.SetupProperty(m => m.HttpStatusCode, HttpStatusCode);
            mockDynamoRequest.SetupProperty(m => m.NextShardIterator, NextShardIterator);
            mockDynamoRequest.SetupProperty(m => m.Records, Records);
            mockDynamoRequest.SetupProperty(m => m.ResponseMetadata, ResponseMetadata);
            return mockDynamoRequest.Object;
        }

        public static GetShardIteratorRequest GetShardIteratorRequest()
        {
            Mock<GetShardIteratorRequest> mockDynamoRequest = new Mock<GetShardIteratorRequest>(MockBehavior.Strict);
            mockDynamoRequest.SetupProperty(m => m.SequenceNumber, SequenceNumber);
            mockDynamoRequest.SetupProperty(m => m.ShardId, ShardId);
            mockDynamoRequest.SetupProperty(m => m.ShardIteratorType, ShardIteratorType);
            mockDynamoRequest.SetupProperty(m => m.StreamArn, StreamArn);
            return mockDynamoRequest.Object;
        }

        public static ListTablesRequest ListTablesRequest()
        {
            Mock<ListTablesRequest> mockDynamoRequest = new Mock<ListTablesRequest>(MockBehavior.Strict);
            mockDynamoRequest.SetupProperty(m => m.ExclusiveStartTableName, ExclusiveStartTableName);
            mockDynamoRequest.SetupProperty(m => m.Limit, Limit);
            return mockDynamoRequest.Object;
        }

        public static ListTablesResponse ListTablesResponse()
        {
            Mock<ListTablesResponse> mockDynamoRequest = new Mock<ListTablesResponse>(MockBehavior.Strict);
            mockDynamoRequest.SetupProperty(m => m.ContentLength, ContentLength);
            mockDynamoRequest.SetupProperty(m => m.HttpStatusCode, HttpStatusCode);
            mockDynamoRequest.SetupProperty(m => m.LastEvaluatedTableName, LastEvaluatedTableName);
            mockDynamoRequest.SetupProperty(m => m.ResponseMetadata, ResponseMetadata);
            mockDynamoRequest.SetupProperty(m => m.TableNames, TableNames);
            return mockDynamoRequest.Object;
        }

        public static PutItemRequest PutItemRequest()
        {
            Mock<PutItemRequest> mockDynamoRequest = new Mock<PutItemRequest>(MockBehavior.Strict);
            mockDynamoRequest.SetupProperty(m => m.ConditionalOperator, ConditionalOperator);
            mockDynamoRequest.SetupProperty(m => m.ConditionExpression, ConditionExpression);
            mockDynamoRequest.SetupProperty(m => m.Expected, Expected);
            mockDynamoRequest.SetupProperty(m => m.ExpressionAttributeNames, ExpressionAttributeNames);
            mockDynamoRequest.SetupProperty(m => m.ExpressionAttributeValues, ExpressionAttributeValues);
            mockDynamoRequest.SetupProperty(m => m.Item, Item);
            mockDynamoRequest.SetupProperty(m => m.ReturnConsumedCapacity, ReturnConsumedCapacity);
            mockDynamoRequest.SetupProperty(m => m.ReturnItemCollectionMetrics, ReturnItemCollectionMetrics);
            mockDynamoRequest.SetupProperty(m => m.ReturnValues, ReturnValues);
            mockDynamoRequest.SetupProperty(m => m.TableName, TableName);
            return mockDynamoRequest.Object;
        }

        public static PutItemResponse PutItemResponse()
        {
            Mock<PutItemResponse> mockDynamoRequest = new Mock<PutItemResponse>(MockBehavior.Strict);
            mockDynamoRequest.SetupProperty(m => m.Attributes, Attributes);
            mockDynamoRequest.SetupProperty(m => m.ConsumedCapacity, ConsumedCapacity);
            mockDynamoRequest.SetupProperty(m => m.ContentLength, ContentLength);
            mockDynamoRequest.SetupProperty(m => m.HttpStatusCode, HttpStatusCode);
            mockDynamoRequest.SetupProperty(m => m.ItemCollectionMetrics, ItemCollectionMetrics);
            mockDynamoRequest.SetupProperty(m => m.ResponseMetadata, ResponseMetadata);
            return mockDynamoRequest.Object;
        }

        public static QueryRequest QueryRequest()
        {
            Mock<QueryRequest> mockDynamoRequest = new Mock<QueryRequest>(MockBehavior.Strict);
            mockDynamoRequest.SetupProperty(m => m.AttributesToGet, AttributesToGet);
            mockDynamoRequest.SetupProperty(m => m.ConditionalOperator, ConditionalOperator);
            mockDynamoRequest.SetupProperty(m => m.ConsistentRead, ConsistentRead);
            mockDynamoRequest.SetupProperty(m => m.ExclusiveStartKey, ExclusiveStartKey);
            mockDynamoRequest.SetupProperty(m => m.ExpressionAttributeNames, ExpressionAttributeNames);
            mockDynamoRequest.SetupProperty(m => m.ExpressionAttributeValues, ExpressionAttributeValues);
            mockDynamoRequest.SetupProperty(m => m.FilterExpression, FilterExpression);
            mockDynamoRequest.SetupProperty(m => m.IndexName, IndexName);
            mockDynamoRequest.SetupProperty(m => m.KeyConditionExpression, KeyConditionExpression);
            mockDynamoRequest.SetupProperty(m => m.KeyConditions, KeyConditions);
            mockDynamoRequest.SetupProperty(m => m.Limit, Limit);
            mockDynamoRequest.SetupProperty(m => m.ProjectionExpression, ProjectionExpression);
            mockDynamoRequest.SetupProperty(m => m.QueryFilter, QueryFilter);
            mockDynamoRequest.SetupProperty(m => m.ReturnConsumedCapacity, ReturnConsumedCapacity);
            mockDynamoRequest.SetupProperty(m => m.ScanIndexForward, ScanIndexForward);
            mockDynamoRequest.SetupProperty(m => m.Select, Select);
            mockDynamoRequest.SetupProperty(m => m.TableName, TableName);
            return mockDynamoRequest.Object;
        }

        public static QueryResponse QueryResponse()
        {
            Mock<QueryResponse> mockDynamoRequest = new Mock<QueryResponse>(MockBehavior.Strict);
            mockDynamoRequest.SetupProperty(m => m.ConsumedCapacity, ConsumedCapacity);
            mockDynamoRequest.SetupProperty(m => m.Count, Count);
            mockDynamoRequest.SetupProperty(m => m.HttpStatusCode, HttpStatusCode);
            mockDynamoRequest.SetupProperty(m => m.Items, Items);
            mockDynamoRequest.SetupProperty(m => m.LastEvaluatedKey, LastEvaluatedKey);
            mockDynamoRequest.SetupProperty(m => m.ResponseMetadata, ResponseMetadata);
            mockDynamoRequest.SetupProperty(m => m.ScannedCount, ScannedCount);
            return mockDynamoRequest.Object;
        }

        public static ScanRequest ScanRequest()
        {
            Mock<ScanRequest> mockDynamoRequest = new Mock<ScanRequest>(MockBehavior.Strict);
            mockDynamoRequest.SetupProperty(m => m.AttributesToGet, AttributesToGet);
            mockDynamoRequest.SetupProperty(m => m.ConditionalOperator, ConditionalOperator);
            mockDynamoRequest.SetupProperty(m => m.ConsistentRead, ConsistentRead);
            mockDynamoRequest.SetupProperty(m => m.ExclusiveStartKey, ExclusiveStartKey);
            mockDynamoRequest.SetupProperty(m => m.ExpressionAttributeNames, ExpressionAttributeNames);
            mockDynamoRequest.SetupProperty(m => m.ExpressionAttributeValues, ExpressionAttributeValues);
            mockDynamoRequest.SetupProperty(m => m.FilterExpression, FilterExpression);
            mockDynamoRequest.SetupProperty(m => m.IndexName, IndexName);
            mockDynamoRequest.SetupProperty(m => m.Limit, Limit);
            mockDynamoRequest.SetupProperty(m => m.ProjectionExpression, ProjectionExpression);
            mockDynamoRequest.SetupProperty(m => m.ReturnConsumedCapacity, ReturnConsumedCapacity);
            mockDynamoRequest.SetupProperty(m => m.ScanFilter, ScanFilter);
            mockDynamoRequest.SetupProperty(m => m.Segment, Segment);
            mockDynamoRequest.SetupProperty(m => m.Select, Select);
            mockDynamoRequest.SetupProperty(m => m.TableName, TableName);
            mockDynamoRequest.SetupProperty(m => m.TotalSegments, TotalSegments);
            return mockDynamoRequest.Object;
        }

        public static ScanResponse ScanResponse()
        {
            Mock<ScanResponse> mockDynamoRequest = new Mock<ScanResponse>(MockBehavior.Strict);
            mockDynamoRequest.SetupProperty(m => m.ConsumedCapacity, ConsumedCapacity);
            mockDynamoRequest.SetupProperty(m => m.ContentLength, ContentLength);
            mockDynamoRequest.SetupProperty(m => m.Count, Count);
            mockDynamoRequest.SetupProperty(m => m.HttpStatusCode, HttpStatusCode);
            mockDynamoRequest.SetupProperty(m => m.Items, Items);
            mockDynamoRequest.SetupProperty(m => m.LastEvaluatedKey, LastEvaluatedKey);
            mockDynamoRequest.SetupProperty(m => m.ResponseMetadata, ResponseMetadata);
            mockDynamoRequest.SetupProperty(m => m.ScannedCount, ScannedCount);
            return mockDynamoRequest.Object;
        }

        public static UpdateItemRequest UpdateItemRequest()
        {
            Mock<UpdateItemRequest> mockDynamoRequest = new Mock<UpdateItemRequest>(MockBehavior.Strict);
            mockDynamoRequest.SetupProperty(m => m.AttributeUpdates, AttributeUpdates);
            mockDynamoRequest.SetupProperty(m => m.ConditionalOperator, ConditionalOperator);
            mockDynamoRequest.SetupProperty(m => m.ConditionExpression, ConditionExpression);
            mockDynamoRequest.SetupProperty(m => m.Expected, Expected);
            mockDynamoRequest.SetupProperty(m => m.ExpressionAttributeNames, ExpressionAttributeNames);
            mockDynamoRequest.SetupProperty(m => m.ExpressionAttributeValues, ExpressionAttributeValues);
            mockDynamoRequest.SetupProperty(m => m.Key, Key);
            mockDynamoRequest.SetupProperty(m => m.ReturnConsumedCapacity, ReturnConsumedCapacity);
            mockDynamoRequest.SetupProperty(m => m.ReturnItemCollectionMetrics, ReturnItemCollectionMetrics);
            mockDynamoRequest.SetupProperty(m => m.ReturnValues, ReturnValues);
            mockDynamoRequest.SetupProperty(m => m.TableName, TableName);
            mockDynamoRequest.SetupProperty(m => m.UpdateExpression, UpdateExpression);
            return mockDynamoRequest.Object;
        }

        public static UpdateItemResponse UpdateItemResponse()
        {
            Mock<UpdateItemResponse> mockDynamoRequest = new Mock<UpdateItemResponse>(MockBehavior.Strict);
            mockDynamoRequest.SetupProperty(m => m.Attributes, Attributes);
            mockDynamoRequest.SetupProperty(m => m.ConsumedCapacity, ConsumedCapacity);
            mockDynamoRequest.SetupProperty(m => m.ContentLength, ContentLength);
            mockDynamoRequest.SetupProperty(m => m.HttpStatusCode, HttpStatusCode);
            mockDynamoRequest.SetupProperty(m => m.ItemCollectionMetrics, ItemCollectionMetrics);
            mockDynamoRequest.SetupProperty(m => m.ResponseMetadata, ResponseMetadata);
            return mockDynamoRequest.Object;
        }

        public static UpdateTableRequest UpdateTableRequest()
        {
            Mock<UpdateTableRequest> mockDynamoRequest = new Mock<UpdateTableRequest>(MockBehavior.Strict);
            mockDynamoRequest.SetupProperty(m => m.AttributeDefinitions, AttributeDefinitions);
            mockDynamoRequest.SetupProperty(m => m.BillingMode, BillingMode);
            mockDynamoRequest.SetupProperty(m => m.GlobalSecondaryIndexUpdates, GlobalSecondaryIndexUpdates);
            mockDynamoRequest.SetupProperty(m => m.ProvisionedThroughput, ProvisionedThroughput);
            mockDynamoRequest.SetupProperty(m => m.SSESpecification, SSESpecification);
            mockDynamoRequest.SetupProperty(m => m.StreamSpecification, StreamSpecification);
            mockDynamoRequest.SetupProperty(m => m.TableName, TableName);
            return mockDynamoRequest.Object;
        }

        public static UpdateTableResponse UpdateTableResponse()
        {
            Mock<UpdateTableResponse> mockDynamoRequest = new Mock<UpdateTableResponse>(MockBehavior.Strict);
            mockDynamoRequest.SetupProperty(m => m.ContentLength, ContentLength);
            mockDynamoRequest.SetupProperty(m => m.HttpStatusCode, HttpStatusCode);
            mockDynamoRequest.SetupProperty(m => m.ResponseMetadata, ResponseMetadata);
            mockDynamoRequest.SetupProperty(m => m.TableDescription, TableDescription);
            return mockDynamoRequest.Object;
        }
    }
}
