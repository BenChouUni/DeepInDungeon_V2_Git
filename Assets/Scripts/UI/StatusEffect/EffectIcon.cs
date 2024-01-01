using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
/// <summary>
/// 掛載Icon上，管理Icon上數值以及顯示
/// </summary>
public class EffectIcon : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image image;
    public Text number;
    public Text nameText;

    public GameObject DesWindow;
    public Text desText;

    public StateEffect stateEffect;
    public void SetStateEffect(StateEffect _stateEffect)
    {
        this.stateEffect = _stateEffect;
        ShowIcon();
    }

    private void Start()
    {
        DesWindow.SetActive(false);
        
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

    public void OnPointerEnter(PointerEventData eventData)
    {
        
        DesWindow.SetActive(true);
        desText.text = stateEffect.EffectDescription();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        DesWindow.SetActive(false);
    }

    
}
