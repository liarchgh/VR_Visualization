using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
//using UnityEngine;

public class VTKManager  {

    //Others.ThreadLoadFilePack package;



    //public void LoadFileTest(string file_name)
    //{
    //    List<string> meshOldData = new List<string>();
    //    StreamReader sr;
    //    if (File.Exists(file_name))
    //    {
    //        sr = File.OpenText(file_name);
    //    }
    //    else
    //    {
    //        return;
    //    }
    //    meshOldData.Clear();
    //    string str;

    //    System.DateTime dt = System.DateTime.Now;

    //    while ((str = sr.ReadLine()) != null)
    //        meshOldData.Add(str);//加上str的临时变量是为了避免sr.ReadLine()在一次循环内执行两次  

    //    System.DateTime dt2 = System.DateTime.Now;
    //    double d = (dt2 - dt).TotalMilliseconds;

    //    sr.Close();

    //    dt = System.DateTime.Now;

    //    string[] strs = File.ReadAllLines(file_name);


    //    dt2 = System.DateTime.Now;
    //    d = (dt2 - dt).TotalMilliseconds;


    //    meshOldData.Clear();
    //    strs = null;

    //}


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
    //        //Debug.LogWarning("Not find files! " + file_name);
    //        return null;
    //    }
    //    meshOldData.Clear();
    //    string str;
    //    while ((str = sr.ReadLine()) != null)
    //        meshOldData.Add(str);//加上str的临时变量是为了避免sr.ReadLine()在一次循环内执行两次  

    //    sr.Close();
    //    return meshOldData;
    //}


    //public void DisposeVTK(object pack)
    //{
    //    package = pack as Others.ThreadLoadFilePack;

    //    package.Frame.timeLoadStart = DateTime.Now;

    //    List<string> listStr = ReadFileList(package.fileAddres.FullName);
    //    DataDispose(listStr, ref package.Frame);
    //    listStr.Clear();
    //    package.Frame.CheckOutMesh();
    //    package.Frame.isLoadFinish = true;
    //}

    //public bool DataDispose(List<string> mesholdData, ref Structure_Frame frame)
    //{
    //    if (mesholdData == null || mesholdData[0] == null)
    //    {
    //        return false;
    //    }
    //    if (mesholdData[0].ToLower().Contains("# vtk datafile version"))
    //    {
    //        string[] strTemp = mesholdData[0].Split(' ');
    //        switch (strTemp[strTemp.Length-1])
    //        {
    //            case "4.1":
    //            case "4.2":
    //                DisposeVTK4_2(mesholdData,ref frame);
    //                break;
    //            default:
    //                break;
    //        }

    //    }


    //    return true;
    //}


    //public bool DisposeVTK4_2(List<string> mesholdData, ref Structure_Frame frame)
    //{
    //    int DataCount = mesholdData.Count;
    //    int lineIndex = 0;
    //    string[] strTemp;
    //    //ASCII
    //    if (!(DataCount >= 3 && mesholdData[2].ToUpper().Trim().Contains("ASCII")))
    //    {
    //        return false;
    //    }
    //    //DATASET POLYDATA 需要处理啥？？？？
    //    if (!(DataCount >= 4))
    //    {
    //        return false;
    //    }
    //    // strTemp= mesholdData[3].Split(' ');


    //    if (!(DataCount >= 5))
    //    {
    //        return false;
    //    }
    //    //POINTS 76610 float  
    //    strTemp = mesholdData[4].Split(' ');
    //    if (strTemp.Length < 3)
    //    {
    //        return false;
    //    }
    //    Others.SameMesh mesh;
    //    if (frame.mesh != null)
    //    {
    //        mesh = frame.mesh;
    //    } else
    //    {
    //        mesh = new Others.SameMesh();
    //        frame.mesh = mesh;
    //    }

    //    switch (strTemp[0])
    //    {
    //        case "POINTS":
    //            #region POINTS
    //            lineIndex = 5;

    //            int pointCount = Convert.ToInt32(strTemp[1]);
    //            if (frame.point.Count != 0)
    //            {
    //                frame.point.Clear();
    //            }
    //            Vector3[] meshVector3 = new Vector3[pointCount];
    //            Vector3 vec3;
    //            int lineValueCount , vecLineCount;
                
