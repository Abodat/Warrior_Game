using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoSingleton<HealthController>
{
    public Slider playerSlider;
    public Slider bossSlider;
    public void SetMaxHealth(int health)
    {
        playerSlider.maxValue = health;
        playerSlider.value = health;
    }

    public void SetHealth(int health)
    {
        playerSlider.value = health;
    }

    public void SetBossMaxHealth(int health)
    {
        bossSlider.maxValue = health;
        bossSlider.value = health;
    }
    public void SetBossHealth(int health)
    {
        bossSlider.value = health;
    }
}
