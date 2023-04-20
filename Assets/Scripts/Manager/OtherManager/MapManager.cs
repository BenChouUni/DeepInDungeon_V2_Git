using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public GameObject StopPrefab;
    public Transform MapPanel;
    public static int layer = 5;
    public Vector3[][] point_positions = new Vector3[layer][];

    void Start()
    {
        Debug.DrawLine(Vector3.zero, new Vector3(200, 0, 0), UnityEngine.Color.black, 50f);
        System.Console.WriteLine("Map");
        System.Console.WriteLine("-------------------------");


        int[] monster = new int[layer];     //每層怪物數量
        System.Random randon = new System.Random();
        int[][] pointer1 = new int[layer][];        //第一次連接到的位置，0 -> 1, 1 -> 2 ...
        int[][] pointer2 = new int[layer][];        //第二次連接到的位置，1 -> 0, 2 -> 1 ...
        int[][] line = new int[layer][];        //當前已被連到的線數

        for (int i = 0; i < layer; i++)
        {
            monster[i] = randon.Next(3, 6);     //設定每層怪物數量
            pointer1[i] = new int[monster[i]];
            pointer2[i] = new int[monster[i]];
            line[i] = new int[monster[i]];
            point_positions[i] = new Vector3[monster[i]];

            //若不是第0層則開始相連
            if (i > 0)
            {
                //頭尾相連
                fpt_and_endpt(i, i - 1, monster, pointer1[i - 1], line[i]);

                //第一次相連
                first_assign(i, i - 1, monster, pointer1[i - 1], line[i]);
                //Show(pointer1, i);

                //第二次相連

                second_assign(i, i - 1, monster, pointer1[i - 1], pointer2[i], line[i]);
                //Show(pointer2, i);
            }
        }
        DrawMap(monster, layer);
        DrawLine(monster, layer, pointer1);

        for (int i = 0; i < layer; i++)
        {
            for(int j = 0; j < monster[i]; j++)
            {
                //Debug.DrawLine(Vector3.zero, new Vector3(0, 5, 0), color);
            }
        }
        Show(pointer1, layer);
        Show(pointer2, layer);
        Show(line, layer);
    }

    public static void Show(int[][] s, int layer)
    {
        for (int i = 0; i < layer; i++)
        {
            for (int j = 0; j < s[i].Length; j++)
            {
                Debug.Log(s[i][j]);
            }
            Debug.Log("\n");
        }
        Debug.Log("\n");
    }


    //頭尾相連
    public static void fpt_and_endpt(int now_layer, int pre_layer, int[] monster_num, int[] pointer, int[] beassign)
    {
        pointer[0] = 0;
        beassign[0]++;
        pointer[monster_num[pre_layer] - 1] = monster_num[now_layer] - 1;
        beassign[monster_num[now_layer] - 1]++;
    }

    //第一次相連，確保上一層都有連接到當前層，但當前層未必都有被連到
    public static void first_assign(int now_layer, int pre_layer, int[] monster_num, int[] pointer, int[] beassign)
    {
        System.Random randon = new System.Random();

        int now_pt = 0;     //當前層可能會連接到的點
        int inst;       //要連接到當前點還是移動到下一點的指令

        if (monster_num[now_layer] > monster_num[pre_layer])        //如果當前層的點大於上一層的話
        {
            now_pt++;   //now_pt直接指向下一個點，避免最後出現狀況
        }
        for (int pre_pt = 1; pre_pt < monster_num[pre_layer] - 1; pre_pt++)
        {


            if (pre_pt == monster_num[pre_layer] - 2 && monster_num[now_layer] - now_pt == 5)   //在 4,6 可能會發生點不夠用的情況，因此先提早避免
            {
                now_pt++;
            }

            inst = randon.Next(0, 2);       //隨機指令，0是指向當前點，1是指向下一點
            if (monster_num[pre_layer] - pre_pt == 3 && monster_num[now_layer] - now_pt == 2)        //倒數第三不能接到最後一個
            {
                inst = 0;
            }
            if (now_pt == monster_num[now_layer] - 1)       //若只剩最後一點能選
            {
                inst = 0;

            }
            if (inst == 0)
            {
                pointer[pre_pt] = now_pt;
            }
            else if (inst == 1)
            {
                now_pt++;
                pointer[pre_pt] = now_pt;
            }

            //測試用
            /*
            if(now_pt == monster_num[now_layer])
            {
                System.Console.WriteLine("超過了");
                System.Console.WriteLine("pre: " + pre_pt + " now: " + now_pt);
                System.Console.WriteLine();
                now_pt--;
            }
            */
            beassign[now_pt]++;     //被指向的點，線數量加一
            if (beassign[now_pt] >= 2)       //線數量若達2即為上限，因此直接跳到下一點
            {
                now_pt++;
            }
        }
    }

    //第二次相連，讓當前層都有被連到，但要注意交叉
    public static void second_assign(int now_layer, int pre_layer, int[] monster_num, int[] pointer1, int[] pointer2, int[] beassign)
    {

        //當pre_pt連接到now_pt(包括左邊)時會發生交叉就將now_pt移位
        //或now_pt的連接數為2時也要將now_pt移位
        //最後一個pre_pt，不能連到倒數第三個點，
        //當pre_pt連接到now_pt(包括右邊)時會發生交叉就將pre_pt移位

        System.Random randon = new System.Random();

        int pre_pt = 0;     //當前層可能會連接到的點
        int inst;       //要連接到當前點還是移動到下一點的指令

        for (int now_pt = 1; now_pt < monster_num[now_layer] - 1; now_pt++)
        {
            if (beassign[now_pt] == 0)      //當前層的這一點未被連接到才執行連接
            {

                bool stillfind = true;
                while (stillfind)      //尋找不會交叉，可連接的點
                {
                    stillfind = false;
                    int right_pre_pt = pre_pt + 1;      //用右邊的點來判斷
                    if (right_pre_pt == monster_num[pre_layer])        //判斷最後一點
                    {
                        right_pre_pt = pre_pt;
                        break;
                    }

                    //測試用
                    /*
                    if(right_pre_pt >= monster_num[pre_layer])
                    {
                        System.Console.WriteLine("Second出現bug了");
                        //System.Console.WriteLine(right_pre_pt);
                        right_pre_pt = monster_num[pre_layer] - 1;
                        
                    }
                    */
                    if (pointer1[right_pre_pt] < now_pt)     //是否會造成交叉
                    {
                        pre_pt++;
                        stillfind = true;
                    }
                }
                inst = randon.Next(0, 2);
                if (monster_num[pre_layer] - pre_pt == 2 && monster_num[now_layer] - now_pt == 3)      //倒數第三個now_pt，不可連到最後一個pre_pt
                {
                    inst = 0;
                }

                if (inst == 0)
                {
                    pointer2[now_pt] = pre_pt;
                }
                else if (inst == 1)
                {
                    pre_pt++;
                    pointer2[now_pt] = pre_pt;
                }
                pre_pt++;   //連接後的pre_pt必為2，所以固定+1往下
                beassign[now_pt]++;

            }
        }
    }


    public void DrawMap(int[] nodenum, int layer)
    {
        for (int i = 0; i < layer; i++)
        {
            for (int j = 0; j < nodenum[i]; j++)
            {
                point_positions[i][j] = new Vector3((1920 / nodenum[i]) * (j + 1) - 100, 1000 - (1000 / layer) * i, 0);
                CreateStoponMap(point_positions[i][j].x, point_positions[i][j].y);
            }
        }
    }
    public void DrawLine(int[] nodenum, int layer, int[][] pointer) 
    {
        for (int i = 0; i < layer-1; i++)
        {
            for (int j = 0; j < nodenum[i]; j++)
            {
                Debug.DrawLine(point_positions[i][j], point_positions[i+1][pointer[i][j]], UnityEngine.Color.white, 2.5f);
            }
        }
    }

    public void CreateStoponMap(float x, float y)
    {
        GameObject new_point;

        new_point = Instantiate(StopPrefab, MapPanel, false);
        new_point.GetComponent<Transform>().position = new Vector2(x, y);

    }
}
