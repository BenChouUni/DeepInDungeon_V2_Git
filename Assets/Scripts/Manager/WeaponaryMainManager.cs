using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponaryMainManager : MonoSingleton<WeaponaryMainManager>
{
    //drop
    private bool hasMainWeapon;
    private bool hasSupportWeapon;
    //weapon dropzone
    public WeaponDropZone mainWeaponDropZone;
    public WeaponDropZone supportWeaponDropZone;
    public WeaponDropZone weaponList;
    public Transform weaponListPanel;//drop地方跟回去parent不一樣
    private WeaponDropZone DropZoneFrom;
    //drag
    private GameObject OnDragGO;
    public bool isDrag; //是否正有東西被拖拽

    private void Awake()
    {
        weaponList.SetZoneType(DropZoneType.WeaponList);
        mainWeaponDropZone.SetZoneType(DropZoneType.MainWeapon);
        supportWeaponDropZone.SetZoneType(DropZoneType.SupportWeapon);
        
    }
    void Start()
    {
        hasMainWeapon = hasSupportWeapon = isDrag = false;
        PlayerDataManager.instance.ShowPlayerData();
        Debug.Log("ShowPlayerData");
    }
    /// <summary>
    /// 通知管理器現在什麼物件被拖動
    /// </summary>
    public void StartDrag(GameObject gameObject,WeaponDropZone dropZone)
    {
        isDrag = true;
        this.OnDragGO = gameObject;
        this.DropZoneFrom = dropZone;
        
    }
    /// <summary>
    /// onDragGO以及DropZoneFron先不改，可能drop要用到
    /// </summary>
    public void EndDrag()
    {
        isDrag = false;
        
    }
    //drop相關管理
    public void WeaponDropRequest(WeaponDropZone dropZone)
    {
        //check if weapon
        if (OnDragGO.TryGetComponent(out WeaponDisplay weaponDisplay) == false)
        {
            return;
        }
        WeaponData data = OnDragGO.GetComponent<WeaponDisplay>().WeaponData;
        DropZoneType type = dropZone.dropZoneType;
        if (type == DropZoneType.WeaponList)
        {
            dropZone.PutInList(OnDragGO,weaponListPanel);
            DropZoneType typeFrom = OnDragGO.GetComponent<DragCard>().currentDropZone.dropZoneType;
            PlayerDataManager.instance.RemoveWeapon(typeFrom);
        }
        else
        {
            if (dropZone.isFull)
            {
                //將上面武器卡牌物件丟回list
                //Debug.Log("將上面武器卡牌物件丟回list");
                GameObject weaponToList = dropZone.weaponOn[0];
                dropZone.weaponOn.RemoveAt(0);
                weaponList.PutInWeapon(weaponToList);
                weaponToList.transform.SetParent(weaponListPanel);
                dropZone.isFull = false;
                PlayerDataManager.instance.RemoveWeapon(type);
            }
            dropZone.PutInWeapon(OnDragGO);
            PlayerDataManager.instance.SetWeapon(type, data);
            dropZone.isFull = true;
        }
    }


}
