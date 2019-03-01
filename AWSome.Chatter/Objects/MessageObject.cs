using System.Xml.Serialization;

namespace Liberator.AWSome.Chatter.Objects
{
    /// <summary>
    /// Abstract class to form the basis of Kinesis messages
    /// </summary>
    public abstract class MessageObject
    {
        /// <summary>
        /// The partition key for the Kinesis messages
        /// </summary>
        [XmlIgnore]
        public string PartitionKey { get; set; }

        /// <summary>
        /// The message object being sent
        /// </summary>
        /// <param name="PartitionKey">The partition key to send with the message</param>
        protected MessageObject(string PartitionKey)
        {
            this.PartitionKey = PartitionKey;
        }
    }
}
