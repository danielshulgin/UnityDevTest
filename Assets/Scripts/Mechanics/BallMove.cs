using System;
using Generic;
using UnityEngine;

namespace Mechanics
{
    [RequireComponent(typeof(Rigidbody))]
    public class BallMove : MonoBehaviour
    {
        public event Action OnEnemyHit;
        
        public event Action OnEnemyBlockHit;
                
        [SerializeField] private float collisionRandomizationForce = 2f;
        
        private Rigidbody _rigidbody;

        private float _speed = 0f;

        public bool Launched { get; private set; }


        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag(Constants.EnemyBlockTag))
            {
                OnEnemyBlockHit?.Invoke();
                Launched = false;
            }
            else if (other.gameObject.CompareTag(Constants.EnemyTag))
            {
                OnEnemyHit?.Invoke();
                Launched = false;
            }
            _rigidbody.AddForce(UnityEngine.Random.insideUnitSphere * collisionRandomizationForce);
        }
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            if (Launched)
            {
                _rigidbody.velocity = _rigidbody.velocity.normalized * _speed;
            }
        }

        public void Launch(Vector3 velocity)
        {
            _rigidbody.AddForce(velocity);
            _speed = velocity.magnitude;
            Launched = true;
        }

        public void ResetBall()
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
            Launched = false;
        }
    }
}