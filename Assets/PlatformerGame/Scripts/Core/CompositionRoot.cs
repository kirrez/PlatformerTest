using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class CompositionRoot : MonoBehaviour
    {
        private static IResourceManager ResourceManager;

        public static IResourceManager GetResourceManager()
        {
            if (ResourceManager == null)
            {
                ResourceManager = new ResourceManager();
            }

            return ResourceManager;
        }

        private void OnDestroy()
        {
            ResourceManager = null;
        }
    }
}