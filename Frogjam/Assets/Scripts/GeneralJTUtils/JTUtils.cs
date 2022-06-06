﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using System.Reflection;

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
        
        public static Vector2 GetMousePositionWorld2D(Camera mainCamera)
        {
            Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            return new Vector2(mousePosition.x, mousePosition.y);
        }
        
        public static void SetAnimations(Animator animator, string active, List<string> inactive)
        {
            animator.SetBool(active,true);
            foreach (var i in inactive)
            {
                animator.SetBool(i,false);    
            }
        }

        public static void CheckForComponents(List<Component> components, Transform objectFromWhichToCheck)
        {
            for (int i = 0; i < components.Count; i++)
            {
                if (components[i] == null)
                {
                    /*string typeName = components[i].name;
                    components[i] = objectFromWhichToCheck.GetComponent(System.GetType(typeName) as components[i].ty);

                    /*components[i]
                        .GetType()
                        .GetMethod(
                            components[i].name, 
                            BindingFlags.Public | 
                            BindingFlags.Instance)
                        ?.Invoke(
                            objectFromWhichToCheck
                                .GetComponent(
                                    components[i]
                                        .GetType()), 
                            new System.Object[] {components[i]});*/

                }
            }
        }
    }
}