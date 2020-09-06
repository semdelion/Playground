namespace Semdelion.DAL.Helpers.Settings
{
    using Newtonsoft.Json;
    using Semdelion.DAL.Helpers.Interfaces;

    public class Environment : IEnvironment
    {
        public string BaseUrl { get; internal set; }

        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
    }
}
