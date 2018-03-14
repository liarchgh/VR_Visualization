using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using UnityEngine;

public class VTKCManager {

    

    public void DisposeData(object pack)
    {
        Others.ThreadLoadFilePack package = pack as Others.ThreadLoadFilePack;

        package.vtkFrame.timeLoadStart = DateTime.Now;

        loadQFFile(package.fileAddres.FullName,package.vtkFrame);

        package.vtkFrame.timeLoadQFEnd = DateTime.Now;

        package.vtkFrame.isLoadFinish = true;
    }

    public void CutModelDispose(object pack)
    {
        GlobalVariableBackground.Instance.CutLoadingCount++;
        Others.ThreadCutModelPack package = pack as Others.ThreadCutModelPack;

        package.vtkFrame.timeLoadStart = DateTime.Now;

        int index = package.fileAddres.FullName.LastIndexOf('\\');
        string strDir = package.fileAddres.FullName.Substring(0, index) + @"\CutModel\";
        string str = strDir + package.fileAddres.Name;
        if (!Directory.Exists(strDir))
        {
            Directory.CreateDirectory(strDir);
        }

        CLoadTest.cutModel(package.fileAddres.FullName, str, package.model16, package.plane16);

        loadQFFile(str, package.vtkFrame);

        package.vtkFrame.timeLoadQFEnd = DateTime.Now;

        package.vtkFrame.isLoadFinish = true;
        pack = null;
        GlobalVariableBackground.Instance.CutLoadingCount--;
    }

    public void SmoothDispose(object pack)
    {
        Others.ThreadSmoothModePack package = pack as Others.ThreadSmoothModePack;

        package.vtkFrame.timeLoadStart = DateTime.Now;

        int index = package.fileAddres.FullName.LastIndexOf('\\');
        string strDir = package.fileAddres.FullName.Substring(0, index) + @"\SmoothModel\";
        string str = strDir + package.fileAddres.Name;
        if (!Directory.Exists(strDir))
        {
            Directory.CreateDirectory(strDir);
        }
        CLoadTest.smoothModel(package.fileAddres.FullName, str, package.smoothCount);

        loadQFFile(str, package.vtkFrame);

        package.vtkFrame.timeLoadQFEnd = DateTime.Now;

        package.vtkFrame.isLoadFinish = true;
        pack = null;
    }

