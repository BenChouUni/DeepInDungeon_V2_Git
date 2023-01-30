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
    private DropZoneType DropZoneFrom;
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
    public void StartDrag(GameObject gameObject,DropZoneType dropZone)
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
    /// <summary>
    /// drop 武器進入時 呼叫
    /// </summary>
    /// <param name="dropZone"></param>
    public void WeaponDropRequest(WeaponDropZone dropZone)
    {
        //dropzone的類型
        DropZoneType type = dropZone.dropZoneType;

        //check if weapon確認是否是武器
        if (OnDragGO.TryGetComponent(out WeaponDisplay weaponDisplay) == false)
        {
            return;
        }
        //放入武器物件事的基本資料跟類型，由Ondrag來決定
        WeaponData data = OnDragGO.GetComponent<WeaponDisplay>().WeaponData;

        //新來武器原先所在的dropzone
        DropZoneType typeFrom = OnDragGO.GetComponent<DragCard>().currentDropZoneType;

        //如果放入武器庫
        if (type == DropZoneType.WeaponList)
        {
            dropZone.PutInWeapon(OnDragGO,weaponListPanel);
            //移除原先武器所在位置的資料
            
            if (typeFrom == DropZoneType.WeaponList)
            {
                return;
            }
            PlayerDataManager.instance.RemoveWeapon(typeFrom);
            DeckManager.instance.RemoveCardsByType(typeFrom);
        }
        else //如果放入主武器輔助武器區域
        {
            //如果區域上面是滿的
            if (dropZone.isFull)
            {
                //將上面武器卡牌物件跟新來卡牌的dropzone區域交換
                WeaponDropZone ExchangeZone = GetDropZoneByType(typeFrom);//交換要去的dropzone

                //原本在此dropzone上的物件
                GameObject weaponOn = dropZone.weaponOn[0];
                dropZone.weaponOn.RemoveAt(0);
                DeckManager.instance.RemoveCardsByType(type);

                if (typeFrom == DropZoneType.WeaponList)
                {
                    ExchangeZone.PutInWeapon(weaponOn, weaponListPanel);

                }
                else
                {
                    WeaponData weaponData = weaponOn.GetComponent<WeaponDisplay>().WeaponData;
                    ExchangeZone.PutInWeapon(weaponOn);
                    CreateDeckByWeapon(data.id, type);
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
    //根據dropzonetype 給出 dropZone
    private WeaponDropZone GetDropZoneByType(DropZoneType type)
    {
        switch (type)
        {
            case DropZoneType.WeaponList:
                return weaponList;
            case DropZoneType.MainWeapon:
                return mainWeaponDropZone;
            case DropZoneType.SupportWeapon:
                return supportWeaponDropZone;
            default:
                return null;
        }
    }
}
