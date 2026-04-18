using System.Collections.Generic;
using MyGame.Core;
using MyGame.Input;
using Ronin.Core;
using UnityEngine;

namespace MyGame.Gameplay
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        [Header("References")] 
        [SerializeField] private InputReader inputReader;

        [Header("Components")] 
        private Rigidbody2D _rb;

        [Header("Movement Settings")] 
        [SerializeField] private float walkSpeed = 5f;
        
        [SerializeField] private float runSpeed = 10f;
        [SerializeField] private float sneakSpeed = 2f;
        
        [Header("Dash Settings")] 
        [SerializeField] private float dashSpeed = 30f;
        [SerializeField] private float dashDuration = 0.1f;
        [SerializeField] private float dashCooldown = 0.2f;
        private HashSet<Timer> _timers;
        private CountdownTimer _dashTimer;
        private CountdownTimer _dashCooldownTimer;
        
        [Header("Detector Setting")] 
        [SerializeField] private Transform direction;
        [SerializeField] private float innerRadius = 10f;
        [SerializeField] private float radius = 10f;
        
        [Header("Attack Settings")] 
        [SerializeField] private Collider2D attackCollider;
        [SerializeField] private float startup = 0.1f;
        [SerializeField] private float strike = 0.1f;
        [SerializeField] private float recovery = 0.2f;
        [SerializeField] private float chargeTime = 1f;
        [SerializeField] private float fadeSpeed = 5f;

        public float InnerRadius => innerRadius;
        public float DetectorRadius => radius;
        
        private Vector2 _moveInput;
        private Vector2 _dashDirection;
        private bool _runSwitch = false;
        private bool _sneakSwitch = false;
        private bool _lockSwitch = false;
        private bool _isAttack = false;
        private bool _isCharge = false;
        private bool _isAim = false;
        private bool CanDash => _dashCooldownTimer.IsFinished;
        
        private PlayerMovement _player;
        private StateMachine _stateMachine;

        private void Awake()
        {
            _rb =  GetComponent<Rigidbody2D>();
            _player = new PlayerMovement(_rb, direction);
        }
        
        private void OnEnable()
        {
            inputReader.MoveEvent += OnMove;
            // inputReader.RunEvent += OnRun;
            // inputReader.SneakEvent += OnSneak;
            // inputReader.DashEvent += OnDash;
            // inputReader.LockClosestEvent += OnLockClosest;
            // inputReader.LockLeftEvent += OnLockLeft;
            // inputReader.LockRightEvent += OnLockRight;
            // inputReader.UnLockEvent += OnUnLock;
            // inputReader.AttackEvent += OnStartAttack;
            // inputReader.ChargeEvent += OnStartChargeAttack;
        }

        private void OnMove(Vector2 direction)
        {
            _player.HandleMove(direction, walkSpeed);
            _player.HandleRotation(direction);
        }

        private void OnDisable()
        {
            inputReader.MoveEvent -= OnMove;
            // inputReader.RunEvent -= OnRun;
            // inputReader.SneakEvent -= OnSneak;
            // inputReader.DashEvent -= OnDash;
            // inputReader.LockClosestEvent -= OnLockClosest;
            // inputReader.LockClosestEvent -= OnLockClosest;
            // inputReader.LockClosestEvent -= OnLockClosest;
            // inputReader.UnLockEvent -= OnUnLock;
            // inputReader.AttackEvent -= OnStartAttack;
            // inputReader.ChargeEvent -= OnStartChargeAttack;
        }

    }
    
    public class PlayerMovement
    {
        private readonly Rigidbody2D _rb;
        private readonly Transform _direction;
        
        public PlayerMovement(Rigidbody2D rb, Transform direction)
        {
            _rb = rb;
            _direction = direction;
        }
        public void HandleMove(Vector2 direction, float speed)
        {
            _rb.linearVelocity = direction * speed;
        }
        public void HandleRotation(Vector2 targetPosition)
        {
            _direction.FaceDirection2D(targetPosition);
        }
    }
}
