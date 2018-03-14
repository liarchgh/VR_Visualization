using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VTK_Frame {

    public UnityEngine.GameObject go;

    public CLoadTest.AttributType pointAttType;

    public CLoadTest.AttributType CellAttType;

    public CLoadTest.VTKMesh[] meshArray;
    
    /// <summary>
    /// 点的属性最大最小值
    /// </summary>
    public List<Others.MaxAndMin> point_MaxAndMin;

    public float minX = 999f;
    public float minY = 999f;
    public float minZ = 999f;
    public float maxX = -999f;
    public float maxY = -999f;
    public float maxZ = -999f;

    public bool isLoadFinish = false;

    public bool isAllFinish = false;

    public int selfFrameIndex = 0;

    public DateTime timeStartLession;

    public DateTime timeLoadStart;

    public DateTime timeLoadEnd;

    public DateTime timeLoadQFEnd;


    public void MeshClear()
    {
        if (meshArray != null)
        {
            CLoadTest.VTKMesh[] tempMesh = meshArray;
            for (int i = 0; i < tempMesh.Length; i++)
            {
                tempMesh[i].Clear();
            }
            tempMesh = null;
        }
    }
}
