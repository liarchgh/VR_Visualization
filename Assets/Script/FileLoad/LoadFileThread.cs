using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using UnityEngine;

public class LoadFileThread : MonoBehaviour
{



    private void Start()
    {
        //Structure_LoadFile file = new Structure_LoadFile();
        //file.name = "boat";
        //file.FileDir = @"D:\Documents\MyUnity\QianFan\VR_Visualization\Assets\streamingAssets\13";
        //file.fileType = Enums.FileType.Tecplot;
        //file.dataType = Enums.DataType.Default;
        //GetFileList(file);

        //file = new Structure_LoadFile();
        //file.name = "water";
        //file.FileDir = @"D:\Documents\MyUnity\QianFan\VR_Visualization\Assets\streamingAssets\14";
        //file.fileType = Enums.FileType.Tecplot;
        //file.dataType = Enums.DataType.Default;
        //GetFileList(file);

        //Structure_LoadFile file = new Structure_LoadFile();
        //file.name = "boat";
        //file.FileDir = @"D:\Documents\MyUnity\QianFan\VR_Visualization\Assets\streamingAssets\VTK01";
        //file.fileType = Enums.FileType.VTK;
        //file.dataType = Enums.DataType.Default;
        //GetFileList(file);

        //file = new Structure_LoadFile();
        //file.name = "water";
        //file.FileDir = @"D:\Documents\MyUnity\QianFan\VR_Visualization\Assets\streamingAssets\VTK02";
        //file.fileType = Enums.FileType.VTK;
        //file.dataType = Enums.DataType.Water;
        //GetFileList(file);


        // CLoadTest.test("tr");

        //Structure_LoadFile file = new Structure_LoadFile();

        //file.name = "boat";
        //file.FileDir = @"D:\Documents\MyUnity\QianFan\VR_Visualization\Assets\streamingAssets\VTKBoat\";
        //file.fileType = Enums.FileType.VTK;
        //file.dataType = Enums.DataType.Default;

        //GetFileList(file);


        //file = new Structure_LoadFile();
        //file.name = "water";
        //file.FileDir = @"D:\Documents\MyUnity\QianFan\VR_Visualization\Assets\streamingAssets\VTKWater\";
        //file.fileType = Enums.FileType.VTK;
        //file.dataType = Enums.DataType.Water;
        //GetFileList(file);




        //using (FileStream stream = File.OpenRead(@"D:\Documents\MyUnity\QianFan\VR_Visualization\Assets\streamingAssets\VTKBoat\ship_2.qf"))
        //{
        //    byte[] content = new byte[stream.Length];

        //    for (int i = 0; i < content.Length; i++)
        //    {
        //        content[i] = (byte)stream.ReadByte();
        //    }
        //    string str = System.Text.Encoding.UTF8.GetString(content);
        //    //Console.WriteLine();
        //    GlobalVariableBackground.Instance.conManager.WriteLog(str);
        //}

        //CLoadTest.Array_Out arr = new CLoadTest.Array_Out();
        //arr.nameLength = 10;
        //arr.name = "234567890";
        //arr.arraytype = 3;

        //arr.arrayData = new float[3];
        //arr.arrayData[0] = 0.1f;
        //arr.arrayData[1] = 0.2f;
        //arr.arrayData[2] = 0.3f;



        //CLoadTest.Array_Out arr2 = new CLoadTest.Array_Out();
        //arr2.nameLength = 10;

        //arr2.name = "334567890";
        //arr2.arraytype = 3;
        //arr2.arrayData = new float[3];
        //arr2.arrayData[0] = 1.1f;
        //arr2.arrayData[1] = 1.2f;
        //arr2.arrayData[2] = 1.3f;

        ////CLoadTest.test2(arr2, @"G:\FBX\sss2222.txt");

        //CLoadTest.Array_Out arr3 = new CLoadTest.Array_Out();
        //arr3.nameLength = 10;

        //arr3.name = "434567890";
        //arr3.arraytype = 3;
        //arr3.arrayData = new float[3];
        //arr3.arrayData[0] = 2.1f;
        //arr3.arrayData[1] = 2.2f;
        //arr3.arrayData[2] = 2.3f;

        //CLoadTest.Array_Out arr4 = new CLoadTest.Array_Out();
        //arr4.nameLength = 10;

        //arr4.name = "534567890";
        //arr4.arraytype = 3;
        //arr4.arrayData = new float[3];
        //arr4.arrayData[0] = 3.1f;
        //arr4.arrayData[1] = 3.2f;
        //arr4.arrayData[2] = 3.3f;

        //CLoadTest.Point_Out po = new CLoadTest.Point_Out();
        //po.x = 1f;
        //po.y = 2f;
        //po.z = 3f;
        //po.color = 4f;

        //po.testint = 1;

        //po.testA = new CLoadTest.Array_Out[1];
        //po.testA[0] = arr3;
        ////po.testA[1] = arr4;

        //po.array1 = arr;
        //po.array2 = arr2;

        //po.normal = new float[3];
        //po.normal[0] = 5f;
        //po.normal[1] = 6f;
        //po.normal[2] = 7f;

        //po.number = 8;

        //CLoadTest.test2(po.testA[0], @"G:\FBX\sss2222.txt");

        //CLoadTest.test(po, @"G:\FBX\sss.txt");


        //CLoadTest.Temp_1 te = new CLoadTest.Temp_1();
        //te.length = 20;
        //te.temp = new float[te.length];

        //for (int i = 0; i < te.length; i++)
        //{
        //    te.temp[i] = i + 0.3f;
        //}



        //CLoadTest.TriangleObject tobj = new CLoadTest.TriangleObject();

        //tobj = CLoadTest.loadQF(@"G:\FBX\ship_2.qf");

        //for (int i = 0; i < tobj.meshCount; i++)
        //{
        //    int length = tobj.mesh[i].pointArray.Length;
        //}

        //CLoadTest.pretreatmentData_Dat(@"G:\FBX\Data\boat\", @"G:\FBX\Data\boat\", 65000);
        //GlobalVariableBackground.Instance.conManager.WriteLog("生成完毕");

        //CLoadTest.pretreatmentData_Dat(@"G:\FBX\Data\water\", @"G:\FBX\Data\water\", 65000);
        //GlobalVariableBackground.Instance.conManager.WriteLog("生成完毕");

        //CLoadTest.pretreatmentData_Vtk(@"D:\Documents\MyUnity\QianFan\VR_Visualization\Assets\streamingAssets\VTKBoat\", @"D:\Documents\MyUnity\QianFan\VR_Visualization\Assets\streamingAssets\VTKBoat\", 65000);
        //GlobalVariableBackground.Instance.conManager.WriteLog("生成完毕");

        //CLoadTest.pretreatmentData_Vtk(@"D:\Documents\MyUnity\QianFan\VR_Visualization\Assets\streamingAssets\VTKWater\", @"D:\Documents\MyUnity\QianFan\VR_Visualization\Assets\streamingAssets\VTKWater\", 65000);
        //GlobalVariableBackground.Instance.conManager.WriteLog("生成完毕");

        //CLoadTest.pretreatmentData_Vtk(@"D:\Documents\MyUnity\QianFan\VR_Visualization\Assets\streamingAssets\VTK04Water\", @"D:\Documents\MyUnity\QianFan\VR_Visualization\Assets\streamingAssets\VTK04Water\", 65000);
        //GlobalVariableBackground.Instance.conManager.WriteLog("生成完毕");

        //List<VTK_Frame> list = new List<VTK_Frame>();
        //DateTime dstart = DateTime.Now;
        ////for (int i = 2; i < 11; i++)
        ////{
        //VTK_Frame frame = new VTK_Frame();
        ////list.Add(frame);
        ////loadQFFile(@"D:\Documents\MyUnity\QianFan\VR_Visualization\Assets\streamingAssets\VTKQFTest\ship_" + i + ".qf", frame);
        ////loadQFFile(@"D:\Documents\MyUnity\QianFan\VR_Visualization\Assets\streamingAssets\VTKQFWater\water_100.qf", frame);
        ////}
        //DateTime dend = DateTime.Now;
        //GlobalVariableBackground.Instance.conManager.WriteLog((dend - dstart).TotalMilliseconds);

        //CLoadTest.pretreatmentData_Dat(@"D:\Documents\MyUnity\QianFan\VR_Visualization\Assets\streamingAssets\14\", @"D:\Documents\MyUnity\QianFan\VR_Visualization\Assets\streamingAssets\14\", 65000);
        //GlobalVariableBackground.Instance.conManager.WriteLog("生成完毕");

        //GlobalVariableBackground.Instance.isUseVtk = true;
        //CLoadTest.pretreatmentData_Dat(@"D:\Documents\MyUnity\QianFan\VR_Visualization\Assets\streamingAssets\20\", @"D:\Documents\MyUnity\QianFan\VR_Visualization\Assets\streamingAssets\20\", 65000);
        //GlobalVariableBackground.Instance.conManager.WriteLog("生成完毕");       
        //CLoadTest.pretreatmentData_Dat(@"D:\Documents\MyUnity\QianFan\VR_Visualization\Assets\streamingAssets\21\", @"D:\Documents\MyUnity\QianFan\VR_Visualization\Assets\streamingAssets\21\", 65000);
        //GlobalVariableBackground.Instance.conManager.WriteLog("生成完毕");
        //CLoadTest.pretreatmentData_Vtk(@"D:\Documents\MyUnity\QianFan\VR_Visualization\Assets\streamingAssets\VTK03Boat\", @"D:\Documents\MyUnity\QianFan\VR_Visualization\Assets\streamingAssets\VTK03Boat\", 65000);
        //GlobalVariableBackground.Instance.conManager.WriteLog("生成完毕");
        //CLoadTest.pretreatmentData_Vtk(@"D:\Documents\MyUnity\QianFan\VR_Visualization\Assets\streamingAssets\VTK01\", @"D:\Documents\MyUnity\QianFan\VR_Visualization\Assets\streamingAssets\VTK01\", 65000);
        //GlobalVariableBackground.Instance.conManager.WriteLog("生成完毕");

        //CLoadTest.pretreatmentData_Vtk(@"D:\Documents\MyUnity\QianFan\VR_Visualization\Assets\streamingAssets\VTK02\", @"D:\Documents\MyUnity\QianFan\VR_Visualization\Assets\streamingAssets\VTK02\", 65000);
        //GlobalVariableBackground.Instance.conManager.WriteLog("生成完毕");

        //Structure_LoadFile file = new Structure_LoadFile();

        //file.FileDir = @"D:\Documents\MyUnity\QianFan\VR_Visualization\Assets\streamingAssets\VTK01\";
        //file.fileType = Enums.FileType.VTK;
        //file.dataType = Enums.DataType.Default;
        //VTKGetFileList(file);

        //file = new Structure_LoadFile();

        //file.FileDir = @"D:\Documents\MyUnity\QianFan\VR_Visualization\Assets\streamingAssets\VTK02\";
        //file.fileType = Enums.FileType.VTK;
        //file.dataType = Enums.DataType.Water;
        //VTKGetFileList(file);

        Structure_LoadFile file = new Structure_LoadFile();

        //file.FileDir = @"G:\FBX\Data\boat\";
        //file.fileType = Enums.FileType.Tecplot;
        //file.dataType = Enums.DataType.Default;
        //VTKGetFileList(file);

        //file = new Structure_LoadFile();
        //file.FileDir = @"G:\FBX\Data\water\";
        //file.fileType = Enums.FileType.Tecplot;
        //file.dataType = Enums.DataType.Water;
        //VTKGetFileList(file);

        //file.FileDir = @"D:\Documents\MyUnity\QianFan\VR_Visualization\Assets\streamingAssets\VTK03Boat\";
        //file.fileType = Enums.FileType.VTK;
        //file.dataType = Enums.DataType.Default;
        //VTKGetFileList(file);

        // file.FileDir = @"D:\Documents\MyUnity\QianFan\VR_Visualization\Assets\streamingAssets\VTKBoat\";
        // file.fileType = Enums.FileType.VTK;
        // file.dataType = Enums.DataType.Default;
        // VTKGetFileList(file);

        //file.FileDir = @"D:\Documents\MyUnity\QianFan\VR_Visualization\Assets\streamingAssets\20\";
        //file.fileType = Enums.FileType.Tecplot;
        //file.dataType = Enums.DataType.Default;
        //VTKGetFileList(file);

        file.FileDir = @"D:\Documents\MyUnity\QianFan\VR_Visualization\Assets\streamingAssets\13\";
        file.fileType = Enums.FileType.Tecplot;
        file.dataType = Enums.DataType.Default;
        VTKGetFileList(file);

        file = new Structure_LoadFile();
        file.FileDir = @"D:\Documents\MyUnity\QianFan\VR_Visualization\Assets\streamingAssets\14\";
        file.fileType = Enums.FileType.Tecplot;
        file.dataType = Enums.DataType.Water;
        VTKGetFileList(file);

        //file = new Structure_LoadFile();
        //file.FileDir = @"D:\Documents\MyUnity\QianFan\VR_Visualization\Assets\streamingAssets\21\";
        //file.fileType = Enums.FileType.VTK;
        //file.dataType = Enums.DataType.Water;
        //VTKGetFileList(file);

        //file = new Structure_LoadFile();
        //file.FileDir = @"D:\Documents\MyUnity\QianFan\VR_Visualization\Assets\streamingAssets\VTKWater\";
        //file.fileType = Enums.FileType.VTK;
        //file.dataType = Enums.DataType.Water;
        //VTKGetFileList(file);

        //file = new Structure_LoadFile();
        //file.FileDir = @"D:\Documents\MyUnity\QianFan\VR_Visualization\Assets\streamingAssets\VTK04Water\";
        //file.fileType = Enums.FileType.VTK;
        //file.dataType = Enums.DataType.Water;
        //VTKGetFileList(file);

        OverallEvent_Manger.Instance.StartReadFile += Instance_StartReadFile;

    }

