using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class TecplotManager 
{


    //public List<string> ReadFileList(string file_name)
    //{
    //    List<string> meshOldData = new List<string>();
    //    StreamReader sr;
    //    if (File.Exists(file_name))
    //    {
    //        sr = File.OpenText(file_name);
    //    }
    //    else
    //    {
    //        Debug.LogWarning("Not find files! " + file_name);
    //        return null;
    //    }
    //    meshOldData.Clear();
    //    string str;
    //    while ((str = sr.ReadLine()) != null)
    //        meshOldData.Add(str);//加上str的临时变量是为了避免sr.ReadLine()在一次循环内执行两次  

    //    sr.Close();
    //    return meshOldData;
    //}

    ///// <summary>
    ///// 数据处理
    ///// </summary>
    ///// <param name="mesholdData">数据String列表</param>
    ///// <returns>格式是否正确</returns>
    //public bool DataDispose(List<string> mesholdData,ref Structure_Frame frame)
    //{
    //    if (mesholdData == null || mesholdData[0] == null)
    //    {
    //        return false;
    //    }
    //    if (mesholdData[0].Contains("VARIABLES="))
    //    {
    //        //存在参数头 栗子：  VARIABLES= "X","Y","Z","PRESS"  
    //        int index = mesholdData[0].IndexOf('=');
    //        string[] strParamArray = mesholdData[0].Substring(index + 1).Split(',');
    //        int Vector3Length = Convert.ToInt32(mesholdData[1].Substring(7, 45).Trim());
    //        int TrianglesLength = Convert.ToInt32(mesholdData[1].Substring(55, 33).Trim());
    //        int posLength = mesholdData[2].Length / strParamArray.Length;
    //        int posTrianglesLength = -1;//三角面连接顺序行的总长度 初始化；
    //        if (frame == null)
    //        {
    //            frame = new Structure_Frame();
    //        }

    //        Vector3[] meshVector3;
    //        if (frame.mesh != null && frame.mesh.vertices != null&&
    //            frame.mesh.vertices.Length == Vector3Length)
    //        {
    //            meshVector3 = frame.mesh.vertices;
    //        }
    //        else
    //        {
    //            meshVector3 = new Vector3[Vector3Length];
    //        }

    //        int[] meshTriangles;

    //        if (frame.mesh != null && frame.mesh.triangles != null &&
    //            frame.mesh.triangles.Length == TrianglesLength * 6)
    //        {
    //            meshTriangles = frame.mesh.triangles;
    //        }
    //        else
    //        {
    //            meshTriangles = new int[TrianglesLength * 6];
    //        }

    //        int count = 0;
    //        int count02 = 0;
    //        int index01 = 0, index02 = 0, index03 = 0, index04 = 0;
    //        int relationIndex=1,gridCount=0;

    //        if (frame.dicGridRelation == null)
    //        {
    //            frame.dicGridRelation = new Dictionary<int, int>();
    //        }
    //        else
    //        {
    //            frame.dicGridRelation.Clear();
    //        }

    //        for (int i = 2; i < mesholdData.Count; i++)
    //        {
    //            if (count < Vector3Length)
    //            {
    //                Vector3 vec = new Vector3();
    //                // Unity 与 数据源的 坐标系不同 此处Y ，Z 调换
    //                vec.x = Convert.ToSingle(mesholdData[i].Substring(0, posLength).Trim());
    //                vec.y = Convert.ToSingle(mesholdData[i].Substring(posLength * 2, posLength).Trim());
    //                vec.z = Convert.ToSingle(mesholdData[i].Substring(posLength, posLength).Trim());
    //                meshVector3[count] = vec;


    //                Structure_Point point;
    //                if (frame.point.Count>count &&  frame.point[count] != null)
    //                {
    //                    point = frame.point[count];
    //                } else
    //                {
    //                    point = new Structure_Point();
    //                    frame.point.Add(point);
    //                }

    //                point.pointID = count;
    //                point.pointVec3 = vec;


    //                if (strParamArray.Length >= 4)
    //                {
    //                    for (int forIndex = 3; forIndex < strParamArray.Length; forIndex++)
    //                    {
    //                        #region PRESS
    //                        //存在压力参数 处理
    //                        if (strParamArray[forIndex].Contains("PRESS"))
    //                        {
    //                            float value = Convert.ToSingle(mesholdData[i].Substring(posLength * forIndex, posLength).Trim());
    //                            if (point.pointParameter.ContainsKey(Enums.Property_ParamType.PRESS))
    //                            {
    //                                point.pointParameter[Enums.Property_ParamType.PRESS] = value;
    //                            }
    //                            else
    //                            {
    //                                point.pointParameter.Add(Enums.Property_ParamType.PRESS, value);
    //                            }

    //                            //Others.MaxAndMin maxAndmin;
    //                            //if (!frame.point_MaxAndMin.ContainsKey(Enums.Property_ParamType.PRESS))
    //                            //{
    //                            //    maxAndmin = new Others.MaxAndMin();
    //                            //    frame.point_MaxAndMin.Add(Enums.Property_ParamType.PRESS, maxAndmin);
    //                            //}
    //                            //maxAndmin = frame.point_MaxAndMin[Enums.Property_ParamType.PRESS];
    //                            //maxAndmin.Max = Math.Max(maxAndmin.Max, value);
    //                            //maxAndmin.Min = Math.Min(maxAndmin.Min, value);
    //                        }
    //                        #endregion


    //                        //待填充 其他属性
    //                    }
    //                }

    //            }
    //            else if (count >= Vector3Length)
    //            {
    //                if (posTrianglesLength == -1)
    //                {
    //                    posTrianglesLength = (mesholdData[i].Length / 4) + 1;//行数不满4倍数 +1补位
    //                }
    //                //三角形连接顺序
    //                index01 = Convert.ToInt32(mesholdData[i].Substring(0, posTrianglesLength).Trim());
    //                index02 = Convert.ToInt32(mesholdData[i].Substring(posTrianglesLength, posTrianglesLength).Trim());
    //                index03 = Convert.ToInt32(mesholdData[i].Substring(posTrianglesLength * 2, posTrianglesLength).Trim());
    //                index04 = Convert.ToInt32(mesholdData[i].Substring(posTrianglesLength * 3, mesholdData[i].Length - (posTrianglesLength * 3)).Trim());



    //                meshTriangles[count02] = index01 - 1;
    //                count02++;
    //                meshTriangles[count02] = index02 - 1;
    //                count02++;
    //                meshTriangles[count02] = index03 - 1;
    //                count02++;

    //                if (frame.dicGridRelation.ContainsKey(relationIndex))
    //                {
    //                    frame.dicGridRelation[relationIndex] = gridCount;
    //                }
    //                else
    //                {
    //                    frame.dicGridRelation.Add(relationIndex, gridCount);//三角网格 与 数据网格的对应关系
    //                }
    //                //frame.dicChildGridRelation.Add(relationIndex, grid.gridID);
    //                relationIndex++;

    //                meshTriangles[count02] = index01 - 1;
    //                count02++;
    //                meshTriangles[count02] = index03 - 1;
    //                count02++;
    //                meshTriangles[count02] = index04 - 1;
    //                count02++;

    //                if (frame.dicGridRelation.ContainsKey(relationIndex))
    //                {
    //                    frame.dicGridRelation[relationIndex] = gridCount;
    //                }
    //                else
    //                {
    //                    frame.dicGridRelation.Add(relationIndex, gridCount);//三角网格 与 数据网格的对应关系
    //                }
    //                relationIndex++;


    //                Structure_Grid grid;
    //                if (frame.grid != null &&
    //                    frame.grid.Count> gridCount &&
    //                    frame.grid[gridCount]!=null)
    //                {
    //                    grid = frame.grid[gridCount];
    //                }
    //                else
    //                {
    //                    grid = new Structure_Grid();
    //                    frame.grid.Add(grid);
    //                }
    //                //grid.gridID = gridID++;//网格编号由1开始
    //                grid.pointCount = 4;//默认固定4边型

    //                if (grid.listPoint != null&& grid.listPoint.Count>=4)
    //                {
    //                    grid.listPoint[0] = frame.point[index01 - 1];
    //                    grid.listPoint[1] = frame.point[index02 - 1];
    //                    grid.listPoint[2] = frame.point[index03 - 1];
    //                    grid.listPoint[3] = frame.point[index04 - 1];
    //                }
    //                else
    //                {
    //                    grid.listPoint.Add(frame.point[index01 - 1]);
    //                    grid.listPoint.Add(frame.point[index02 - 1]);
    //                    grid.listPoint.Add(frame.point[index03 - 1]);
    //                    grid.listPoint.Add(frame.point[index04 - 1]);
    //                }
    //                gridCount++;
    //            }
    //            count++;
    //        }


    //        if (frame.mesh == null)
    //        {
    //            Others.SameMesh mesh = new Others.SameMesh();
    //            mesh.vertices = meshVector3;
    //            mesh.triangles = meshTriangles;

    //            frame.mesh = mesh;
    //        }


    //    }
    //    else
    //    {
    //        return false;
    //    }
    //    return true;

    //}

    //public string filename;
    //public GameObject objPrim;

    ////private void Start()
    ////{
    ////    List<string> listStr = ReadFileList(filename);
    ////    Structure_Frame frame = gameObject.AddComponent<Structure_Frame>();
    ////    //frame = new Structure_Frame();
    ////    DataDispose(listStr,ref frame);

    ////    frame.CheckOutMesh();

    ////    if (frame.tooBig)
    ////    {
    ////        GameObject newOBJ;

    ////        foreach (var item in frame.listChildMesh)
    ////        {
    ////            newOBJ = GameObject.Instantiate(objPrim, transform.position, objPrim.transform.rotation);
    ////            newOBJ.name = "Obj" + transform.childCount;
    ////            newOBJ.transform.parent = transform;

    ////            Mesh objMesh = newOBJ.GetComponent<MeshFilter>().mesh;
    ////            objMesh.vertices = item.vertices;
    ////            objMesh.triangles = item.triangles;
    ////            objMesh.RecalculateNormals();
    ////        }

    ////    }
    ////    else
    ////    {
    ////        MeshFilter meshFilter = transform.GetComponent<MeshFilter>();
    ////        meshFilter.mesh.vertices = frame.mesh.vertices;
    ////        meshFilter.mesh.triangles = frame.mesh.triangles;
    ////        meshFilter.mesh.RecalculateNormals();
    ////    }
    ////    gameObject.AddComponent<MeshCollider>();

    ////}

    ////void Update()
    ////{
    ////    /**点选*/
    ////    if (Input.GetMouseButtonDown(0))
    ////    {//点出鼠标左键  
    ////     //通过鼠标点击，摄像机从屏幕上一点发射出一条射线，此处的参数应为屏幕坐标  
    ////        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    ////        RaycastHit hit;//用来保存射线碰撞信息  
    ////                       //射线投射  
    ////        if (Physics.Raycast(ray, out hit))
    ////        {
    ////            if (hit.transform == null)//如果射线没有碰撞到Transform,return  
    ////                return;

    ////            int indexData = gameObject.GetComponent<Structure_Frame>().dicChildGridRelation[hit.triangleIndex];
    ////            GlobalVariableBackground.Instance.conManager.WriteLog("hit.triangleIndex is" + hit.triangleIndex+ " , indexData is "+ indexData);

    ////        }
    ////    }
    ////}


    //public void DisposeTecplot(object pack)
    //{
    //    Others.ThreadLoadFilePack package = pack as Others.ThreadLoadFilePack;

    //    package.Frame.timeLoadStart = DateTime.Now;

    //    List<string> listStr = ReadFileList(package.fileAddres.FullName);

    //    DataDispose(listStr, ref package.Frame);
    //    listStr.Clear();
    //    package.Frame.CheckOutMesh();
    //    package.Frame.isLoadFinish = true;
    //}


    ///// <summary>
    ///// 获取单帧的所有参数的最大最小值并与所有帧比对大小
    ///// </summary>
    ///// <param name="pack"></param>
    //public void GetValueMaxAndMin(object pack)
    //{
    //    Others.ThreadGetMaxAndMinPack package = pack as Others.ThreadGetMaxAndMinPack;
    //    List<string> listStr = ReadFileList(package.fileAddres.FullName);
    //    Dictionary<Enums.Property_ParamType, Others.MaxAndMin> dic
    //        = LoadFileGetMaxAndMin(listStr);
    //    lock (package.objLock)
    //    {
    //        foreach (var item in dic)
    //        {
    //            if (!package.AllFrame.point_MaxAndMin.ContainsKey(item.Key))
    //            {
    //                package.AllFrame.point_MaxAndMin.Add(item.Key, item.Value);
    //            }
    //            else
    //            {
    //                package.AllFrame.point_MaxAndMin[item.Key].Max = Math.Max(package.AllFrame.point_MaxAndMin[item.Key].Max, item.Value.Max);
    //                package.AllFrame.point_MaxAndMin[item.Key].Min = Math.Min(package.AllFrame.point_MaxAndMin[item.Key].Min, item.Value.Min);
    //            }
    //        }
    //    }
    //}

    //public Dictionary<Enums.Property_ParamType, Others.MaxAndMin> LoadFileGetMaxAndMin(List<string> mesholdData)
    //{
    //    Dictionary<Enums.Property_ParamType, Others.MaxAndMin> dic = new Dictionary<Enums.Property_ParamType, Others.MaxAndMin>();
    //    if (mesholdData == null || mesholdData[0] == null)
    //    {
    //        //return false;
    //    }
    //    if (mesholdData[0].Contains("VARIABLES="))
    //    {
    //        //存在参数头 栗子：  VARIABLES= "X","Y","Z","PRESS"  
    //        int index = mesholdData[0].IndexOf('=');
    //        string[] strParamArray = mesholdData[0].Substring(index + 1).Split(',');
    //        int paramlength = strParamArray.Length;

    //        int Vector3Length = Convert.ToInt32(mesholdData[1].Substring(7, 45).Trim());
    //        int posLength = mesholdData[2].Length / strParamArray.Length;

    //        for (int i = 3; i < paramlength; i++)
    //        {
    //            if (strParamArray[i].Contains("PRESS"))
    //            {
    //                Others.MaxAndMin maxAndMin = new Others.MaxAndMin();
    //                dic.Add(Enums.Property_ParamType.PRESS, maxAndMin);
    //            }
    //            //补充其他参数
    //        }
    //        int count = 0;
    //        for (int i = 2; i < mesholdData.Count; i++)
    //        {
    //            if (count < Vector3Length)
    //            {
    //                if (strParamArray.Length >= 4)
    //                {
    //                    for (int forIndex = 3; forIndex < strParamArray.Length; forIndex++)
    //                    {
    //                        #region PRESS
    //                        //存在压力参数 处理
    //                        if (strParamArray[forIndex].Contains("PRESS"))
    //                        {
    //                            float value = Convert.ToSingle(mesholdData[i].Substring(posLength * forIndex, posLength).Trim());
    //                            dic[Enums.Property_ParamType.PRESS].Max = Math.Max(dic[Enums.Property_ParamType.PRESS].Max, value);
    //                            dic[Enums.Property_ParamType.PRESS].Min = Math.Min(dic[Enums.Property_ParamType.PRESS].Min, value);

    //                        }
    //                        #endregion


    //                        //待填充 其他属性
    //                    }
    //                }
    //            }
    //            else
    //            {
    //                break;
    //            }
    //        }

    //    }
    //    return dic;
    //}
}
