using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    public Slider slider; 
    public PlayerController player;
    public void Update() {
        float currhealth = player.GetCurrHealth();
        float maxHealth = player.GetMaxHealth();

        slider.maxValue = maxHealth;
        slider.value = currhealth;
    }
}
