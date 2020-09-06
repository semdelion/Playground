namespace Semdelion.Core.Helpers
{
    using Semdelion.Core.Helpers.Interfaces;
    using System.IO;
    using YamlDotNet.Serialization;

    /// <summary>
    ///     Yaml parser.
    /// </summary>
    public class ConfiguratorParser : IConfiguratorParser
    {
        private IDeserializer _deserializer;

        public ConfiguratorParser()
        {
            _deserializer = new DeserializerBuilder().IgnoreUnmatchedProperties().Build();
        }

        /// <inheritdoc />
        public T Get<T>(string content)
        {
            T deserializedConfig;

            using (var reader = new StringReader(content))
                deserializedConfig = _deserializer.Deserialize<T>(reader);

            return deserializedConfig;
        }
    }
}
