using Amazon.DynamoDBv2.DocumentModel;

namespace Liberator.AWSome.Dynamite.Objects
{
    /// <summary>
    /// abstract basis for a table object
    /// </summary>
    public abstract class TableObject
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns>A document representing a Dynamo DB row</returns>
        public abstract Document GetDocument();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValuePairs"></param>
        public abstract void Populate(Document keyValuePairs);
    }
}
