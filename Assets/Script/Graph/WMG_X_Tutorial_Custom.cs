using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using DG.Tweening;

public class WMG_X_Tutorial_Custom : MonoBehaviour {

	public GameObject emptyGraphPrefab;

    public Object indicatorPrefab;
    private GameObject graphGO;
    WMG_Axis_Graph graph;

    WMG_Series series1;

    /// <summary>
    /// 显示数值线下方区域颜色，重启有效
    /// </summary>
    public bool useAreaShading;

    /// <summary>
    /// 最终点闪烁效果
    /// </summary>
    public bool blinkCurrentPoint;

    [SerializeField]
    private bool _plottingData;
    public bool plottingData
    {
        get { return _plottingData; }
        set
        {
            if (_plottingData != value)
            {
                _plottingData = value;
                plottingDataC.Changed();
            }
        }
    }


    /// <summary>
    /// X轴的间隔
    /// </summary>
    public float xInterval = 10;

    GameObject graphOverlay;
    GameObject indicatorGO;

    public float plotIntervalSeconds;
    public float plotAnimationSeconds;



    public bool moveXaxisMinimum;

    Ease plotEaseType = Ease.OutQuad;

    float addPointAnimTimeline;

    Tween blinkingTween;

    private WMG_Change_Obj plottingDataC = new WMG_Change_Obj();


    float blinkScale = 2;
    public float blinkAnimDuration;

    /// <summary>
    /// 浮动的线上的小数位数
    /// </summary>
    //public int indicatorNumDecimals;

    private void Awake()
    {
        GlobalVariableBackground.Instance.TableObject = this;
        gameObject.SetActive(false);
    }

    System.Globalization.NumberFormatInfo yAxisNumberFormatInfo = new System.Globalization.CultureInfo("en-US", false).NumberFormat;
    // Use this for initialization
    void Start () {


        init();

        //plottingDataC.OnChange += PlottingDataChanged;

        //plottingData = true;
    }
    public void init()
    {
        graphGO = GameObject.Instantiate(emptyGraphPrefab);
        graphGO.transform.SetParent(this.transform, false);
        graph = graphGO.GetComponent<WMG_Axis_Graph>();
        //series1 = graph.addSeries();
        //X轴最大数数
        graph.xAxis.AxisMaxValue = 20;
        //是否使用自动动画
        graph.autoAnimationsEnabled = false;
        //X轴隐藏显示内容
        graph.xAxis.hideLabels = true;


        //Y轴隐藏显示内容
        graph.yAxis.hideLabels = false;
        ////Y轴最大数值
        //graph.yAxis.AxisMaxValue = 20;
        //Y轴Label字体大小
        graph.yAxis.AxisLabelSize = 18;
        //Y轴长度分割点数量
        graph.yAxis.AxisNumTicks = 5;
        // Y轴最大值自动生长
        graph.yAxis.MaxAutoGrow = true;
        // Y轴最小值自动生长
        graph.yAxis.MinAutoGrow = true;
        graph.yAxis.MaxAutoShrink = true;
        System.Globalization.NumberFormatInfo yAxisNumberFormatInfo = new System.Globalization.CultureInfo("en-US", false).NumberFormat;

        series1 = graph.addSeries();
        series1.seriesName = "名称";
        series1.pointColor = Color.red;
        series1.lineColor = Color.green;
        series1.lineScale = 0.5f;
        series1.pointWidthHeight = 8;
        graph.yAxis.axisLabelLabeler = customAxisLabelLabelerToE;
        //颜色区域
        if (useAreaShading)
        {
            series1.areaShadingType = WMG_Series.areaShadingTypes.Gradient;
            series1.areaShadingAxisValue = graph.yAxis.AxisMinValue;
            series1.areaShadingColor = new Color(80f / 255f, 100f / 255f, 60f / 255f, 1f);
        }

        graphOverlay = new GameObject();
        graphOverlay.AddComponent<RectTransform>();
        graphOverlay.name = "Graph Overlay";
        graphOverlay.transform.SetParent(graphGO.transform, false);

        indicatorGO = GameObject.Instantiate(indicatorPrefab) as GameObject;
        indicatorGO.transform.SetParent(graphOverlay.transform, false);
        indicatorGO.SetActive(false);

        graph.GraphBackgroundChanged += UpdateIndicatorSize;
        graph.paddingLeftRight = new Vector2(65, 60);
        graph.paddingTopBottom = new Vector2(40, 40);
    }

