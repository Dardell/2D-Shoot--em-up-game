using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Player player;
    void Start()
    {
    }
    void Update()
    {
        slider.maxValue = player.healthMax;
        slider.value = player.health;
    }
}
