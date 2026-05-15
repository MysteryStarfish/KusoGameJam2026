using UnityEngine;

namespace MyGame.Gameplay
{
    [CreateAssetMenu(menuName = "MyGame/Gameplay/RollDataHandler")]
    public class RollDataHandler : ScriptableObject
    {
        [SerializeField] private ItemsDataHandler itemsHandler;
        
        [Header("機率")]
        [SerializeField] private int SSRProbability;
        [SerializeField] private int SSRpProbability;
        [SerializeField] private int SSR2pProbability;
        [SerializeField] private int SSR3pProbability;
        [SerializeField] private int SSR4pProbability;
        [SerializeField] private int SSRTwelveProbability;
        private int Total => SSRProbability + 
                             SSRpProbability + 
                             SSR2pProbability + 
                             SSR3pProbability + 
                             SSR4pProbability +
                             SSRTwelveProbability;
        
        private int RareDecide(int score)
        {
            if (score < SSRProbability) return 0;
            score -= SSRProbability;
            if (score < SSRpProbability) return 1;
            score -= SSRpProbability;
            if (score < SSR2pProbability) return 2;
            score -= SSR2pProbability;
            if (score < SSR3pProbability) return 3;
            score -= SSR3pProbability;
            if (score < SSR4pProbability) return 4;
            score -= SSR4pProbability;
            if (score < SSRTwelveProbability) return 5;
            return 0;
        }
        
        public Item Roll()
        {
            int score = Random.Range(0, Total);
            int rare = RareDecide(score);
            return itemsHandler.GetRareItem(rare);
        }

        public Item RollFirstSSR()
        {
            return itemsHandler.GetItem(0);
        }
        
        public Item RollFood()
        {
            return itemsHandler.GetRareItem(2 );
        }
    }
}