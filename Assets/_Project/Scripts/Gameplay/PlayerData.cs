using System;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame.Gameplay
{
    [CreateAssetMenu(menuName = "MyGame/Gameplay/PlayerData")]
    public class PlayerData : ScriptableObject
    {
        [SerializeField] private int money;
        public int Money => money;
        [SerializeField] private int day;
        public int Day => day;
        
        [SerializeField] private int barcodeLenght;
        public int BarcodeLenght => barcodeLenght;
        [SerializeField] private int barcodeStage;
        public int BarcodeStage => barcodeStage;
        [SerializeField] private float efficientLevel = 1;
        public float EfficientLevel => efficientLevel;
        
        [SerializeField] private int maxHp = 3;
        public int MaxHp => maxHp;
        [SerializeField] private int currentHp;
        public int CurrentHp => currentHp;
        public bool IsDead => currentHp <= 0;

        [SerializeField] private int collectCapacity = 12;
        
        public List<int> Collect = new ();
        public List<int> Bin = new ();
        
        private void Init()
        {
            Collect.Clear();
            Bin.Clear();
        }
        
        public void Store(int id)
        {
            if (Collect.Count >= collectCapacity)
            {
                Throw(id);
                return;
            }
            Collect.Add(id);
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }
        public void StoreAt(int idx, int id)
        {
            Debug.Log($"[PlayerData] {idx}");
            Debug.Log($"[PlayerData] {Collect.Count}");
            Collect[idx] = id;
        }
        public void RemoveFromCabinet(int idx)
        {
            Collect.RemoveAt(idx);
        }
        public void Throw(int id)
        {
            Bin.Add(id);
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }
        public void ThrowAt(int idx, int id)
        {
            Bin[idx] = id;
        }
        public void RemoveFromBin(int idx)
        {
            Bin.RemoveAt(idx);
        }

        public void UpgradeHp()
        {
            maxHp++;
        }

        public void UpgradeBarcodeStage()
        {
            barcodeStage++;
        }

        public void UpgradeEfficient()
        {
            efficientLevel += 0.5f;
        }

        public void DecreaseHp()
        {
            currentHp--;
        }

        public void ResetHp()
        {
            currentHp = maxHp;
        }

        public void GetMoney(int salary)
        {
            money += salary;
        }

        public void DecreaseMoney(int cost)
        {
            money -= cost;
        }
    }
}
