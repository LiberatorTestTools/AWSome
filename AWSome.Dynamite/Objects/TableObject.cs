using Amazon.DynamoDBv2.DocumentModel;

namespace Liberator.AWSome.Dynamite.Objects
{
    /// <summary>
    /// Abstract basis for a table object
    /// </summary>
    public abstract class TableObject
    {
        /// <summary>
        /// Gets the document that represent the Dynamo DB row
        /// </summary>
        /// <returns>A document representing a Dynamo DB row</returns>
        public abstract Document GetDocument();

        /// <summary>
        /// Populates a dynamo request with the contents of a document
        /// </summary>
        /// <param name="keyValuePairs">The document object (as key value pairs)</param>
        public abstract void Populate(Document keyValuePairs);
    }
}
