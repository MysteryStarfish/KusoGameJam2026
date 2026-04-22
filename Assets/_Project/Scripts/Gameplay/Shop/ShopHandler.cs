using System.Collections.Generic;
using UnityEngine;

namespace MyGame.Gameplay
{
    public class ShopHandler : MonoBehaviour
    {
        [SerializeField] private GlobalData _globalData;
        [SerializeField] private Typewriter typewriterFirstDay;
        [SerializeField] private Typewriter typewriter;
        [SerializeField] private TextManager textManager;
        [SerializeField] PlayerData _playerData;
        [SerializeField] RollDataHandler _normalRollHandler;
        [SerializeField] RollDataHandler _rareRollHandler;
        [SerializeField] RollDataHandler _superRollHandler;
        [SerializeField] ItemsDataHandler _itemsDataHandler;
        [SerializeField] ShopViewer _shopViewer;
        [SerializeField] GameObject _blockPanel;
        [SerializeField] GameObject _dialoguePanel;
        [SerializeField] GameObject _firstRollButton;
        [SerializeField] GameObject _teachBinButton;
        [SerializeField] GameObject _exitBinButton;
        
        [SerializeField] AudioClip _buyEffect;
        [SerializeField] AudioClip _homeEffect;
        [SerializeField] AudioClip _trashEffect;
        [SerializeField] AudioClip _rollEffect;
        [SerializeField] AudioClip _horseEffect;
        [SerializeField] AudioClip _horseNoEffect;

        [SerializeField] private int normalRollCost;
        [SerializeField] private int rareRollCost;
        [SerializeField] private int superRollCost;
        [SerializeField] private int hpUpgradeCost;
        [SerializeField] private int codeUpgradeCost;
        [SerializeField] private int efficientUpgradeCost;

        private int currentRollId = -1;
        private int currentRoll = 0;

        private int selectBinIdx = -1;

        private bool isFirstRoll = true;
        private bool isFoodRoll = false;
        
        public void FirstDayHorse()
        {
            textManager.SetTypewriter(typewriterFirstDay);
            _blockPanel.SetActive(true);
            _dialoguePanel.SetActive(true);
            _firstRollButton.SetActive(false);
            string[] merchantLines = 
            {
                "馬老闆：嘶~~這裡就是大馬商店",
                "馬老闆：欸?強制推銷?沒有的事嘶~~",
                "馬老闆：剛剛名稱是\"???\"，可不是什馬馬老闆嘶",
                "馬老闆：別生氣，看在我們挺投緣的份上，就送你一個\"必出 SSR\"的模型盲盒嘶！"
            };

            textManager.StartDialogue(merchantLines, () => 
            {
                _firstRollButton.SetActive(true);
            });
        }
        
        private void FirstDayHorse2()
        {
            _firstRollButton.SetActive(false);
            string[] merchantLines = 
            {
                "馬老闆：嗯?歐歐，你抽到 SSR 了，真是好運氣嘶",
                "馬老闆：我先幫你收到右下角的暫存區嘶~~",
                "馬老闆：如果你想把祂帶走，直接點擊右下方的暫存區嘶"
            };
            textManager.StartDialogue(merchantLines, () => 
            {
                _teachBinButton.SetActive(true);
            });
        }
        
        public void FirstDayHorse3()
        {
            _exitBinButton.SetActive(false);
            string[] merchantLines = 
            {
                "馬老闆：你可以在右邊回收區選擇你要的模型嘶",
                "馬老闆：如果你想要，可以移動到左邊的收藏區嘶（點擊一個右邊物品後，點擊左邊任意格子即可交換或移動）",
                "馬老闆：如果你不帶走，我會直接幫你收購，heheeee~~~",
                "馬老闆：挑好了就按左下角返回商店繼續抽吧，嘶~~"
            };
            textManager.StartDialogue(merchantLines, () => 
            {
                _exitBinButton.SetActive(true);
            });
        }

        public void FirstDayHorseFinished()
        {
            _blockPanel.SetActive(false);
            _dialoguePanel.SetActive(false);
            textManager.SetTypewriter(typewriter);
        }
        
        private void Start()
        {
            if (_globalData.Day == 1)
            {
                FirstDayHorse();
            }
        }
        
        public void RollFirstSSRItem()
        {
            Item item = _normalRollHandler.RollFirstSSR();
            currentRollId = _itemsDataHandler.GetItemId(item);
            _shopViewer.ShowRollItem(item);
            _firstRollButton.SetActive(false);
            _blockPanel.SetActive(false);
            OnRoll();
        }
        
        public void RollFood()
        {
            if (!DecreaseMoney(superRollCost)) return;
            Item item = _normalRollHandler.RollFood();
            currentRollId = _itemsDataHandler.GetItemId(item);
            _shopViewer.ShowRollItem(item);
            isFoodRoll = true;
            OnClickItem();
            OnRoll();
        }

        public void RollNormalItem()
        {
            if (!DecreaseMoney(normalRollCost)) return;
            Item item = _normalRollHandler.Roll();
            currentRollId = _itemsDataHandler.GetItemId(item);
            _shopViewer.ShowRollItem(item);
            OnClickItem();
            OnRoll();
        }
        public void RollRareItem()
        {
            if (!DecreaseMoney(rareRollCost)) return;
            Item item = _rareRollHandler.Roll();
            currentRollId = _itemsDataHandler.GetItemId(item);
            _shopViewer.ShowRollItem(item);
            OnClickItem();
            OnRoll();
        }
        public void RollSuperItem()
        {
            if (!DecreaseMoney(superRollCost)) return;
            Item item = _superRollHandler.Roll();
            currentRollId = _itemsDataHandler.GetItemId(item);
            _shopViewer.ShowRollItem(item);
            OnRoll();
        }

