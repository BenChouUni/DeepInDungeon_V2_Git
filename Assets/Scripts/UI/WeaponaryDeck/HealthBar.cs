using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Text numberText;
    private int maxHp;
    private int currentHp;

    public void SetMaxHealth(int maxHp)
    {
        this.maxHp = maxHp;
        this.slider.maxValue = maxHp;

        SetHealth(maxHp);
        SetText();
    }

    public void SetHealth(int hp)
    {
        this.currentHp = hp;
        this.slider.value = hp;
        SetText();
    }

    private void SetText()
    {
        numberText.text = string.Format("{0}/{1}",currentHp,maxHp);
    }
}
