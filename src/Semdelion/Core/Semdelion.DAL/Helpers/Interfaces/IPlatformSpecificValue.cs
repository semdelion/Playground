namespace Semdelion.DAL.Helpers.Interfaces
{
    public interface IPlatformSpecificValue
    {
    }

    public interface IPlatformSpecificValue<out T> : IPlatformSpecificValue
    {
        new T Value { get; }
    }
}
