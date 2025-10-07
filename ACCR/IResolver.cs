using System;
using System.Collections.Generic;
using ACCR.Utils;

namespace ACCR.ACCR
{
    public interface IResolver : IObservable
    {
        public event ResolverEventHandler OnFire;
        public List<IAspect> Aspects { get; set; }
        public void AddAspect(IAspect aspect);
        public void RemoveAspect(IAspect aspect);
        public void Initialize();
        public void Fire<T>(Info<T> info);
        public void Fire<X,Y>(Info<X> infoX, Info<Y> infoY);
        public void Fire<T>(IAspect aspect, Info<T> info);
        public void Fire<T>(List<IAspect> aspects, Info<T> info);
        public void Fire<X,Y>(List<IAspect> aspects, Info<X> infoX, Info<Y> infoY);
    }
    public delegate void ResolverEventHandler(object sender, EventArgs e, IAspect aspect, Info info);
}