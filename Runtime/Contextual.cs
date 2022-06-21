using UnityEngine;

namespace Aether
{
    public class Contextual<C> : MonoBehaviour where C : MonoBehaviour, IContext
    {
        private C context;

        public C Context
        {
            get
            {
                if (context == null)
                {
                    context = gameObject.GetComponentInParent<C>(true);
                }
                return context;
            }
        }
    }
}