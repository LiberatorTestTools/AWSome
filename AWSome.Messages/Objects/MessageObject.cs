using System.Xml.Serialization;

namespace AWSome.Messages.Objects
{
    /// <summary>
    /// Abstract class to form the basis of Kinesis messages
    /// </summary>
    public abstract class MessageObject
    {
        [XmlIgnore]
        public string PartitionKey { get; set; }

        protected MessageObject(string PartitionKey)
        {
            this.PartitionKey = PartitionKey;
        }
    }
}
