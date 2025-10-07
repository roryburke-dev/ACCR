using ACCR.Generated;

namespace ACCR.ACCR
{
    public interface IAspect
    {
        public int Id { get; set; }
        public IContainer Container { get; set; }
        public T GetContainer<T>() where T : class, IContainer;
        public void Initialize<T>(int id, IContainer container, T scriptableObject);
    }
}