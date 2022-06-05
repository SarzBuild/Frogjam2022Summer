using UnityEngine;

namespace Generics
{
    public class SelfDestroyAfterTimeAmount : SelfDestroy
    {
        [SerializeField] public float Seconds;

        private void OnEnable()
        {
            if (transform.parent != null) transform.parent = null;
            StartCoroutine(DestroyAfterTimeAmount(gameObject,Seconds));
        }
    }
}