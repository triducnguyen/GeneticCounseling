using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Application utilities namespace
/// </summary>
namespace App.Utility
{

    /// <summary>Creates a lazy singleton in unity.</summary>
    /// <typeparam name="T">The type of object to create a singleton of.</typeparam>
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        /// <summary>
        /// The instance.
        /// </summary>
        private static T _instance;

        /// <summary>
        /// The instance lock.
        /// </summary>
        private static readonly object _instanceLock = new object();

        /// <summary>
        /// True if application is quitting.
        /// </summary>
        private static bool _quitting = false;

        /// <summary>
        /// Gets or sets the instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static T instance
		{
			get => _instance;
			protected set => _instance = value;
		}

		protected virtual void Awake()
		{
			CheckSingleton();
		}

		protected virtual void OnApplicationQuit()
		{
			_quitting = true;
		}

        /// <summary>
        /// Checks the singleton.
        /// </summary>
        protected void CheckSingleton()
		{
			lock (_instanceLock)
			{
				if (instance == null && !_quitting)
				{

					instance = GameObject.FindObjectOfType<T>();
					if (instance == null)
					{
						instance = gameObject.AddComponent<T>();
					}
				}
			}
		}
	}
}


