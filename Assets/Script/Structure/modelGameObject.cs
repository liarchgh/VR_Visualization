using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using UnityEngine;
using UnityEngine.Rendering;

public class modelGameObject : MonoBehaviour
{

    public VTK_AllFrame vtkAllFrame;
    //public Structure_AllFrame AllFrame;

    private int vecModeCount;
    private Mesh vecModeMesh;
    private Material vecModeMaterial;
    private ComputeBuffer vecModeArgsBuffer;
    private ComputeBuffer vecModeColorBuffer;
    private ComputeBuffer vecModePositionBuffer;
    private ComputeBuffer vecModeRotationBuffer;
    private uint[] vecModeargs = new uint[5] { 0, 0, 0, 0, 0 };

    private int instanceCount;
    private Mesh instanceMesh;
    private Material instanceMaterial;

    private ShadowCastingMode castShadows = ShadowCastingMode.Off;
    private bool receiveShadows = false;

    private ComputeBuffer argsBuffer;
    private ComputeBuffer colorBuffer;
    private ComputeBuffer positionBuffer;
    private ComputeBuffer rotationBuffer;

    private uint[] args = new uint[5] { 0, 0, 0, 0, 0 };
    public Enums.ShowMode showMode = Enums.ShowMode.Cell;

    private int pointModeIndex = -1;
    private int lineModeIndex = -1;
    private int pointArrowModeIndex = -1;

    private List<Mesh> listLineMesh = new List<Mesh>();
    private Mesh ms;
    private Material smat;
    private List<Vector3> meshVec;
    private List<int> meshTri;
    private bool isLineAndCell = false;

    public event ControlManager.CustomEventHandler ChildToDrawColor;
    public event ControlManager.CustomEventHandler ChildToSymmetric;
    public bool isPointHaveColor = false;

    private void VectorMode()
    {
        if (vtkAllFrame.loadFile.dataType != Enums.DataType.Default)
        {
            vtkAllFrame.modelState.isArrowMode = false;
            return;
        }

        if (pointArrowModeIndex == GlobalVariableBackground.Instance.FrameAllIndex &&
            args[1] != 0 && vtkAllFrame.modelState.isUsecolor == isPointHaveColor)
        {
            vtkAllFrame.modelState.isArrowMode = true;
            return;
        }

        pointArrowModeIndex = GlobalVariableBackground.Instance.FrameAllIndex;

        vecModeMesh = GlobalVariableBackground.Instance.ArrowMesh;
        vecModeMaterial = GlobalVariableBackground.Instance.MaterialGPURows1;
        vecModeArgsBuffer = new ComputeBuffer(5, sizeof(uint), ComputeBufferType.IndirectArguments);
        vecModeMesh.bounds = new Bounds(Vector3.zero, Vector3.one * 100000f); //avoid culling
        List<Vector4> listVec = new List<Vector4>();
        List<Vector4> listColor = new List<Vector4>();
        List<Vector4> listRotation = new List<Vector4>();
        Mesh mesh;
        int index = GlobalVariableBackground.Instance.FrameAllIndex % GlobalVariableBackground.Instance.modelCacheCount;
        for (int i = 0; i < vtkAllFrame.listFrame[index].meshArray.Length; i++)
        {
            mesh = vtkAllFrame.listFrame[index].go.transform.GetChild(i).GetComponent<MeshFilter>().mesh;
            Vector3[] vecTemp = mesh.vertices;
            Color[] tempColor = mesh.colors;
            Vector3[] tempnormals = mesh.normals;
            for (int j = 0; j < tempColor.Length; j++)
            {
                listColor.Add(tempColor[j]);
            }
            tempColor = null;
            for (int j = 0; j < vecTemp.Length; j++)
            {
                Vector3 vec = transform.TransformPoint(vecTemp[j]);
                listVec.Add(new Vector4(vec.x,vec.y,vec.z,GlobalVariableBackground.Instance.ArrowWigth));
            }
            vecTemp = null;
            //此处需更改 改为取对应点或网格的三维属性值与其颜色部分
            for (int j = 0; j < tempnormals.Length; j++)
            {
                //if (tempnormals[j].x * tempnormals[j].x + tempnormals[j].y * tempnormals[j].y + tempnormals[j].z * tempnormals[j].z < 0.1)
                //{
                //    continue;
                //}
                Quaternion q = Quaternion.LookRotation(tempnormals[j]);
                //Vector4 v4 = new Vector4(q.w, q.y, q.z, q.w);
                Vector4 v4 = new Vector4(q.eulerAngles.x , q.eulerAngles.y , q.eulerAngles.z , 1);
                //Vector4 v4 = new Vector4(0, 0, 3.1415f / 2, 1);
                listRotation.Add(v4);
            }
            tempnormals = null;
        }

        Vector4[] buffer = listVec.ToArray();
        listVec.Clear();
        vecModeCount = buffer.Length;

        if (vecModePositionBuffer != null) vecModePositionBuffer.Release();
        vecModePositionBuffer = new ComputeBuffer(vecModeCount, 16);
        if (vecModeColorBuffer != null) vecModeColorBuffer.Release();
        vecModeColorBuffer = new ComputeBuffer(vecModeCount, 16);
        if (vecModeRotationBuffer != null) vecModeRotationBuffer.Release();
        vecModeRotationBuffer = new ComputeBuffer(vecModeCount, 16);

        vecModePositionBuffer.SetData(buffer);
        vecModeMaterial.SetBuffer("positionBuffer",vecModePositionBuffer);

        buffer = null;
        buffer = listColor.ToArray();
        if (buffer.Length != 0)
        {
            isPointHaveColor = true;
        }
        else
        {
            isPointHaveColor = false;
        }
        vecModeColorBuffer.SetData(buffer);
        vecModeMaterial.SetBuffer("colorBuffer", vecModeColorBuffer);

        buffer = null;
        buffer = listRotation.ToArray();

        vecModeRotationBuffer.SetData(buffer);
        vecModeMaterial.SetBuffer("rotationBuffer", vecModeRotationBuffer);

        uint numindices = (vecModeMesh != null) ? (uint)vecModeMesh.GetIndexCount(0) : 0;
        vecModeargs[0] = numindices;
        vecModeargs[1] = (uint)vecModeCount;
        vecModeArgsBuffer.SetData(vecModeargs);
        vtkAllFrame.modelState.isArrowMode = true;
    }

