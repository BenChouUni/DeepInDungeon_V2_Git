using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SmallWeaponShowUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    GameObject weaponInformation;

    [Range(1, 3)]
    public float ZoomSize;

    void Start()
    {

    }
    /// <summary>
    /// 顯示武器詳細資料以及放大
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("OnPointer");
        weaponInformation = BattlePlayerDataManager.instance.CallWeaponInformation(this.GetComponent<WeaponDisplay>().WeaponData);
        this.transform.localScale = Vector2.one * ZoomSize;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        this.transform.localScale = Vector2.one;
        BattlePlayerDataManager.instance.RemoveWeaponInformation(weaponInformation);
    }
}
