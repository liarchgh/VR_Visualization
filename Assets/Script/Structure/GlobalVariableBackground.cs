using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariableBackground
{

    private static GlobalVariableBackground instance = null;



    public static GlobalVariableBackground Instance

    {
        get
        {
            if (instance == null)
            {
                instance = new GlobalVariableBackground();
            }
            return instance;
        }

    }

    /// <summary>
    /// 所有对象的当前帧值
    /// </summary>
    public int FrameAllIndex = 0;

    /// <summary>
    /// 模型缓存帧数量
    /// </summary>
    public int modelCacheCount = 5;

    /// <summary>
    /// 播放总帧数
    /// </summary>
    public int FrameALLCount = 0;

    /// <summary>
    /// 播放标识
    /// </summary>
    public bool playRun = false;

    /// <summary>
    /// 进度条的刷新时间间隔 秒
    /// </summary>
    public int timeIntervalPercentage = 1;

    /// <summary>
    /// 控制管理对象
    /// </summary>
    public ControlManager conManager;

    /// <summary>
    /// 面模式父物体预制体
    /// </summary>
    public GameObject modelPerfab;


    public GameObject waterPerfab;

    /// <summary>
    /// 点模式基础预制体
    /// </summary>
    public GameObject pointPerfab;

    /// <summary>
    /// 点模式 所需的迷你Mesh
    /// </summary>
    public Mesh miniCubeMesh;


    public Mesh ArrowMesh;

    /// <summary>
    /// 点模式使用的GPURows 材质
    /// </summary>
    public Material MaterialGPURows;
    public Material MaterialGPURows1;
    /// <summary>
    /// 面和线模式使用的 boat材质
    /// </summary>
    public Material Materialboat;

    /// <summary>
    /// 拾取线和点使用的 高光
    /// </summary>
    public Material MaterialLine;

    /// <summary>
    /// 拾取线和点使用的 高光2
    /// </summary>
    public Material MaterialSelectLine;

    /// <summary>
    /// 点模式材质缓存 汇总对象
    /// </summary>
    public Others.MaterialPerfab materialPerfab;

    /// <summary>
    /// 是否使用VTK解析文件
    /// </summary>
//    public bool isUseVtk = false;

    /// <summary>
    /// 切刀
    /// </summary>
    public GameObject CutKnife;

    public WMG_X_Tutorial_Custom TableObject;

    /// <summary>
    /// 切割加载数据
    /// </summary>
    public int CutLoadingCount = 0;

    /// <summary>
    /// 线条宽度
    /// </summary>
    public float Linewight = 1f;

    public float pointWigth = 0.4f;

    public float ArrowWigth = 1f;
}
