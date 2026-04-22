using System.Collections.Generic;
using MyGame.Core;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MyGame.Gameplay
{
    public class WorkHandler : MonoBehaviour
    {
        [SerializeField] TypingHandler _typingHandler;
        [SerializeField] WorkVisualHandler _workVisualHandler;
        [SerializeField] PlayerData _playerData;
        [SerializeField] GlobalData _globalData;
        [SerializeField] AudioClip _correctEffect;
        [SerializeField] AudioClip _incorrectEffect;
        
        [SerializeField] private TextManager textManager;
        
        CountdownTimer _countdownTimer;
        private int score;
        [SerializeField] private ItemsDataHandler _itemsDataHandler;
        private bool isFirstType;
        private bool isTeach;
        private int lenght => _globalData.Day == 0 ? 13 : 5;
        private int workTime => _globalData.Day == 0 ? 10 : 30;
        private void Awake()
        {
            _countdownTimer = new CountdownTimer(workTime);

            _countdownTimer.OnStart += _playerData.ResetHp;
            
            _countdownTimer.OnStop += Settlement;
            _countdownTimer.OnStop += () => { Keyboard.current.onTextInput -= OnTyped; };
            
            isFirstType = true;
            if (_globalData.Day == 0) isTeach = true;
        }

        private void Start()
        {
            ShowCabinet();
            _workVisualHandler.UpdateVisual(new WorkVisualHandler.Data(_playerData.CurrentHp, score, (int)_countdownTimer.Time));
            if (_globalData.Day == 0)
            {
                ShowTeachDialog();
            }
            else
            {
                _typingHandler.Init(lenght, _playerData.BarcodeStage);
            }
        }

        private void ShowTeachDialog()
        {
            string[] merchantLines = 
            {
                "ouo：逼逼，我是小助手，想看下一句可以按 b",
                "ouo：等等會出現一系列編譯成地球語的外星文",
                "ouo：左下角會顯示翻譯的句數，右下角會顯示容許的失誤區間",
                "ouo：中間上面倒數計時的時間，打下第一個字後會開始計時",
                "ouo：時間歸零或失誤過多，今天一天的工作就結束了。工作結束將發放工資",
                "ouo：總之，準備好就開始吧！"
            };
            textManager.StartDialogue(merchantLines, () => 
            {
                _typingHandler.Init(lenght, _playerData.BarcodeStage);
                Keyboard.current.onTextInput += OnTyped;
            });
        }

        private void Settlement()
        {
            int salary = (int)(score * 10 * _playerData.EfficientLevel);
            _workVisualHandler.ShowEarn(salary);
            _playerData.GetMoney(salary);
        }

        void OnEnable()
        {
            if (_globalData.Day == 0) return;
            Keyboard.current.onTextInput += OnTyped;
        }

        void OnDisable()
        {
            Keyboard.current.onTextInput -= OnTyped;
        }

        void Update()
        {
            _countdownTimer.Tick(Time.deltaTime);
            UpdateVisual();
        }

        void DetectedType(char key)
        {
            bool missed = !_typingHandler.CheckIsNotMissed(key);

            if (missed) OnMissed();
            else OnCorrect();
                
            if (_typingHandler.IsFinished) OnWordFinished();
            if (_playerData.IsDead) _countdownTimer.Stop();
        }
        void OnTyped(char key)
        {
            if (isTeach)
            {
                isTeach = false;
                return;
            }
            if (isFirstType)
            {
                _countdownTimer.Start();
                DetectedType(key);
                textManager.Hide();
                isFirstType = false;
                return;
            }
            Debug.Log($"Typed: {key} at {Time.time}");
            DetectedType(key);
        }

        void UpdateVisual()
        {
            _workVisualHandler.UpdateVisual(new WorkVisualHandler.Data(_playerData.CurrentHp, score, (int)_countdownTimer.Time));
        }

        public void ShowCabinet()
        {
            _workVisualHandler.ShowCabinet(IdsToItems(_playerData.Collect));
        }
        
        private List<Item> IdsToItems(List<int> ids)
        {
            List<Item> result = new List<Item>();
            foreach (int id in ids)
            {
                result.Add(_itemsDataHandler.GetItem(id)); 
            }
            return result;
        }
    
        void OnWordFinished()
        {
            score++;
            _typingHandler.GenerateWord(lenght, _playerData.BarcodeStage);
        }
        
        void OnMissed()
        {
            _playerData.DecreaseHp();
            AudioManager.Instance.PlayEffect(_correctEffect);
        }

        void OnCorrect()
        {
            AudioManager.Instance.PlayEffect(_incorrectEffect);
        }
    }
}
