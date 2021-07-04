using System;
using Generic;
using UnityEngine;

namespace Mechanics
{
    public class EnemyBlock : MonoBehaviour
    {
        public event Action<EnemyBlock> OnBallHit;

        public bool Destroyed { get; private set; }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag(Constants.BallTag))
            {
                Destroyed = true;
                gameObject.SetActive(false);
                OnBallHit?.Invoke(this);
            }
        }

        public void ResetBlock()
        {
            Destroyed = false;
            gameObject.SetActive(true);
        }
    }
}