using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace MyGame.Gameplay
{
    public class WorkVisualHandler : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI currentHp;
        [SerializeField] private TextMeshProUGUI currentScore;
        [SerializeField] private TextMeshProUGUI leftTime;
        
        [SerializeField] private GameObject scorePanel;
        [SerializeField] private TextMeshProUGUI scoreText;
        
        [SerializeField] private GameObject cabinet;
        public void UpdateVisual(Data data)
        {
            currentHp.text = data.Hp.ToString();
            currentScore.text = data.Score.ToString();
            leftTime.text = data.LeftTime.ToString();
        }

        public class Data
        {
            public int Hp;
            public int Score;
            public int LeftTime;
            public Data(int hp, int score, int leftTime)
            {
                Hp = hp;
                Score = score;
                LeftTime = leftTime;
            }
        }

        public void ShowEarn(int score)
        {
            scoreText.text = score.ToString();
            scorePanel.SetActive(true);
        }

        public void ShowCabinet(List<Item> playerDataCabinet)
        {
            cabinet.SetActive(true);
            cabinet.GetComponent<CabinetInitializer>().InitItem(playerDataCabinet);
        }
    }
}