    private void Instance_StartReadFile(Structure_LoadFile args)
    {
        VTKGetFileList(args);
    }



    /// <summary>
    /// 按照一定时间刷新读取文件的百分比
    /// </summary>
    /// <param name="fileAddress"></param>
    /// <param name="fileCount"></param>
    /// <returns></returns>
    IEnumerator GetPercentage(string fileAddress, int fileCount)
    {
        int time = Percentage(fileAddress, fileCount);
        if (time != 100)
        {
            yield return new WaitForSeconds(GlobalVariableBackground.Instance.timeIntervalPercentage);
            GetPercentage(fileAddress, fileCount);
        }
        else
        {

        }
    }


    /// <summary>
    /// 获取文件列表
    /// </summary>
    //public void GetFileList(Structure_LoadFile structure_loadFile)
    //{
    //    string extensionName = "";
    //    switch (structure_loadFile.fileType)
    //    {
    //        case Enums.FileType.Tecplot:
    //            extensionName = "*.dat";
    //            break;
    //        case Enums.FileType.VTK:
    //            extensionName = "*.vtk";
    //            break;
    //        default:
    //            break;
    //    }
    //    //创建所有帧对象
    //    Structure_AllFrame AllFrame = new Structure_AllFrame();
    //    //创建 单帧字典
    //    AllFrame.dicFrame = new Dictionary<int, Structure_Frame>();