    void Start()
    {
        GlobalVariableBackground.Instance.conManager.FrameChange += Instance_FrameChange;
        GlobalVariableBackground.Instance.conManager.FrameInitValue += ConManager_FrameInitValue;
        GlobalVariableBackground.Instance.conManager.ToPointMode += ConManager_ToPointMode;
        GlobalVariableBackground.Instance.conManager.ToCellMode += ConManager_ToCellMode;
        GlobalVariableBackground.Instance.conManager.ToLineMode += ConManager_ToLineMode;
        GlobalVariableBackground.Instance.conManager.ToLineAndCellMode += ConManager_ToLineAndCellMode;
        GlobalVariableBackground.Instance.conManager.ToCutModel += ConManager_CutModel;
        GlobalVariableBackground.Instance.conManager.ToSymmetric += ConManager_ToSymmetric;
        GlobalVariableBackground.Instance.conManager.ToSmooth += ConManager_ToSmooth;
        GlobalVariableBackground.Instance.conManager.ToArrowMode += ConManager_ToArrowMode;


        ms = new Mesh();
        meshVec = new List<Vector3>();
        meshTri = new List<int>();
        smat = GlobalVariableBackground.Instance.Materialboat;
    }

    private void ConManager_ToArrowMode()
    {
        vtkAllFrame.modelState.isArrowMode = !vtkAllFrame.modelState.isArrowMode;
        if (vtkAllFrame.modelState.isArrowMode)
        {
            VectorMode();
        }
    }

    private void ConManager_ToSmooth()
    {
        vtkAllFrame.modelState.isSmooth = !vtkAllFrame.modelState.isSmooth;
        if (vtkAllFrame.loadFile.dataType == Enums.DataType.Water)
        {
            modelInitReloadAllFrameByIndex(GlobalVariableBackground.Instance.FrameAllIndex);
        }
    }

    private void ConManager_ToSymmetric(Enums.SymmetricMode args)
    {
        vtkAllFrame.modelState.symmetricmode = args;
        if (ChildToSymmetric != null)
        {
            ChildToSymmetric();
        }
    }

