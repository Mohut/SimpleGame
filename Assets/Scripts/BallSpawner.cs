using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace DM.Balls
{
    public class BallSpawner : MonoBehaviour
    {
        [SerializeField] private Transform spawnerTransform;
        [SerializeField] private Transform spawnPosition;
        [SerializeField] private GameObject ballPrefab;
        [SerializeField] private int spawnRate;
        [SerializeField] private int maxBalls;
        private int currentActiveBalls;
        private bool active;
        private Vector3 rightRotation;
        private Tween rightRotationTween;
        private WaitForSeconds delay = new WaitForSeconds(0.2f);
        private List<GameObject> balls;

        private void Start()
        {
            EventManager.GameOverEvent += DeleteAllBalls;
            EventManager.GameOverEvent += SetNotActive;
            EventManager.GameStartEvent += SetActive;
            EventManager.KillEnemyEvent += ReduceCurrentActiveBalls;
            
            rightRotation = new Vector3(0, 0, 40);
            balls = new List<GameObject>();
            
            Swing();
            
            InvokeRepeating(nameof(SpawnBall), 1, spawnRate);
        }

        private void OnDestroy()
        {
            EventManager.GameOverEvent -= DeleteAllBalls;
            EventManager.GameOverEvent -= SetNotActive;
            EventManager.GameStartEvent -= SetActive;
            EventManager.KillEnemyEvent -= ReduceCurrentActiveBalls;
        }

        private void SetActive()
        {
            active = true;
        }

        private void SetNotActive()
        {
            active = false;
        }

        private void ReduceCurrentActiveBalls()
        {
            currentActiveBalls--;
        }
        
        private void Swing()
        {
            rightRotationTween = spawnerTransform.DORotate(rightRotation, 2);
            rightRotationTween.SetEase(Ease.Linear);
            rightRotationTween.SetLoops(-1, LoopType.Yoyo);
        }

        private void SpawnBall()
        {
            if (currentActiveBalls < maxBalls && active)
            {
                StartCoroutine(Co_SpawnBall());
                currentActiveBalls++;
            }
                
        }

        private void DeleteAllBalls()
        {
            for(int i = 0; i < balls.Count; i++)
                Destroy(balls[i]);

            currentActiveBalls = 0;
        }
        

        IEnumerator Co_SpawnBall()
        {
            rightRotationTween.Pause();
            GameObject newBall = Instantiate(ballPrefab, spawnPosition.position, Quaternion.identity);
            balls.Add(newBall);
            yield return delay;
            rightRotationTween.Play();
        }
    }
}

