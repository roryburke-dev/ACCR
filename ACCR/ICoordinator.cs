using System;
using System.Collections.Generic;
using Utils;

namespace ACCR
{
    public interface ICoordinator : IObserver, IStateMachine
    {
        public List<IContainer> Containers { get; }
        public T GetContainer<T>(string id) where T : IContainer;
        public List<T> GetContainers<T>() where T : IContainer;
        public List<IContainer> GetAllContainers();
        public void Initialize(List<IContainer> containers);
    }
}