    //            //处理点数据
    //            for (int pointIndex = 0; pointIndex < pointCount;)
    //            {
    //                strTemp = mesholdData[lineIndex].Trim().Split(' ');
    //                lineValueCount = strTemp.Length;
    //                vecLineCount = lineValueCount / 3;
    //                for (int Index = 0; Index < vecLineCount; Index++)
    //                {
    //                    vec3 = new Vector3();
    //                    // Unity 与 数据源的 坐标系不同 此处Y ，Z 调换
    //                    vec3.x = Convert.ToSingle(strTemp[Index * 3 + 0]);
    //                    vec3.y = Convert.ToSingle(strTemp[Index * 3 + 2]);
    //                    vec3.z = Convert.ToSingle(strTemp[Index * 3 + 1]);

    //                    frame.minX = Mathf.Min(vec3.x, frame.minX);
    //                    frame.minY = Mathf.Min(vec3.y, frame.minY);
    //                    frame.minZ = Mathf.Min(vec3.z, frame.minZ);

    //                    frame.maxX = Mathf.Max(vec3.x, frame.maxX);
    //                    frame.maxY = Mathf.Max(vec3.x, frame.maxY);
    //                    frame.maxZ = Mathf.Max(vec3.x, frame.maxZ);

    //                    meshVector3[pointIndex] = vec3;
    //                    if (package.structure.dataType == Enums.DataType.Default)
    //                    { 
    //                        Structure_Point point = new Structure_Point();
    //                        point.pointID = pointIndex;
    //                        point.pointVec3 = vec3;
    //                        frame.point.Add(point);
    //                    }
    //                    pointIndex++;
    //                }
    //                lineIndex++;

    //            }
    //            mesh.vertices = null;
    //            mesh.vertices = meshVector3;

    //            #endregion
    //            break;
    //        default:
    //            break;
    //    }
    //    for (; lineIndex < DataCount; lineIndex++)
    //    {
    //        if (mesholdData[lineIndex].ToUpper().Contains("POLYGONS")||
    //            mesholdData[lineIndex].ToUpper().Contains("CELLS") ||
    //            mesholdData[lineIndex].ToUpper().Contains("TRIANGLE_STRIPS"))
    //        {
    //            break;
    //        }
    //    }
    //    strTemp = mesholdData[lineIndex].Trim().ToUpper().Split(' ');
    //    if (strTemp.Length < 3)
    //    {
    //        return false;
    //    }
    //    if (package.structure.dataType == Enums.DataType.Default)
    //    {
    //        if (frame.dicGridRelation == null)
    //        {
    //            frame.dicGridRelation = new Dictionary<int, int>();
    //        }
    //        else
    //        {
    //            frame.dicGridRelation.Clear();
    //        }
    //    }

    //    if (frame.dicGridRelation != null)
    //    {
    //        frame.dicGridRelation.Clear();
    //    }
    //    if (frame.grid != null)
    //    {
    //        foreach (var item in frame.grid)
    //        {
    //            item.listPoint.Clear();
    //        }
    //        frame.grid.Clear();
    //    }


    //    switch (strTemp[0])
    //    {
    //        case "POLYGONS":
    //        case "TRIANGLE_STRIPS":

    //            #region POLYGONS & TRIANGLE_STRIPS
    //            int TrianglesLineCount = Convert.ToInt32(strTemp[1]);
    //            //int secondCount = Convert.ToInt32(strTemp[2]);
    //            //int trianglesCount = TrianglesLineCount * 3 + (secondCount - (TrianglesLineCount * 4)) * 6;
    //            //int[] meshTriangles = new int[trianglesCount];



    //            List<int> listMeshTriangles = new List<int>();
    //            lineIndex++;
    //            int relationIndex = 0;
    //            int forCount = lineIndex + TrianglesLineCount;
    //            for (; lineIndex < forCount; lineIndex++)
    //            {
    //                strTemp = mesholdData[lineIndex].Trim().Split(' ');
    //                for (int i = 0; i < strTemp.Length-3; i++)
    //                {
    //                        listMeshTriangles.Add(Convert.ToInt32(strTemp[1]));
    //                        listMeshTriangles.Add(Convert.ToInt32(strTemp[i + 2]));
    //                        listMeshTriangles.Add(Convert.ToInt32(strTemp[i + 3]));
    //                    if (package.structure.dataType == Enums.DataType.Default)
    //                    {
    //                        frame.dicGridRelation.Add(relationIndex, frame.grid.Count);
    //                        ////frame.dicChildGridRelation.Add(relationIndex, grid.gridID);
    //                        relationIndex++;
    //                    }
    //                    //????点如何加入到网格数组中 且不重复
    //                }

