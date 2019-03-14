using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Liberator.AWSome.Dynamite.Config;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Liberator.AWSome.Dynamite.Client
{
    /// <summary>
    /// The abstract class on which a service is based
    /// </summary>
    public abstract class Service
    {
        /// <summary>
        /// The message to display when creating a table
        /// </summary>
        private const string createTableMessage = "Create (Amazon): {0}. Table status: {1}";

        /// <summary>
        /// The message to display when updating a table
        /// </summary>
        private const string updateTableMessage = "Update (Amazon): {0}. Table status: {1}";

        /// <summary>
        /// The message to display when deleting a table
        /// </summary>
        private const string deleteTableMessage = "Delete (Amazon): {0}. Table status: {1}";

        /// <summary>
        /// Lists the Key Schema for the Dynamo DB table
        /// </summary>
        public List<KeySchemaElement> keySchemaElements;

        /// <summary>
        /// Lists the Attribute Definitions for the Dynamo DB table
        /// </summary>
        public List<AttributeDefinition> attributeDefinitions;

        /// <summary>
        /// The description of the table under investigation
        /// </summary>
        public TableDescription tableDescription;

        /// <summary>
        /// Default constructor for the Service class used to communicate with Dynamo DB
        /// </summary>
        public Service()
        {
            keySchemaElements = new List<KeySchemaElement>();
            attributeDefinitions = new List<AttributeDefinition>();
            tableDescription = new TableDescription();
        }


        /// <summary>
        /// Creates a Dynamo DB Table
        /// </summary>
        /// <param name="tableName">The name of the table to be created</param>
        public void CreateTable(string tableName)
        {
            using (IAmazonDynamoDB client = Preferences.GetDynamoDbClient())
            {
                CreateTableRequest createTableRequest = new CreateTableRequest
                {
                    AttributeDefinitions = attributeDefinitions,
                    KeySchema = keySchemaElements,
                    StreamSpecification = StreamSpecification(),
                    SSESpecification = SSESpecification(),
                    ProvisionedThroughput = new ProvisionedThroughput() { ReadCapacityUnits = 1, WriteCapacityUnits = 1 },
                    TableName = tableName
                };

                Task<CreateTableResponse> task = client.CreateTableAsync(createTableRequest);
                task.Wait();
                CreateTableResponse createTableResponse = task.Result;
                TableStatus tableStatus = GetTableStatus(createTableResponse);

                Debug.WriteLine(string.Format(createTableMessage, tableName, tableStatus));

                while (tableStatus == TableStatus.CREATING || tableStatus == TableStatus.UPDATING)
                {
                    //TODO: This is a code smell, but I wanted a quick band aid - replace this Thread Sleep!!!
                    Thread.Sleep(1000);
                    tableStatus = GetTableStatus(createTableResponse);
                }

                Debug.WriteLine(string.Format(createTableMessage, tableName, tableStatus));
            }
        }

        /// <summary>
        /// Updates a Dynamo DB table definition
        /// </summary>
        /// <param name="tableName">The name of the table to be updated</param>
        public void UpdateTable(string tableName)
        {
            using (IAmazonDynamoDB client = Preferences.GetDynamoDbClient())
            {
                UpdateTableRequest updateTableRequest = new UpdateTableRequest
                {
                    TableName = tableName,
                    ProvisionedThroughput = new ProvisionedThroughput() { ReadCapacityUnits = 2, WriteCapacityUnits = 2 }
                };

                Task<UpdateTableResponse> updateTableResponse = client.UpdateTableAsync(updateTableRequest);
                updateTableResponse.Wait();
            }
        }

        /// <summary>
        /// Deletes a Dynamo DB Table
        /// </summary>
        /// <param name="tableName">The name of the table to be deleted</param>
        public void DeleteTable(string tableName)
        {
            using (IAmazonDynamoDB client = Preferences.GetDynamoDbClient())
            {
                DeleteTableRequest deleteTableRequest = new DeleteTableRequest(tableName);
                Task<DeleteTableResponse> response = client.DeleteTableAsync(deleteTableRequest);
                response.Wait();
            }
        }


        /// <summary>
        /// Gets a list of the all tables in the connected Dynamo database
        /// </summary>
        /// <returns>A list containing the names of the tables within the Dynamo DB</returns>
        public List<string> GetListOfTables()
        {
            using (IAmazonDynamoDB client = Preferences.GetDynamoDbClient())
            {
                Task<ListTablesResponse> response = client.ListTablesAsync();
                response.Wait();
                ListTablesResponse listTablesResponse = response.Result;
                return listTablesResponse.TableNames;
            }
        }

        /// <summary>
        /// Outputs the details for each table in Dynamo DB to the console
        /// </summary>
        public void GetDetailsForTables()
        {
            List<string> tables = GetListOfTables();
            using (IAmazonDynamoDB client = Preferences.GetDynamoDbClient())
            {
                foreach (string table in tables)
                {
                    DescribeTableRequest describeTableRequest = new DescribeTableRequest(table);

                    Task<DescribeTableResponse> response = client.DescribeTableAsync(describeTableRequest);
                    response.Wait();
                    DescribeTableResponse describeTableResponse = response.Result;
                    TableDescription tableDescription = describeTableResponse.Table;
                    Debug.WriteLine(string.Format("Printing information about table {0}:", tableDescription.TableName));
                    Debug.WriteLine(string.Format("Created at: {0}", tableDescription.CreationDateTime));
                    List<KeySchemaElement> keySchemaElements = tableDescription.KeySchema;
                    foreach (KeySchemaElement schema in keySchemaElements)
                    {
                        Debug.WriteLine(string.Format("Key name: {0}, key type: {1}", schema.AttributeName, schema.KeyType));
                    }
                    Debug.WriteLine(string.Format("Item count: {0}", tableDescription.ItemCount));
                    ProvisionedThroughputDescription throughput = tableDescription.ProvisionedThroughput;
                    Debug.WriteLine(string.Format("Read capacity: {0}", throughput.ReadCapacityUnits));
                    Debug.WriteLine(string.Format("Write capacity: {0}", throughput.WriteCapacityUnits));
                    List<AttributeDefinition> tableAttributes = tableDescription.AttributeDefinitions;
                    foreach (AttributeDefinition attDefinition in tableAttributes)
                    {
                        Debug.WriteLine(string.Format("Table attribute name: {0}", attDefinition.AttributeName));
                        Debug.WriteLine(string.Format("Table attribute type: {0}", attDefinition.AttributeType));
                    }
                    Debug.WriteLine(string.Format("Table size: {0}b", tableDescription.TableSizeBytes));
                    Debug.WriteLine(string.Format("Table status: {0}", tableDescription.TableStatus));
                    Debug.WriteLine("====================================================");

                }
            }
        }

        /// <summary>
        /// Adds a key schema element to the collection which is used to add keys to the database table
        /// </summary>
        /// <param name="attributeName"></param>
        /// <param name="keyType"></param>
        public void AddKeySchemaElement(string attributeName, KeyType keyType)
        {
            KeySchemaElement element = new KeySchemaElement
            {
                AttributeName = attributeName,
                KeyType = keyType
            };

            keySchemaElements.Add(element);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="attributeName"></param>
        /// <param name="scalarAttributeType"></param>
        public void AddAttributeDefinition(string attributeName, ScalarAttributeType scalarAttributeType)
        {
            AttributeDefinition attributeDefinition = new AttributeDefinition
            {
                AttributeName = attributeName,
                AttributeType = scalarAttributeType
            };

            attributeDefinitions.Add(attributeDefinition);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public StreamSpecification StreamSpecification()
        {
            StreamSpecification streamSpecification = new StreamSpecification
            {
                StreamEnabled = Preferences.StreamEnabled,
                StreamViewType = Preferences.StreamViewType
            };

            return streamSpecification;
        }

        /// <summary>
        /// Creates an SSE Specification for the table under investigation
        /// </summary>
        /// <returns>An object representing the SSE Specification</returns>
        public SSESpecification SSESpecification()
        {
            SSESpecification specification = new SSESpecification
            {
                Enabled = Preferences.SSEEnabled,
                KMSMasterKeyId = Preferences.KMSMasterKeyId,
                SSEType = Preferences.SSEType
            };

            return specification;
        }

        /// <summary>
        /// Gets the status of the table under investigation
        /// </summary>
        /// <param name="createTableResponse">The response from the create table request</param>
        /// <returns>An object representing the status of the table</returns>
        public TableStatus GetTableStatus(CreateTableResponse createTableResponse)
        {
            TableDescription tableDescription = createTableResponse.TableDescription;
            return tableDescription.TableStatus.Value;
        }


    }
}
