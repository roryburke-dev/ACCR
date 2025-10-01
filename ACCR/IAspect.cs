namespace ACCR.ACCR
{
    public interface IAspect
    {
        public IContainer Container {get; set;}
        public T GetContainer<T>() where T : class, IContainer;
        public void Initialize<T>(IContainer container, T scriptableObject);
    }
}