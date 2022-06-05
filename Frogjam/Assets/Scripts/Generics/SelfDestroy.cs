using System.Collections;
using UnityEngine;

namespace Generics
{
    public class SelfDestroy : MonoBehaviour
    {
        protected IEnumerator DestroyAfterTimeAmount(GameObject gameObjectToDestroy, float seconds = 0f)
        {
            yield return new WaitForSeconds(seconds);
            Destroy(gameObjectToDestroy);
        }
    }
}