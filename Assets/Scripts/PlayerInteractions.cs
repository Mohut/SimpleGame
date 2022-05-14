using System.Collections;
using DM.Balls;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    [SerializeField] private SpriteRenderer playerSpriteRenderer;
    [SerializeField] private float invincibilityTime = 1;
    [SerializeField] private Color gotHitColor;
    [SerializeField] private Color playerColor;
    [SerializeField] private Color powerUpColor;
    [SerializeField] private PowerUpSpawner powerUpSpawner;
    [SerializeField] private BallSpawner ballSpawner;
    
    private WaitForSeconds invincibilityDelay;
    private bool hasPowerUp;
    private int lifes = 2;

    private void Start()
    {
        invincibilityDelay = new WaitForSeconds(invincibilityTime);
        
        EventManager.PillCollectedEvent += MakePlayerInvincible;
        EventManager.PlayerGotHitEvent += SubstractLife;
        EventManager.PlayerGotHitEvent += ChangePlayerAppearance;
    }

    private void OnDestroy()
    {
        EventManager.PillCollectedEvent -= MakePlayerInvincible;
        EventManager.PlayerGotHitEvent -= SubstractLife;
        EventManager.PlayerGotHitEvent -= ChangePlayerAppearance;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ball"))
        {
            if (hasPowerUp)
            {
                EventManager.KillEnemy();
                Destroy(col.gameObject);
                return;
            }

            EventManager.PlayerGotHit();
            return;
        }

        if (col.gameObject.CompareTag("PowerUp"))
        {
            EventManager.PillCollected();
            Destroy(col.gameObject);
        }
    }
    private void ChangePlayerAppearance()
    {
        playerSpriteRenderer.color = gotHitColor;
    }
    private void SubstractLife()
    {
        lifes--;

        if (lifes == 0)
        {
            EventManager.GameOver();
        }
    }
    private void MakePlayerInvincible()
    {
        StartCoroutine(Co_MakePlayerInvincible());
    }
    IEnumerator Co_MakePlayerInvincible()
    {
        hasPowerUp = true;
        playerSpriteRenderer.color = powerUpColor;
        yield return invincibilityDelay;
        playerSpriteRenderer.color = playerColor;
        hasPowerUp = false;
    }
}
