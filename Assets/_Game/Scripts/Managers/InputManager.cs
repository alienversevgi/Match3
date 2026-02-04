using _Game.Scripts.Components;
using _Game.Signals;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace _Game.Scripts.Managers
{
    public class InputManager : GameBehaviour
    {
        [Inject(Id = "Main")] private Camera _camera;

        private PlayerInput _playerInput;
        private BoardInput _input;
        private Vector2 _dragPosition;
        private bool _isTouchDown;
        private bool _canDrag;
        private Vector3 _startTouchPosition;

        private const float DRAG_THRESHOLD = 0.25f;

        public void Start()
        {
            _playerInput = this.GetComponent<PlayerInput>();
            _input = new BoardInput();
            _input.Board.Drag.performed += OnDrag;
            _input.Board.Click.started += OnClickStarted;
            _input.Board.Click.canceled += OnClickCanceled;
            Enable();
        }

        [Button]
        public void Enable()
        {
            _input.Enable();
        }

        [Button]
        public void Disable()
        {
            _input.Disable();
        }

        private void OnClickCanceled(InputAction.CallbackContext context)
        {
            _isTouchDown = false;
            _dragPosition = Vector2.zero;
            _startTouchPosition = Vector2.zero;
        }

        private void OnClickStarted(InputAction.CallbackContext context)
        {
            Vector2 screenPos = _input.Board.Drag.ReadValue<Vector2>();

            _startTouchPosition = _camera.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, 0));

            SignalBus.Fire(new BoardSignals.Click()
            {
                Position = _startTouchPosition
            });
            
            _isTouchDown = true;
            _canDrag = true;
        }

        private void OnDrag(InputAction.CallbackContext context)
        {
            if (!_isTouchDown || !_canDrag)
                return;

            var delta = context.ReadValue<Vector2>();

            if (delta.magnitude < DRAG_THRESHOLD)
                return;

            _dragPosition = _camera.ScreenToWorldPoint(context.ReadValue<Vector2>());
            float xDiff = _dragPosition.x - _startTouchPosition.x;
            float yDiff = _dragPosition.y - _startTouchPosition.y;

            if (Mathf.Abs(yDiff) > Mathf.Abs(xDiff) && Mathf.Abs(yDiff) > DRAG_THRESHOLD)
            {
                SignalBus.Fire(new BoardSignals.Swipe()
                {
                    Position = _startTouchPosition,
                    Direction = yDiff > 0 ? new Vector2Int(0, 1) : new Vector2Int(0, -1)
                });
                _canDrag = false;
            }
            else if (Mathf.Abs(xDiff) > DRAG_THRESHOLD)
            {
                SignalBus.Fire(new BoardSignals.Swipe()
                {
                    Position = _startTouchPosition,
                    Direction = xDiff > 0 ? new Vector2Int(1, 0) : new Vector2Int(-1, 0)
                });
                _canDrag = false;
            }
        }
    }
}