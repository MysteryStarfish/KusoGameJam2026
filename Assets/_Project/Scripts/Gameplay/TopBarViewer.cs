using System;
using TMPro;
using UnityEngine;

namespace MyGame.Gameplay
{
    public class TopBarViewHandler : MonoBehaviour
    {
        [SerializeField] private GlobalData data;
        [SerializeField] private TextMeshProUGUI day;
        [SerializeField] private TextMeshProUGUI money;

        private void Update()
        {
            day.text = "Day: " + data.Day.ToString();
            money.text = "Money: " + data.PlayerData.Money.ToString();
        }
    }
}
