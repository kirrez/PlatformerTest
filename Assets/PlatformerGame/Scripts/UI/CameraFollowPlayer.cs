using UnityEngine;

namespace Platformer
{
    public class CameraFollowPlayer : MonoBehaviour
    {
        public Transform Target;

        private void Update()
        {
            Vector3 newPosition = new Vector3(Target.position.x, Target.position.y, -10f);
            transform.position = newPosition;
        }
    }
}