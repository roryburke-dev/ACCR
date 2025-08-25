using System;
using System.Collections.Generic;

namespace ACCR
{
    public interface IContainer
    {
        public Dictionary<Type, IAspect> Aspects { get; set; }

        public void Initialize();

        public T GetAspect<T>() where T : struct;
                                         
        public Dictionary<Type, IAspect> GetAllAspects();
    }
}