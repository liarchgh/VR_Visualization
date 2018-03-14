using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 所有帧数据结构体
/// </summary>
public class Structure_AllFrame {

    /// <summary>
    /// 所有帧名称
    /// </summary>
    public string name;

    /// <summary>
    /// 单帧数据结构体字典
    /// </summary>
    public Dictionary<int,Structure_Frame> dicFrame;


    /// <summary>
    /// 点的属性最大最小值
    /// </summary>
    public Dictionary<Enums.Property_ParamType, Others.MaxAndMin> point_MaxAndMin;

    /// <summary>
    /// 总帧数
    /// </summary>
    public int FileCount = 0;


    /// <summary>
    /// 加载文件地址对象
    /// </summary>
    public Structure_LoadFile loadFile;

}
