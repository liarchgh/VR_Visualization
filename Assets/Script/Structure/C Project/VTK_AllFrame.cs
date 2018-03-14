using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VTK_AllFrame  {

    public string name;

    public List<VTK_Frame> listFrame;

    /// <summary>
    /// 点的属性最大最小值
    /// </summary>
    public List<Others.MaxAndMin> point_MaxAndMin;

    /// <summary>
    /// 总帧数
    /// </summary>
    public int FileCount = 0;

    /// <summary>
    /// 加载文件地址对象
    /// </summary>
    public Structure_LoadFile loadFile;

    public Others.ModelState modelState;

    public void ClearListFrame()
    {
        if (listFrame != null)
        {
            List<VTK_Frame> temp = listFrame;
            for (int i = 0; i < temp.Count; i++)
            {
                temp[i].MeshClear();
                temp[i] = null;
            }
            temp = null;
        }
    }
}
