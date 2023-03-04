using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
public enum WeaponDropZoneType
{
    InList,MainWeapon,SupportWeapon
}
public class WeaponDropZone : MonoBehaviour, IDropHandler
{
    //在此dropzone的武器 之所以用list是因為包含上面的全武器庫
    [SerializeField]
    private List<GameObject> weaponOn;
   
    
    public bool isFull;
    [SerializeField]
    public WeaponDropZoneType dropZoneType;

    public void OnDrop(PointerEventData eventData)
    {
        //Debug.Log("DropNow");
        //拖拽scroll bar 也會觸發
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
        //如果是武器區則最多有一個weaponOn
        if (this.dropZoneType!=WeaponDropZoneType.InList)
        {
            if (weaponOn.Count != 0)
            {
                this.weaponOn.Clear();
            }
        }
        this.weaponOn.Add(GO);
        isFull = true;
       

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
    public void SetZoneType(WeaponDropZoneType type)
    {
        dropZoneType = type;
    }
    /// <summary>
    /// 取第一個
    /// </summary>
    /// <returns></returns>
    public GameObject GetWeaponOn()
    {
        return weaponOn[0];
    }
    public GameObject GetWeaponOn(int index)
    {
        return weaponOn[index];
    }

    public void RemoveWeaponOn()
    {
        weaponOn.RemoveAt(0);
    }
}
