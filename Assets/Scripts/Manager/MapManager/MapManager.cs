using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public GameObject StopPrefab;
    public Transform MapPanel;
    public int layer;
    public Vector3[][] point_positions;

    private void Awake()
    {
        point_positions = new Vector3[layer][];
    }
    void Start()
    {
        System.Console.WriteLine("Map");
        System.Console.WriteLine("-------------------------");


        int[] monster = new int[layer];     //�C�h�Ǫ��ƶq
        System.Random randon = new System.Random();
        int[][] pointer1 = new int[layer][];        //�Ĥ@���s���쪺��m�A0 -> 1, 1 -> 2 ...
        int[][] pointer2 = new int[layer][];        //�ĤG���s���쪺��m�A1 -> 0, 2 -> 1 ...
        int[][] line = new int[layer][];        //���e�w�Q�s�쪺�u��

        for (int i = 0; i < layer; i++)
        {
            monster[i] = randon.Next(3, 6);     //�]�w�C�h�Ǫ��ƶq
            pointer1[i] = new int[monster[i]];
            pointer2[i] = new int[monster[i]];
            line[i] = new int[monster[i]];
            point_positions[i] = new Vector3[monster[i]];

            //�Y���O��0�h�h�}�l�۳s
            if (i > 0)
            {
                //�Y���۳s
                fpt_and_endpt(i, i - 1, monster, pointer1[i - 1], line[i]);

                //�Ĥ@���۳s
                first_assign(i, i - 1, monster, pointer1[i - 1], line[i]);
                //Show(pointer1, i);

                //�ĤG���۳s

                second_assign(i, i - 1, monster, pointer1[i - 1], pointer2[i], line[i]);
                //Show(pointer2, i);
            }
        }
        DrawMap(monster, layer);
        DrawLine(monster, layer, pointer1);

        DrawLineManager.Setpoint(monster);
        DrawLineManager.Draw(point_positions[0][0], point_positions[1][pointer1[0][0]]);
        //Show(pointer1, layer);
        //Show(pointer2, layer);
        //Show(line, layer);
    }

    public void Show(int[][] s, int layer)
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


    //�Y���۳s
    public static void fpt_and_endpt(int now_layer, int pre_layer, int[] monster_num, int[] pointer, int[] beassign)
    {
        pointer[0] = 0;
        beassign[0]++;
        pointer[monster_num[pre_layer] - 1] = monster_num[now_layer] - 1;
        beassign[monster_num[now_layer] - 1]++;
    }

    //�Ĥ@���۳s�A�T�O�W�@�h�����s������e�h�A�����e�h���������Q�s��
    public static void first_assign(int now_layer, int pre_layer, int[] monster_num, int[] pointer, int[] beassign)
    {
        System.Random randon = new System.Random();

        int now_pt = 0;     //���e�h�i��|�s���쪺�I
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

    //�ĤG���۳s�A�����e�h�����Q�s��A���n�`�N��e
    public static void second_assign(int now_layer, int pre_layer, int[] monster_num, int[] pointer1, int[] pointer2, int[] beassign)
    {

        //��pre_pt�s����now_pt(�]�A����)�ɷ|�o�ͥ�e�N�Nnow_pt����
        //��now_pt���s���Ƭ�2�ɤ]�n�Nnow_pt����
        //�̫�@��pre_pt�A����s��˼ƲĤT���I�A
        //��pre_pt�s����now_pt(�]�A�k��)�ɷ|�o�ͥ�e�N�Npre_pt����

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
