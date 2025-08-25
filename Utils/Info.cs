namespace Utils
{
    public abstract class Info
    {
        
    }

    public class Info<T> : Info
    {
        public T Data { get; set; }
        
        public Info(T data)
        {
            Data = data;
        }
    }
}