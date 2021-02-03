namespace Semdelion.Core.Log
{
    using Realms;

    public class LogEntry : RealmObject
    {
        public string Id { get; set; }

        public string Text { get; set; }
    }
}
