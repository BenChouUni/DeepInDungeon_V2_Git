using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierArrows : MonoBehaviour
{
    #region Public Field
    [Tooltip("the prefab of arrow head")]
    public GameObject ArrowHeadPrefab;

    [Tooltip("The prefab of arrow nodes")]
    public GameObject ArrowNodePrefab;

    [Tooltip("The number of nodes")]
    public int nodeNumber;

    [Tooltip("THe scale multiplier for arrow node")]
    public float scaleFactor = 1f;
    #endregion

    #region Private Field
    /// <summary>
    /// the posistion of arrow emitter point(出發的固定點)
    /// </summary>
    private RectTransform origin;

    private List<RectTransform> arrowNodes = new List<RectTransform>();

    private List<Vector2> controlPoints = new List<Vector2>();

    /// <summary>
    /// 決定貝茲曲線所需要的參數 此參數為別人所調適的結果
    /// </summary>
    private readonly List<Vector2> controlPointFactors = new List<Vector2> { new Vector2(-0.3f, 0.8f), new Vector2(0.1f, 1.4f) };
    #endregion

    #region Private Method
    private void Awake()
    {
        this.origin = this.GetComponent<RectTransform>();

        for (int i = 0; i < this.nodeNumber; i++)
        {
            this.arrowNodes.Add(Instantiate(this.ArrowNodePrefab,this.transform).GetComponent<RectTransform>());

        }

        this.arrowNodes.Add(Instantiate(this.ArrowHeadPrefab, this.transform).GetComponent<RectTransform>());

        this.arrowNodes.ForEach(a => a.GetComponent<RectTransform>().position = new Vector2(-1000, -1000));
        //this.gameObject.SetActive(false);

        //貝茲曲線所需要的四個點
        for (int i = 0; i < 4; i++)
        {
            this.controlPoints.Add(Vector2.zero);
        }
    }

    private void Update()
    {
        //計算4個control points
        this.controlPoints[0] = new Vector2(this.origin.position.x, this.origin.position.y);

        this.controlPoints[3] = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        //P1 = P0 + (P3 - P0)*Vector2(-0.3,0.8)
        //P2 = P0 + (P3 - P0)*Vector2(0.1,1.4)
        this.controlPoints[1] = this.controlPoints[0] + (controlPoints[3] - controlPoints[1]) * this.controlPointFactors[0];
        this.controlPoints[2] = this.controlPoints[0] + (controlPoints[3] - controlPoints[1]) * this.controlPointFactors[1];


        for (int i = 0; i < this.arrowNodes.Count; i++)
        {
            //Calculate t
            var t = Mathf.Log(1f * i / (this.arrowNodes.Count - 1) + 1f, 2f);

            //Cubic Bezier curve
            // B(t) = (1-t)^3 * P0 + 3*(1-t)^2 * t * P1 + 3*(1-t)t^2*P2 + t^3 * P3
            this.arrowNodes[i].position =
                Mathf.Pow(1 - t, 3) * controlPoints[0] +
                3 * Mathf.Pow(1 - t, 2) * t * controlPoints[1] +
                3 * (1-t) * Mathf.Pow(t, 2) * controlPoints[2] +
                Mathf.Pow(t, 3) * controlPoints[3];

            //Claculate rotation
            if (i>0)
            {
                var euler = new Vector3(0, 0, Vector2.SignedAngle(Vector2.up, this.arrowNodes[i].position - this.arrowNodes[i-1].position));
                this.arrowNodes[i].rotation = Quaternion.Euler(euler);
            }

            //Calculate Scale
            var scale = this.scaleFactor * (1f - 0.03f * (this.arrowNodes.Count - 1 - i));
            this.arrowNodes[i].localScale = new Vector3(scale, scale, 1f);
        }

        // the first arrow node rotate
        this.arrowNodes[0].transform.rotation = this.arrowNodes[1].transform.rotation;

    }
    #endregion
}
