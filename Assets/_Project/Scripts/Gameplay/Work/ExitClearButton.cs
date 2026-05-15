using System;
using MyGame.Input;
using UnityEditor.SearchService;
using UnityEngine;

namespace MyGame.Gameplay
{
    public class ExitClearButton : MonoBehaviour
    {
        [SerializeField] InputReader inputReader;
        public event Action OnChangeSceneEvent;
        private void OnEnable()
        {
            inputReader.NextDialogEvent += OnNextDialog;
        }

        private void OnDisable()
        {
            inputReader.NextDialogEvent -= OnNextDialog;
        }

        private void OnNextDialog()
        {
            OnChangeSceneEvent?.Invoke();
        }
    }
}
