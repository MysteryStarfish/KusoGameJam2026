using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace MyGame.Gameplay
{
    public class ItemHandler : MonoBehaviour
    {
        [SerializeField] private Item item;
        private Image _spriteRenderer;
        private TMPro.TextMeshProUGUI _rare;
        
        private void Awake()
        {
            _spriteRenderer = GetComponentInChildren<Image>();
            _rare = GetComponentInChildren<TMPro.TextMeshProUGUI>();

            _spriteRenderer.sprite = item.Sprite;
            _rare.text = item.Rare;
        }
    }
}
