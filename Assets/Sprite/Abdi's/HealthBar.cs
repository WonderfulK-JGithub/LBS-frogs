using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Slider slider;
    public Gradient gradient;
    public Image Fill;

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;

        Fill.color = gradient.Evaluate(1f);// vi kan få en färg från vår gradient vid en specifik punkt
    }

    public void SetHealth(int health)
    {
        slider.value = health;

        Fill.color = gradient.Evaluate(slider.normalizedValue);// När vi tar damage och vill ställa vår health till nytt
    }
}