    public void loadQFFile(string fileAddress, VTK_Frame frame)
    {
        FileStream fs = new FileStream(fileAddress, FileMode.Open, FileAccess.Read);
        BinaryReader br = new BinaryReader(fs);
        int meshCount = 0;
        int pointArrayCount = 0;
        int cellArrayCount = 0;
        int NameLength = 0;
        int tempCount = 0;

        frame.minX = br.ReadSingle();
        frame.maxX = br.ReadSingle();

        frame.minZ = br.ReadSingle();
        frame.maxZ = br.ReadSingle();

        frame.minY = br.ReadSingle();
        frame.maxY = br.ReadSingle();

        //meshCount
        br.ReadChars(10);
        meshCount = br.ReadInt32();


        Thread t = new Thread(frame.MeshClear);
        t.IsBackground = true;
        t.Start();

        //frame.MeshClear();
        frame.meshArray = new CLoadTest.VTKMesh[meshCount];

        //pointArrayCount
        br.ReadChars(16);
        pointArrayCount = br.ReadInt32();

        //cellArrayCount
        br.ReadChars(15);
        cellArrayCount = br.ReadInt32();

        if (frame.pointAttType == null)
        {
            frame.pointAttType = new CLoadTest.AttributType();
        }

        if (frame.pointAttType.listAttName == null)
        {
            frame.pointAttType.listAttName = new List<string>();
            frame.pointAttType.listAttCount = new List<int>();
        }
        else
        {
            frame.pointAttType.listAttName.Clear();
            frame.pointAttType.listAttCount.Clear();
        }

        for (int i = 0; i < pointArrayCount; i++)
        {
            NameLength = br.ReadInt32();

            frame.pointAttType.listAttName.Add(new string(br.ReadChars(NameLength)));
            frame.pointAttType.listAttCount.Add(br.ReadInt32());
        }

        if (frame.CellAttType == null)
        {
            frame.CellAttType = new CLoadTest.AttributType();
        }

        if (frame.CellAttType.listAttName == null)
        {
            frame.CellAttType.listAttName = new List<string>();
            frame.CellAttType.listAttCount = new List<int>();
        }
        else
        {
            frame.CellAttType.listAttName.Clear();
            frame.CellAttType.listAttCount.Clear();
        }

        for (int i = 0; i < cellArrayCount; i++)
        {
            NameLength = br.ReadInt32();

            frame.CellAttType.listAttName.Add(new string(br.ReadChars(NameLength)));
            frame.CellAttType.listAttCount.Add(br.ReadInt32());
        }

        int pointCount = 0;
        int cellCount = 0;
        for (int i = 0; i < meshCount; i++)
        {
            //mesh
            br.ReadChars(5);
            tempCount = br.ReadInt32();
            CLoadTest.VTKMesh vtkMesh = new CLoadTest.VTKMesh();
            frame.meshArray[tempCount] = vtkMesh;
            vtkMesh.meshIndex = tempCount;
            //pointCount
            br.ReadChars(11);
            pointCount = br.ReadInt32();
            vtkMesh.pointArray = new CLoadTest.Point_Out[pointCount];
            vtkMesh.vec3Array = new Vector3[pointCount];
            for (int j = 0; j < pointCount; j++)
            {
                CLoadTest.Point_Out point = new CLoadTest.Point_Out();
                vtkMesh.pointArray[j] = point;
                Vector3 vec3 = new Vector3();

                vec3.x = br.ReadSingle();

                vec3.z = br.ReadSingle();

                vec3.y = br.ReadSingle();

                vtkMesh.vec3Array[j] = vec3;

                //point.color = br.ReadSingle();
                br.ReadSingle();
                point.number = br.ReadInt32();
                point.attArray = new CLoadTest.Attribut[pointArrayCount];
                for (int k = 0; k < pointArrayCount; k++)
                {
                    CLoadTest.Attribut att = new CLoadTest.Attribut();
                    point.attArray[k] = att;

                    att.arrayData = new float[frame.pointAttType.listAttCount[k]];
                    for (int m = 0; m < frame.pointAttType.listAttCount[k]; m++)
                    {
                        att.arrayData[m] = br.ReadSingle();
                    }
                }
            }
            
            //cellCount
            br.ReadChars(10);
            cellCount = br.ReadInt32();
            vtkMesh.cellArray = new CLoadTest.Cell_Out[cellCount];
            vtkMesh.triArray = new int[cellCount*3];
            //vtkMesh.listTri = new List<int>();
            for (int j = 0; j < cellCount; j++)
            {
                CLoadTest.Cell_Out cell = new CLoadTest.Cell_Out();
                vtkMesh.cellArray[j] = cell;
                //cell.index_1 = br.ReadInt32();
                //cell.index_2 = br.ReadInt32();
                //cell.index_3 = br.ReadInt32();

                vtkMesh.triArray[j * 3 + 2] = br.ReadInt32();
                vtkMesh.triArray[j * 3 + 1] = br.ReadInt32();
                vtkMesh.triArray[j * 3] = br.ReadInt32();
                //vtkMesh.listTri.Add(br.ReadInt32());
                //vtkMesh.listTri.Add(br.ReadInt32());
                //vtkMesh.listTri.Add(br.ReadInt32());

                cell.number = br.ReadInt32();
                cell.attArray = new CLoadTest.Attribut[cellArrayCount];
                for (int k = 0; k < cellArrayCount; k++)
                {
                    CLoadTest.Attribut att = new CLoadTest.Attribut();
                    cell.attArray[k] = att;

                    att.arrayData = new float[frame.CellAttType.listAttCount[k]];
                    for (int m = 0; m < frame.CellAttType.listAttCount[k]; m++)
                    {
                        att.arrayData[m] = br.ReadSingle();

                    }
                }
            }

            for (int j = 0; j < vtkMesh.pointArray.Length; j++)
            {
                if (vtkMesh.pointArray[j] == null)
                {
                    Debug.Log("读数据阶段 vtkMesh.pointArray["+j+"] is null");
                }
            }

        }
        br.Close();
        fs.Close();
    }

}
