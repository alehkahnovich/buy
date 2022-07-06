using System;

namespace Buy.Kafka.Consumers.Attributes {
    public class TopicAttribute : Attribute {
        public string Topic { get; set; }
        public TopicAttribute(string topic) => Topic = topic;
    }
}
