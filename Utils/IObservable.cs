using System.Collections.Generic;

namespace ACCR.Utils
{
    public interface IObservable
    {
        public List<IObserver> Observers { get; set; }
        public void Register(IObserver observer);
        public void Unregister(IObserver observer);
        public void Notify<T>(Info<T> info);
    }
}