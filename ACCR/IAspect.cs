#nullable enable

using System;
using UnityEngine;

namespace ACCR
{
    public interface IAspect
    {
        public IContainer Container {get; set;}
        public void Initialize<T>(IContainer container, T scriptableObject);
    }
}