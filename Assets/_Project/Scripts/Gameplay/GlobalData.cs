using UnityEngine;

namespace MyGame.Gameplay
{
    [CreateAssetMenu(fileName = "GlobalData", menuName = "Scriptable Objects/GlobalData")]
    public class GlobalData : ScriptableObject
    {
        [SerializeField] private PlayerData _playerData;
        public  PlayerData PlayerData => _playerData;
        [SerializeField] private int day;
        public int Day => day;

        public void IncreaseDay()
        {
            day++;
        }
    }
}
