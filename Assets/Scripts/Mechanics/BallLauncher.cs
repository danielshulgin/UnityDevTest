using MyInput;
using UnityEngine;
using Zenject;

namespace Mechanics
{
    public class BallLauncher : MonoBehaviour
    {
        [SerializeField] private float speedK = 4f;
        
        [SerializeField] private float maxLaunchDirectionMagnitude = 5f;

        [SerializeField] private float touchRayDistance = 100f;
        
        [SerializeField] private LayerMask touchRayMask;
        
        [SerializeField] private Transform ballStartTransform;
        
        [SerializeField] private Transform playerStartTransform;
        
        [SerializeField] private Ball ball;
        
        [SerializeField] private GameObject playerGameObject;
        
        [SerializeField] private Arrow arrow;
        
        private ClickManager _clickManager;
        
        private Camera _mainCamera;
        
        private bool _swiping;
        
        
        [Inject]
        private void Resolve(ClickManager clickManager, Camera mainCamera)
        {
            _mainCamera = mainCamera;
            _clickManager = clickManager;
            _clickManager.OnStartSwipe += HandleStartSwipe;
            _clickManager.OnSwipe += HandleSwipe;
            _clickManager.OnEndSwipe += HandleEndSwipe;
        }

        private void Awake()
        {
            ball.OnEnemyHit += HandleBallHit;
            ball.OnEnemyBlockHit += HandleBallHit;
        }

        private void OnDestroy()
        {
            _clickManager.OnStartSwipe -= HandleStartSwipe;
            _clickManager.OnSwipe -= HandleSwipe;
            _clickManager.OnEndSwipe -= HandleEndSwipe;
        }

        private void HandleStartSwipe(Touch touch)
        {
            if (ball.Launched) return;

            if (TouchToFieldPosition(touch, out var fieldPosition))
            {
                _swiping = true;
                arrow.gameObject.SetActive(true);
            }
        }
        
        private void HandleSwipe(Touch touch)
        {
            if (!_swiping && ball.Launched) return;
            
            if (TouchToFieldPosition(touch, out var fieldPosition))
            {
                fieldPosition = ClampFieldPosition(fieldPosition);
                var playerPosition = new Vector3(fieldPosition.x, playerStartTransform.position.y, fieldPosition.z);
                playerGameObject.transform.position = playerPosition;
                arrow.gameObject.SetActive(true);
                var swipeDirection = fieldPosition - ballStartTransform.position;
                arrow.UpdatePosition(playerPosition, swipeDirection);
            }
        }

        private void HandleEndSwipe(Touch touch)
        {
            if (!_swiping && ball.Launched) return;
            
            if (TouchToFieldPosition(touch, out var fieldPosition))
            {
                fieldPosition = ClampFieldPosition(fieldPosition);
                var swipeDirection = fieldPosition - ballStartTransform.position;
                ball.Launch(swipeDirection * speedK);
                _swiping = false;
                arrow.gameObject.SetActive(false);
                ResetPlayer();
            }
        }

        private void HandleBallHit()
        {
            ResetPlayer();
            ResetBall();
        }
        
        private void ResetPlayer()
        {
            playerGameObject.transform.position = playerStartTransform.position;
        }

        private void ResetBall()
        {
            ball.ResetBall();
            ball.transform.position = ballStartTransform.position;
            ball.transform.rotation = ballStartTransform.rotation;
        }

        private Vector3 ClampFieldPosition(Vector3 fieldPosition)
        {
            var ballStartPosition = ballStartTransform.position;
            if (fieldPosition.z < ballStartPosition.z)
            {
                fieldPosition.z = ballStartPosition.z;
            }
            var direction = fieldPosition - ballStartPosition;
            var directionMagnitude = direction.magnitude;
            
            return ballStartPosition + direction.normalized 
                * Mathf.Clamp(directionMagnitude, 0f, maxLaunchDirectionMagnitude);
        }

        private bool TouchToFieldPosition(Touch touch, out Vector3 touchFieldPosition)
        {
            var ray = _mainCamera.ScreenPointToRay(touch.position);

            if (Physics.Raycast(ray, out var raycastHit, touchRayDistance, touchRayMask))
            {
                touchFieldPosition = raycastHit.point;
                return true;
            }
            touchFieldPosition = Vector3.zero;
            return false;
        }
    }
}