using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Events
{
    public class GameEventListener : MonoBehaviour
    {
        public List<GameEvent> GameEvents = new List<GameEvent>();

        private void OnEnable()
        {
            if(GameEvents.Count == 0) return;
            foreach (var e in GameEvents.Where(e => e != null))
            {
                if (e.Event != null)
                {
                    e.Event.RegisterListener(e);
                }
            }
        }

        private void OnDisable()
        {
            if(GameEvents.Count == 0) return;
            foreach (var e in GameEvents.Where(e => e != null))
            {
                if (e.Event != null)
                {
                    e.Event.RegisterListener(e);
                }
            }
        }
    }
    
    [System.Serializable]
    public class GameEvent 
    {
        [Tooltip("Event to register with.")]
        public GameEventData Event;
 
        [Tooltip("Response to invoke when Event is raised.")]
        public UnityEvent Response;

        public void OnEventRaised()
        {
            Response.Invoke();
        }
    }
    
}