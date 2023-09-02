using JetBrains.Annotations;
using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;

public class MapManager : MonoSingleton<MapManager>
{
    public GameObject StopPrefab;
    public GameObject LayerPrefab;
    public Transform MapPanel;
    private int layer;
    public int[] levels;  
    public Vector3[][] point_positions;
    

    private void Awake()
    {
        point_positions = new Vector3[layer][];
    }

    public int[] CreateLevels(int layer)
    {
        this.layer = layer;
        int[] monster = new int[layer];
        System.Random randon = new System.Random();
        for (int i = 0; i < layer; i++)
        {
            monster[i] = randon.Next(3, 6);
        }
        levels = monster;
        return levels;

    }
    public void CreateMap(int _layer)
    {
        this.layer = _layer;
        Debug.LogFormat("{0}", layer);
        int[] monster = new int[layer]; 
        

        System.Random randon = new System.Random();
        int[][] pointer1 = new int[layer][];
        int[][] pointer2 = new int[layer][];        
        int[][] line = new int[layer][];        

        for (int i = 0; i < layer; i++)
        {

            monster[i] = randon.Next(3, 5);

            //CreateStoponMap();
            
            pointer1[i] = new int[monster[i]];
            resetPointer(pointer1[i]);
            pointer2[i] = new int[monster[i]];
            resetPointer(pointer2[i]);
            line[i] = new int[monster[i]];
            //point_positions[i] = new Vector3[monster[i]];
            
            if (i > 0)
            {
                
                fpt_and_endpt(i, i - 1, monster, pointer1[i - 1], line[i]);

                
                first_assign(i, i - 1, monster, pointer1[i - 1], line[i]);
                //Show(pointer1, i);

                second_assign(i, i - 1, monster, pointer1[i - 1], pointer2[i], line[i]);
                //Show(pointer2, i);
            }
            
        }


        //DrawMap(monster, layer);
        //DrawLine(monster, layer, pointer1);

        //DrawLineManager.Setpoint(monster);
        //DrawLineManager.Draw(point_positions[0][0], point_positions[1][pointer1[0][0]]);
        Show(pointer1, layer);
        Show(pointer2, layer);
        Show(line, layer);
    }

    public List<string> CreateLine(int[] monster, int layer)
    {
        int[][] pointer1 = new int[layer][];
        int[][] pointer2 = new int[layer][];
        int[][] line = new int[layer][];

        for(int i = 0; i < layer; i++)
        {
            pointer1[i] = new int[monster[i]];
            resetPointer(pointer1[i]);
            pointer2[i] = new int[monster[i]];
            resetPointer(pointer2[i]);
            line[i] = new int[monster[i]];
            //point_positions[i] = new Vector3[monster[i]];

            if (i > 0)
            {

                fpt_and_endpt(i, i - 1, monster, pointer1[i - 1], line[i]);


                first_assign(i, i - 1, monster, pointer1[i - 1], line[i]);
                //Show(pointer1, i);

                second_assign(i, i - 1, monster, pointer1[i - 1], pointer2[i], line[i]);
                //Show(pointer2, i);
            }

        }
        List<string> LineInfo= new List<string>();
        foreach(string _line in combine_line(pointer1, layer))
        {
            LineInfo.Add(_line);
        }
        foreach (string _line in combine_line(pointer2, layer))
        {
            LineInfo.Add(_line);
        }
        /*
        foreach (string _line in combine_line(line, layer))
        {
            LineInfo.Add(_line);
        }
        */
        return LineInfo;
    }


    public void resetPointer(int[] s)
    {
        for(int i = 0; i < s.Length; i++)
        {
            s[i] = -1;
        }
    }
    public void Show(int[][] s, int layer)
    {
        for (int i = 0; i < layer; i++)
        {
            string line = "";
            for (int j = 0; j < s[i].Length; j++)
            {
                line += s[i][j];
                //Debug.Log(s[i][j]);
            }
            Debug.Log(line);
        }
        Debug.Log("\n");
    }

    public List<string> combine_line(int[][] s, int layer)
    {
        List<string> list = new List<string>();
        for (int i = 0; i < layer; i++)
        {
            string line = "";
            for (int j = 0; j < s[i].Length; j++)
            {
                if(s[i][j] == -1)
                {
                    line += "*";
                }
                else
                {
                    line += s[i][j];
                }
                //Debug.Log(s[i][j]);
            }
            list.Add(line);
        }
        return list;
    }


