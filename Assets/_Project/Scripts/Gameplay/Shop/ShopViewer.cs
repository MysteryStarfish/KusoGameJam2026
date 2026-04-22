using System.Collections.Generic;
using UnityEngine;

namespace MyGame.Gameplay
{
    public class ShopViewer : MonoBehaviour
    {
        [SerializeField] private RectTransform ShowPlace;
        [SerializeField] private GameObject Panel;
        [SerializeField] private GameObject itemPrefab;
        [SerializeField] private GameObject Bin;
        [SerializeField] private GameObject ExitBinButton;
        [SerializeField] private GameObject Cabinet;
        public void ShowRollItem(Item item)
        {
            Panel.SetActive(true);
            itemPrefab.GetComponent<ItemInitializer>().InitItem(item);
            itemPrefab.SetActive(true);
        }
        public void OffRollItem()
        {
            Panel.SetActive(false);
            itemPrefab.SetActive(false);
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