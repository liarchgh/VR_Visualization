using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class CLoadTest : MonoBehaviour
{


    /// <summary>
    /// 属性结构体
    /// </summary>
    public class Attribut 
    {
        //public string name ;

        public float[] arrayData;

        public void Clear()
        {
            if (arrayData != null)
            {
                arrayData = null;
            }
        }
    }

    /// <summary>
    /// 属性类型及数量名称
    /// </summary>
    public class AttributType
    {
        //public Enums.AttributType type;
        public List<string> listAttName;
        public List<int> listAttCount;
        //public List<float> attMax;
        //public List<float> attMin;

        public void Clear()
        {
            if (listAttName != null)
            {
                listAttName.Clear();
                listAttName = null;
            }
            if (listAttCount != null)
            {
                listAttCount.Clear();
                listAttCount = null;
            }

        }
    }

    /// <summary>
    /// 点结构体
    /// </summary>
    public class Point_Out
    {
        //public float x;

        //public float y;

        //public float z;

        public float color;

        public int testint;

        public Attribut[] attArray;

        public int number;

        public void Clear()
        {
            if (attArray != null)
            {
                for (int i = 0; i < attArray.Length; i++)
                {
                    attArray[i].Clear();
                    attArray[i] = null;
                }
                attArray = null;
            }
        }
    }

    /// <summary>
    /// 网格的点关系拓扑结构体
    /// </summary>
    public class Cell_Out
    {
        //public int index_1;
        //public int index_2;
        //public int index_3;

        public int number;

        public Attribut[] attArray;
        public void Clear()
        {
            if (attArray != null)
            {
                for (int i = 0; i < attArray.Length; i++)
                {
                    attArray[i].Clear();
                    attArray[i] = null;
                }
                attArray = null;
            }
        }
    }

    public class VTKMesh
    {
        public int meshIndex = 0;

        public Point_Out[] pointArray;

        public Cell_Out[] cellArray;

        public Vector3[] vec3Array;

        public int[] triArray;

        public float[] Color;
        // public List<int> listTri;

        public void Clear()
        {
            if (pointArray != null)
            {
                for (int i = 0; i < pointArray.Length; i++)
                {
                    pointArray[i].Clear();
                    pointArray[i] = null;
                }
                pointArray = null;
            }
            if (cellArray != null)
            {
                for (int i = 0; i < cellArray.Length; i++)
                {
                    cellArray[i].Clear();
                    cellArray[i] = null;
                }
                cellArray = null;
            }
            vec3Array = null;
            triArray = null;
        }
    }

    ///// <summary>
    ///// 数据结构体
    ///// </summary>
    //public struct ModelData
    //{
    //    /// <summary>
    //    /// 点数量
    //    /// </summary>
    //    public int pointCount;

    //    /// <summary>
    //    /// 网格关系的拓扑数量
    //    /// </summary>
    //    public int cellCount;

    //    /// <summary>
    //    /// 点集合
    //    /// </summary>
    //    public Point[] pointArray;

    //    /// <summary>
    //    /// 拓扑集合
    //    /// </summary>
    //    public Cell[] cellArray;
    //}


    //[DllImport("Geometry")]
    //private static extern void cutModelVtk(string pathName, string outFileName, double[] planePos, double[] planeNormal);

    //[DllImport("Geometry")]
    //private static extern void cutModelDat(string pathName, string outFileName, double[] planePos, double[] planeNormal);

    [DllImport("Geometry")]
    public static extern void pretreatmentData_Dat(string inputAddress, string outAddress, int maxCount);

    [DllImport("Geometry")]
    public static extern void pretreatmentData_Vtk(string inputAddress, string outAddress, int maxCount);

    [DllImport("Geometry")]
    public static extern void cutModel(string addressIn, string addressOut, float[] modelMatrix16, float[] PlaneMatrix16);

    [DllImport("Geometry")]
    public static extern void smoothModel(string addressIn, string addressOut, int smoothCount);

}
