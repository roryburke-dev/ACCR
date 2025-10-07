using System;
using System.Collections.Generic;
using ACCR.Generated;

namespace ACCR.ACCR
{
    public interface IContainer
    {
        public int Id { get; set; }
        public Dictionary<int, IAspect> Aspects { get; set; }
        public void Initialize(int id);
        public T GetAspect<T>() where T : struct, IAspect;
        public T GetAspect<T>(int id) where T : struct, IAspect;
        public bool GetAspect<T>(out T aspect) where T : struct, IAspect;
        public bool GetAspect<T>(int id, out T aspect) where T : struct, IAspect;
        public List<T> GetAspects<T>() where T : struct, IAspect;
        public void UpdateAspect(IAspect aspect);
    }
}