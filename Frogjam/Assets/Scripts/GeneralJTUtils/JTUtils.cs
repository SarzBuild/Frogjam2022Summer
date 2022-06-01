using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace GeneralJTUtils
{
    /// <summary>
    /// This namespace is full of random functionalities that may find themselves useful
    /// instead of creating function with single purpose in classes that doesn't match.
    /// </summary>
    public class JTUtils
    {
        /// <summary>
        /// <see cref="ToggleObjects"/> is a function that is used to enable or disable <param name="objects"></param>
        /// based on <param name="toggle"></param> value. 
        /// </summary>
        public static void ToggleObjects(List<GameObject> objects, bool toggle)
        {
            for (int i = 0; i < objects.Count; i++)
            {
                objects[i].SetActive(toggle);
            }
        }
        
        /// <summary>
        /// <see cref="CheckForCooldownTime"/> is a function that is used to calculate if a cooldown has elapsed.
        /// <param name="timeWhenEventOccured"></param> is the time when an event was raised and logged.
        /// <param name="cooldown"></param> is the time in seconds to wait for until it returns true.
        /// </summary>
        public static bool CheckForCooldownTime(float timeWhenEventOccured, float cooldown)
        {
            return (Time.time - timeWhenEventOccured + cooldown) <= 0;
        }
        
        /// <summary>
        /// <see cref="CheckIfObjectsAreInRange"/> is a function that uses to look if the distance between two vectors
        /// are smaller or equal than the input range.
        /// <param name="positions"></param> are the vectors to look the distance from.
        /// <param name="range"></param> is the range in unity units. 
        /// </summary>
        public static bool CheckIfObjectAreInRange(List<Vector3> positions, float range)
        {
            Assert.IsTrue(positions.Count < 2, "The range check called only takes two positions as parameters, extra positions in the list are discarded");
            float distance = Vector3.Distance(positions[0], positions[1]);
            return distance <= range;
        }
    }
}