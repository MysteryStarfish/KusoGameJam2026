using System;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MyGame.Gameplay
{
    [CreateAssetMenu(menuName = "Scriptable Object/Items Handler")]
    public class ItemsDataHandler : ScriptableObject
    {
        [Header("SSR")]
        [SerializeField] private Item[] _SSRItems;
        [Header("SSR+")]
        [SerializeField] private Item[] _SSRpItems;
        [Header("SSR2+")]
        [SerializeField] private Item[] _SSR2pItems;
        [Header("SSR3+")]
        [SerializeField] private Item[] _SSR3pItems;
        [Header("SSR4+")]
        [SerializeField] private Item[] _SSR4pItems;
        [Header("SSR?")]
        [SerializeField] private Item[] _SSRTwelveItems;

        public int GetItemId(Item target)
        {
            int id = 0;
            for (int i = 0; i < _SSRItems.Length; i++)
            {
                if (_SSRItems[i] == target)
                {
                    return i;
                }
            }
            id += _SSRItems.Length;
            for (int i = 0; i < _SSRpItems.Length; i++)
            {
                if (_SSRpItems[i] == target)
                {
                    return id + i;
                }
            }
            id += _SSRpItems.Length;
            for (int i = 0; i < _SSR2pItems.Length; i++)
            {
                if (_SSR2pItems[i] == target) 
                {
                    return id + i;
                }
            }
            id += _SSR2pItems.Length;
            for (int i = 0; i < _SSR3pItems.Length; i++)
            {
                if (_SSR3pItems[i] == target)
                {
                    return id + i;
                }
            }
            id += _SSR3pItems.Length;
            for (int i = 0; i < _SSR4pItems.Length; i++)
            {
                if (_SSR4pItems[i] == target)
                {
                    return id + i;
                }
            }
            id += _SSR4pItems.Length;
            for (int i = 0; i < _SSRTwelveItems.Length; i++)
            {
                if (_SSRTwelveItems[i] == target)
                {
                    return id + i;
                }
            }

            throw new ArgumentException("No item found");
        }

        public Item GetItem(int id)
        {
            if (id < _SSRItems.Length) return _SSRItems[id];
            id -= _SSRItems.Length;
            if (id < _SSRpItems.Length) return _SSRpItems[id];
            id -= _SSRpItems.Length;
            if (id < _SSR2pItems.Length) return _SSR2pItems[id];
            id -= _SSR2pItems.Length;
            if (id < _SSR3pItems.Length) return _SSR3pItems[id];
            id -= _SSR3pItems.Length;
            if (id < _SSR4pItems.Length) return _SSR4pItems[id];
            id -= _SSR4pItems.Length;
            if (id < _SSRTwelveItems.Length) return _SSRTwelveItems[id];
            throw new ArgumentException("Invalid item id " + id);
        }

        public Item GetRareItem(int rare)
        {
            if (rare == 0) return GetSSRItem();
            if (rare == 1) return GetSSRpItem();
            if (rare == 2) return GetSSR2pItem();
            if (rare == 3) return GetSSR3pItem();
            if (rare == 4) return GetSSR4pItem();
            if (rare == 5) return GetSSRSpecialItem();
            return GetSSRItem();
        }
        
        private Item GetSSRItem()
        {
            return _SSRItems[Random.Range(0, _SSRItems.Length)];
        }

        private Item GetSSRpItem()
        {
            return _SSRpItems[Random.Range(0, _SSRpItems.Length)];
        }

        private Item GetSSR2pItem()
        {
            return _SSR2pItems[Random.Range(0, _SSR2pItems.Length)];
        }

        private Item GetSSR3pItem()
        {
            return _SSR3pItems[Random.Range(0, _SSR3pItems.Length)];
        }

        private Item GetSSR4pItem()
        {
            return _SSR4pItems[Random.Range(0, _SSR4pItems.Length)];
        }

        private Item GetSSRSpecialItem()
        {
            return _SSRTwelveItems[Random.Range(0, _SSRTwelveItems.Length)];
        }
    }
}
