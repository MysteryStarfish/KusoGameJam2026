using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace MyGame.Gameplay
{
    public class ItemInitializer : MonoBehaviour
    {
        [SerializeField] private Image spriteRenderer;
        [SerializeField] private TMPro.TextMeshProUGUI rare;

        public void InitItem(Item item)
        {
            spriteRenderer.sprite = item.Sprite;
            spriteRenderer.SetNativeSize();
            rare.text = item.Rare;
        }
    }
}
