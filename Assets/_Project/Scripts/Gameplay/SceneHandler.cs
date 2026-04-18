using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyGame.Gameplay
{
    public class SceneHandler : MonoBehaviour
    {
        [SerializeField] string mainSceneName;
        [SerializeField] string shopSceneName;
        [SerializeField] string workSceneName;
        [SerializeField] string bossSceneName;
        
        public static SceneHandler Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject); 
            }
            else
            {
                Destroy(gameObject); 
            }
        }
        private void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        public void LoadMainScene()
        {
            LoadScene(mainSceneName);
        }
        
        public void LoadShopScene()
        {
            LoadScene(shopSceneName);
        }
        
        public void LoadWorkScene()
        {
            LoadScene(workSceneName);
        }
        
        public void LoadBossScene()
        {
            LoadScene(bossSceneName);
        }
        
    }
}
