using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
public enum WeaponType
{
    InList,MainWeapon,SupportWeapon
}
public class WeaponDropZone : MonoBehaviour, IDropHandler
{
    public List<GameObject> weaponOn; //在此dropzone的武器
    public bool isFull;
    [SerializeField]
    public WeaponType dropZoneType;

    public void OnDrop(PointerEventData eventData)
    {
        //Debug.Log("DropNow");
        WeaponaryMainManager.instance.WeaponDropRequest(this);
    }

    public void PutInWeapon(GameObject GO)
    {
        //放入武器
        DragCard dragCard;
        dragCard = GO.GetComponent<DragCard>();
        GO.transform.SetParent(this.transform);
        dragCard.parentReturnTo = this.transform;
        dragCard.currentDropZoneType = this.dropZoneType;
        this.weaponOn.Add(GO);
        isFull = true;
        /*
        WeaponData data = OnDragGO.GetComponent<WeaponDisplay>().WeaponData;
        PlayerDataManager.instance.SetWeapon(type, data);*/

    }
    /// <summary>
    /// 由於weaponlist的drop感應區域並非parent，所以多載
    /// </summary>
    /// <param name="GO"></param>
    /// <param name="panel"></param>
    public void PutInWeapon(GameObject GO,Transform panel)
    {
        DragCard dragCard;
        dragCard = GO.GetComponent<DragCard>();
        GO.transform.SetParent(panel);
        dragCard.parentReturnTo = panel;
        dragCard.currentDropZoneType = this.dropZoneType;
        this.weaponOn.Add(GO);
        isFull = true;
    }
    /*
    public void MoveWeaponToParent(WeaponDropZone dropZone)
    {
        foreach (GameObject item in weaponOn)
        {
            DragCard dragCard;
            dragCard = item.GetComponent<DragCard>();
            dragCard.parentReturnTo = dropZone.transform;
            dragCard.currentDropZone = dropZone;
        }
        isFull = false;
    }
    */
    public void SetZoneType(WeaponType type)
    {
        dropZoneType = type;
    }
}
