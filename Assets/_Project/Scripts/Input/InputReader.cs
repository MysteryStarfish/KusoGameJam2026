using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MyGame.Input
{
    [CreateAssetMenu(fileName = "InputReader", menuName = "Input/InputReader")]
    public class InputReader : ScriptableObject, GameAction.IUIActions
    {
        public event Action NextDialogEvent = delegate { };
        
        private GameAction _gameInput;
        
        private void OnEnable()
        {
            if (_gameInput == null)
            {
                _gameInput = new GameAction();
                _gameInput.UI.SetCallbacks(this);
            }
            
            _gameInput.UI.Enable();
        }

        private void OnDisable()
        {
            _gameInput.UI.Disable();
        }
        
        public void OnNextDialog(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                NextDialogEvent?.Invoke();
            }
        }
    }
}
