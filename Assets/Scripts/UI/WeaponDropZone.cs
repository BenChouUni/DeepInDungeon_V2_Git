using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WeaponDropZone : MonoBehaviour, IDropHandler
{
    private GameObject weaponOn; //在此dropzone的武器
    /// <summary>
    /// 0 is main 1 is support
    /// </summary>
    [SerializeField]
    private int dropZoneType;

    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropItem = eventData.pointerDrag;

        PutInWeapon(dropItem);
        
    }

    public void PutInWeapon(GameObject weaponCard)
    {
        if (weaponOn != null) //如果裡面有東西
        {
            RemoveWeapon(weaponOn);
        }
        //放入武器
        if (weaponCard.TryGetComponent(out DragCard dragCard))
        {
            dragCard.parentReturnTo = this.transform;
            this.weaponOn = weaponCard;
            WeaponData data = weaponCard.GetComponent<WeaponDisplay>().WeaponData;
            PlayerDataManager.instance.SetWeapon(dropZoneType, data);
        }
    }

    public void RemoveWeapon(GameObject weaponCard)
    {
        if (weaponCard.TryGetComponent(out DragCard dragCard))
        {
            dragCard.ReturnToStartParent();
            this.weaponOn = null;
        }
    }

    public void SetZoneType(int n)
    {
        dropZoneType = n;
    }
}
