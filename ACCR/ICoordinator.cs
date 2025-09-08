using System.Collections.Generic;
using ACCR.Utils;

namespace ACCR.ACCR
{
    public interface ICoordinator : IObserver, IStateMachine
    {
        public Dictionary<string, IContainer> Containers { get; }
        public void AddContainer(string id, IContainer container);
        public T GetContainer<T>(string id) where T : IContainer;
        public List<T> GetContainers<T>() where T : IContainer;
        public Dictionary<string, IContainer> GetAllContainers();
        public void Initialize(Dictionary<string, IContainer> containers);
    }
}
