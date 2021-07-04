using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MyInput
{
    public class ClickManager : MonoBehaviour
    {
        public event Action<Touch> OnSwipe;
        
        public event Action<Touch> OnStartSwipe;
        
        public event Action<Touch> OnEndSwipe;
        
        public event Action OnExitButtonClick;
        
        public event Action<Vector2> OnStartClick;
        
        public event Action<Vector2> OnEndClick;
        
        [SerializeField] private float swipeThreshold = 20f;
        
        private Vector2 _fingerNow;
        
        private bool _swiping;

        private Vector2 _fingerDown;

        private float VerticalDelta => Mathf.Abs(_fingerNow.y - _fingerDown.y);
        
        private float HorizontalValDelta => Mathf.Abs(_fingerNow.x - _fingerDown.x);

        public bool Press { get; private set; }

        public Vector2 LastPosition { get; private set; }

        
        private void Awake()
        {
            LastPosition = Vector2.zero;
        }

        private void Update()
        {
    #if UNITY_EDITOR
            CheckMouseTouch();
    #else
            CheckFingerTouch();
    #endif
            
            if (Input.GetKeyDown(KeyCode.Escape) )
            {
                OnExitButtonClick?.Invoke();
            }
        }

        private void CheckFingerTouch()
        {
            if (Input.touchCount == 1)
            {
                var touch = Input.GetTouch(0);
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        if (IsPointerOverGameObject())
                        {
                            return;
                        }
                        Press = true;
                        OnStartClick?.Invoke(touch.position);
                        _fingerDown = touch.position;
                        _fingerNow = touch.position;
                        break;
                    case TouchPhase.Moved:
                        break;
                    case TouchPhase.Ended:
                        _fingerNow = touch.position;
                        CheckSwipe(touch);
                        Press = false;
                        OnEndClick?.Invoke(touch.position);
                        break;
                    case TouchPhase.Canceled:
                        _fingerNow = touch.position;
                        CheckSwipe(touch);
                        Press = false;
                        OnEndClick?.Invoke(touch.position);
                        break;
                    case TouchPhase.Stationary:
                        break;
                }

                if (Press)
                {
                    CheckSwipe(touch);
                }
            }
        }

    #if UNITY_EDITOR
        private void CheckMouseTouch()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (IsPointerOverGameObject())
                {
                    return;
                }
                
                Press = true;
                _fingerNow = _fingerDown = Input.mousePosition;
                var touch = new Touch();
                LastPosition = Input.mousePosition;
                touch.deltaPosition = (Vector2) Input.mousePosition - LastPosition;
                touch.position = _fingerNow;
                touch.phase = TouchPhase.Began;
                
                OnStartClick?.Invoke(Input.mousePosition);
                CheckSwipe(touch);
                LastPosition = Input.mousePosition;
            }
            else if (Press && (Mathf.Abs(Input.GetAxisRaw("Mouse X")) > float.Epsilon
                               || Mathf.Abs(Input.GetAxisRaw("Mouse Y")) > float.Epsilon))
            {
                _fingerNow = Input.mousePosition;
                var touch = new Touch();
                touch.deltaPosition = (Vector2) Input.mousePosition - LastPosition;
                touch.position = _fingerNow;
                touch.phase = TouchPhase.Moved;
                
                CheckSwipe(touch);
                LastPosition = Input.mousePosition;
            }

            if (Input.GetMouseButtonUp(0))
            {
                OnEndClick?.Invoke(Input.mousePosition);
                _fingerNow = Input.mousePosition;
                Press = false;
                var touch = new Touch();
                touch.deltaPosition = (Vector2) Input.mousePosition - LastPosition;
                touch.position = _fingerNow;
                touch.phase = TouchPhase.Ended;
                CheckSwipe(touch);
            }
        }
    #endif


        private void CheckSwipe(Touch touch)
        {
            _fingerNow = touch.position;
            
            if (!_swiping && touch.phase == TouchPhase.Began)
            {
                OnStartSwipe?.Invoke(touch);
                OnSwipe?.Invoke(touch);
                _swiping = true;
            }
            else
            {
                if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary )
                {
                    OnSwipe?.Invoke(touch);
                }
                else
                {
                    OnEndSwipe?.Invoke(touch);
                    _swiping = false;
                }
            }
        }

        public static bool IsPointerOverGameObject()
        {
            // Check mouse
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return true;
            }
     
            // Check touches
            for (int i = 0; i < Input.touchCount; i++)
            {
                var touch = Input.GetTouch(i);
                if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                {
                    return true;
                }
            }
            return false;
        }
    }
}