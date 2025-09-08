using System;
using System.Collections.Generic;

namespace ACCR
{
    public interface IContainer
    {
        public string Id { get; set; }
        public Dictionary<Type, IAspect> Aspects { get; set; }

        public void Initialize(string id);

        public T GetAspect<T>() where T : struct;
                                         
        public Dictionary<Type, IAspect> GetAllAspects();
    }
}