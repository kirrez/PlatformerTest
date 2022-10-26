using System;
using UnityEngine;

namespace Platformer
{
    public interface IResourceManager
    {
        T CreatePrefab<T, E>(E type) where E : Enum;
        GameObject GetFromPool<E>(E objType) where E : Enum;
    }
}