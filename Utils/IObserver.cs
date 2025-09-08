namespace ACCR.Utils
{
    public interface IObserver
    {
        public Info<T> OnNotify<T>(Info<T> info);
    }
}