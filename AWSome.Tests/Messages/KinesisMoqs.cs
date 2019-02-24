using Amazon.Kinesis;
using Amazon.Kinesis.Model;
using Amazon.Runtime;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace Liberator.AWSome.Tests.Messages
{
    public class KinesisMoqs
    {
        public static IClientConfig KinesisConfig { get; set; }
        public static int ShardCount { get; set; }
        public static string StreamName { get; set; }
        public static long ContentLength { get; set; }
        public static HttpStatusCode HttpStatusCode { get; set; }
        public static ResponseMetadata ResponseMetadata { get; set; }
        public static bool EnforceConsumerDeletion { get; set; }
        public static string ExclusiveStartShardId { get; set; }
        public static int Limit { get; set; }
        public static StreamDescription StreamDescription { get; set; }
        public static string ShardIterator { get; set; }
        public static long MillisBehindLatest { get; set; }
        public static string NextShardIterator { get; set; }
        public static List<Record> Records { get; set; }
        public static string ShardId { get; set; }
        public static ShardIteratorType ShardIteratorType { get; set; }
        public static string StartingSequenceNumber { get; set; }
        public static DateTime Timestamp { get; set; }
        public static int MaxResults { get; set; }
        public static string NextToken { get; set; }
        public static DateTime StreamCreationTimestamp { get; set; }
        public static List<Shard> Shards { get; set; }
        public static string ExclusiveStartStreamName { get; set; }
        public static bool HasMoreStreams { get; set; }
        public static List<string> StreamNames { get; set; }
        public static MemoryStream Data { get; set; }
        public static string ExplicitHashKey { get; set; }
        public static string PartitionKey { get; set; }
        public static string SequenceNumberForOrdering { get; set; }
        public static EncryptionType EncryptionType { get; set; }
        public static string SequenceNumber { get; private set; }
        public static List<PutRecordsRequestEntry> PutRecordEntries { get; private set; }
        public static int FailedRecordCount { get; private set; }
        public static List<PutRecordsResultEntry> PutRecordResults { get; private set; }

        public static AmazonKinesisClient AmazonKinesisClient()
        {
            Mock<AmazonKinesisClient> mockKinesisRequest = new Mock<AmazonKinesisClient>(MockBehavior.Strict);
            mockKinesisRequest.SetupProperty(m => m.Config, KinesisConfig);

            mockKinesisRequest.Setup(m => m.CreateStream(CreateStreamRequest())).Returns(CreateStreamResponse);
            mockKinesisRequest.Setup(m => m.DeleteStream(DeleteStreamRequest())).Returns(DeleteStreamResponse);
            mockKinesisRequest.Setup(m => m.DescribeStream(DescribeStreamRequest())).Returns(DescribeStreamResponse);
            mockKinesisRequest.Setup(m => m.GetRecords(GetRecordsRequest())).Returns(GetRecordsResponse);
            mockKinesisRequest.Setup(m => m.GetShardIterator(GetShardIteratorRequest())).Returns(GetShardIteratorResponse);
            mockKinesisRequest.Setup(m => m.ListShards(ListShardsRequest())).Returns(ListShardsResponse);
            mockKinesisRequest.Setup(m => m.ListStreams(ListStreamsRequest())).Returns(ListStreamsResponse);
            mockKinesisRequest.Setup(m => m.PutRecord(PutRecordRequest())).Returns(PutRecordResponse);
            mockKinesisRequest.Setup(m => m.PutRecords(PutRecordsRequest())).Returns(PutRecordsResponse);

            return mockKinesisRequest.Object;
        }

        private static CreateStreamRequest CreateStreamRequest()
        {
            Mock<CreateStreamRequest> mockKinesisRequest = new Mock<CreateStreamRequest>(MockBehavior.Strict);
            mockKinesisRequest.SetupProperty(m => m.ShardCount, ShardCount);
            mockKinesisRequest.SetupProperty(m => m.StreamName, StreamName);
            return mockKinesisRequest.Object;
        }

        private static CreateStreamResponse CreateStreamResponse()
        {
            Mock<CreateStreamResponse> mockKinesisRequest = new Mock<CreateStreamResponse>(MockBehavior.Strict);
            mockKinesisRequest.SetupProperty(m => m.ContentLength, ContentLength);
            mockKinesisRequest.SetupProperty(m => m.HttpStatusCode, HttpStatusCode);
            mockKinesisRequest.SetupProperty(m => m.ResponseMetadata, ResponseMetadata);
            return mockKinesisRequest.Object;
        }

        private static DeleteStreamRequest DeleteStreamRequest()
        {
            Mock<DeleteStreamRequest> mockKinesisRequest = new Mock<DeleteStreamRequest>(MockBehavior.Strict);
            mockKinesisRequest.SetupProperty(m => m.EnforceConsumerDeletion, EnforceConsumerDeletion);
            mockKinesisRequest.SetupProperty(m => m.StreamName, StreamName);
            return mockKinesisRequest.Object;
        }

        private static DeleteStreamResponse DeleteStreamResponse()
        {
            Mock<DeleteStreamResponse> mockKinesisRequest = new Mock<DeleteStreamResponse>(MockBehavior.Strict);
            mockKinesisRequest.SetupProperty(m => m.ContentLength, ContentLength);
            mockKinesisRequest.SetupProperty(m => m.HttpStatusCode, HttpStatusCode);
            mockKinesisRequest.SetupProperty(m => m.ResponseMetadata, ResponseMetadata);
            return mockKinesisRequest.Object;
        }

        private static DescribeStreamRequest DescribeStreamRequest()
        {
            Mock<DescribeStreamRequest> mockKinesisRequest = new Mock<DescribeStreamRequest>(MockBehavior.Strict);
            mockKinesisRequest.SetupProperty(m => m.ExclusiveStartShardId, ExclusiveStartShardId);
            mockKinesisRequest.SetupProperty(m => m.Limit, Limit);
            mockKinesisRequest.SetupProperty(m => m.StreamName, StreamName);
            return mockKinesisRequest.Object;
        }

        private static DescribeStreamResponse DescribeStreamResponse()
        {
            Mock<DescribeStreamResponse> mockKinesisRequest = new Mock<DescribeStreamResponse>(MockBehavior.Strict);
            mockKinesisRequest.SetupProperty(m => m.ContentLength, ContentLength);
            mockKinesisRequest.SetupProperty(m => m.HttpStatusCode, HttpStatusCode);
            mockKinesisRequest.SetupProperty(m => m.ResponseMetadata, ResponseMetadata);
            mockKinesisRequest.SetupProperty(m => m.StreamDescription, StreamDescription);
            return mockKinesisRequest.Object;
        }

        private static GetRecordsRequest GetRecordsRequest()
        {
            Mock<GetRecordsRequest> mockKinesisRequest = new Mock<GetRecordsRequest>(MockBehavior.Strict);
            mockKinesisRequest.SetupProperty(m => m.Limit, Limit);
            mockKinesisRequest.SetupProperty(m => m.ShardIterator, ShardIterator);
            return mockKinesisRequest.Object;
        }

        private static GetRecordsResponse GetRecordsResponse()
        {
            Mock<GetRecordsResponse> mockKinesisRequest = new Mock<GetRecordsResponse>(MockBehavior.Strict);
            mockKinesisRequest.SetupProperty(m => m.ContentLength, ContentLength);
            mockKinesisRequest.SetupProperty(m => m.HttpStatusCode, HttpStatusCode);
            mockKinesisRequest.SetupProperty(m => m.MillisBehindLatest, MillisBehindLatest);
            mockKinesisRequest.SetupProperty(m => m.NextShardIterator, NextShardIterator);
            mockKinesisRequest.SetupProperty(m => m.Records, Records);
            mockKinesisRequest.SetupProperty(m => m.ResponseMetadata, ResponseMetadata);
            return mockKinesisRequest.Object;
        }

        private static GetShardIteratorRequest GetShardIteratorRequest()
        {
            Mock<GetShardIteratorRequest> mockKinesisRequest = new Mock<GetShardIteratorRequest>(MockBehavior.Strict);
            mockKinesisRequest.SetupProperty(m => m.ShardId, ShardId);
            mockKinesisRequest.SetupProperty(m => m.ShardIteratorType, ShardIteratorType);
            mockKinesisRequest.SetupProperty(m => m.StartingSequenceNumber, StartingSequenceNumber);
            mockKinesisRequest.SetupProperty(m => m.StreamName, StreamName);
            mockKinesisRequest.SetupProperty(m => m.Timestamp, Timestamp);
            return mockKinesisRequest.Object;
        }

        private static GetShardIteratorResponse GetShardIteratorResponse()
        {
            Mock<GetShardIteratorResponse> mockKinesisRequest = new Mock<GetShardIteratorResponse>(MockBehavior.Strict);
            mockKinesisRequest.SetupProperty(m => m.ContentLength, ContentLength);
            mockKinesisRequest.SetupProperty(m => m.HttpStatusCode, HttpStatusCode);
            mockKinesisRequest.SetupProperty(m => m.ResponseMetadata, ResponseMetadata);
            mockKinesisRequest.SetupProperty(m => m.ShardIterator, ShardIterator);
            return mockKinesisRequest.Object;
        }

        private static ListShardsRequest ListShardsRequest()
        {
            Mock<ListShardsRequest> mockKinesisRequest = new Mock<ListShardsRequest>(MockBehavior.Strict);
            mockKinesisRequest.SetupProperty(m => m.ExclusiveStartShardId, ExclusiveStartShardId);
            mockKinesisRequest.SetupProperty(m => m.MaxResults, MaxResults);
            mockKinesisRequest.SetupProperty(m => m.NextToken, NextToken);
            mockKinesisRequest.SetupProperty(m => m.StreamCreationTimestamp, StreamCreationTimestamp);
            mockKinesisRequest.SetupProperty(m => m.StreamName, StreamName);
            return mockKinesisRequest.Object;
        }

        private static ListShardsResponse ListShardsResponse()
        {
            Mock<ListShardsResponse> mockKinesisRequest = new Mock<ListShardsResponse>(MockBehavior.Strict);
            mockKinesisRequest.SetupProperty(m => m.ContentLength, ContentLength);
            mockKinesisRequest.SetupProperty(m => m.HttpStatusCode, HttpStatusCode);
            mockKinesisRequest.SetupProperty(m => m.NextToken, NextToken);
            mockKinesisRequest.SetupProperty(m => m.ResponseMetadata, ResponseMetadata);
            mockKinesisRequest.SetupProperty(m => m.Shards, Shards);
            return mockKinesisRequest.Object;
        }

        private static ListStreamsRequest ListStreamsRequest()
        {
            Mock<ListStreamsRequest> mockKinesisRequest = new Mock<ListStreamsRequest>(MockBehavior.Strict);
            mockKinesisRequest.SetupProperty(m => m.ExclusiveStartStreamName, ExclusiveStartStreamName);
            mockKinesisRequest.SetupProperty(m => m.Limit, Limit);
            return mockKinesisRequest.Object;
        }

        private static ListStreamsResponse ListStreamsResponse()
        {
            Mock<ListStreamsResponse> mockKinesisRequest = new Mock<ListStreamsResponse>(MockBehavior.Strict);
            mockKinesisRequest.SetupProperty(m => m.ContentLength, ContentLength);
            mockKinesisRequest.SetupProperty(m => m.HasMoreStreams, HasMoreStreams);
            mockKinesisRequest.SetupProperty(m => m.HttpStatusCode, HttpStatusCode);
            mockKinesisRequest.SetupProperty(m => m.ResponseMetadata, ResponseMetadata);
            mockKinesisRequest.SetupProperty(m => m.StreamNames, StreamNames);
            return mockKinesisRequest.Object;
        }

        private static PutRecordRequest PutRecordRequest()
        {
            Mock<PutRecordRequest> mockKinesisRequest = new Mock<PutRecordRequest>(MockBehavior.Strict);
            mockKinesisRequest.SetupProperty(m => m.Data, Data);
            mockKinesisRequest.SetupProperty(m => m.ExplicitHashKey, ExplicitHashKey);
            mockKinesisRequest.SetupProperty(m => m.PartitionKey, PartitionKey);
            mockKinesisRequest.SetupProperty(m => m.SequenceNumberForOrdering, SequenceNumberForOrdering);
            mockKinesisRequest.SetupProperty(m => m.StreamName, StreamName);
            return mockKinesisRequest.Object;
        }

        private static PutRecordResponse PutRecordResponse()
        {
            Mock<PutRecordResponse> mockKinesisRequest = new Mock<PutRecordResponse>(MockBehavior.Strict);
            mockKinesisRequest.SetupProperty(m => m.ContentLength, ContentLength);
            mockKinesisRequest.SetupProperty(m => m.EncryptionType, EncryptionType);
            mockKinesisRequest.SetupProperty(m => m.HttpStatusCode, HttpStatusCode);
            mockKinesisRequest.SetupProperty(m => m.ResponseMetadata, ResponseMetadata);
            mockKinesisRequest.SetupProperty(m => m.SequenceNumber, SequenceNumber);
            mockKinesisRequest.SetupProperty(m => m.ShardId, ShardId);
            return mockKinesisRequest.Object;
        }

        private static PutRecordsRequest PutRecordsRequest()
        {
            Mock<PutRecordsRequest> mockKinesisRecords = new Mock<PutRecordsRequest>(MockBehavior.Strict);
            mockKinesisRecords.SetupProperty(m => m.Records, PutRecordEntries);
            mockKinesisRecords.SetupProperty(m => m.StreamName, StreamName);
            return mockKinesisRecords.Object;
        }

        private static PutRecordsResponse PutRecordsResponse()
        {
            Mock<PutRecordsResponse> mockKinesisRequest = new Mock<PutRecordsResponse>(MockBehavior.Strict);
            mockKinesisRequest.SetupProperty(m => m.ContentLength, ContentLength);
            mockKinesisRequest.SetupProperty(m => m.EncryptionType, EncryptionType);
            mockKinesisRequest.SetupProperty(m => m.FailedRecordCount, FailedRecordCount);
            mockKinesisRequest.SetupProperty(m => m.HttpStatusCode, HttpStatusCode);
            mockKinesisRequest.SetupProperty(m => m.Records, PutRecordResults);
            mockKinesisRequest.SetupProperty(m => m.ResponseMetadata, ResponseMetadata);
            return mockKinesisRequest.Object;
        }
        
    }
}