    public static void fpt_and_endpt(int now_layer, int pre_layer, int[] monster_num, int[] pointer, int[] beassign)
    {
        pointer[0] = 0;
        beassign[0]++;
        pointer[monster_num[pre_layer] - 1] = monster_num[now_layer] - 1;
        beassign[monster_num[now_layer] - 1]++;
    }

    
    public static void first_assign(int now_layer, int pre_layer, int[] monster_num, int[] pointer, int[] beassign)
    {
        System.Random randon = new System.Random();

        int now_pt = 0;     
        int inst;       //�n�s������e�I�٬O���ʨ�U�@�I�����O

        if (monster_num[now_layer] > monster_num[pre_layer])        //�p�G���e�h���I�j��W�@�h����
        {
            now_pt++;   //now_pt�������V�U�@���I�A�קK�̫�X�{���p
        }
        for (int pre_pt = 1; pre_pt < monster_num[pre_layer] - 1; pre_pt++)
        {


            if (pre_pt == monster_num[pre_layer] - 2 && monster_num[now_layer] - now_pt == 5)   //�b 4,6 �i��|�o���I�����Ϊ����p�A�]���������קK
            {
                now_pt++;
            }

            inst = randon.Next(0, 2);       //�H�����O�A0�O���V���e�I�A1�O���V�U�@�I
            if (monster_num[pre_layer] - pre_pt == 3 && monster_num[now_layer] - now_pt == 2)        //�˼ƲĤT���౵��̫�@��
            {
                inst = 0;
            }
            if (now_pt == monster_num[now_layer] - 1)       //�Y�u�ѳ̫�@�I���
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

            //���ե�
            /*
            if(now_pt == monster_num[now_layer])
            {
                System.Console.WriteLine("�W�L�F");
                System.Console.WriteLine("pre: " + pre_pt + " now: " + now_pt);
                System.Console.WriteLine();
                now_pt--;
            }
            */
            beassign[now_pt]++;     //�Q���V���I�A�u�ƶq�[�@
            if (beassign[now_pt] >= 2)       //�u�ƶq�Y�F2�Y���W���A�]����������U�@�I
            {
                now_pt++;
            }
        }
    }

    public static void second_assign(int now_layer, int pre_layer, int[] monster_num, int[] pointer1, int[] pointer2, int[] beassign)
    {

        //當pre_pt連接到now_pt(包括左邊)時會發生交叉就將now_pt移位
        //或now_pt的連接數為2時也要將now_pt移位
        //最後一個pre_pt，不能連到倒數第三個點，
        //當pre_pt連接到now_pt(包括右邊)時會發生交叉就將pre_pt移位

        System.Random randon = new System.Random();

        int pre_pt = 0;     //���e�h�i��|�s���쪺�I
        int inst;       //�n�s������e�I�٬O���ʨ�U�@�I�����O

        for (int now_pt = 1; now_pt < monster_num[now_layer] - 1; now_pt++)
        {
            if (beassign[now_pt] == 0)      //���e�h���o�@�I���Q�s����~����s��
            {

                bool stillfind = true;
                while (stillfind)      //�M�䤣�|��e�A�i�s�����I
                {
                    stillfind = false;
                    int right_pre_pt = pre_pt + 1;      //�Υk�䪺�I�ӧP�_
                    if (right_pre_pt == monster_num[pre_layer])        //�P�_�̫�@�I
                    {
                        right_pre_pt = pre_pt;
                        break;
                    }

                    //���ե�
                    /*
                    if(right_pre_pt >= monster_num[pre_layer])
                    {
                        System.Console.WriteLine("Second�X�{bug�F");
                        //System.Console.WriteLine(right_pre_pt);
                        right_pre_pt = monster_num[pre_layer] - 1;
                        
                    }
                    */
                    if (pointer1[right_pre_pt] < now_pt)     //�O�_�|�y����e
                    {
                        pre_pt++;
                        stillfind = true;
                    }
                }
                inst = randon.Next(0, 2);
                if (monster_num[pre_layer] - pre_pt == 2 && monster_num[now_layer] - now_pt == 3)      //�˼ƲĤT��now_pt�A���i�s��̫�@��pre_pt
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
                pre_pt++;   //�s���᪺pre_pt����2�A�ҥH�T�w+1���U
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
                //CreateStoponMap(point_positions[i][j].x, point_positions[i][j].y);
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

    public void CreateStoponMap()
    {
        Debug.Log("生成地圖");
        GameObject new_point;
        GameObject new_layer;
        new_layer = Instantiate(LayerPrefab, MapPanel, false);
        new_point = Instantiate(StopPrefab, new_layer.transform, false);
        //new_point.GetComponent<Transform>().position = new Vector2(x, y);

    }
}
