using System;
using System.Collections.Generic;
using UnityEngine;
using MyGame.Input;
using Random = UnityEngine.Random;

namespace MyGame.Gameplay
{
    public class TextManager : MonoBehaviour
    {
        [SerializeField] Typewriter _typewriter;
        [SerializeField] private List<String> contents;
        [SerializeField] private InputReader _inputReader;
        public bool IsTyping => _typewriter.IsTyping;
        public bool IsDialog = false;
        private Queue<string> _dialogueQueue = new Queue<string>();
        private Action _onAllFinished;
        private bool _isWaitingForClick = false;
        private bool isTriggeredAllFinished;

        private void OnEnable()
        {
            _inputReader.NextDialogEvent += OnTyped;
        }
        void OnDisable()
        {
            _inputReader.NextDialogEvent -= OnTyped;
        }

        void Awake()
        {
            IsDialog = false;
        }
        public void SetTypewriter(Typewriter typewriter)
        {
            _typewriter = typewriter;
        }
        // 外部呼叫這個方法，傳入一整串對話陣列
        public void StartDialogue(string[] dialogues, Action onAllFinished = null)
        {
            IsDialog = true;
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
                    IsDialog = false;
                }
            }
        }
        private void OnTyped()
        {
            if (_typewriter.IsTyping)
            {
                _typewriter.SkipToFullText();
            }
            else if (_isWaitingForClick)
            {
                ShowNextSentence();
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
