﻿using UnityEngine;

namespace Script.Generics
{
    public class SingletonMonoBehaviour<T> : MonoBehaviour where T : SingletonMonoBehaviour<T>
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (Instantiated) return _instance;

                if (_instance == null)
                {
                    _instance = (T)FindObjectOfType(typeof(T));

                    if (_instance == null)
                    {
                        var gameObject = new GameObject(typeof(T).ToString());
                        _instance = gameObject.AddComponent<T>();
                    }
                }

                Instantiated = true;

                return _instance;
            }
        }

        public static bool Instantiated { get; private set; }


        protected virtual void Awake()
        {
            if (_instance == null)
            {
                _instance = (T)this;
                Instantiated = true;
            }

            else if (_instance != this)
            {
                Debug.LogWarning("Singleton " + typeof(T) + " is already exists.");
                Destroy(this);
            }
        }

        protected virtual void OnDestroy()
        {
            _instance = null;
            Instantiated = false;
        }
    }
}