    //    AllFrame.name = structure_loadFile.name;

    //    //创建 场景中的 数据模型载体
    //    GameObject newNodel = new GameObject();
    //    newNodel.transform.parent = transform;
    //    //挂上 功能脚本
    //    modelGameObject modelgo = newNodel.AddComponent<modelGameObject>();
    //    modelgo.AllFrame = AllFrame;
    //    modelgo.AllFrame.loadFile = structure_loadFile;
    //    //创建锁
    //    object objLock = new object();

    //    DirectoryInfo folder = new DirectoryInfo(structure_loadFile.FileDir);
    //    FileInfo[] fileinfoArray = folder.GetFiles(extensionName);
    //    SortAsFileName(ref fileinfoArray);
    //    AllFrame.FileCount = fileinfoArray.Length;
    //    GlobalVariableBackground.Instance.FrameALLCount = fileinfoArray.Length;
    //    //设定线程最大数量 
    //    ThreadPool.SetMaxThreads(GlobalVariableBackground.Instance.modelCacheCount, GlobalVariableBackground.Instance.modelCacheCount);

    //    //获取模型所有参数的最大最小值
    //    foreach (FileInfo file in fileinfoArray)
    //    {
    //        switch (structure_loadFile.fileType)
    //        {
    //            case Enums.FileType.Tecplot:

