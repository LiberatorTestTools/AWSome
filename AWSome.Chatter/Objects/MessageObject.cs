using System.Xml.Serialization;

namespace Liberator.AWSome.Chatter.Objects
{
    /// <summary>
    /// Abstract class to form the basis of Kinesis messages
    /// </summary>
    public abstract class MessageObject
    {
        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore]
        public string PartitionKey { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="PartitionKey"></param>
        protected MessageObject(string PartitionKey)
        {
            this.PartitionKey = PartitionKey;
        }
    }
}
