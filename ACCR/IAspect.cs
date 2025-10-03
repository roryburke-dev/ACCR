namespace ACCR.ACCR
{
    public interface IAspect
    {
        public string Id { get; set; }
        public IContainer Container { get; set; }
        public T GetContainer<T>() where T : class, IContainer;
        public void Initialize<T>(string id, IContainer container, T scriptableObject);
    }
}