    //                Others.ThreadGetMaxAndMinPack pack = new Others.ThreadGetMaxAndMinPack();
    //                pack.AllFrame = AllFrame;
    //                pack.fileAddres = file;
    //                pack.objLock = objLock;
    //                TecplotManager t = new TecplotManager();
    //                //t.DisposeTecplot(pack);
    //                ThreadPool.QueueUserWorkItem(new WaitCallback(t.GetValueMaxAndMin), pack as object);

    //                break;
    //            case Enums.FileType.VTK:

    //                break;
    //            default:
    //                break;
    //        }

    //    }

    //    //加载指定缓存数量的模型数据
    //    for (int i = 0; i < GlobalVariableBackground.Instance.modelCacheCount; i++)
    //    {

    //        //生成 线程数据包
    //        GameObject goChild = GameObject.Instantiate(GlobalVariableBackground.Instance.cubePerfab);
    //        goChild.transform.parent = newNodel.transform;
    //        modelFrameGameObject modelChild = goChild.AddComponent<modelFrameGameObject>();
    //        Others.ThreadLoadFilePack pack = new Others.ThreadLoadFilePack();
    //        //创建 单帧
    //        Structure_Frame frame = new Structure_Frame();

    //        modelChild.frame = frame;
    //        modelChild.isCollider = structure_loadFile.dataType == Enums.DataType.Default;
    //        pack.vtkAllFrame = vtk;
    //        //填充 线程数据包-》单帧
    //        pack.Frame = frame;
    //        //填充 线程数据包-》单一文件
    //        pack.fileAddres = fileinfoArray[i];
    //        int fileIndex = i;
    //        //填充 单帧字典 ->按照文件编号
    //        AllFrame.dicFrame.Add(fileIndex, frame);