    //                if (package.structure.dataType == Enums.DataType.Default)
    //                {
    //                    Structure_Grid grid = new Structure_Grid();
    //                    grid.pointCount = strTemp.Length - 1;
    //                    for (int i = 1; i < strTemp.Length; i++)
    //                    {
    //                        grid.listPoint.Add(frame.point[Convert.ToInt32(strTemp[i])]);
    //                    }
    //                    frame.grid.Add(grid);
    //                }
    //            }

    //            mesh.triangles = listMeshTriangles.ToArray();
    //            listMeshTriangles.Clear();
    //            #endregion

    //            break;
    //        case "CELLS":
    //            int TrianglesLineCount01 = Convert.ToInt32(strTemp[1]);
    //            List<int> listMeshTriangles01 = new List<int>();
    //            lineIndex++;
    //            int relationIndex01 = 0;
    //            int forCount01 = lineIndex + TrianglesLineCount01;
    //            for (; lineIndex < forCount01; lineIndex++)
    //            {
    //                strTemp = mesholdData[lineIndex].Trim().Split(' ');
    //                for (int i = 0; i < strTemp.Length - 3; i++)
    //                {
    //                    listMeshTriangles01.Add(Convert.ToInt32(strTemp[1]));
    //                    listMeshTriangles01.Add(Convert.ToInt32(strTemp[i + 2]));
    //                    listMeshTriangles01.Add(Convert.ToInt32(strTemp[i + 3]));
    //                    if (package.structure.dataType == Enums.DataType.Default)
    //                    {
    //                        frame.dicGridRelation.Add(relationIndex01, frame.grid.Count);
    //                        ////frame.dicChildGridRelation.Add(relationIndex, grid.gridID);
    //                        relationIndex01++;
    //                    }
    //                    //????点如何加入到网格数组中 且不重复
    //                }
    //                if (package.structure.dataType == Enums.DataType.Default)
    //                {
    //                    Structure_Grid grid = new Structure_Grid();
    //                    grid.pointCount = Convert.ToInt32(strTemp[0]);
    //                    for (int i = 1; i < strTemp.Length; i++)
    //                    {
    //                        grid.listPoint.Add(frame.point[Convert.ToInt32(strTemp[i])]);
    //                    }
    //                    frame.grid.Add(grid);
    //                }
    //            }
    //            mesh.triangles = listMeshTriangles01.ToArray();
    //            listMeshTriangles01.Clear();
    //            break;

    //        default:
    //            break;
    //    }
    //    string NextAtt = "" ;
    //        while (lineIndex< DataCount)
    //    {
    //        while (true)
    //        {
    //            if (lineIndex >= DataCount)
    //            {
    //                break;
    //            }
    //            else if (mesholdData[lineIndex].Contains("POINT_DATA") && mesholdData[lineIndex + 1].Contains("NORMALS"))
    //            {
    //                NextAtt = "NORMALS";
    //                break;
    //            }
    //            else
    //            if (mesholdData[lineIndex].Contains("Pressure"))
    //            {
    //                NextAtt = "Pressure";
    //                break;
    //            }
    //            else if (mesholdData[lineIndex].Contains("CELL_DATA") && mesholdData[lineIndex + 1].Contains("SCALARS") && mesholdData[lineIndex + 1].Contains("cell_scalars"))
    //            {
    //                NextAtt = "cell_scalars";
    //                break;
    //            }
    //            lineIndex++;
    //        }
    //        switch (NextAtt)
    //        {
    //            case "NORMALS"://矢量值
    //                //lineIndex += 2;
    //                //int pointCount = frame.point.Count;
    //                //for (int i = 0; i < pointCount; i++)
    //                //{

    //                //}
    //                break;
    //            case "Pressure":
    //                break;
    //            case "cell_scalars":
    //                break;
    //            default:
    //                break;
    //        }
    //        lineIndex++;
    //    }

    //    return true; 
    //}



}
