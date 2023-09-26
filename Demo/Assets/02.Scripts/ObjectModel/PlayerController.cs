using UnityEngine;

namespace ObjectModel
{
    public class PlayerController : MonoBehaviour
    {
        private Movement _movement;

        private void Awake()
        {
            _movement = GetComponent<Movement>();
        }

        private void Update()
        {
            _movement.move = new Vector3(Input.GetAxis("Horizontal"),
                                         0.0f,
                                         Input.GetAxis("Vertical")).normalized;
        }

    }
}