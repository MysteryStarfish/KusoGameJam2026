using System;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MyGame.Gameplay
{
    public class RollHandler : MonoBehaviour
    {
        [SerializeField] private RectTransform GeneratePlace;
        [Header("機率")]
        [SerializeField] private int SSRProbability;
        [SerializeField] private int SSRpProbability;
        [SerializeField] private int SSR2pProbability;
        [SerializeField] private int SSR3pProbability;
        [SerializeField] private int SSR4pProbability;
        [SerializeField] private int SSRTrashProbability;
        private int Total => SSRProbability + 
                             SSRpProbability + 
                             SSR2pProbability + 
                             SSR3pProbability + 
                             SSR4pProbability +
                             SSRTrashProbability;
        
        [Header("SSR")]
        [SerializeField] private GameObject[] _SSRItems;
        [Header("SSR+")]
        [SerializeField] private GameObject[] _SSRpItems;
        [Header("SSR2+")]
        [SerializeField] private GameObject[] _SSR2pItems;
        [Header("SSR3+")]
        [SerializeField] private GameObject[] _SSR3pItems;
        [Header("SSR4+")]
        [SerializeField] private GameObject[] _SSR4pItems;
        [Header("SSR?")]
        [SerializeField] private GameObject[] _SSRTrashItems;
        
        private int RareDecide(int score)
        {
            if (score < SSRProbability) return 0;
            score -= SSRProbability;
            if (score < SSRpProbability) return 1;
            score -= SSRpProbability;
            if (score < SSR2pProbability) return 2;
            score -= SSR2pProbability;
            if (score < SSR3pProbability) return 3;
            score -= SSR3pProbability;
            if (score < SSR4pProbability) return 4;
            score -= SSR4pProbability;
            if (score < SSRTrashProbability) return 5;
            return 0;
        }

        public void GenerateItem()
        {
            Instantiate(Roll(), GeneratePlace);
        }
        
        private GameObject Roll()
        {
            int score = Random.Range(0, Total);
            int rare = RareDecide(score);
            return GetRareItem(rare);
        }

        private GameObject GetRareItem(int rare)
        {
            if (rare == 0) return GetSSRItem();
            if (rare == 1) return GetSSRpItem();
            if (rare == 2) return GetSSR2pItem();
            if (rare == 3) return GetSSR3pItem();
            if (rare == 4) return GetSSR4pItem();
            if (rare == 5) return GetSSRTrashItem();
            return GetSSRItem();
        }
        
        private GameObject GetSSRItem()
        {
            return _SSRItems[Random.Range(0, _SSRItems.Length)];
        }

        private GameObject GetSSRpItem()
        {
            return _SSRpItems[Random.Range(0, _SSRpItems.Length)];
        }

        private GameObject GetSSR2pItem()
        {
            return _SSR2pItems[Random.Range(0, _SSR2pItems.Length)];
        }

        private GameObject GetSSR3pItem()
        {
            return _SSR3pItems[Random.Range(0, _SSR3pItems.Length)];
        }

        private GameObject GetSSR4pItem()
        {
            return _SSR4pItems[Random.Range(0, _SSR4pItems.Length)];
        }

        private GameObject GetSSRTrashItem()
        {
            return _SSRTrashItems[Random.Range(0, _SSRTrashItems.Length)];
        }
    }
}
