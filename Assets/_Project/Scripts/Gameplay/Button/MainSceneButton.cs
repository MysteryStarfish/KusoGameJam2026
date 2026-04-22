using UnityEngine;
using UnityEngine.UI;

namespace MyGame.Gameplay
{
    public class MainSceneButton : MonoBehaviour
    {
        private void Awake()
        {
            var button =  GetComponent<Button>();
        
            if (button != null)
            {
                button.onClick.AddListener(() => SceneHandler.Instance.LoadMainScene());
            }
        }
    }
}