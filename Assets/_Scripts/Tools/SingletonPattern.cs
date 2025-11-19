using UnityEngine;

namespace Haven.DuneBuggyRacing.Tools
{
    // File: Singleton.cs
    // Author: Adrian Caamino
    // Purpose: Reusable and extendable singleton pattern.
    // To use: just inherit the class that will use this singleton.
    // Date: 2025-10-30
    // AI usage: No AI code was generated automatically.

    public class Singleton<T> : MonoBehaviour where T : Component
    {
        protected static T _instance;
        public static bool HasInstance => _instance != null;
        public static T TryGetInstance() => HasInstance ? _instance : null;
        public static T Current => _instance;

        /// <summary>
        /// Singleton design pattern
        /// </summary>
        /// <value>The instance.</value>
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindFirstObjectByType<T>();
                    if (_instance == null)
                    {
                        GameObject obj = new GameObject();
                        obj.name = typeof(T).Name + "_AutoCreated";
                        _instance = obj.AddComponent<T>();
                    }
                }
                return _instance;
            }
        }

        /// <summary>
        /// Initialize our instance on awake
        /// </summary>
        protected virtual void Awake()
        {
            InitializeSingleton();
        }

        /// <summary>
        /// Initializes the singleton.
        /// </summary>
        protected virtual void InitializeSingleton()
        {
            if (!Application.isPlaying)
            {
                return;
            }

            _instance = this as T;
        }
        
        
    }
}