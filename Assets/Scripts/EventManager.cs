using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static event Action GameStartEvent;
    public static event Action PlayerGotHitEvent;
    public static event Action PillCollectedEvent;
    public static event Action KillEnemyEvent;
    public static event Action GameOverEvent;

    public static void GameStart()
    {
        GameStartEvent?.Invoke();
    }
    public static void PillCollected()
    {
        PillCollectedEvent?.Invoke();
    }

    public static void PlayerGotHit()
    {
        PlayerGotHitEvent?.Invoke();
    }

    public static void KillEnemy()
    {
        KillEnemyEvent?.Invoke();
    }

    public static void GameOver()
    {
        GameOverEvent?.Invoke();
    }
}