        private void OnNotEnoughMoney()
        {
            textManager.Show("你的錢不夠啦=厶=");
            OnBuyCancel();
        }

        public void FinishedRoll()
        {
            if (isFoodRoll)
            {
                SceneHandler.Instance.LoadFoodEndScene();
            }
            _shopViewer.OffRollItem();
            _playerData.Throw(currentRollId);
            if (_globalData.Day == 1)
            {
                _blockPanel.SetActive(true);
                FirstDayHorse2();
            }

            AudioManager.Instance.Stop();
        }

        private List<Item> IdsToItems(List<int> ids)
        {
            List<Item> result = new List<Item>();
            foreach (int id in ids)
            {
                result.Add(_itemsDataHandler.GetItem(id)); 
            }
            return result;
        }
        public void ShowCabinet()
        {
            _shopViewer.ShowCabinet(IdsToItems(_playerData.Collect));
        }
        public void OffCabinet()
        {
            _shopViewer.OffCabinet();
        }

        public void SelectBinSlot(SlotInitializer slot)
        {
            if (slot.Index >= _playerData.Bin.Count || slot.Index < 0) return; 
            selectBinIdx = slot.Index;
            ShowCabinet();
        }
        public void ChangeCabinetToBin(int idx)
        {
            if (selectBinIdx < 0 || selectBinIdx >= _playerData.Bin.Count) return;

            int binItemId = _playerData.Bin[selectBinIdx];
            int cabinetItemId = _playerData.Collect.Count <= idx ? -1 : _playerData.Collect[idx];

            if (cabinetItemId >= 0)
            {
                _playerData.ThrowAt(selectBinIdx, cabinetItemId);
                _playerData.StoreAt(idx, binItemId);
            }
            else 
            {
                _playerData.RemoveFromBin(selectBinIdx);
                _playerData.Store(binItemId);
            }
    
            ShowCabinet();
            UpdateBin();
            selectBinIdx = -1;
        }
        public void UpdateBin()
        {
            _shopViewer.UpdateBin(IdsToItems(_playerData.Bin));
        }
        public void ShowAndUpdateBin()
        {
            ShowBin();
            _shopViewer.UpdateBin(IdsToItems(_playerData.Bin));
        }
        public void ShowBin()
        {
            _shopViewer.ShowBin();
        }
        public void OffBin()
        {
            _shopViewer.OffBin();
            if (_globalData.Day == 1) FirstDayHorseFinished();
        }
        public void UpgradePlayerHp()
        {
            if (!DecreaseMoney(hpUpgradeCost)) return;
            _playerData.UpgradeHp();
            OnClickItem();
        }
        public void UpgradePlayerBarcodeStage()
        {
            if (_playerData.BarcodeStage >= 3)
            {
                textManager.Show("嘶~~難怪要買腦子=厶=");
                OnBuyCancel();
                return;
            }
            if (!DecreaseMoney(codeUpgradeCost)) return;
            _playerData.UpgradeBarcodeStage();
            OnClickItem();
        }
        public void UpgradePlayerEfficient()
        {
            if (_playerData.EfficientLevel >= 4)
            {
                textManager.Show("嘶~~貪心的傢伙你還點=厶=");
                OnBuyCancel();
                return;
            }
            if (!DecreaseMoney(efficientUpgradeCost)) return;
            _playerData.UpgradeEfficient();
            OnClickItem();
        }
        public void Recycle()
        {
            int earn = 0;
            foreach (var i in _playerData.Bin)
            {
                earn += _itemsDataHandler.GetItem(i).Value;
            }
            _playerData.Bin.Clear();
            _playerData.GetMoney(earn);
        }
        private bool DecreaseMoney(int cost)
        {
            if (_playerData.Money < cost)
            {
                OnNotEnoughMoney();
                return false;
            };
            _playerData.DecreaseMoney(cost);
            return true;
        }
        
        public void ToolTipNormal()
        {
            textManager.Show("一般的盒子：50 元");
        }
        
        public void ToolTipRare()
        {
            textManager.Show("稀有的盒子：200 元");
        }
        
        public void ToolTipFood()
        {
            textManager.Show("神秘的盒子：1000 元（這個很貴別買）");
        }
        public void ToolTipHp()
        {
            textManager.Show("一顆耐心：100 元（更多失誤機會）");
        }
        
        public void ToolTipBrain()
        {
            if (_playerData.BarcodeStage >= 3) textManager.Show("已經太簡單啦");
            else textManager.Show("老闆的編馬教學：300 元（工作時文字種類更少）");
        }
        
        public void ToolTipCoin()
        {
            if (_playerData.EfficientLevel >= 4) textManager.Show("再賺你就變老闆啦");
            else textManager.Show("招財的魔法：200 元（可以提高賺錢效率）");
        }
        
        public void OnClickItem()
        {
            AudioManager.Instance.PlayEffect(_buyEffect);
        }
        
        public void OnClickHome()
        {
            AudioManager.Instance.PlayEffect(_homeEffect);
        }
        
        public void OnClickTrash()
        {
            AudioManager.Instance.PlayEffect(_trashEffect);
        }
        
        public void OnRoll()
        {
            AudioManager.Instance.Play(_rollEffect);
        }
        
        public void OnClickPanel()
        {
            AudioManager.Instance.PlayEffect(_horseEffect);
        }
        
        public void OnBuyCancel()
        {
            AudioManager.Instance.PlayEffect(_horseNoEffect);
        }
    }
}