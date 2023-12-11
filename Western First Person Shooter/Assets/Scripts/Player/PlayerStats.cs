using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    int maxHealth;
    public int MaxHealth { get { return maxHealth; } }

    public int currentHealth { get; private set; }

    [SerializeField]
    int maxRevolverAmmo;

    public int currentRevolverAmmo { get; private set; }

    [SerializeField]
    int maxRifleAmmo;

    public int currentRifleAmmo { get; private set; }

    [SerializeField]
    int maxShotGunAmmo;

    public int currentShotGunAmmo { get; private set; }

    public static int currentScore { get; private set; }

    public delegate void PlayerDead();
    public static event PlayerDead PlayerDied;

    public delegate void UpdateHealth(int updatedHealthValue);
    public static event UpdateHealth HealthUpdated;
    // Start is called before the first frame update
    void Start()
    {
        InitializeStats();
    }


    void InitializeStats()
    {
        currentHealth = maxHealth;
        currentRevolverAmmo = maxRevolverAmmo;
        currentRifleAmmo = maxRifleAmmo;
        currentShotGunAmmo = maxShotGunAmmo;
        
    }

    //This needs to be subscribed to the healthInteractable
    public void AddHealthToPlayer(int _healthToAdd)
    {
        if(currentHealth < maxHealth)
        {
            int _newHealthValue = currentHealth + _healthToAdd;

            if (_newHealthValue <= maxHealth) currentHealth += _newHealthValue;

            else
            {
                int _amountToRemoveFromAddedHealth = _newHealthValue - maxHealth;
                _healthToAdd -= _amountToRemoveFromAddedHealth;
                currentHealth += _healthToAdd;
            }
            HealthUpdated?.Invoke(currentHealth);
        }
    }

    public void SubtractPlayerHealth(int _damageDealtToPlayer)
    {
        currentHealth -= _damageDealtToPlayer;
        Debug.LogWarning("player health = " + currentHealth);
        if(currentHealth <= 0) { PlayerDied?.Invoke(); }
        HealthUpdated?.Invoke(currentHealth);
    }

    public void AddToScore(int _scoreToAdd)
    {
        currentScore += _scoreToAdd;
    }

    public void ResetPlayerScore()
    {
        currentScore = 0;
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }
}