    void PlottingDataChanged()
    {
        //GlobalVariableBackground.Instance.conManager.WriteLog("plottingData: " + plottingData);
        if (plottingData)
        {
            StartCoroutine(plotData());
        }
    }

    public void ClearValue()
    {
        if (transform.childCount > 0)
        {
            Destroy(graph);
            Destroy(transform.GetChild(0).gameObject);
            init();
        }
    }

    public IEnumerator plotData()
    {
        while (true)
        {
            yield return new WaitForSeconds(plotIntervalSeconds);

            if (!plottingData) break;
            animateAddPointFromEnd(new Vector2((series1.pointValues.Count == 0 ? 0 : (series1.pointValues[series1.pointValues.Count - 1].x + xInterval)), Random.Range(graph.yAxis.AxisMinValue, graph.yAxis.AxisMaxValue * 1.2f)), plotAnimationSeconds);
            //最终点闪烁效果
            if (blinkCurrentPoint)
            {
                blinkCurrentPointAnimation();
            }
        }
    }

    public void plotData(float YValue)
    {
            animateAddPointFromEnd(new Vector2((series1.pointValues.Count == 0 ? 0 : (series1.pointValues[series1.pointValues.Count - 1].x + xInterval)), YValue), plotAnimationSeconds);
            //最终点闪烁效果
            if (blinkCurrentPoint)
            {
                blinkCurrentPointAnimation();
            }
    }

    void animateAddPointFromEnd(Vector2 pointVec, float animDuration)
    {
        if (series1.pointValues.Count == 0)
        { // no end to animate from, just add the point
            series1.pointValues.Add(pointVec);
            indicatorGO.SetActive(true);
            graph.Refresh(); // Ensures gamobject list of series points is up to date based on pointValues
            updateIndicator();
        }
        else
        {
            series1.pointValues.Add(series1.pointValues[series1.pointValues.Count - 1]);
            if (pointVec.x > graph.xAxis.AxisMaxValue)
            { // the new point will exceed the x-axis max
                addPointAnimTimeline = 0; // animates from 0 to 1
                Vector2 oldEnd = new Vector2(series1.pointValues[series1.pointValues.Count - 1].x, series1.pointValues[series1.pointValues.Count - 1].y);
                Vector2 newStart = new Vector2(series1.pointValues[1].x, series1.pointValues[1].y);
                Vector2 oldStart = new Vector2(series1.pointValues[0].x, series1.pointValues[0].y);
                WMG_Anim.animFloatCallbacks(() => addPointAnimTimeline, x => addPointAnimTimeline = x, animDuration, 1,
                                            () => onUpdateAnimateAddPoint(pointVec, oldEnd, newStart, oldStart),
                                            () => onCompleteAnimateAddPoint(), plotEaseType);
            }
            else
            {
                WMG_Anim.animVec2CallbackU(() => series1.pointValues[series1.pointValues.Count - 1], x => series1.pointValues[series1.pointValues.Count - 1] = x, animDuration, pointVec,
                                           () => updateIndicator(), plotEaseType);
            }
        }
    }


    void onCompleteAnimateAddPoint()
    {
        if (moveXaxisMinimum)
        {
            series1.pointValues.RemoveAt(0);
            blinkCurrentPointAnimation(true);
        }
    }

