using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 掛載Icon上，管理Icon上數值以及顯示
/// </summary>
public class EffectIcon : MonoBehaviour
{
    public Image image;
    public Text number;
    public Text nameText;

    public StateEffect stateEffect;
    public void SetStateEffect(StateEffect _stateEffect)
    {
        this.stateEffect = _stateEffect;
        ShowIcon();
    }
    public void ShowIcon()
    {


        number.text = stateEffect.Layer.ToString();
        nameText.text = stateEffect.effectName;
        
    }

    public void SelfDesTroy()
    {
        Destroy(this.gameObject);
    }
}