    //        goChild.name = AllFrame.name + "_" + fileIndex;
    //        frame.selfFrameIndex = fileIndex;


    //        switch (structure_loadFile.fileType)
    //        {
    //            case Enums.FileType.Tecplot:
    //                TecplotManager t = new TecplotManager();
    //                ThreadPool.QueueUserWorkItem(new WaitCallback(t.DisposeTecplot), pack as object);
    //                break;
    //            case Enums.FileType.VTK:
    //                VTKManager v = new VTKManager();
    //                //v.DisposeVTK(pack);
    //                ThreadPool.QueueUserWorkItem(new WaitCallback(v.DisposeVTK), pack as object);
    //                break;
    //            default:
    //                break;
    //        }
    //    }
    //    folder = null;
    //    fileinfoArray = null;

    //}
   


    public string GetSameName(string address )
    {
        DirectoryInfo folder = new DirectoryInfo(address);
        FileInfo[] fileinfoArray = folder.GetFiles("*.qf");

        char[] cha1 = fileinfoArray[0].Name.ToCharArray();
        char[] cha2 = fileinfoArray[fileinfoArray.Length-1].Name.ToCharArray();

        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < cha2.Length; i++)
        {
            if (cha1.Length > i)
            {
                if (cha2[i].Equals(cha1[i]))
                {
                    sb.Append(cha2[i]);
                }
                else
                {
                    break;
                }
            }
            else
            {
                break;
            }
        }
        return sb.ToString();
    }

    /// <summary>
    /// 获取文件列表
    /// </summary>
    public void VTKGetFileList(Structure_LoadFile structure_loadFile)
    {
        if (structure_loadFile.FileDir == null)
        {
            return;
        }

        List<Others.MaxAndMin> listm = new List<Others.MaxAndMin>();
        if (structure_loadFile.dataType == Enums.DataType.Default)
        {

            StreamReader sr;
            string file_name = structure_loadFile.FileDir + @"\Arr.mm";
            sr = File.OpenText(file_name);
            string strAboutAttMaxMin = sr.ReadLine();
            sr.Close();
            sr.Dispose();

            string[] strTempArray = strAboutAttMaxMin.Trim().Split(' ');
            Others.MaxAndMin mn;
            for (int i = 0; i < strTempArray.Length;)
            {
                mn = new Others.MaxAndMin();
                mn.Attname = strTempArray[i++];
                mn.Dimension = Convert.ToInt32(strTempArray[i++]);
                mn.isPointAtt = strTempArray[i++].Contains("point");
                mn.Min = Convert.ToSingle(strTempArray[i++]);
                mn.Max = Convert.ToSingle(strTempArray[i++]);
                listm.Add(mn);
            }
        }
        structure_loadFile.name = GetSameName(structure_loadFile.FileDir);

        string extensionName = "*.qf";

        //switch (structure_loadFile.fileType)
        //{
        //    case Enums.FileType.Tecplot:
        //        extensionName = "*.dat";
        //        break;
        //    case Enums.FileType.VTK:
        //        extensionName = "*.vtk";
        //        break;
        //    default:
        //        break;
        //}

        //创建所有帧对象
        VTK_AllFrame AllFrame = new VTK_AllFrame();

        AllFrame.point_MaxAndMin = listm;

        AllFrame.modelState = new Others.ModelState();
        int pointAttIndex = 0;
        int cellAttIndex = 0;
        for (int i = 0; i < listm.Count; i++)
        {
            if (listm[i].Dimension == 1)
            {
                AllFrame.modelState.isUsecolor = true;
                AllFrame.modelState.isPointAtt = listm[i].isPointAtt;
                if (listm[i].isPointAtt)
                {
                    AllFrame.modelState.attIndex = pointAttIndex;
                }
                else
                {
                    AllFrame.modelState.attIndex = cellAttIndex;
                }
                AllFrame.modelState.maxminIndex = i;
                break;
            }
            else
            {
                if (listm[i].isPointAtt)
                {
                    pointAttIndex++;
                }
                else
                {
                    cellAttIndex++;
                }
            }
        }

        AllFrame.listFrame = new List<VTK_Frame>();

        AllFrame.name = structure_loadFile.name;

        //创建 场景中的 数据模型载体
        GameObject newNodel = new GameObject();
        newNodel.name = structure_loadFile.name;
        newNodel.transform.parent = transform;
        //挂上 功能脚本
        modelGameObject modelgo = newNodel.AddComponent<modelGameObject>();
        modelgo.vtkAllFrame = AllFrame;
        AllFrame.loadFile = structure_loadFile;

                
        DirectoryInfo folder = new DirectoryInfo(structure_loadFile.FileDir);
        FileInfo[] fileinfoArray = folder.GetFiles(extensionName);
        Array.Sort(fileinfoArray, new FileNameSort());

        AllFrame.FileCount = fileinfoArray.Length;

        GlobalVariableBackground.Instance.FrameALLCount = fileinfoArray.Length;

        //加载指定缓存数量的模型数据
        for (int i = 0; i < GlobalVariableBackground.Instance.modelCacheCount; i++)
        {
            GameObject goModelFrame;
            //生成 线程数据包
            goModelFrame = GameObject.Instantiate(GlobalVariableBackground.Instance.modelPerfab);

            goModelFrame.transform.parent = newNodel.transform;

            modelFrameManager modelManager = goModelFrame.AddComponent<modelFrameManager>();
            goModelFrame.AddComponent<ClickGameObjectEvent>();
            modelManager.modelState = AllFrame.modelState;

            Others.ThreadLoadFilePack pack = new Others.ThreadLoadFilePack();

            //pack.strAboutArrMAxMin = strAboutAttMaxMin;

            //创建 单帧
            VTK_Frame frame = new VTK_Frame();
            frame.point_MaxAndMin = AllFrame.point_MaxAndMin;
            modelManager.vtkframe = frame;
            frame.go = goModelFrame;
            modelManager.isCollider = structure_loadFile.dataType == Enums.DataType.Default;
            pack.vtkAllFrame = AllFrame;
            //填充 线程数据包-》单帧
            pack.vtkFrame = frame;
            //填充 线程数据包-》单一文件
            pack.fileAddres = fileinfoArray[i];
            int fileIndex = i;

            AllFrame.listFrame.Add(frame);

            goModelFrame.name = AllFrame.name + "#" + fileIndex;
            frame.selfFrameIndex = fileIndex;

            VTKCManager m = new VTKCManager();
            frame.timeStartLession = DateTime.Now;
            m.DisposeData(pack);

            //Thread t = new Thread(m.DisposeData);
            //t.IsBackground = true;
            //t.Start(pack);

            //ThreadPool.QueueUserWorkItem(new WaitCallback(m.DisposeData), pack as object);
        }
        folder = null;
        fileinfoArray = null;

    }

    public int Percentage(string fileAddress, int fileCount)
    {
        DirectoryInfo folder = new DirectoryInfo(fileAddress);
        FileInfo[] fileinfoArray = folder.GetFiles("*.qf");
        return fileCount / fileinfoArray.Length;
    }

    //private void OnDestroy()
    //{
    //    GC.Collect();
    //}



}

public class FileNameSort : IComparer
{
    //调用DLL  
    [System.Runtime.InteropServices.DllImport("Shlwapi.dll", CharSet = CharSet.Unicode)]
    private static extern int StrCmpLogicalW(string param1, string param2);


    //前后文件名进行比较。  
    public int Compare(object name1, object name2)
    {
        if (null == name1 && null == name2)
        {
            return 0;
        }
        if (null == name1)
        {
            return -1;
        }
        if (null == name2)
        {
            return 1;
        }
        return StrCmpLogicalW(name1.ToString(), name2.ToString());
    }
}