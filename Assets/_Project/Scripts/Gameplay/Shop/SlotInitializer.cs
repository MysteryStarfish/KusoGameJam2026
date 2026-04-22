using UnityEngine;
using UnityEngine.UI;

namespace MyGame.Gameplay
{
    public class SlotInitializer : MonoBehaviour
    {
        [SerializeField] private Image img;
        [SerializeField] private Image init;
        private Item _item;
        private int _index;
        public int Index => _index;

        public void InitItem(Item item, int index)
        {
            if (item == null)
            {
                img.sprite = init.sprite;
                img.color = init.color;
                return;
            }
            _index = index;
            _item = item;
            img.sprite = item.Sprite;
            img.SetNativeSize();
            img.color = Color.white;
            img.gameObject.SetActive(true);
        }
    }
}
