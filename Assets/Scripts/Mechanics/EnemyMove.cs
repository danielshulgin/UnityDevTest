using Data;
using UnityEngine;
using Zenject;

namespace Mechanics
{
    public class EnemyMove : MonoBehaviour
    {
        [SerializeField] private Transform ballTransform;
        
        [SerializeField] private float startSpeed = 1f;
        
        [SerializeField] private float levelSpeedIncrease = 0.3f;

        private PlayerData _playerData;
        
        private float _currentSpeed;
        
        
        [Inject]
        private void Resolve(PlayerData playerData)
        {
            _playerData = playerData;
            _playerData.OnLevelChanged += HandleLevelChanged;
            HandleLevelChanged(_playerData.CurrentLevel);
        }

        private void FixedUpdate()
        {
            var currentPosition = transform.position;
            var desiredPosition = new Vector3(ballTransform.position.x, currentPosition.y, currentPosition.z);
            transform.position = Vector3.MoveTowards(currentPosition, desiredPosition, _currentSpeed * Time.fixedDeltaTime);
        }

        private void HandleLevelChanged(int currentLevel)
        {
            _currentSpeed = startSpeed + (currentLevel - 1) * levelSpeedIncrease;
        }
    }
}