using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 点结构体
/// </summary>
public class Structure_Point{

    /// <summary>
    /// 点ID
    /// </summary>
    public int pointID;

    /// <summary>
    /// 点坐标
    /// </summary>
    public Vector3 pointVec3;

    /// <summary>
    /// 点属性对象
    /// </summary>
    public Dictionary<Enums.Property_ParamType, float> pointParameter = new Dictionary<Enums.Property_ParamType, float>();

}

