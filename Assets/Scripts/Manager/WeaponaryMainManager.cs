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
        //WeaponToCardConverter.instance.WeaponIdToCardId(0);
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
            dropZone.PutInWeapon(OnDragGO,weaponListPanel);
            DropZoneType typeFrom = OnDragGO.GetComponent<DragCard>().currentDropZone.dropZoneType;
            PlayerDataManager.instance.RemoveWeapon(typeFrom);
        }
        else //主武器輔助武器區域
        {
            if (dropZone.isFull)
            {
                //將上面武器卡牌物件跟新來卡牌的dropzone區域交換
                WeaponDropZone ExchangeZone = OnDragGO.GetComponent<DragCard>().currentDropZone;//交換要去的dropzone
                DropZoneType typeFrom =ExchangeZone.dropZoneType; 
                //原本在此dropzone上的物件
                GameObject weaponOn = dropZone.weaponOn[0];
                dropZone.weaponOn.RemoveAt(0);

                if (typeFrom == DropZoneType.WeaponList)
                {
                    ExchangeZone.PutInWeapon(weaponOn, weaponListPanel);

                }
                else
                {
                    WeaponData weaponData = weaponOn.GetComponent<WeaponDisplay>().WeaponData;
                    ExchangeZone.PutInWeapon(weaponOn);
                    PlayerDataManager.instance.SetWeapon(typeFrom,weaponData);
                }
                
                
            }
            dropZone.PutInWeapon(OnDragGO);
            CreateDeckByWeapon(data.id,type);
            PlayerDataManager.instance.SetWeapon(type, data);
            dropZone.isFull = true;
        }
    }
    /// <summary>
    /// 根據放入武器創建卡牌
    /// </summary>
    /// <param name="weaponId"></param>
    private void CreateDeckByWeapon(int weaponId,DropZoneType type)
    {
        List<int> cardIDs = WeaponToCardConverter.instance.WeaponIdToCardId(weaponId);

        foreach (int cardId in cardIDs)
        {
            CardData data = DeckManager.instance.GetCardDataByID(cardId);
            DeckManager.instance.CreateCardOnPanel(data, type);
        }
        
    }
}
