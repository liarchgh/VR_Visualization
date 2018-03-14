using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ClipMesh : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// 切割面法线
    /// </summary>
    public Vector3 m_ClipPlaneNormal;

    /// <summary>
    /// 切割面点坐标
    /// </summary>
    public Vector3 m_ClipPlanePoint;

    /// <summary>
    /// 
    /// </summary>
    private bool m_IsClearSamePoint = false;

    /// <summary>
    /// 切割自己的Mesh网格 根据 切割面法线和点坐标
    /// </summary>
    void ClipSelfMesh()
    {
        MeshFilter mf = this.gameObject.GetComponent<MeshFilter>();

        List<Vector3> listVectice = mf.mesh.vertices.ToList<Vector3>();
        List<int> listTriangle = mf.mesh.triangles.ToList<int>();
        List<Vector3> listNormal = mf.mesh.normals.ToList<Vector3>();
        List<Vector2> listUV = mf.mesh.uv.ToList<Vector2>();

        //检查每个三角面，是否存在两个顶点连接正好在直线上
        for (int triangleIndex = 0; triangleIndex < listTriangle.Count;)
        {
            int trianglePointIndex0 = listTriangle[triangleIndex];
            int trianglePointIndex1 = listTriangle[triangleIndex + 1];
            int trianglePointIndex2 = listTriangle[triangleIndex + 2];

            Vector3 vecTrianglePoint0 = listVectice[trianglePointIndex0];
            Vector3 vecTrianglePoint1 = listVectice[trianglePointIndex1];
            Vector3 vecTrianglePoint2 = listVectice[trianglePointIndex2];

            //0-1 ,1-2 相连线段被切割
            if (GetPointToClipPlaneDis(vecTrianglePoint0) * GetPointToClipPlaneDis(vecTrianglePoint1) < 0 &&
                GetPointToClipPlaneDis(vecTrianglePoint1) * GetPointToClipPlaneDis(vecTrianglePoint2) < 0)
            {
                Vector3 vecNewPoint01 = GetLinePlaneCrossPoint(vecTrianglePoint0, vecTrianglePoint1);
                int index01 = GetIndexInList(listVectice, vecNewPoint01);
                if (index01 == -1 || !m_IsClearSamePoint)
                {
                    index01 = CreateNewPointInList(listVectice, vecNewPoint01, vecTrianglePoint0, vecTrianglePoint1, listUV, trianglePointIndex0, trianglePointIndex1, listNormal);
                    //listVectice.Add(vecNewPoint01);
                    //index01 = listVectice.Count - 1;
                    //float k01 = 0;
                    //if (!IsEqual(vecNewPoint01.x, vecTrianglePoint0.x) && !IsEqual(vecTrianglePoint1.x, vecTrianglePoint0.x))
                    //{
                    //    k01 = (vecNewPoint01.x - vecTrianglePoint0.x) / (vecTrianglePoint1.x - vecTrianglePoint0.x);
                    //}
                    //else if (!IsEqual(vecNewPoint01.y, vecTrianglePoint0.y) && !IsEqual(vecTrianglePoint1.y, vecTrianglePoint0.y))
                    //{
                    //    k01 = (vecNewPoint01.y - vecTrianglePoint0.y) / (vecTrianglePoint1.y - vecTrianglePoint0.y);
                    //}
                    //else
                    //{
                    //    k01 = (vecNewPoint01.z - vecTrianglePoint0.z) / (vecTrianglePoint1.z - vecTrianglePoint0.z);
                    //}

                    //if (listUV.Count > 0)
                    //{
                    //    Vector2 uv0 = listUV[trianglePointIndex0];
                    //    Vector2 uv1 = listUV[trianglePointIndex1];

                    //    float newUV_x = (uv1.x - uv0.x) * k01 + uv0.x;
                    //    float newUV_y = (uv1.y - uv0.y) * k01 + uv0.y;
                    //    listUV.Add(new Vector2(newUV_x,newUV_y));
                    //}

                    ////法向量
                    //Vector3 vecNormalX0 = listNormal[trianglePointIndex0];
                    //Vector3 vecNormalX1 = listNormal[trianglePointIndex1];
                    //float vecNewNormalX01 = (vecNormalX1.x - vecNormalX1.x) * k01 + vecNormalX0.x;
                    //float vecNewNormalY01 = (vecNormalX1.y - vecNormalX1.y) * k01 + vecNormalX0.y;
                    //float vecNewNormalZ01 = (vecNormalX1.z - vecNormalX1.z) * k01 + vecNormalX0.z;
                    //listNormal.Add(new Vector3(vecNewNormalX01,vecNewNormalY01,vecNewNormalZ01));
                }

                //求得1-2与切平面的交点
                Vector3 vecNewPoint12 = GetLinePlaneCrossPoint(vecTrianglePoint1, vecTrianglePoint2);
                int index12 = GetIndexInList(listVectice, vecNewPoint12);

                if (index12 == -1 || !m_IsClearSamePoint)
                {
                    index12 = CreateNewPointInList(listVectice, vecNewPoint12, vecTrianglePoint1, vecTrianglePoint2, listUV, trianglePointIndex1, trianglePointIndex2, listNormal);
                }

                //插入顶点索引，以此构建新三角形
                listTriangle.Insert(triangleIndex + 1, index01);
                listTriangle.Insert(triangleIndex + 2, index12);

                listTriangle.Insert(triangleIndex + 3, index12);
                listTriangle.Insert(triangleIndex + 4, index01);

                listTriangle.Insert(triangleIndex + 6, trianglePointIndex0);
                listTriangle.Insert(triangleIndex + 7, index12);
                triangleIndex += 9;
            }
            //1-2，2-0相连线段被切割
            else if (GetPointToClipPlaneDis(vecTrianglePoint1) * GetPointToClipPlaneDis(vecTrianglePoint2) < 0 &&
                GetPointToClipPlaneDis(vecTrianglePoint2) * GetPointToClipPlaneDis(vecTrianglePoint0) < 0)
            {
                //求得1-2与切平面的交点
                Vector3 vecNewPoint12 = GetLinePlaneCrossPoint(vecTrianglePoint1, vecTrianglePoint2);
                int index12 = GetIndexInList(listVectice, vecNewPoint12);

                if (index12 == -1 || !m_IsClearSamePoint)
                {
                    index12 = CreateNewPointInList(listVectice, vecNewPoint12, vecTrianglePoint1, vecTrianglePoint2, listUV, trianglePointIndex1, trianglePointIndex2, listNormal);
                }
                //求得0-2与切平面的交点
                Vector3 vecNewPoint02 = GetLinePlaneCrossPoint(vecTrianglePoint0, vecTrianglePoint2);
                int index02 = GetIndexInList(listVectice,vecNewPoint02);
                if (index02 == -1 || !m_IsClearSamePoint)
                {
                    index02 = CreateNewPointInList(listVectice,vecNewPoint02,vecTrianglePoint0,vecTrianglePoint2,listUV,trianglePointIndex0,trianglePointIndex2,listNormal);
                }


            }


        }

    }


    private int CreateNewPointInList(List<Vector3> listVectice,Vector3 vecNewPoint01,Vector3 vecTrianglePoint0,Vector3 vecTrianglePoint1,List<Vector2> listUV,int trianglePointIndex0,int trianglePointIndex1,List<Vector3> listNormal)
    {
        listVectice.Add(vecNewPoint01);
        int index01 = listVectice.Count - 1;
        float k01 = 0;
        if (!IsEqual(vecNewPoint01.x, vecTrianglePoint0.x) && !IsEqual(vecTrianglePoint1.x, vecTrianglePoint0.x))
        {
            k01 = (vecNewPoint01.x - vecTrianglePoint0.x) / (vecTrianglePoint1.x - vecTrianglePoint0.x);
        }
        else if (!IsEqual(vecNewPoint01.y, vecTrianglePoint0.y) && !IsEqual(vecTrianglePoint1.y, vecTrianglePoint0.y))
        {
            k01 = (vecNewPoint01.y - vecTrianglePoint0.y) / (vecTrianglePoint1.y - vecTrianglePoint0.y);
        }
        else
        {
            k01 = (vecNewPoint01.z - vecTrianglePoint0.z) / (vecTrianglePoint1.z - vecTrianglePoint0.z);
        }

        if (listUV.Count > 0)
        {
            Vector2 uv0 = listUV[trianglePointIndex0];
            Vector2 uv1 = listUV[trianglePointIndex1];

            float newUV_x = (uv1.x - uv0.x) * k01 + uv0.x;
            float newUV_y = (uv1.y - uv0.y) * k01 + uv0.y;
            listUV.Add(new Vector2(newUV_x, newUV_y));
        }

        //法向量
        Vector3 vecNormalX0 = listNormal[trianglePointIndex0];
        Vector3 vecNormalX1 = listNormal[trianglePointIndex1];
        float vecNewNormalX01 = (vecNormalX1.x - vecNormalX1.x) * k01 + vecNormalX0.x;
        float vecNewNormalY01 = (vecNormalX1.y - vecNormalX1.y) * k01 + vecNormalX0.y;
        float vecNewNormalZ01 = (vecNormalX1.z - vecNormalX1.z) * k01 + vecNormalX0.z;
        listNormal.Add(new Vector3(vecNewNormalX01, vecNewNormalY01, vecNewNormalZ01));

        return index01;
    }

    private int GetIndexInList(List<Vector3> list, Vector3 vec)
    {
        return list.FindIndex(a => a.x == vec.x && a.y == vec.y && a.z == vec.z);
    }

    /// <summary>
    /// 判断是否相等 忽略精确等级
    /// </summary>
    /// <param name="num1"></param>
    /// <param name="num2"></param>
    /// <returns></returns>
    bool IsEqual(float num1, float num2)
    {
        float absX = Mathf.Abs(num1 - num2);
        return absX < 0.00001f;
    }

    /// <summary>
    /// 获取点积
    /// </summary>
    /// <param name="point"></param>
    /// <returns></returns>
    float GetPointToClipPlaneDis(Vector3 point)
    {
        return Vector3.Dot((point - m_ClipPlanePoint), m_ClipPlaneNormal);
    }

    /// <summary>
    /// 获得两点
    /// </summary>
    /// <param name="lineBegin"></param>
    /// <param name="lineEnd"></param>
    /// <returns></returns>
    Vector3 GetLinePlaneCrossPoint(Vector3 lineBegin, Vector3 lineEnd)
    {
        //GlobalVariableBackground.Instance.conManager.WriteLog("起点：" + lineBegin + "," + "终点：" + lineEnd);
        float x = 0;
        float y = 0;
        float z = 0;
        float offsetZ = lineEnd.z - lineBegin.z;
        float offsetY = lineEnd.y - lineBegin.y;
        if (offsetZ != 0)
        {
            float k1 = (m_ClipPlaneNormal.x * (lineEnd.x - lineBegin.x) + m_ClipPlaneNormal.y * (lineEnd.y - lineBegin.y)) / offsetZ + m_ClipPlaneNormal.z;
            float k2 = (m_ClipPlaneNormal.x * lineBegin.z * (lineEnd.x - lineBegin.x) + m_ClipPlaneNormal.y * lineBegin.z * (lineEnd.y - lineBegin.y)) / offsetZ;
            float k3 = m_ClipPlaneNormal.x * lineBegin.x - m_ClipPlaneNormal.x * m_ClipPlanePoint.x + m_ClipPlaneNormal.y * lineBegin.y - m_ClipPlaneNormal.y * m_ClipPlanePoint.y - m_ClipPlaneNormal.z * m_ClipPlanePoint.z;
            z = (k2 - k3) / k1;
            x = (z - lineBegin.z) * (lineEnd.x - lineBegin.x) / offsetZ + lineBegin.x;
            y = (z - lineBegin.z) * (lineEnd.y - lineBegin.y) / offsetZ + lineBegin.y;
        }
        else if (offsetY != 0)
        {
            z = lineBegin.z;
            float k1 = m_ClipPlaneNormal.y + m_ClipPlaneNormal.x * (lineEnd.x - lineBegin.x) / (lineEnd.y - lineBegin.y);
            float k2 = m_ClipPlaneNormal.x * lineBegin.y * (lineEnd.x - lineBegin.x) / (lineEnd.y - lineBegin.y);
            float k3 = lineBegin.x * m_ClipPlaneNormal.x - m_ClipPlanePoint.x * m_ClipPlaneNormal.x - m_ClipPlaneNormal.y * m_ClipPlanePoint.y + m_ClipPlaneNormal.z * z - m_ClipPlaneNormal.z * m_ClipPlanePoint.z;
            y = (k2 - k3) / k1;
            x = (y - lineBegin.y) * (lineEnd.x - lineBegin.x) / (lineEnd.y - lineBegin.y) + lineBegin.x;
        }
        else
        {
            z = lineBegin.z;
            y = lineBegin.y;
            x = (m_ClipPlaneNormal.x * m_ClipPlanePoint.x - m_ClipPlaneNormal.y * (y - m_ClipPlanePoint.y) - m_ClipPlaneNormal.z * (z - m_ClipPlanePoint.z)) / m_ClipPlaneNormal.x;
        }



        // GlobalVariableBackground.Instance.conManager.WriteLog(x + "," + y + "," + z);
        return new Vector3(x, y, z);
    }

}
