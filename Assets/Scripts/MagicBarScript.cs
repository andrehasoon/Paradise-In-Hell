using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicBarScript : MonoBehaviour
{
    public Slider slider; 
    public PlayerController player;
    public void Update() {
        float currMagic = player.GetCurrMagic();
        float maxMagic = player.GetMaxMagic();

        slider.maxValue = maxMagic;
        slider.value = currMagic;
    }
}
