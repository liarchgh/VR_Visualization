using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using HTC.UnityPlugin.PoseTracker;
//using HTC.UnityPlugin.Vive;
using System;

/// <summary>
/// 全局事件单例类 执行优先级 高
/// </summary>
public class OverallEvent_Manger : Singleton<OverallEvent_Manger>
{
    //public Action StartReadFile;

    #region 委托声明

    /// <summary>
    /// 定义泛型委托
    /// </summary>
    /// <typeparam name="T">参数类型</typeparam>
    /// <param name="args">参数</param>
    public delegate void FAction<T>(T args);

    #endregion

    #region 事件声明

    /// <summary>
    /// 开始加载文件事件 需要写在 Awake 里注册事件 
    /// </summary>
    public event FAction<Structure_LoadFile> StartReadFile;

    /// <summary>
    /// 动画状态操作 对应 枚举 AnimationFrame
    /// </summary>
    public event FAction<AnimationFrame> AnimationState;

    /// <summary>
    /// 设置起始帧动画
    /// </summary>
    public event FAction<int> StartFPS;

    /// <summary>
    /// 显示点线面的状态
    /// </summary>
    public event FAction<ShowPointLinePlaneState> ShowState;

    /// <summary>
    /// 设置对称类型
    /// </summary>
    public event FAction<string> SetSymmetryType;

    /// <summary>
    /// 更新 显示UI上的transform 的值
    /// </summary>
    public event Action UpdateTransformTest;

    /// <summary>
    /// 设置拾取方式 （点 或 网格）
    /// </summary>
    public event FAction<Pickup> SetPickup;

    #endregion

    #region 内部事件声明

    /// <summary>
    /// 设置数据操作类型 操作方式例如移动缩放
    /// </summary>
    public event FAction<DataOperationType> SetDataOperationType;

    /// <summary>
    /// 设置全局声音
    /// </summary>
    public Action SetVoice;

    /// <summary>
    /// 选中物体的事件  物体被选中则调用（显示菜单用）
    /// </summary>
    public event FAction<GameObject> PitchOnEvent;
    #endregion


    #region 事件方法广播

    /// <summary>
    /// 开始读取文件 事件广播
    /// </summary>
    /// <param name="structure_LoadFile"></param>
    public void BroadcastStartReadFile(Structure_LoadFile structure_LoadFile)
    {
        if (StartReadFile != null)
        {
            StartReadFile(structure_LoadFile);
        }
        Debug.Log(structure_LoadFile.name);
        Debug.Log(structure_LoadFile.name + "      " + structure_LoadFile.FileDir);
        Debug.Log(structure_LoadFile.name + "      " + structure_LoadFile.fileType);
    }

    /// <summary>
    /// 广播动画帧播放动画
    /// </summary>
    /// <param name="animation">动画状态枚举类型</param>
    public void BroadcastAnimationState(AnimationFrame animation)
    {
        Debug.Log("调用" + animation);
        if (AnimationState != null)
        {
            AnimationState(animation);
        }
    }

    /// <summary>
    /// 设置起始帧
    /// </summary>
    /// <param name="fps">起始帧数</param>
    public void SetStartFPS(int fps)
    {
        Debug.Log("StartFPS :     " + fps);
        if (StartFPS != null)
        {
            StartFPS(fps);
        }
    }

    /// <summary>
    /// 显示点线面
    /// </summary>
    public void ShowPointLinePlane(ShowPointLinePlaneState showPointLinePlaneState)
    {
        //Debug.Log("调用" + showPointLinePlaneState);
        //if (ShowState != null)
        //{
        //    ShowState(showPointLinePlaneState);
        //}
        ////隐藏 二层级
        //if (UI_Manger.Instance != null)
        //{
        //    UI_Manger.Instance.TwoMenu_ShowList.SetActive(false);
        //}
    }

    /// <summary>
    /// 选中事件 会显示出此操作的对应方法 （菜单）例如移动旋转
    /// </summary>
    public void PitchOnObj(GameObject Obj)
    {
        if (PitchOnEvent != null)
        {
            PitchOnEvent(Obj);
        }
    }


    /// <summary>
    /// 切换数据操作类型的方法
    /// </summary>
    /// <param name="dataOperationType">数据操作类型枚举</param>
    public void SwitchDataOperationType(DataOperationType dataOperationType)
    {
        //设置数据操作类型
        if (SetDataOperationType != null)
        {
            SetDataOperationType(dataOperationType);
        }
    }

    /// <summary>
    /// 设置对称 
    /// </summary>
    /// <param name="str">沿着哪个轴对称</param>
    public void SetSymmetry(string str)
    {
        //Debug.Log("设置对称轴为 ： " + str);
        //if (SetSymmetryType != null)
        //    SetSymmetryType(str);
        ////隐藏 二层级
        //if (UI_Manger.Instance != null)
        //{
        //    UI_Manger.Instance.TwoMenu_ShowList.SetActive(false);
        //}
    }


    /// <summary>
    /// 设置拾取类型 （点 或 网格）
    /// </summary>
    /// <param name="pickup">拾取类型枚举</param>
    public void SetPickupState(Pickup pickup)
    {
        //Debug.Log("设置拾取类型" + pickup);
        //if (SetPickup != null)
        //{
        //    SetPickup(pickup);
        //}
        ////隐藏 二层级
        //if (UI_Manger.Instance != null)
        //{
        //    UI_Manger.Instance.TwoMenu_ShowList.SetActive(false);
        //}
    }

    #endregion



    /// <summary>
    /// 更新 ui 上选中物体的transform值
    /// </summary>
    public void UpdateTest()
    {
        if (UpdateTransformTest != null)
        {
            UpdateTransformTest();
        }
    }

    /// <summary>
    /// 更新当前对象的 fixedUpdate 的方法
    /// </summary>
    private GameObject UpdateObj;


    /// <summary>
    /// 时时更新选中的对象身上的脚本
    /// </summary>
    //private void Update()
    //{
    //    if (GloableVariables.PitchOnGameObject == UpdateObj) return;
    //    if (UpdateObj != null && UpdateObj.GetComponent<NewBehaviourScript>())
    //    {
    //        UpdateObj.GetComponent<NewBehaviourScript>().enabled = false;
    //    }
    //    UpdateObj = GloableVariables.PitchOnGameObject;
    //    if (UpdateObj != null && UpdateObj.GetComponent<NewBehaviourScript>())
    //        UpdateObj.GetComponent<NewBehaviourScript>().enabled = true;
    //}
}
