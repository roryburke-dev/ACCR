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
        
    }
    public delegate void ResolverEventHandler(object sender, EventArgs e, IAspect aspect, Info info);
}