using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace MyGame.Gameplay
{
    public class ShopViewer : MonoBehaviour
    {
        [SerializeField] private RectTransform ShowPlace;
        [SerializeField] private GameObject Panel;
        [SerializeField] private GameObject Bin;
        [SerializeField] private GameObject ExitBinButton;
        [SerializeField] private GameObject Cabinet;

        [SerializeField] private GameObject itemPrefab;
        [SerializeField] private GameObject light;
        [SerializeField] private GameObject itemImage;
        [SerializeField] private int targetHeight;
        [SerializeField] private Animator RareAnimator;
        
        [SerializeField] private float SSRDelay;
        [SerializeField] private float SSRPDelay;
        [SerializeField] private float SSR3PDelay;
        [SerializeField] private float SSR4PDelay;
        [SerializeField] private float SSRSPDelay;
        [SerializeField] private float SSRTwelveDelay;
        private LayoutElement _itemImageLayout;
        private bool isFinished;
        private Sequence sequence;

        private void Awake()
        {
            _itemImageLayout =  itemImage.GetComponent<LayoutElement>();
        }

        private void ShowItemSprite()
        {
            sequence.AppendCallback(() => {
                light.SetActive(true);
            });
            var itemImageLayout =  _itemImageLayout;
            sequence.Append( DOTween.To(
                () => itemImageLayout.preferredHeight, 
                x => itemImageLayout.preferredHeight = x, 
                targetHeight, 
                0.5f
                ).SetEase(Ease.OutBack)
                );
            sequence.Join( DOTween.To(
                () => itemImage.transform.localScale, 
                x => itemImage.transform.localScale = x, 
                Vector3.one, 
                0.5f
            ).SetEase(Ease.OutBack)
            );
            sequence.OnComplete(() =>
            {
                light.SetActive(true);
                itemImageLayout.preferredHeight = -1;
                isFinished = true; 
            });
        }

        private void FinishedAnimation()
        {
            var stateInfo = RareAnimator.GetCurrentAnimatorStateInfo(0);
            RareAnimator.Play(stateInfo.fullPathHash, 0, 1f);
            RareAnimator.Update(0);
        }

        private void OffRareItem()
        {
            light.SetActive(false);
            _itemImageLayout.preferredHeight = 0;
            itemImage.transform.localScale = Vector3.zero;
        }

        private void ShowItemRare(Item item)
        {
            RareAnimator.Play(item.RareAnimation);
        }
        public void ShowRollItem(Item item)
        {
            isFinished = false;
            Panel.SetActive(true);
            itemPrefab.GetComponent<ItemInitializer>().InitItem(item);
            itemPrefab.SetActive(true);
            ShowItemRare(item);
            var delay = 2f;
            if (item.RareAnimation == "SSR") delay = SSRDelay;
            if (item.RareAnimation == "SSR_1P") delay = SSRPDelay;
            if (item.RareAnimation == "SSR_3P") delay = SSR3PDelay;
            if (item.RareAnimation == "SSR_4P") delay = SSR4PDelay;
            if (item.RareAnimation == "SSR_SP") delay = SSRSPDelay;
            if (sequence != null && sequence.IsActive()) sequence.Kill();
            sequence = DOTween.Sequence();
            sequence.AppendInterval(delay);
            ShowItemSprite();
        }

        private void SkipAnimation()
        {
            FinishedAnimation();
            sequence.Complete();
            _itemImageLayout.preferredHeight = -1;
            isFinished = true; 
        }
        public bool OffRollItem()
        {
            if (!isFinished)
            {
                SkipAnimation();
                return false;
            }
            Panel.SetActive(false);
            itemPrefab.SetActive(false);
            OffRareItem();
            return true;
        }
        public void ShowCabinet(List<Item> playerDataCabinet)
        {
            Cabinet.SetActive(true);
            Cabinet.GetComponent<CabinetInitializer>().InitItem(playerDataCabinet);
        }
        public void OffCabinet()
        {
            Cabinet.SetActive(false);
        }
        public void ShowBin()
        {
            Bin.SetActive(true);
            ExitBinButton.SetActive(true);
        }

        public void UpdateBin(List<Item> playerDataBin)
        {
            Bin.GetComponent<BinInitializer>().InitItem(playerDataBin);
        }
        public void OffBin()
        {
            Bin.SetActive(false);
            ExitBinButton.SetActive(false);
            OffCabinet();
        }
    }
}