using System;
using System.Collections.Generic;

namespace ACCR.ACCR
{
    public interface IContainer
    {
        public string Id { get; set; }
        public Dictionary<string, IAspect> Aspects { get; set; }
        public void Initialize(string id);
        public T GetAspect<T>(string id) where T : struct, IAspect;
        public bool GetAspect<T>(string id, out T aspect) where T : struct, IAspect;
        public void UpdateAspect(IAspect aspect);
    }
}