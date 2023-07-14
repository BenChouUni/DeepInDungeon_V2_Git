using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 血條的顯示管理
/// </summary>
public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Text numberText;
    //public HpState hpState;
    ///// <summary>
    ///// 初始設定，必須在所有相關顯示時呼叫
    ///// </summary>
    ///// <param name="_hpState"></param>
    //public void InitSet(HpState _hpState)
    //{
    //    this.hpState = _hpState;
    //}

    public void Show(HpState _hpState)
    {
        Debug.Log("Show hp");
        if (_hpState==null)
        {
            Debug.Log("hpState沒有初始化");
            return;
        }
        this.slider.maxValue = _hpState.MaxHp;
        this.slider.value = _hpState.CurrentHp;
        SetText(_hpState);
    }

    private void SetText(HpState hpState)
    {
        numberText.text = string.Format("{0}/{1}",hpState.CurrentHp,hpState.MaxHp);
    }

}