    private void ConManager_CutModel()
    {
        float[] plane16 = new float[16];
        GameObject plane = GlobalVariableBackground.Instance.CutKnife;
        Matrix4x4 mat = plane.transform.localToWorldMatrix;

        plane16[0] = mat.m00;
        plane16[1] = mat.m01;
        plane16[2] = mat.m02;
        plane16[3] = mat.m03;

        plane16[4] = mat.m10;
        plane16[5] = mat.m11;
        plane16[6] = mat.m12;
        plane16[7] = mat.m13;

        plane16[8] = mat.m20;
        plane16[9] = mat.m21;
        plane16[10] = mat.m22;
        plane16[11] = mat.m23;

        plane16[12] = mat.m30;
        plane16[13] = mat.m31;
        plane16[14] = mat.m32;
        plane16[15] = mat.m33;

        float[] model16 = new float[16];

        mat = transform.localToWorldMatrix;

        model16[0] = mat.m00;
        model16[1] = mat.m01;
        model16[2] = mat.m02;
        model16[3] = mat.m03;

        model16[4] = mat.m10;
        model16[5] = mat.m11;
        model16[6] = mat.m12;
        model16[7] = mat.m13;

        model16[8] = mat.m20;
        model16[9] = mat.m21;
        model16[10] = mat.m22;
        model16[11] = mat.m23;

        model16[12] = mat.m30;
        model16[13] = mat.m31;
        model16[14] = mat.m32;
        model16[15] = mat.m33;

        string NewName = name + "&CutModel";
        GameObject newNodel;
        modelFrameManager modelChild;
        Transform t = transform.Find(NewName);

        if (t == null)
        {
            newNodel = GameObject.Instantiate(GlobalVariableBackground.Instance.modelPerfab);
            newNodel.transform.parent = transform;
            newNodel.name = NewName;

            newNodel.transform.localPosition = Vector3.zero;
            newNodel.transform.localRotation = new Quaternion(0,0,0,1);
            newNodel.transform.localScale = Vector3.one;

            modelChild = newNodel.AddComponent<modelFrameManager>();
            modelChild.isCutModelGO = true;
            modelChild.modelState = vtkAllFrame.modelState;
        }
        else
        {
            newNodel = t.gameObject;
            modelChild = newNodel.GetComponent<modelFrameManager>();
        }
        VTK_Frame frame = new VTK_Frame();
        modelChild.vtkframe = frame;
        frame.point_MaxAndMin = vtkAllFrame.point_MaxAndMin;
        frame.go = newNodel;
        modelChild.isCollider = vtkAllFrame.loadFile.dataType == Enums.DataType.Default;

        Others.ThreadCutModelPack pack = new Others.ThreadCutModelPack();

        pack.model16 = model16;
        pack.plane16 = plane16;

        //填充 线程数据包-》单帧
        pack.vtkAllFrame = vtkAllFrame;
        pack.vtkFrame = frame;
        DirectoryInfo folder = new DirectoryInfo(vtkAllFrame.loadFile.FileDir);


        FileInfo[] fileinfoArray;
        fileinfoArray = folder.GetFiles("*.qf");

        Array.Sort(fileinfoArray, new FileNameSort());

        //填充 线程数据包-》单一文件
        pack.fileAddres = fileinfoArray[GlobalVariableBackground.Instance.FrameAllIndex];

        //
        frame.selfFrameIndex = GlobalVariableBackground.Instance.FrameAllIndex;
        frame.timeStartLession = System.DateTime.Now;
        VTKCManager m = new VTKCManager();
        //m.CutModelDispose(pack);
        ThreadPool.QueueUserWorkItem(new WaitCallback(m.CutModelDispose), pack as object);
    }

