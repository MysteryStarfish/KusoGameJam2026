using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace MyGame.Gameplay
{
    public class TextManager : MonoBehaviour
    {
        [SerializeField] Typewriter _typewriter;
        [SerializeField] private List<String> contents;
        private Queue<string> _dialogueQueue = new Queue<string>();
        private Action _onAllFinished;
        private bool _isWaitingForClick = false;
        private bool isTriggeredAllFinished;

        public void SetTypewriter(Typewriter typewriter)
        {
            _typewriter = typewriter;
        }
        // 外部呼叫這個方法，傳入一整串對話陣列
        public void StartDialogue(string[] dialogues, Action onAllFinished = null)
        {
            _onAllFinished = onAllFinished;
            isTriggeredAllFinished = false;
            _dialogueQueue.Clear();

            // 把所有對話塞進排隊區
            foreach (var line in dialogues)
            {
                _dialogueQueue.Enqueue(line);
            }

            // 播放第一句
            ShowNextSentence();
        }

        private void ShowNextSentence()
        {
            if (_dialogueQueue.Count > 0)
            {
                _isWaitingForClick = false;
                string nextLine = _dialogueQueue.Dequeue();
                
                // 播完字後，不馬上播下一句，而是進入「等待點擊」狀態
                _typewriter.PlayText(nextLine, () => 
                {
                    _isWaitingForClick = true;
                });
            }
            else
            {
                // 佇列空了，代表對話全數播完
                if (!isTriggeredAllFinished)
                {
                    _onAllFinished?.Invoke();
                    isTriggeredAllFinished = true;
                }
            }
        }
        
        void OnEnable()
        {
            Keyboard.current.onTextInput += OnTyped;
        }

        void OnDisable()
        {
            Keyboard.current.onTextInput -= OnTyped;
        }
        private void OnTyped(char key)
        {
            // 偵測玩家左鍵點擊 (或按下空白鍵)
            if (key == 'b')
            {
                if (_typewriter.IsTyping)
                {
                    // 狀況A：字還在打，玩家想快轉 -> 跳到全滿
                    _typewriter.SkipToFullText();
                }
                else if (_isWaitingForClick)
                {
                    // 狀況B：字已經打完，玩家點擊 -> 播下一句
                    ShowNextSentence();
                }
            }
        }

        public void Show()
        {
            var content = contents[Random.Range(0, contents.Count)];
            _typewriter.PlayText(content,() => {});
        }
        
        public void Show(string content)
        {
            _typewriter.PlayText(content,() => {});
        }
        
        public void Show(string content, Action onFinish)
        {
            _typewriter.PlayText(content, onFinish);
        }

        public void Hide()
        {
            _typewriter.Hide();
        }
    }
}
