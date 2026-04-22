using UnityEngine;

namespace MyGame.Gameplay
{
    public class EndManager : MonoBehaviour
    {
        [SerializeField] private TextManager _textManager;

        void Start()
        {
            _textManager.Show("你一時衝\"丼\"，買了最貴的玩具，打開後心情卻沒有想像中激\"丼\"，這碗豬排丼普通的不能再普通，吃完感到空虛，早知道就多\"丼丼\"腦子");
        }
    }
}
