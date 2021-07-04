using Data;
using Mechanics;
using UnityEngine;
using Zenject;

namespace Flow
{
    public class LevelController : MonoBehaviour
    {
        public EnemyBlock leftEnemyBlock;
        
        public EnemyBlock rightEnemyBlock;
        
        public EnemyBlock middleEnemyBlock;
        
        private PlayerData _playerData;

        public bool AllBlockDestroyed => leftEnemyBlock.Destroyed && rightEnemyBlock.Destroyed && middleEnemyBlock.Destroyed;

                
        [Inject]
        private void Resolve(PlayerData playerData)
        {
            _playerData = playerData;
        }

        private void Awake()
        {
            leftEnemyBlock.ResetBlock();
            rightEnemyBlock.ResetBlock();
            middleEnemyBlock.ResetBlock();
        }

        private void OnEnable()
        {
            leftEnemyBlock.OnBallHit += HandleEnemyBlockHit;
            rightEnemyBlock.OnBallHit += HandleEnemyBlockHit;
            middleEnemyBlock.OnBallHit += HandleEnemyBlockHit;
        }
        
        private void OnDisable()
        {
            leftEnemyBlock.OnBallHit -= HandleEnemyBlockHit;
            rightEnemyBlock.OnBallHit -= HandleEnemyBlockHit;
            middleEnemyBlock.OnBallHit -= HandleEnemyBlockHit;
        }

        private void HandleEnemyBlockHit(EnemyBlock enemyBlock)
        {
            if (!AllBlockDestroyed) return;
            
            _playerData.IncreaseLevel();
            
            leftEnemyBlock.ResetBlock();
            rightEnemyBlock.ResetBlock();
            middleEnemyBlock.ResetBlock();
        }
    }
}