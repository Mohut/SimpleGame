using UnityEngine;

namespace DM.Player{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Transform playerTransform;
        [SerializeField] private float movementSpeed = 2;

        private InputMap inputMap;
        private void Awake()
        {
            inputMap = new InputMap();
        }
        
        private void Update()
        {
            Move();
        }
        
        private void Move()
        {
           Vector3 direction = inputMap.Player.Movement.ReadValue<Vector2>() * movementSpeed;
           playerTransform.position += direction * Time.deltaTime;
        }

        private void OnEnable()
        {
            inputMap.Enable();
        }

        private void OnDisable()
        {
            inputMap.Disable();
        }
    }
}

