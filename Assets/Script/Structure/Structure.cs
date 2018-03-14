using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using UnityEngine;

/// <summary>
/// 对象结构体
/// </summary>
public class Structure
{


    /// <summary>
    /// 结构体名称
    /// </summary>
    //public string name;

    /// <summary>
    /// 所有帧数据结构体
    /// </summary>
    public Structure_AllFrame allFrame;


    public VTK_AllFrame vtkAllFrame;


    /// <summary>
    /// 船运动数据
    /// </summary>
    public Dictionary<Enums.ShipMotionType, List<float>> shipMotion_Data;

    /// <summary>
    /// 加载文件地址对象
    /// </summary>
    public Structure_LoadFile loadFile;



    ///// <summary>
    ///// 
    ///// </summary>
    ///// <param name="index">modelIndex</param>
    //public void ModelLoad(int index)
    //{
    //    int modelCount = GlobalVariableBackground.Instance.modelCacheCount;
    //    int modelMax = allFrame.FileCount;
    //    int modelmin = Math.Max(index - ((modelCount / 2)), 0);
    //    Dictionary<int, Structure_Frame> dicModel = allFrame.dicFrame;

    //    int modelmax = Math.Max(index + ((modelCount / 2)), modelCount - 1);
    //    modelmax = Math.Min(modelmax, modelMax - (modelCount / 2) );

    //    int min = modelmin;
    //    int max = modelmax;

    //    foreach (var item in dicModel.Keys)
    //    {
    //        min = Math.Min(min, item);
    //        max = Math.Max(max, item);
    //    }

    //    for (int i = min; i <= max; i++)
    //    {
    //        if (i < modelmin && dicModel.ContainsKey(i))
    //        {
    //            dicModel.Remove(i);
    //        }
    //        else if (i > modelmax && dicModel.ContainsKey(i))
    //        {
    //            dicModel.Remove(i);
    //        }
    //        else if (i >= modelmin && i <= modelmax && !dicModel.ContainsKey(i))
    //        {
    //            //dicModel.Add(i, "frame " + i);
    //        }

    //    }


    //}


}
