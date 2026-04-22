using System.Collections.Generic;
using UnityEngine;

namespace MyGame.Gameplay
{
    public class MainVisualHandler : MonoBehaviour
    {
        [SerializeField] private GameObject Cabinet;
        public void ShowCabinet(List<Item> playerDataCabinet)
        {
            Cabinet.SetActive(true);
            Cabinet.GetComponent<CabinetInitializer>().InitItem(playerDataCabinet);
        }
    }
}