    private void ConManager_ToPointMode()
    {
        int index = GlobalVariableBackground.Instance.FrameAllIndex % GlobalVariableBackground.Instance.modelCacheCount;

        if (vtkAllFrame.loadFile.dataType != Enums.DataType.Default)
        {
            return;
        }

        if (pointModeIndex == GlobalVariableBackground.Instance.FrameAllIndex &&
            args[1] != 0 && vtkAllFrame.modelState.isUsecolor == isPointHaveColor)
        {
            showMode = Enums.ShowMode.Point;
            return;
        }

        pointModeIndex = GlobalVariableBackground.Instance.FrameAllIndex;

        instanceMesh = GlobalVariableBackground.Instance.miniCubeMesh;
        instanceMaterial = GlobalVariableBackground.Instance.MaterialGPURows;
        argsBuffer = new ComputeBuffer(5, sizeof(uint), ComputeBufferType.IndirectArguments);
        instanceMesh.bounds = new Bounds(Vector3.zero, Vector3.one * 100000f); //avoid culling

        if (positionBuffer != null) positionBuffer.Release();

        List<Vector4> listVec = new List<Vector4>();
        //List<Vector4> listVTKVec = new List<Vector4>();
        List<Vector4> listColor = new List<Vector4>();
        Mesh mesh;
        //System.DateTime dtStart = DateTime.Now;
        //System.DateTime dtEnd01 = DateTime.Now;
        //System.DateTime dtEnd02 = DateTime.Now;

        //dtStart = DateTime.Now;

        for (int i = 0; i < vtkAllFrame.listFrame[index].meshArray.Length; i++)
        {
            mesh = vtkAllFrame.listFrame[index].go.transform.GetChild(i).GetComponent<MeshFilter>().mesh;

            Vector3[] vecTemp = mesh.vertices;
            Color[] tempColor = mesh.colors;

            for (int j = 0; j < tempColor.Length; j++)
            {
                listColor.Add(tempColor[j]);
            }
            tempColor = null;
            for (int j = 0; j < vecTemp.Length; j++)
            {
                Vector3 vec = transform.TransformPoint(vecTemp[j]);
                listVec.Add(new Vector4(vec.x, vec.y, vec.z, 1));
            }
            vecTemp = null;

            //for (int j = 0; j < mesh.vertices.Length; j++)
            //{
            //    vec = mesh.vertices[j];
            //    listVec.Add(new Vector4(vec.x,vec.y,vec.z,1));

            //    //listVec.Add(new Vector4(mesh.vertices[j].x, mesh.vertices[j].y, mesh.vertices[j].z, 1));
            //    //listColor.Add(mesh.colors[j]);
            //    if (j == 1)
            //    {
            //        break;
            //    }
            //}
            //break;
        }
        //dtEnd01 = DateTime.Now;
        //GlobalVariableBackground.Instance.conManager.WriteLog((dtEnd01 - dtStart).TotalMilliseconds + "ms");
        //dtEnd01 = DateTime.Now;
        //for (int i = 0; i < vtkAllFrame.listFrame[index].meshArray.Length; i++)
        //{
        //    vtkMesh = vtkAllFrame.listFrame[index].meshArray[i];
        //    for (int j = 0; j < vtkMesh.vec3Array.Length; j++)
        //    {
        //        vec = vtkMesh.vec3Array[j];
        //        listVTKVec.Add(vec);
        //        //listVTKVec.Add(new Vector4(vtkMesh.vec3Array[j].x, vtkMesh.vec3Array[j].y, vtkMesh.vec3Array[j].z, 1));
        //        //listColor.Add(mesh.colors[j]);
        //    }
        //}
        //dtEnd02 = DateTime.Now;
        //GlobalVariableBackground.Instance.conManager.WriteLog((dtEnd01 - dtStart).TotalMilliseconds + " , " + (dtEnd02 - dtEnd01).TotalMilliseconds);

        Vector4[] buffer = listVec.ToArray();
        listVec.Clear();
        instanceCount = buffer.Length;

        if (positionBuffer != null) positionBuffer.Release();
        positionBuffer = new ComputeBuffer(instanceCount, 16);
        if (colorBuffer != null) colorBuffer.Release();
        colorBuffer = new ComputeBuffer(instanceCount, 16);
        //if (rotationBuffer != null) rotationBuffer.Release();
        //rotationBuffer = new ComputeBuffer(instanceCount, 16);

        positionBuffer.SetData(buffer);
        instanceMaterial.SetBuffer("positionBuffer", positionBuffer);

        //buffer = new Vector4[instanceCount];
        //for (int i = 0; i < instanceCount; i++)
        //{
        //    buffer[i] = UnityEngine.Random.ColorHSV();
        //}
        //for (int i = 0; i < vtkAllFrame.listFrame[index].meshArray.Length; i++)
        //{
        //    mesh = vtkAllFrame.listFrame[index].meshArray[i];
        //    for (int j = 0; j < mesh.vec3Array.Length; j++)
        //    {
        //        buffer[i] = mesh.
        //        list.Add(new Vector4(mesh.vec3Array[j].x, mesh.vec3Array[j].y, mesh.vec3Array[j].z, 1));
        //    }

        //}

        buffer = listColor.ToArray();
        if (buffer.Length != 0)
        {
            isPointHaveColor = true;
        }
        else
        {
            isPointHaveColor = false;
        }
        colorBuffer.SetData(buffer);
        instanceMaterial.SetBuffer("colorBuffer", colorBuffer);

        uint numIndices = (instanceMesh != null) ? (uint)instanceMesh.GetIndexCount(0) : 0;
        args[0] = numIndices;
        args[1] = (uint)instanceCount;
        argsBuffer.SetData(args);

        showMode = Enums.ShowMode.Point;


    }

    private void ConManager_ToLineAndCellMode()
    {
        isLineAndCell = true;
        DrawLine();
        isLineAndCell = false;
        
    }

    private void ConManager_ToLineMode()
    {
        DrawLine();
    }

    private void ConManager_ToCellMode()
    {
        showMode = Enums.ShowMode.Cell;

    }

