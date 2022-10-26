using System;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class ResourceManager : IResourceManager
    {
        private List<PoolItem> ObjectPool = new List<PoolItem>();

        public T CreatePrefab<T, E>(E type)
            where E : Enum
        {
            var path = type.GetType().Name + "/" + type.ToString();
            var asset = Resources.Load<GameObject>(path);
            var instance = GameObject.Instantiate(asset);
            var component = instance.GetComponent<T>();

            return component;
        }

        public GameObject GetFromPool<E>(E objType)
            where E : Enum
        {
            PoolItem unit;
            unit.type = objType.GetType();
            unit.value = objType;

            if (ObjectPool.Count > 0)
            {
                foreach (PoolItem element in ObjectPool)
                {
                    if (element.type.Equals(unit.type) && element.value.Equals(unit.value) && element.item.activeInHierarchy == false)
                    {
                        unit.item = element.item;
                        unit.item.SetActive(true);
                        return unit.item;
                    }
                }
            }

            string path = "";
            if (unit.type.Namespace != null)
            {
                path = unit.type.ToString();
                int length = unit.type.Namespace.Length;
                path = path.Remove(0, length + 1) + "/"; // removing namespace and "." char
            }
            else
            {
                path = unit.type.ToString();
            }
            path += unit.value.ToString();

            var asset = Resources.Load<GameObject>(path);
            var instance = GameObject.Instantiate(asset);

            unit.item = instance;
            ObjectPool.Add(unit);
            return unit.item;
        }
    }
}

