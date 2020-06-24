using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    public class CharacterEventSystem : MonoBehaviour
    {
        private Dictionary<string, UnityEvent> _eventDictionary;
        private static CharacterEventSystem _eventSystem = null;
    
        // Getter to use event system. If there is no event system, create new one.
        public static CharacterEventSystem SharedInstance
        {
            get
            {
                if (!_eventSystem)
                {
                    _eventSystem = FindObjectOfType(typeof(CharacterEventSystem)) as CharacterEventSystem;

                    if (!_eventSystem)
                    {
                        Debug.LogError ("There needs to be one active EventManger script on a GameObject in your scene.");
                    }
                    else
                    {
                        _eventSystem.Initialize();
                    }
                }

                return _eventSystem;
            }
        }


        private void Initialize()
        {
            if (_eventDictionary == null)
            {
                _eventDictionary = new Dictionary<string, UnityEvent>();
            }
        }


        // Add a new listener to event with specific name.
        // If there is no event with that name, create new event with
        // that name and add listener to it.
        public static void StartListening (string eventName, UnityAction listener)
        {
            UnityEvent thisEvent = null;

            if (SharedInstance._eventDictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent.AddListener(listener);
            } 
            else
            {
                thisEvent = new UnityEvent();
                thisEvent.AddListener(listener);
                SharedInstance._eventDictionary.Add(eventName, thisEvent);
            }
        }


        // Remove listener from event with a specific name 
        // if event system was created and if event with that specific name is exist.
        public static void StopListening (string eventName, UnityAction listener)
        {
            if (_eventSystem == null)
            {
                return;
            }

            UnityEvent thisEvent = null;

            if (SharedInstance._eventDictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent.RemoveListener (listener);
            }
        }


        // Trigger event with specific name. If that event is exist.
        public static void TriggerEvent (string eventName)
        {
            UnityEvent thisEvent = null;

            if (SharedInstance._eventDictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent.Invoke ();
            }
        }
    }
}
