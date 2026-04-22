using System.Collections.Generic;
using UnityEngine;

namespace MyGame.Gameplay
{
    public class CabinetInitializer : MonoBehaviour
    {
        [SerializeField] private GameObject content;
        private List<SlotInitializer> _slots;

        private void Awake()
        {
            _slots = new List<SlotInitializer>(content.GetComponentsInChildren<SlotInitializer>());
        }

        public void InitItem(List<Item> item)
        {
            for (int i = 0; i < _slots.Count; i++)
            {
                Item itemToAssign = (item != null && i < item.Count) ? item[i] : null;
                _slots[i].InitItem(itemToAssign, i);
            }
        }
    }
}