using Amazon.Kinesis;
using Amazon.Kinesis.Model;
using AWSome.Messages.Config;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AWSome.Messages.Transactions
{
    /// <summary>
    /// Receiver for Kinesis messages
    /// </summary>
    public class KinesisReceiver
    {
        /// <summary>
        /// Gets records from the Kinesis stream
        /// </summary>
        /// <returns>A list of records from the Kinesis stream</returns>
        public static List<Record> GetRecordsFromKinesisQueue(DateTime fromDateTime)
        {
            AmazonKinesisClient kinesisClient = Preferences.GetInstance().GetKinesisClient();

            DescribeStreamRequest describeRequest = new DescribeStreamRequest
            {
                StreamName = Preferences.KinesisStreamName
            };

            DescribeStreamResponse describeResponse = kinesisClient.DescribeStream(describeRequest);

            return GetRecords(kinesisClient, describeResponse, fromDateTime);
        }


        /// <summary>
        /// Gets the records from the named stream
        /// </summary>
        /// <param name="kinesisClient">The Kinesis client object</param>
        /// <param name="describeResponse">The stream response descriptor</param>
        private static List<Record> GetRecords(AmazonKinesisClient kinesisClient, DescribeStreamResponse describeResponse, DateTime fromDateTime)
        {
            List<Shard> shards = describeResponse.StreamDescription.Shards;
            List<Record> records = new List<Record>();

            foreach (Shard shard in shards)
            {
                GetShardIteratorRequest iteratorRequest = new GetShardIteratorRequest
                {
                    StreamName = Preferences.KinesisStreamName,
                    ShardId = shard.ShardId,
                    ShardIteratorType = ShardIteratorType.AT_TIMESTAMP,
                    Timestamp = fromDateTime
                };

                records.AddRange(PopulateRecordsFromShards(kinesisClient, iteratorRequest));
            }

            return records;
        }

        /// <summary>
        /// Populates record objects from shards
        /// </summary>
        /// <param name="kinesisClient">The Kinesis client object</param>
        /// <param name="shardIteratorRequest"></param>
        private static List<Record> PopulateRecordsFromShards(AmazonKinesisClient kinesisClient, GetShardIteratorRequest shardIteratorRequest)
        {
            GetShardIteratorResponse iteratorResponse = kinesisClient.GetShardIterator(shardIteratorRequest);
            string iteratorId = iteratorResponse.ShardIterator;
            List<Record> records = new List<Record>();

            while (!string.IsNullOrEmpty(iteratorId))
            {
                GetRecordsRequest getRequest = new GetRecordsRequest
                {
                    Limit = 1000,
                    ShardIterator = iteratorId
                };

                Task<GetRecordsResponse> getResponse = kinesisClient.GetRecordsAsync(getRequest);
                getResponse.Wait();

                GetRecordsResponse getRecordsResponse = getResponse.Result;
                records.AddRange(getRecordsResponse.Records);

                if (getResponse.Result.MillisBehindLatest == 0)
                    break;

                iteratorId = getRecordsResponse.NextShardIterator;
            }

            return records;
        }
    }
}
