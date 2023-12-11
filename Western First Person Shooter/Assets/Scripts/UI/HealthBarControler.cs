using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarControler : MonoBehaviour
{
    [SerializeField]
    float maxHealthBarValue;
    Image healthBarFill;

    // Start is called before the first frame update
    void Start()
    {
        maxHealthBarValue = FindObjectOfType<PlayerStats>().MaxHealth;
        healthBarFill = GetComponent<Image>();
        healthBarFill.fillAmount = 1;
    }

    void UpdateHealthBar(int _Updatedhealth)
    {
        float _floatHealth = _Updatedhealth;
        float _updatedHealthBarFillAmount = _floatHealth / maxHealthBarValue;
        if(_updatedHealthBarFillAmount >= 0) { healthBarFill.fillAmount = _updatedHealthBarFillAmount; }
        else { healthBarFill.fillAmount = 0; }
    }

    private void OnEnable()
    {
        PlayerStats.HealthUpdated += UpdateHealthBar;
    }

    private void OnDisable()
    {
        PlayerStats.HealthUpdated -= UpdateHealthBar;
    }
}
