using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

namespace MyGame.Gameplay
{
    public class TypingHandler : MonoBehaviour
    {
        enum State
        {
            None,
            Correct,
            Incorrect,
        }

        private Color[] StateColor = {
            Color.black, Color.mediumSpringGreen, Color.orangeRed
        };
        
        List<TextMeshProUGUI> _texts;
        private RandomWordGenerator _wordGenerator;
        private State[] _correct;
        private int _index;
        private int _len;

        private String _ans;
        void Awake()
        {
            _wordGenerator = new RandomWordGenerator();
            _texts = new List<TextMeshProUGUI>(GetComponentsInChildren<TextMeshProUGUI>());
            _len = _texts.Count;
            _correct = new State[_texts.Count];
            GenerateWord();
        }

        private void GenerateWord()
        {
            _index = 0;
            _ans = _wordGenerator.generateWord(_len);
            for (int i = 0; i < _len; i++)
            {
                _correct[i] = State.None;
                _texts[i].text = _ans[i].ToString();
            }
            Visual();
        }
        private void Visual()
        {
            for (int i = 0; i < _len; i++)
            {
                _texts[i].color = StateColor[(int)_correct[i]];
            }
        }
        private void Visual(int index)
        {
            _texts[index].color = StateColor[(int)_correct[index]];
        }
        
        void OnEnable()
        {
            Keyboard.current.onTextInput += OnTyped;
        }

        void OnDisable()
        {
            Keyboard.current.onTextInput -= OnTyped;
        }

        void OnTyped(char key)
        {
            if (!(key is >= 'a' and <= 'z')) return;
            _correct[_index] = _ans[_index] == key ? State.Correct : State.Incorrect;
            Visual(_index);
            _index++;
            if (!(_index >= _ans.Length)) return;
            GenerateWord();
        }
    }
}
