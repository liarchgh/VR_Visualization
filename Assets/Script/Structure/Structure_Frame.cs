
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 单帧数据结构体
/// </summary>
public class Structure_Frame
{

    private int MaxCount = 65000;

    /// <summary>
    /// Mesh
    /// </summary>
    public Others.SameMesh mesh;

    /// <summary>
    /// 点列表
    /// </summary>
    public List<Structure_Point> point = new List<Structure_Point>();

    /// <summary>
    /// 网格列表
    /// </summary>
    public List<Structure_Grid> grid = new List<Structure_Grid>();


    /// <summary>
    /// 点的属性最大最小值
    /// </summary>
    public Dictionary<Enums.Property_ParamType, Others.MaxAndMin> point_MaxAndMin = new Dictionary<Enums.Property_ParamType, Others.MaxAndMin>();

    public bool tooBig = false;//点数量或网格关系超过了56000 限制数量则为True

    /// <summary>
    /// 拆分后的Mesh集合
    /// </summary>
    public List<Others.SameMesh> listChildMesh;

    /// <summary>
    /// 拆分后的点关系 与原始数据的点关系映射关系
    /// </summary>
    public List<Dictionary<int, int>> listDicChildPointRelation;

    /// <summary>
    /// 
    /// </summary>
    public List<Dictionary<int, int>> listDicChildGridRelation;


    public Dictionary<int, int> dicGridRelation;


    public bool isLoadFinish = false;

    public int selfFrameIndex = 0;

    public DateTime timeLoadStart;

    public DateTime timeLoadEnd;

    /// <summary>
    /// 是否有点和网格数据信息
    /// </summary>
    private bool isHavePointAndGrid = true;

    /// <summary>
    /// 校验Mesh是否过大
    /// </summary>
    public void CheckOutMesh()
    {
        if (mesh == null)
        {
            return;
        }
        if (point.Count == 0 && grid.Count == 0)
        {
            isHavePointAndGrid = false;
        }

            tooBig = (mesh.vertices.Length > MaxCount || mesh.triangles.Length > MaxCount);
        if (!tooBig)
        {
            return;
        }
        listChildMesh = new List<Others.SameMesh>();
        if (isHavePointAndGrid)
        {
            listDicChildPointRelation = new List<Dictionary<int, int>>();
            listDicChildGridRelation = new List<Dictionary<int, int>>();
        }
        List<Vector3> listNewVec = new List<Vector3>();
        List<Int32> listNewTriangles = new List<Int32>();

        int indexTri=0, indexNewTri = 0,indexNewVec = 0;
        Dictionary<int, int> dicOldToNewIndex = new Dictionary<int, int>();//创建 新旧三角面点关系映射字典  键为旧 值为新
        Dictionary<int, int> dicNewToOldIndex;
        Dictionary<int, int> dicChildgridrelation;
        dicNewToOldIndex = new Dictionary<int, int>();//创建 新旧三角面点关系映射字典  键为新 值为旧
        dicChildgridrelation = new Dictionary<int, int>();//创建 网格新旧关系映射 键为新，值为旧

        bool ishave = false;
        Others.SameMesh NewMesh;
        int indexGridRelation = 0;
        for (; indexTri < mesh.triangles.Length; indexTri++)
        {
            if (indexTri!=0 && indexTri % 3 == 0)
            {
                //每满MaxCount 则生成一个新的子物体保存
                if (listNewVec.Count + 3 > MaxCount || listNewTriangles.Count + 3 > MaxCount)
                {
                    NewMesh = new Others.SameMesh();

                    NewMesh.vertices = listNewVec.ToArray();
                    NewMesh.triangles = listNewTriangles.ToArray();
                    listChildMesh.Add(NewMesh);
                    if (isHavePointAndGrid)
                    {
                        listDicChildPointRelation.Add(dicNewToOldIndex);
                        listDicChildGridRelation.Add(dicChildgridrelation);
                    }
                    listNewVec.Clear();
                    listNewTriangles.Clear();
                    dicOldToNewIndex.Clear();
                    if (isHavePointAndGrid)
                    {
                        dicNewToOldIndex = new Dictionary<int, int>();
                        dicChildgridrelation = new Dictionary<int, int>();
                    }
                }
            }

            ishave = dicOldToNewIndex.ContainsKey(mesh.triangles[indexTri]);//先判断 当前点是否已存在数组中
            if (ishave)
            {
                listNewTriangles.Add(dicOldToNewIndex[mesh.triangles[indexTri]]);
                indexNewTri = listNewTriangles.Count - 1;
                
            }
            else
            {
                //映射字典中不存在此关系则
                listNewVec.Add(mesh.vertices[mesh.triangles[indexTri]]);//根据当前三角面的点关系 获取 旧的点坐标并映射到新的 点坐标数组
                indexNewVec = listNewVec.Count - 1;//新的点数组索引 = 新的数组总数-1;

                listNewTriangles.Add(indexNewVec);//将 新的三角面点关系 保存至新的点关系数组
                dicOldToNewIndex.Add(mesh.triangles[indexTri], listNewTriangles[listNewTriangles.Count-1]);//将 新旧的映射保存在字典中
                if (isHavePointAndGrid)
                {
                    dicNewToOldIndex.Add(listNewTriangles[listNewTriangles.Count - 1], mesh.triangles[indexTri]);
                }
            }
            if (isHavePointAndGrid)
            {
                //每处理3个点则 默认处理完毕一个三角面，则将新的面ID 与数据的面ID 映射 保存
                if ((indexTri + 1) % 3 == 0)
                {
                    dicChildgridrelation.Add((indexTri) / 3, dicGridRelation[indexGridRelation]);
                    indexGridRelation++;
                }
            }

        }
        NewMesh = new Others.SameMesh();

        NewMesh.vertices = listNewVec.ToArray();
        NewMesh.triangles = listNewTriangles.ToArray();
        listChildMesh.Add(NewMesh);
        if (isHavePointAndGrid)
        {
            listDicChildPointRelation.Add(dicNewToOldIndex);
            listDicChildGridRelation.Add(dicChildgridrelation);
        }
        listNewVec.Clear();
        listNewTriangles.Clear();
        mesh.triangles = null;//释放内存
        mesh.vertices = null;//释放内存
        mesh = null;

    }

    public float minX=999f;
    public float minY=999f;
    public float minZ=999f;
    public float maxX=-999f;
    public float maxY=-999f;
    public float maxZ=-999f;

}
