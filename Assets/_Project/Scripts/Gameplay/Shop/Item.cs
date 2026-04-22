using System;
using UnityEngine;

namespace MyGame.Gameplay
{
    [CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
    public class Item : ScriptableObject
    {
        [SerializeField] private Sprite _sprite;
        public Sprite Sprite => _sprite;
        [SerializeField] private RareType _rare;
        public String Rare => RareToChinese(_rare);
        [SerializeField] private string _name;
        public String Name => _name;
        [SerializeField] private string _description;
        public String Description => _description;
        
        [SerializeField] private int value = 0;
        public int Value => getValueByRare(_rare);

        private int getValueByRare(RareType rare)
        {
            if (rare == RareType.SSR) return 10;
            if (rare == RareType.SSRp) return 20;
            if (rare == RareType.SSR2p) return 1000;
            if (rare == RareType.SSR3p) return 100;
            if (rare == RareType.SSR4p) return 500;
            if (rare == RareType.SSRSpecial) return 0;
            return 0;
        }
        
        private string RareToChinese(RareType rare)
        {
            if (rare == RareType.SSR) return "SSR";
            else if (rare == RareType.SSRp) return "SSR 十";
            else if (rare == RareType.SSR2p) return "SSR 丼";
            else if (rare == RareType.SSR3p) return "SSR 卉";
            else if (rare == RareType.SSR4p) return "SSR 井";
            else if (rare == RareType.SSRSpecial) return "SSR 十 二";
            throw new ArgumentException("Unknown RareType");
        }
    }

    internal enum RareType
    {
        SSR,
        SSRp,
        SSR2p,
        SSR3p,
        SSR4p,
        SSRSpecial,
    }
}
