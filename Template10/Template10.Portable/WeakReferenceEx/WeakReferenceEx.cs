﻿using System;

namespace Template10.Common
{
    public class WeakReferenceEx<TInstance, TSource> where TInstance : class
    {
        public WeakReference<TInstance> Reference { get; protected set; }
        public Action<TInstance, TSource> EventAction { get; set; }
        public Action<TInstance, WeakReferenceEx<TInstance, TSource>> DetachAction { get; set; }
        public WeakReferenceEx(TInstance instance) { Reference = new WeakReference<TInstance>(instance); }

        public virtual void Handler(TSource source)
        {
            TInstance instance;
            if (Reference != null && Reference.TryGetTarget(out instance))
            {
                EventAction?.Invoke(instance, source);
            }
            else
            {
                // Instance surely doesn't survive garbage collections, so passing null for it
                // Don't removed unnecessary delegate parameter for backward compatibility
                DetachAction?.Invoke(null, this);
            }
        }
    }
}
