﻿// Unite 2017 - Game Architecture with Scriptable Objects
// Author: Ryan Hipple
// Date:   10/04/17

using System.Collections.Generic;
using UnityEngine;

namespace Events
{
    [CreateAssetMenu(fileName = "newGameEvent", menuName = "Data/Events/GameEvent", order = 0)]
    public class GameEventData : ScriptableObject
    {
        private readonly List<GameEvent> _eventListeners = new List<GameEvent>();

        //Function to call to activate the event. Use 'EventName'.Raise() to call it.
        public void Raise() 
        {
            for(int i = _eventListeners.Count -1; i >= 0; i--)
                _eventListeners[i].OnEventRaised();
        }

        public void RegisterListener(GameEvent listener)
        {
            if (!_eventListeners.Contains(listener))
                _eventListeners.Add(listener);
        }

        public void UnregisterListener(GameEvent listener)
        {
            if (_eventListeners.Contains(listener))
                _eventListeners.Remove(listener);
        }
    }
}