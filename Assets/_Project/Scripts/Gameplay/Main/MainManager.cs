using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MyGame.Gameplay
{
    public class MainManager : MonoBehaviour
    {
        [SerializeField] private AudioManager audioManager;
        [SerializeField] private AudioClip shopEffect;   
        [SerializeField] private AudioClip workEffect;
        [SerializeField] private GlobalData _globalData;
        [SerializeField] private TextManager _textManager;
        [SerializeField] private MainVisualHandler _mainVisualHandler;
        [SerializeField] private GameObject Shop;
        [SerializeField] private GameObject mockShop;
        [SerializeField] private GameObject _blockPanel;
        [SerializeField] private PlayerData _playerData;
        [SerializeField] private ItemsDataHandler _itemsDataHandler;
        [SerializeField] private Typewriter trueTypewriter;
        [SerializeField] private Typewriter teachTypewriter;
        [SerializeField] private GameObject _teachDialogue;

        private void Start()
        {
            if (_globalData.Day == 0) Shop.SetActive(false);
            if (_globalData.Day == 1)
            {
                Shop.SetActive(false);
                FirstDayHorse();
            }
            else _blockPanel.SetActive(false);
            
            _mainVisualHandler.ShowCabinet(IdsToItems(_playerData.Collect));
        }
        
        public List<Item> IdsToItems(List<int> ids)
        {
            List<Item> result = new List<Item>();
            foreach (int id in ids)
            {
                result.Add(_itemsDataHandler.GetItem(id)); 
            }
            return result;
        }

        public void FirstDayHorse()
        {
            _blockPanel.SetActive(true);
            mockShop.SetActive(false);
            _textManager.SetTypewriter(teachTypewriter);
            string[] merchantLines = 
            {
                "???：嘶~~",
                "???：Hey，年輕人嘶",
                "???：「打工也太多馬了吧，這遊戲是給人玩的馬」...我猜你是這樣想的吧，嘶~~~",
                "???：（喀噠噠噠）嗯...我看看嘶（喀噠噠噠）",
                "???：呼~~透過一點簡單的編馬技巧，你之後只要輸入五位就相當於十三位的功效嘶",
                "???：喔對，我還順便幫你加裝了一個系統，不用客氣嘶~~"
            };

            _textManager.StartDialogue(merchantLines, () => 
            {
                _textManager.SetTypewriter(trueTypewriter);
                mockShop.SetActive(true);
                mockShop.GetComponentInChildren<Image>().color = new Color32(255, 255, 255, 255);
                Shop.SetActive(true);
            });
        }

        public void OnHitPanel()
        {
            _textManager.Show();
        }

        public void OnClickShop()
        {
            AudioManager.Instance.PlayEffect(shopEffect);
        }
        
        public void OnClickWork()
        {
            AudioManager.Instance.PlayEffect(workEffect);
        }
    }
}
