using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;


/// <summary>
///管理狀態列，管理ICON顯示
/// </summary>
public class EffectListDisplay :MonoBehaviour
{

    public Character character;

    //由於物件可以自我刪除，於是就選擇只存EffectIcon部件
    private List<EffectIcon> effectIcons = new List<EffectIcon>();

    //prefab
    public GameObject IconObj;

    public EffectListDisplay()
    {

    }
    /// <summary>
    /// 先刪除全部再重新顯示，可能設計不好但先暫時妥協
    /// </summary>
    /// <param name="states"></param>
    public void ShowStateList(List<StateEffect> states)
    {
        RemoveAllIcon();
        foreach (StateEffect item in states)
        {
            CreateIcon(item);
        }

    }
    ///// <summary>
    ///// 會自動增加層數
    ///// </summary>
    ///// <param name="statusEffect"></param>
    ///// <param name="layer">會自動增加幾個</param>
    //public void AddStatusEffect(StateEffect statusEffect,int layer)
    //{
    //    StateEffect editEffect = statusEffectList.Find(effect => effect.effectType == statusEffect.effectType);
        
    //    if (editEffect != null)//原本就有
    //    {
    //        Debug.LogFormat("Find effct exist{0}", editEffect.effectName);
    //        editEffect.AddLayer(layer);
    //        Debug.LogFormat("Has Layer{0}", editEffect.getLayer());
    //        SetIconLayer(statusEffect.effectType, editEffect.getLayer());//這邊先增加完後才獲取
    //    }
    //    else//原本沒有，要新增
    //    {
    //        statusEffect.setLayer(layer);
    //        statusEffectList.Add(statusEffect);
    //        CreateIcon(statusEffect, layer);
    //    }
    //}



    //public void RemoveEffect(StateEffect statusEffect, int layer)
    //{
    //    StateEffect editEffect = statusEffectList.Find(effect => effect == statusEffect);
    //    if (editEffect == null)//effect找不到
    //    {
    //        Debug.LogWarning("要移除的effect不存在");
    //        return;
    //    }
    //    else//可以正常移除
    //    {
    //        editEffect.AddLayer(-layer);
    //        SetIconLayer(statusEffect.effectType, editEffect.getLayer());
    //        if (editEffect.getLayer()==0)//如果剛好沒了就要刪掉
    //        {
    //            statusEffectList.Remove(editEffect);
    //            RemoveIcon(statusEffect.effectType);
                
    //        }
    //    }
    //}

    /*Icon 相關*/
    //public void ShowAllIcons()
    //{
    //    foreach (EffectIcon item in effectIcons)
    //    {
    //        item.ShowIcon();
    //    }
    //}

    private void CreateIcon(StateEffect stateEffect)
    {
        GameObject iconObj = Instantiate(IconObj, this.transform, false);
        EffectIcon effectIcon = iconObj.GetComponent<EffectIcon>();
        effectIcon.SetStateEffect(stateEffect);

        effectIcons.Add(effectIcon);
    }
    /// <summary>
    /// 只設定，不會自己加減
    /// </summary>
    /// <param name="type"></param>
    /// <param name="layer"></param>
    //private void SetIconLayer(StateEffectType type, int layer)
    //{
    //    EffectIcon effectIcon = effectIcons.Find(ei => ei.getType() == type);
    //    if (effectIcon != null)
    //    {
    //        effectIcon.SetLayer(layer);
    //    }
    //    else
    //    {
    //        Debug.Log("找不到ICon物件");
    //    }


    //}
    private void RemoveAllIcon()
    {
        foreach (EffectIcon item in effectIcons)
        {
            item.SelfDesTroy();
        }
    }

}
