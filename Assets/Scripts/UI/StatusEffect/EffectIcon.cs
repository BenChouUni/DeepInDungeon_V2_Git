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

    private EffectEnum type;
    private string effectName;
    private int layer;

    public void setIcon(EffectEnum _type, string _effectName, int _layer)
    {
        this.type = _type;
        this.effectName = _effectName;
        this.layer = _layer;
    }
    public EffectEnum getType()
    {
        return this.type;
    }
    public void SetLayer(int n)
    {
        this.layer = n;
    }

    public void ShowIcon()
    {
        number.text = layer.ToString();
        nameText.text = effectName;
    }

    public void SelfDesTroy()
    {
        Destroy(this.gameObject);
    }
}
