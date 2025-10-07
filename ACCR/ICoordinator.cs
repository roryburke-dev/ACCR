using System.Collections.Generic;
using ACCR.Utils;
using ACCR.Generated;

namespace ACCR.ACCR
{
    public interface ICoordinator : IObserver, IStateMachine
    {
        public Dictionary<int, IContainer> Containers { get; }
        public void AddContainer(int id, IContainer container);
        public T GetContainer<T>(int id) where T : IContainer;
        public List<T> GetContainers<T>() where T : IContainer;
        public Dictionary<int, IContainer> GetAllContainers();
        public void Initialize(Dictionary<int, IContainer> containers);
    }
}
