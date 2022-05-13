using System;
using UnityEngine;

namespace DM.Balls
{
    public class Ball : MonoBehaviour
    {
        [SerializeField] private Transform ballTransform;
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private float maxSpeed;
        [SerializeField] private float speed;
        private Vector3 spawnerRootPosition;
        private Vector3 direction;

        private void OnCollisionEnter2D(Collision2D col)
        {
            direction = col.GetContact(0).normal.x == 0 ?
                new Vector3(direction.x, -direction.y, 0) :
                new Vector3(-direction.x, direction.y, 0);
        }

        private void Start()
        {
            spawnerRootPosition = new Vector3(0, 5.5f, 0);
            direction = (ballTransform.position - spawnerRootPosition).normalized * speed;
        }

        void Update()
        {
            ballTransform.position += direction * Time.deltaTime;
        }
    }
}
