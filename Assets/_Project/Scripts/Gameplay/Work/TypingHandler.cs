using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
            Color.white, Color.mediumSpringGreen, Color.orangeRed
        };
        [SerializeField] GameObject wordPrefab;
        List<TextMeshProUGUI> _texts;
        private RandomWordGenerator _wordGenerator;
        private State[] _correct;
        private int _index;

        private String _ans;
        public bool IsFinished => _index >= _ans.Length;

        public void Init(int len, int stage)
        {
            for (int i = 0; i < len; i++) Instantiate(wordPrefab, transform);
            _wordGenerator = new RandomWordGenerator();
            _texts = new List<TextMeshProUGUI>(GetComponentsInChildren<TextMeshProUGUI>());
            _correct = new State[_texts.Count];
            GenerateWord(len, stage);
        }
        public void GenerateWord(int lenght, int stage)
        {
            Debug.Log($"Generating word {lenght} at {stage}");
            _index = 0;
            _ans = _wordGenerator.generateWord(lenght, stage);
            for (int i = 0; i < lenght; i++)
            {
                _correct[i] = State.None;
                _texts[i].text = _ans[i].ToString();
            }
            Visual();
        }
        private void Visual()
        {
            for (int i = 0; i < _texts.Count; i++)
            {
                _texts[i].color = StateColor[(int)_correct[i]];
            }
        }
        private void Visual(int index)
        {
            _texts[index].color = StateColor[(int)_correct[index]];
        }

        public bool CheckIsNotMissed(char key)
        {
            if (!(key is >= 'a' and <= 'z')) return true;
            var res = _ans[_index] == key;
            _correct[_index] = res ? State.Correct : State.Incorrect;
            Visual(_index);
            _index++;
            return res;
        }

    }
}
