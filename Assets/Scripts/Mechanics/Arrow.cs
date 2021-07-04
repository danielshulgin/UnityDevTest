using UnityEngine;

namespace Mechanics
{
    public class Arrow : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer arrowSpriteRenderer;
        
        [SerializeField] private float lengthK;
        

        public void UpdatePosition(Vector3 startPosition, Vector3 direction)
        {
            transform.position = startPosition;
            transform.rotation = Quaternion.LookRotation(direction);
            arrowSpriteRenderer.size = new Vector2(lengthK * direction.magnitude, arrowSpriteRenderer.size.y);
        }
    }
}