    private void Update()
    {
        if (vtkAllFrame.modelState.isArrowMode&&vtkAllFrame.loadFile.dataType==Enums.DataType.Default)
        {
            Graphics.DrawMeshInstancedIndirect(vecModeMesh, 0, vecModeMaterial, vecModeMesh.bounds, vecModeArgsBuffer, 0, null, castShadows, receiveShadows);
        }

        if (showMode == Enums.ShowMode.Point)
        {
            if (pointModeIndex == GlobalVariableBackground.Instance.FrameAllIndex)
            {
                Graphics.DrawMeshInstancedIndirect(instanceMesh, 0, instanceMaterial, instanceMesh.bounds, argsBuffer, 0, null, castShadows, receiveShadows);
            }
            else
            {
                ConManager_ToPointMode();
            }
        }
        else if (showMode == Enums.ShowMode.Line || showMode == Enums.ShowMode.CellAndLine)
        {
            if (lineModeIndex == GlobalVariableBackground.Instance.FrameAllIndex)
            {
                foreach (var item in listLineMesh)
                {
                    Graphics.DrawMesh(item, transform.localToWorldMatrix, smat, 0);
                }
            }
            else if (showMode == Enums.ShowMode.Line)
            {
                ConManager_ToLineMode();
            }
            else if (showMode == Enums.ShowMode.CellAndLine)
            {
                ConManager_ToLineAndCellMode();
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ToDrawColor();
        }
    }

    private void DrawLine()
    {
        if (vtkAllFrame.loadFile.dataType == Enums.DataType.Water)
        {
            return;
        }

        int index = GlobalVariableBackground.Instance.FrameAllIndex % GlobalVariableBackground.Instance.modelCacheCount;

        if (lineModeIndex==GlobalVariableBackground.Instance.FrameAllIndex && listLineMesh.Count > 0 && isLineAndCell == false)
        {
            showMode = Enums.ShowMode.Line;
            return;
        }
        else if (lineModeIndex==GlobalVariableBackground.Instance.FrameAllIndex && listLineMesh.Count > 0 && isLineAndCell == true)
        {
            showMode = Enums.ShowMode.CellAndLine;
            return;
        }
        else
        {
            listLineMesh.Clear();
            ms = new Mesh();
        }

        lineModeIndex = GlobalVariableBackground.Instance.FrameAllIndex;

        Mesh mesh;
        for (int i = 0; i < vtkAllFrame.listFrame[index].meshArray.Length; i++)
        {
            mesh = vtkAllFrame.listFrame[index].go.transform.GetChild(i).GetComponent<MeshFilter>().mesh;
            int[] triArray = mesh.triangles;
            Vector3[] vecArray = mesh.vertices;
            for (int j = 0; j < triArray.Length;)
            {
                if (meshVec.Count > 65001 || meshTri.Count > 65001)
                {
                    ms.SetVertices(meshVec);
                    ms.triangles = meshTri.ToArray();
                    listLineMesh.Add(ms);
                    meshVec = null;
                    meshTri = null;
                    ms = new Mesh();
                    meshVec = new List<Vector3>();
                    meshTri = new List<int>();
                }

                //if ((j + 1) % 3 == 0)
                //{
                //    //画第三点与第一点之间的连线
                //    AddLineArray(meshVec, meshTri, MakeQuad(vecArray[triArray[j]], vecArray[triArray[j - 2]], 0.001f));
                //}
                //else
                //{
                //    //画 第一点和第二点之间的连线或第二点与第三点之间的连线
                //    AddLineArray(meshVec, meshTri, MakeQuad(vecArray[triArray[j]], vecArray[triArray[j + 1]], 0.001f));
                //}

                Vector3 v1 = transform.TransformPoint(vecArray[triArray[j++]]);
                Vector3 v2 = transform.TransformPoint(vecArray[triArray[j++]]);
                Vector3 v3 = transform.TransformPoint(vecArray[triArray[j++]]);
                //if (v1 != v2 && v2 != v3 && v3 != v1)
                //{
                //    GlobalVariableBackground.Instance.conManager.WriteLog(v1.x+","+v1.y+","+v1.z+" "+ v2.x + "," + v2.y + "," + v2.z + " "+v3.x + "," + v3.y + "," + v3.z + " ");
                //}

                AddLineArray3(meshVec, meshTri, MakeQuad(v1, v2, v3,GlobalVariableBackground.Instance.Linewight));
            }

        }
        ms.SetVertices(meshVec);
        ms.triangles = meshTri.ToArray();
        listLineMesh.Add(ms);

        meshVec = null;
        meshVec = new List<Vector3>();
        meshTri = null;
        meshTri = new List<int>();
        foreach (var item in listLineMesh)
        {
            //item.RecalculateBounds();
            item.bounds = new Bounds(Vector3.zero, Vector3.one * 100000f); //avoid culling
            item.RecalculateNormals();
        }

        if (isLineAndCell)
        {
            showMode = Enums.ShowMode.CellAndLine;
        }
        else
        {
            showMode = Enums.ShowMode.Line;
        }
    }

    Vector3[] MakeQuad(Vector3 s, Vector3 e, float w)
    {
        w = w / 2;
        Vector3[] q = new Vector3[4];
        Vector3 n = Vector3.Cross(s, e);
        Vector3 l = Vector3.Cross(n, e - s);
        l.Normalize();

        q[0] = transform.InverseTransformPoint(s + l * w);
        q[1] = transform.InverseTransformPoint(s + l * -w);
        q[2] = transform.InverseTransformPoint(e + l * w);
        q[3] = transform.InverseTransformPoint(e + l * -w);
        return q;
    }

    Vector3[] MakeQuad(Vector3 p1, Vector3 p2, Vector3 p3, float w)
    {
        p1 = p1 * 100;
        p2 = p2 * 100;
        p3 = p3 * 100;

        w = w / 2;
        Vector3[] q = new Vector3[12];
        Vector3 s = p2 - p1;
        Vector3 e = p3 - p1;
        Vector3 f = p3 - p2;

        Vector3 n = Vector3.Cross(s, e);
        Vector3 n1 = Vector3.Cross(s, n);
        Vector3 n2 = Vector3.Cross(n, e);
        Vector3 n3 = Vector3.Cross(f, n);

        n1.Normalize();
        n2.Normalize();
        n3.Normalize();

        q[0] = transform.InverseTransformPoint(p2 + n1 * w);
        q[1] = transform.InverseTransformPoint(p2 + n1 * -w);
        q[2] = transform.InverseTransformPoint(p1 + n1 * w);
        q[3] = transform.InverseTransformPoint(p1 + n1 * -w);

        q[4] = transform.InverseTransformPoint(p1 + n2 * w);
        q[5] = transform.InverseTransformPoint(p1 + n2 * -w);
        q[6] = transform.InverseTransformPoint(p3 + n2 * w);
        q[7] = transform.InverseTransformPoint(p3 + n2 * -w);

        q[8] = transform.InverseTransformPoint(p3 + n3 * w);
        q[9] = transform.InverseTransformPoint(p3 + n3 * -w);
        q[10] = transform.InverseTransformPoint(p2 + n3 * w);
        q[11] = transform.InverseTransformPoint(p2 + n3 * -w);

        for (int i = 0; i < q.Length; i++)
        {
            q[i] = q[i] / 100f;
        }
        return q;
    }

    public void AddLineArray(List<Vector3> listVec, List<int> listInt, Vector3[] quad)
    {
        int index = listVec.Count;
        listVec.Add(quad[0]);
        listVec.Add(quad[1]);
        listVec.Add(quad[2]);
        listVec.Add(quad[3]);

        listInt.Add(index);
        listInt.Add(index + 1);
        listInt.Add(index + 2);
        listInt.Add(index + 1);
        listInt.Add(index + 3);
        listInt.Add(index + 2);
    }

    public void AddLineArray3(List<Vector3> listVec, List<int> listInt, Vector3[] quad)
    {
        int index = listVec.Count;
        listVec.Add(quad[0]);
        listVec.Add(quad[1]);
        listVec.Add(quad[2]);
        listVec.Add(quad[3]);

        listVec.Add(quad[4]);
        listVec.Add(quad[5]);
        listVec.Add(quad[6]);
        listVec.Add(quad[7]);

        listVec.Add(quad[8]);
        listVec.Add(quad[9]);
        listVec.Add(quad[10]);
        listVec.Add(quad[11]);

        listInt.Add(index + 0);
        listInt.Add(index + 1);
        listInt.Add(index + 2);
        listInt.Add(index + 1);
        listInt.Add(index + 3);
        listInt.Add(index + 2);

        listInt.Add(index + 4);
        listInt.Add(index + 5);
        listInt.Add(index + 6);
        listInt.Add(index + 5);
        listInt.Add(index + 7);
        listInt.Add(index + 6);

        listInt.Add(index + 8);
        listInt.Add(index + 9);
        listInt.Add(index + 10);
        listInt.Add(index + 9);
        listInt.Add(index + 11);
        listInt.Add(index + 10);
 
    }

 
    private void ConManager_FrameInitValue()
    {
        modelInitReloadALLFrame();
    }

    private void Instance_FrameChange()
    {
        ModelReload(GlobalVariableBackground.Instance.FrameAllIndex);
    }

    private int lastIndex = 0;

    public void ModelReload(int index)
    {
        //旧的缓存 帧数 最大值
        int oldCacheMaxCount = lastIndex + GlobalVariableBackground.Instance.modelCacheCount / 2;

        //判断 旧缓存 帧数最大值不超过总帧数
        oldCacheMaxCount = Math.Min(oldCacheMaxCount, vtkAllFrame.FileCount - 1);

        //判断 旧缓存 帧数最大值不小于总缓存数
        oldCacheMaxCount = Math.Max(oldCacheMaxCount, GlobalVariableBackground.Instance.modelCacheCount - 1);

        //旧的缓存 帧数 最小值
        int oldCacheMinCount = lastIndex - GlobalVariableBackground.Instance.modelCacheCount / 2;

        //旧的缓存 帧数 最小值 最大不超过 总帧数-缓存数
        oldCacheMinCount = Math.Min(oldCacheMinCount, vtkAllFrame.FileCount - GlobalVariableBackground.Instance.modelCacheCount);

        //旧的缓存 帧数 最小值 最小不超过 0
        oldCacheMinCount = Math.Max(oldCacheMinCount, 0);

        //新的缓存 帧数 最大值
        int newCacheMaxCount = index + GlobalVariableBackground.Instance.modelCacheCount / 2;

        //判断 新缓存 帧数最大值不超过总帧数
        newCacheMaxCount = Math.Min(newCacheMaxCount, vtkAllFrame.FileCount - 1);

        //判断 新缓存 帧数最大值不小于总缓存数
        newCacheMaxCount = Math.Max(newCacheMaxCount, GlobalVariableBackground.Instance.modelCacheCount - 1);

        //新的缓存 帧数 最小值
        int newCacheMinCount = index - GlobalVariableBackground.Instance.modelCacheCount / 2;

        //新的缓存 帧数 最小值 最大不超过 总帧数-缓存数
        newCacheMinCount = Math.Min(newCacheMinCount, vtkAllFrame.FileCount - GlobalVariableBackground.Instance.modelCacheCount);

        //新的缓存 帧数 最小值 最小不超过 0
        newCacheMinCount = Math.Max(newCacheMinCount, 0);

        if (index >= vtkAllFrame.FileCount)
        {
            //最末帧
        }
        else if (index == 0)
        {
            //第一帧
        }

        if (index > lastIndex)
        {
            //新帧 大于 旧帧

            modelReloadNewFile(oldCacheMinCount, newCacheMaxCount);

        }
        else if (lastIndex > index)
        {
            //旧帧 大于 新帧
            modelReloadNewFile(oldCacheMaxCount, newCacheMinCount);
        }
        lastIndex = index;
    }

    private void modelReloadNewFile(int oldCount, int NewCount)
    {
        //指定 单帧
        int newIndex = NewCount % GlobalVariableBackground.Instance.modelCacheCount;
        if (vtkAllFrame.listFrame[newIndex].selfFrameIndex == NewCount)
        {
            return;
        }


        DirectoryInfo folder = new DirectoryInfo(vtkAllFrame.loadFile.FileDir);


        FileInfo[] fileinfoArray;
        fileinfoArray = folder.GetFiles("*.qf");


        Array.Sort(fileinfoArray, new FileNameSort());

        vtkAllFrame.listFrame[newIndex].selfFrameIndex = NewCount;

        Others.ThreadLoadFilePack pack = new Others.ThreadLoadFilePack();
        //填充 线程数据包-》单帧
        pack.vtkAllFrame = vtkAllFrame;
        pack.vtkFrame = vtkAllFrame.listFrame[newIndex];
        pack.vtkFrame.isAllFinish = false;

        //填充 线程数据包-》单一文件
        pack.fileAddres = fileinfoArray[NewCount];
        //
        pack.vtkFrame.timeStartLession = System.DateTime.Now;
        VTKCManager m = new VTKCManager();

        //m.DisposeData(pack);

        //ThreadPool.QueueUserWorkItem(new WaitCallback(m.DisposeData), pack as object);

        if (vtkAllFrame.loadFile.dataType == Enums.DataType.Water && vtkAllFrame.modelState.isSmooth)
        {
            Others.ThreadSmoothModePack packsm = new Others.ThreadSmoothModePack();
            packsm.fileAddres = pack.fileAddres;
            packsm.smoothCount = 20;
            packsm.vtkAllFrame = pack.vtkAllFrame;
            packsm.vtkFrame = pack.vtkFrame;
            Thread t = new Thread(m.SmoothDispose);
            t.IsBackground = true;
            t.Start(packsm);
        }
        else
        {
            Thread t = new Thread(m.DisposeData);
            t.IsBackground = true;
            t.Start(pack);
        }


    }

    /// <summary>
    /// 初始化帧到第一帧
    /// </summary>
    private void modelInitReloadALLFrame()
    {
        GlobalVariableBackground.Instance.conManager.WriteLog("帧初始化开始");
        if (vtkAllFrame.listFrame.Count == 0)
        {
            return;
        }
        lastIndex = 0;
        GlobalVariableBackground.Instance.FrameAllIndex = 0;
        //vtkAllFrame.ClearListFrame();
        //vtkAllFrame.listFrame =new List<VTK_Frame>();
        for (int i = 0; i < GlobalVariableBackground.Instance.modelCacheCount; i++)
        {
            modelFrameManager modelFrameManager = transform.GetChild(i).GetComponent<modelFrameManager>();
            if (modelFrameManager == null)
            {
                transform.GetChild(i).gameObject.AddComponent<modelFrameManager>();
                modelFrameManager.vtkframe = new VTK_Frame();
                modelFrameManager.isCollider = vtkAllFrame.loadFile.dataType == Enums.DataType.Default;
            }
            else
            {
                if (modelFrameManager.isCutModelGO)
                {
                    //transform.GetChild(i) 里面含有切割生成的对象
                    continue;
                }
            }
            VTK_Frame vtkframe = modelFrameManager.vtkframe;
            vtkframe.go = transform.GetChild(i).gameObject;

            //vtkAllFrame.listFrame.Add(vtkframe);
            Others.ThreadLoadFilePack pack = new Others.ThreadLoadFilePack();

            //填充 线程数据包-》单帧
            pack.vtkFrame = vtkframe;

            pack.vtkAllFrame = vtkAllFrame;

            DirectoryInfo folder = new DirectoryInfo(vtkAllFrame.loadFile.FileDir);
            FileInfo[] fileinfoArray;
            fileinfoArray = folder.GetFiles("*.qf");

            Array.Sort(fileinfoArray, new FileNameSort());

            //填充 线程数据包-》单一文件
            pack.fileAddres = fileinfoArray[i];
            //
            vtkframe.selfFrameIndex = i;

            VTKCManager m = new VTKCManager();
            vtkframe.timeStartLession = DateTime.Now;
            ThreadPool.QueueUserWorkItem(new WaitCallback(m.DisposeData), pack as object);

        }
        //GlobalVariableBackground.Instance.conManager.WriteLog("帧初始化任务分发完毕");
    }

    private void modelInitReloadAllFrameByIndex(int indexFrame)
    {
        int min, max = 0;
        int half = GlobalVariableBackground.Instance.modelCacheCount / 2;
        if (indexFrame - half < 0)
        {
            min = 0;
            max = GlobalVariableBackground.Instance.modelCacheCount-1;
        }
        else if (indexFrame + half > vtkAllFrame.FileCount)
        {
            min = vtkAllFrame.FileCount-1 - GlobalVariableBackground.Instance.modelCacheCount;
            max = vtkAllFrame.FileCount-1;
        }
        else
        {
            min = indexFrame - half;
            max = indexFrame + half;
        }
        for (int i = min; i <= max; i++)
        {
            int childIndex = i % GlobalVariableBackground.Instance.modelCacheCount;
            modelFrameManager modelFrameManager = transform.GetChild(childIndex).GetComponent<modelFrameManager>();
            if (modelFrameManager == null)
            {
                transform.GetChild(childIndex).gameObject.AddComponent<modelFrameManager>();
                modelFrameManager.vtkframe = new VTK_Frame();
                modelFrameManager.isCollider = vtkAllFrame.loadFile.dataType == Enums.DataType.Default;
            }
            else
            {
                if (modelFrameManager.isCutModelGO)
                {
                    //transform.GetChild(i) 里面含有切割生成的对象
                    continue;
                }
            }
            VTK_Frame vtkframe = modelFrameManager.vtkframe;
            vtkframe.go = transform.GetChild(childIndex).gameObject;
            Others.ThreadLoadFilePack pack = new Others.ThreadLoadFilePack();
            pack.vtkFrame = vtkframe;
            pack.vtkAllFrame = vtkAllFrame;
            DirectoryInfo folder = new DirectoryInfo(vtkAllFrame.loadFile.FileDir);
            FileInfo[] fileinfoArray;
            fileinfoArray = folder.GetFiles("*.qf");

            Array.Sort(fileinfoArray, new FileNameSort());
            
            //填充 线程数据包-》单一文件
            pack.fileAddres = fileinfoArray[i];
            vtkframe.selfFrameIndex = i;
            VTKCManager m = new VTKCManager();
            vtkframe.timeStartLession = DateTime.Now;

            if (vtkAllFrame.modelState.isSmooth && vtkAllFrame.loadFile.dataType == Enums.DataType.Water)
            {
                Others.ThreadSmoothModePack packsm = new Others.ThreadSmoothModePack();
                packsm.fileAddres = pack.fileAddres;
                packsm.smoothCount = 20;
                packsm.vtkAllFrame = pack.vtkAllFrame;
                packsm.vtkFrame = pack.vtkFrame;
                m.SmoothDispose(packsm);
                //ThreadPool.QueueUserWorkItem(new WaitCallback(m.SmoothDispose), packsm as object);
                //Thread t = new Thread(m.SmoothDispose);
                //t.IsBackground = true;
                //t.Start(packsm);
            }
            else
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(m.DisposeData), pack as object);
            }

        }

    }


    private void ToDrawColor()
    {

        vtkAllFrame.modelState.attIndex = 2;
        vtkAllFrame.modelState.isUsecolor = true;
        if (ChildToDrawColor != null)
        {
            ChildToDrawColor();
        }
    }

    private void OnDestroy()
    {
        //GlobalVariableBackground.Instance.conManager.FrameChange -= Instance_FrameChange;
        //GlobalVariableBackground.Instance.conManager.FrameInitValue -= ConManager_FrameInitValue;
        if (positionBuffer != null) positionBuffer.Release();
        positionBuffer = null;
        if (colorBuffer != null) colorBuffer.Release();
        colorBuffer = null;
        if (rotationBuffer != null) rotationBuffer.Release();
        rotationBuffer = null;

    }
}