    void onUpdateAnimateAddPoint(Vector2 newEnd, Vector2 oldEnd, Vector2 newStart, Vector2 oldStart)
    {
        series1.pointValues[series1.pointValues.Count - 1] = WMG_Util.RemapVec2(addPointAnimTimeline, 0, 1, oldEnd, newEnd);
        graph.xAxis.AxisMaxValue = WMG_Util.RemapFloat(addPointAnimTimeline, 0, 1, oldEnd.x, newEnd.x);

        updateIndicator();

        if (moveXaxisMinimum)
        {
            series1.pointValues[0] = WMG_Util.RemapVec2(addPointAnimTimeline, 0, 1, oldStart, newStart);
            graph.xAxis.AxisMinValue = WMG_Util.RemapFloat(addPointAnimTimeline, 0, 1, oldStart.x, newStart.x);
        }
    }

    void updateIndicator()
    {
        if (series1.getPoints().Count == 0) return;
        WMG_Node lastPoint = series1.getLastPoint().GetComponent<WMG_Node>();
        graph.changeSpritePositionToY(indicatorGO, lastPoint.transform.localPosition.y);
        Vector2 nodeData = series1.getNodeValue(lastPoint);
        //indicatorLabelNumberFormatInfo.CurrencyDecimalDigits = indicatorNumDecimals;
        string textToSet = nodeData.y.ToString();
        graph.changeLabelText(indicatorGO.transform.GetChild(0).GetChild(0).gameObject, textToSet);
    }

    void blinkCurrentPointAnimation(bool fromOnCompleteAnimateAdd = false)
    {
        graph.Refresh(); // Ensures gamobject list of series points is up to date based on pointValues
        WMG_Node lastPoint = series1.getLastPoint().GetComponent<WMG_Node>();
        string blinkingPointAnimId = series1.GetHashCode() + "blinkingPointAnim";
        DOTween.Kill(blinkingPointAnimId);
        blinkingTween = lastPoint.objectToScale.transform.DOScale(new Vector3(blinkScale, blinkScale, blinkScale), blinkAnimDuration).SetEase(plotEaseType)
            .SetUpdate(false).SetId(blinkingPointAnimId).SetLoops(-1, LoopType.Yoyo);
        if (series1.pointValues.Count > 1)
        { // ensure previous point scale reset
            WMG_Node blinkingNode = series1.getPoints()[series1.getPoints().Count - 2].GetComponent<WMG_Node>();
            if (fromOnCompleteAnimateAdd)
            { // removing a pointValues index deletes the gameobject at the end, so need to set the timeline from the previous tween
                blinkingTween.Goto(blinkAnimDuration * blinkingNode.objectToScale.transform.localScale.x / blinkScale, true);
            }
            blinkingNode.objectToScale.transform.localScale = Vector3.one;
        }
    }

    void UpdateIndicatorSize(WMG_Axis_Graph aGraph)
    {
        aGraph.changeSpritePositionTo(graphOverlay, aGraph.graphBackground.transform.parent.transform.localPosition);
        float indicatorWidth = (aGraph.getSpriteWidth(aGraph.graphBackground) - aGraph.paddingLeftRight[0] - aGraph.paddingLeftRight[1]);
        aGraph.changeSpriteSize(indicatorGO, Mathf.RoundToInt(indicatorWidth), 2);
        aGraph.changeSpritePositionToX(indicatorGO, indicatorWidth / 2f);
        //updateIndicator();
    }

    string customAxisLabelLabelerToE(WMG_Axis axis, int labelIndex)
    {
        float num = axis.AxisMinValue + labelIndex * (axis.AxisMaxValue - axis.AxisMinValue) / (axis.axisLabels.Count - 1);
        yAxisNumberFormatInfo.CurrencyDecimalDigits = axis.numDecimalsAxisLabels;
        string str = num.ToString("E", yAxisNumberFormatInfo);
        string strStart = str.Substring(0, 3);
        string strEnd = str.Substring(7, str.Length - 7);
        return strStart + strEnd;
    }

}
