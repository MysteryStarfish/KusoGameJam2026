using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MyGame.Gameplay
{
    public class Typewriter : MonoBehaviour
    {
        [Header("UI 參照")]
        [SerializeField] private Image panel;
        [SerializeField] private TextMeshProUGUI uiText;

        [Header("打字設定")]
        [Tooltip("每個字元出現的間隔時間 (秒)")]
        [SerializeField] private float timePerChar = 0.05f; 

        private Coroutine typingCoroutine;
        private Action currentOnComplete;

        public void Hide()
        {
            panel.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            uiText.text = "";
        }
        
        // 將 isTyping 開放成唯讀屬性，讓 TextManager 可以知道狀態
        public bool IsTyping { get; private set; } 
        
        private void Awake()
        {
            Hide();
        }

        public void PlayText(string content, Action onComplete = null)
        {
            currentOnComplete = onComplete;
            
            // 修正：給一個固定的淡入時間 (例如 0.3秒)，並使用 DOFade
            panel.DOFade(1f, 0.3f);

            uiText.text = content;
            uiText.maxVisibleCharacters = 0;

            if (typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine);
            }

            typingCoroutine = StartCoroutine(TypeTextCoroutine());
        }

        private IEnumerator TypeTextCoroutine()
        {
            IsTyping = true;
            uiText.ForceMeshUpdate();
            int totalCharacters = uiText.textInfo.characterCount;
            int visibleCount = 0;

            while (visibleCount < totalCharacters)
            {
                visibleCount++;
                uiText.maxVisibleCharacters = visibleCount;
                yield return new WaitForSeconds(timePerChar);
            }

            IsTyping = false;
            currentOnComplete?.Invoke();
        }

        public void SkipToFullText()
        {
            if (IsTyping)
            {
                if (typingCoroutine != null) StopCoroutine(typingCoroutine);
                
                uiText.ForceMeshUpdate();
                uiText.maxVisibleCharacters = uiText.textInfo.characterCount;
                
                IsTyping = false;
                currentOnComplete?.Invoke();
            }
        }
    }
}
