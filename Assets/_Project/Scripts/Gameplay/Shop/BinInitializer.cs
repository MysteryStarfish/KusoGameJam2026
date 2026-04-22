using System.Collections.Generic;
using UnityEngine;

namespace MyGame.Gameplay
{
    public class BinInitializer : MonoBehaviour
    {
        [SerializeField] private GameObject slot;
        [SerializeField] private GameObject content;
        private List<SlotInitializer> _slots;
        private int initialSlotCount = 12; 

        private void Awake()
        {
            _slots = new List<SlotInitializer>();
            for (int i = 0; i < initialSlotCount; i++)
            {
                var s = Instantiate(slot, content.transform);
                _slots.Add(s.GetComponent<SlotInitializer>());
            }
        }

        public void InitItem(List<Item> item)
        {
            ExpandSlot(item?.Count ?? 0); 
            
            for (int i = 0; i < _slots.Count; i++)
            {
                var getItem = (item != null && i < item.Count) ? item[i] : null;
                _slots[i].InitItem(getItem, i);
            }
        }

        private void ExpandSlot(int itemCount)
        {
            while (_slots.Count < itemCount)
            {
                var s = Instantiate(slot, content.transform);
                _slots.Add(s.GetComponent<SlotInitializer>());
            }
        }
    }
}