using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Liberator.AWSome.Dynamite.Config;
using Liberator.AWSome.Dynamite.Objects;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Liberator.AWSome.Dynamite.Client
{
    /// <summary>
    /// The service for the DynamoDB
    /// </summary>
    /// <typeparam name="TObject">The type of object being used</typeparam>
    public class DynamoService<TObject> : Service
        where TObject : TableObject
    {
        /// <summary>
        /// Instance of the Service objecvt
        /// </summary>
        public TObject ObjectReference { get; set; }
        

        ///<summary>
        ///Validates that the table can be accessed via the connection
        ///</summary>
        ///<param name="tableName">The name of the table to be validated</param>
        public void ValidateTableConnection(string tableName)
        {
            List<String> listOfTables = GetListOfTables();
            Assert.Contains(tableName, listOfTables);
        }


        /// <summary>
        /// Adds a single row to a Dynamo DB table
        /// </summary>
        /// <param name="tableName">The name of the table to which a row is to be added</param>
        /// <param name="objectReference">The row based object to create in the Dynamo DB</param>
        public void AddRow(string tableName, TObject objectReference)
        {
            ObjectReference = objectReference;
            using (IAmazonDynamoDB client = Preferences.GetDynamoDbClient())
            {
                Table table = Table.LoadTable(client, tableName);
                Task<Document> putItemResponse = table.PutItemAsync(ObjectReference.GetDocument());
                putItemResponse.Wait();
            }
        }

        /// <summary>
        /// Updates a record within a Dynamo DB table using a row based object
        /// </summary>
        /// <param name="tableName">The name of the table containing the row to update</param>
        /// <param name="objectReference">The row based object to update in the Dynamo DB</param>
        public void UpdateRecord(string tableName, TObject objectReference)
        {
            ObjectReference = objectReference;
            using (IAmazonDynamoDB client = Preferences.GetDynamoDbClient())
            {
                Table table = Table.LoadTable(client, tableName);

            }
        }

        /// <summary>
        /// Gets records in a named DynamoDB table, if no filter has been set method will get all records
        /// </summary>
        /// <param name="tableName">The name of the table to retrieve</param>
        /// <param name="filter">The query or scan filter to be used to return objects with certain filters</param>
        /// <returns>A collection of row based objects which represent the data in the table</returns>
        public List<TObject> GetRecords(string tableName, [Optional, DefaultParameterValue(null)]Filter filter)
        {
            List<Document> documents = new List<Document>();
            List<TObject> objects = new List<TObject>();
            Filter recordFilter = filter;
            Search search = null;

            using (IAmazonDynamoDB client = Preferences.GetDynamoDbClient())
            {
                Table table = Table.LoadTable(client, tableName);

                if (filter == null)
                {
                    recordFilter = new ScanFilter();
                }

                if (filter.GetType() == typeof(QueryFilter))
                {
                    search = table.Query((QueryFilter)recordFilter);
                }
                else if (filter.GetType() == typeof(ScanFilter))
                {
                    search = table.Scan((ScanFilter)recordFilter);

                }

                Task<List<Document>> documentsTask = search.GetRemainingAsync();
                documentsTask.Wait();
                documents = documentsTask.Result;
                Type type = typeof(TObject);

                foreach (Document keyValuePairs in documents)
                {
                    TObject obj = (TObject)Activator.CreateInstance(type);
                    obj.Populate(keyValuePairs);
                    objects.Add(obj);
                }
            }
            return objects;
        }

        /// <summary>
        /// Adds a query parameter for a Dynamo DB query
        /// </summary>
        /// <param name="queryFilter">An existing filter. If null, this will be created.</param>
        /// <param name="attribute">The attribute to be queried</param>
        /// <param name="queryOperator">The operator to use in the query</param>
        /// <param name="values">The values to be used in the query</param>
        /// <returns>A populated query filter object</returns>
        public QueryFilter CreateQueryFilterParameter(ref QueryFilter queryFilter, string attribute, QueryOperator queryOperator, List<AttributeValue> values)
        {
            /*
            string uid = "uid"; //attribute
            QueryOperator query = QueryOperator.Equal;
            AttributeValue value = new AttributeValue("12345678");
            */

            if (queryFilter == null)
            {
                queryFilter = new QueryFilter(attribute, queryOperator, values);
            }
            else
            {
                queryFilter.AddCondition(attribute, queryOperator, values);
            }
            return queryFilter;
        }

        /// <summary>
        /// Adds a scan parameter for Dynamo DB query
        /// </summary>
        /// <param name="scanFilter">An existing filter. If null, this will be created</param>
        /// <param name="attribute">The attribute to be scanned</param>
        /// <param name="scanOperator">The operator to use in the scan</param>
        /// <param name="values">The values to be used in the scan</param>
        /// <returns>A populated scan filter object</returns>
        private ScanFilter AddScanFilterParameter(ScanFilter scanFilter, string attribute, ScanOperator scanOperator, List<AttributeValue> values)
        {
            if (scanFilter == null)
            {
                scanFilter = new ScanFilter();
            }
            scanFilter.AddCondition(attribute, scanOperator, values);
            return scanFilter;
        }

        /// <summary>
        /// Method to delete all items in a dynamo with a particular key
        /// </summary>
        /// <param name="tableName">Name of table to delete items from</param>
        /// <param name="keyName">Name of key which will be used to delete all items with it</param>
        /// <param name="sortkey">The key on which to sort.</param>
        public void DeleteAllItems(string tableName, string keyName, [Optional] string sortkey)
        {
            using (IAmazonDynamoDB client = Preferences.GetDynamoDbClient())
            {
                List<string> attributesToGet = new List<string>
                {
                    keyName
                };

                if (sortkey != null)
                {
                    attributesToGet.Add(sortkey);
                }

                Task<ScanResponse> scanResponseTask = client.ScanAsync(tableName, attributesToGet);
                scanResponseTask.Wait();
                ScanResponse scanResponse = scanResponseTask.Result;

                if (scanResponse.Items.Count > 0)
                {
                    List<WriteRequest> deleteRequests = new List<WriteRequest>();

                    foreach (Dictionary<string, AttributeValue> item in scanResponse.Items)
                    {
                        DeleteRequest deleteRequest = new DeleteRequest(item);
                        deleteRequests.Add(new WriteRequest(deleteRequest));

                        if (deleteRequests.Count == 25)
                        {
                            Task<BatchWriteItemResponse> response = SendBatchWriteItemRequest(client, deleteRequests, tableName, keyName);
                            response.Wait();
                            deleteRequests.Clear();
                        }
                    }

                    if (deleteRequests.Count > 0)
                    {
                        Task<BatchWriteItemResponse> response = SendBatchWriteItemRequest(client, deleteRequests, tableName, keyName);
                        response.Wait();
                    }
                }
            }
        }

        private Task<BatchWriteItemResponse> SendBatchWriteItemRequest(
            IAmazonDynamoDB client, List<WriteRequest> deleteRequests, string tableName, string keyName)
        {
            BatchWriteItemRequest request = new BatchWriteItemRequest();
            request.RequestItems.Add(tableName, deleteRequests);
            return client.BatchWriteItemAsync(request);
        }
    }
}
