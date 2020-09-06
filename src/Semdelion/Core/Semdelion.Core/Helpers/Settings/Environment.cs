namespace Semdelion.Core.Helpers.Settings
{
    using Newtonsoft.Json;
    using Semdelion.Core.Helpers.Interfaces;

    public class Environment : IEnvironment
    {
        public string BaseUrl { get; internal set; }

        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
    }
}
