using System;
using System.Collections.Generic;
using Utils;

namespace ACCR
{
    public interface ICoordinator : IObserver, IStateMachine
    {
        public List<IContainer> Containers { get; }
        public IContainer GetContainer<T>() where T :  IContainer;
        public List<IContainer> GetContainers<T>() where T :  IContainer;
        public void Initialize();
    }
}
