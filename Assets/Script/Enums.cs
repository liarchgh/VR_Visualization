using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 枚举类
/// </summary>
public class Enums {

    /// <summary>
    /// 属性参数
    /// </summary>
    public enum Property_ParamType
    {
        /// <summary>
        /// 压力
        /// </summary>
        PRESS,
        /// <summary>
        /// yita
        /// </summary>
        yita

    }

    /// <summary>
    /// 船运动参数
    /// </summary>
    public enum ShipMotionType
    {
        /// <summary>
        /// 横摇
        /// </summary>
        rolling,
        /// <summary>
        /// 纵摇
        /// </summary>
        pitching,
        /// <summary>
        /// 艏摇
        /// </summary>
        yawing,
        /// <summary>
        /// 纵荡
        /// </summary>
        surging,
        /// <summary>
        /// 横荡
        /// </summary>
        swaying,
        /// <summary>
        /// 垂荡
        /// </summary>
        heaving
    }


    /// <summary>
    /// 数据类型
    /// </summary>
    public enum DataType
    {
        /// <summary>
        /// 默认类型
        /// </summary>
        Default,
        /// <summary>
        /// 水类型
        /// </summary>
        Water
    }

    /// <summary>
    /// 文件类型
    /// </summary>
    public enum FileType
    {
        Tecplot,
        VTK,
        NONE
    }

    /// <summary>
    /// 数据模型显示模式
    /// </summary>
    public enum ShowMode
    {
        Point,
        Line,
        Cell,
        CellAndLine
    }

    //public enum AttributType
    //{
    //    PointType,
    //    CellType
    //}

    public enum PickMode
    {
        Point,
        cell
    }

    /// <summary>
    /// 对称模式
    /// </summary>
    public enum SymmetricMode
    {
        NONE,
        X,
        Y,
        Z
    }


}
