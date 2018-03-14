using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class modelFrameManager : MonoBehaviour
{

    // public Structure_Frame frame;
    public VTK_Frame vtkframe;
    //根据文件属性类型取决是否有碰撞
    public bool isCollider = false;
    private bool isLoadModelFininsh = false;
    private bool isLoadColorFininsh = false;
    public bool CutModeHide = false;
    public bool isCutModelGO = false;
    public bool goHaveCutMode = false;
    public Others.ModelState modelState;

    public float min;
    public float max;
    Others.PackUpObject pick;

    // Use this for initialization
    void Start()
    {
        GlobalVariableBackground.Instance.conManager.FrameEnable += Instance_FrameEnable;
        modelGameObject modelGameobject;
        if (transform.parent != null)
        {
            modelGameobject = transform.parent.GetComponent<modelGameObject>();
            if (modelGameobject != null) {
                modelGameobject.ChildToDrawColor += ModelFrameManager_ChildToDrawColor;
                modelGameobject.ChildToSymmetric += ModelGameobject_ChildToSymmetric;
              }
        }
        pick = GlobalVariableBackground.Instance.conManager.pickClick;
    }

    private void ModelGameobject_ChildToSymmetric()
    {
        if (modelState.symmetricmode != Enums.SymmetricMode.NONE)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).GetComponent<ChildMeshManager>().stateUpdate();
            }
        }
    }

    private void ModelFrameManager_ChildToDrawColor()
    {
        StartCoroutine("ToDrawColor");
    }

    private void Instance_FrameEnable()
    {
        ToEnable();
    }

    private void BoxColliderChange()
    {
        MeshFilter mf = GetComponent<MeshFilter>();
        Mesh mesh = mf.mesh;

        Vector3[] vecArray = new Vector3[24];

        vecArray[0]  = transform.TransformPoint(new Vector3(vtkframe.maxX, vtkframe.minY, vtkframe.maxZ));
        vecArray[1]  = transform.TransformPoint(new Vector3(vtkframe.minX, vtkframe.minY, vtkframe.maxZ));
        vecArray[2]  = transform.TransformPoint(new Vector3(vtkframe.maxX, vtkframe.maxY, vtkframe.maxZ));
        vecArray[3]  = transform.TransformPoint(new Vector3(vtkframe.minX, vtkframe.maxY, vtkframe.maxZ));
        vecArray[4]  = transform.TransformPoint(new Vector3(vtkframe.maxX, vtkframe.maxY, vtkframe.minZ));
        vecArray[5]  = transform.TransformPoint(new Vector3(vtkframe.minX, vtkframe.maxY, vtkframe.minZ));
        vecArray[6]  = transform.TransformPoint(new Vector3(vtkframe.maxX, vtkframe.minY, vtkframe.minZ));
        vecArray[7]  = transform.TransformPoint(new Vector3(vtkframe.minX, vtkframe.minY, vtkframe.minZ));
        vecArray[8]  = transform.TransformPoint(new Vector3(vtkframe.maxX, vtkframe.maxY, vtkframe.maxZ));
        vecArray[9]  = transform.TransformPoint(new Vector3(vtkframe.minX, vtkframe.maxY, vtkframe.maxZ));
        vecArray[10] = transform.TransformPoint(new Vector3(vtkframe.maxX, vtkframe.maxY, vtkframe.minZ));
        vecArray[11] = transform.TransformPoint(new Vector3(vtkframe.minX, vtkframe.maxY, vtkframe.minZ));
        vecArray[12] = transform.TransformPoint(new Vector3(vtkframe.maxX, vtkframe.minY, vtkframe.minZ));
        vecArray[13] = transform.TransformPoint(new Vector3(vtkframe.maxX, vtkframe.minY, vtkframe.maxZ));
        vecArray[14] = transform.TransformPoint(new Vector3(vtkframe.minX, vtkframe.minY, vtkframe.maxZ));
        vecArray[15] = transform.TransformPoint(new Vector3(vtkframe.minX, vtkframe.minY, vtkframe.minZ));
        vecArray[16] = transform.TransformPoint(new Vector3(vtkframe.minX, vtkframe.minY, vtkframe.maxZ));
        vecArray[17] = transform.TransformPoint(new Vector3(vtkframe.minX, vtkframe.maxY, vtkframe.maxZ));
        vecArray[18] = transform.TransformPoint(new Vector3(vtkframe.minX, vtkframe.maxY, vtkframe.minZ));
        vecArray[19] = transform.TransformPoint(new Vector3(vtkframe.minX, vtkframe.minY, vtkframe.minZ));
        vecArray[20] = transform.TransformPoint(new Vector3(vtkframe.maxX, vtkframe.minY, vtkframe.minZ));
        vecArray[21] = transform.TransformPoint(new Vector3(vtkframe.maxX, vtkframe.maxY, vtkframe.minZ));
        vecArray[22] = transform.TransformPoint(new Vector3(vtkframe.maxX, vtkframe.maxY, vtkframe.maxZ));
        vecArray[23] = transform.TransformPoint(new Vector3(vtkframe.maxX, vtkframe.minY, vtkframe.maxZ));
        mesh.vertices = vecArray;

        MeshCollider meshC = GetComponent<MeshCollider>();
        if (meshC == null)
        {
            meshC = gameObject.AddComponent<MeshCollider>();
        }
        meshC.inflateMesh = mesh;
    }

    System.DateTime dtColorStart;

    // Update is called once per frame

    void Update()
    {
        if (vtkframe != null && vtkframe.isLoadFinish)
        {
            vtkframe.isLoadFinish = false;
            BoxColliderChange();
            //if (transform.parent.GetComponent<modelGameObject>().vtkAllFrame.loadFile.dataType == Enums.DataType.Default)
            //{
            //}
            StartCoroutine("vtkLoadModel");
        }
        if (isLoadModelFininsh)
        {
            isLoadModelFininsh = false;
            dtColorStart = System.DateTime.Now;
            //ToDrawColor();
            StartCoroutine("ToDrawColor");
        }

        if (isLoadColorFininsh)
        {
            isLoadColorFininsh = false;
            System.DateTime dtColorEnd = System.DateTime.Now;
            ToEnable();

           // TestMothed();

            name = name.Split('#')[0] + "#" + vtkframe.selfFrameIndex;
            vtkframe.timeLoadEnd = System.DateTime.Now;

            GlobalVariableBackground.Instance.conManager.WriteLog(vtkframe.selfFrameIndex +
                " Lession to threadStart is " + (vtkframe.timeLoadStart - vtkframe.timeStartLession).TotalMilliseconds +
                "ms, threadStart to threadEnd is " + (vtkframe.timeLoadQFEnd - vtkframe.timeLoadStart).TotalMilliseconds +
                "ms, threadEnd to Enable is " + (vtkframe.timeLoadEnd - vtkframe.timeLoadQFEnd).TotalMilliseconds +
                "ms, ColorStart To ColorEnd is " + (dtColorEnd - dtColorStart).TotalMilliseconds +
                "ms,  AllTime is " + (vtkframe.timeLoadEnd - vtkframe.timeStartLession).TotalMilliseconds + "ms ");

            //float time = System.Convert.ToSingle((vtkframe.timeLoadEnd - vtkframe.timeLoadStart).TotalSeconds / (GlobalVariableBackground.Instance.modelCacheCount / 2));

            //GlobalVariableBackground.Instance.conManager.timeIndex++;

            //GlobalVariableBackground.Instance.conManager.timeArray[GlobalVariableBackground.Instance.conManager.timeIndex % 5] = time;
            //float Sum = 0;
            //for (int i = 0; i < GlobalVariableBackground.Instance.conManager.timeArray.Length; i++)
            //{
            //    Sum += GlobalVariableBackground.Instance.conManager.timeArray[i];
            //}
            //GlobalVariableBackground.Instance.conManager.timeFloat =Mathf.Max(2f, Sum / 5) ;

            // GlobalVariableBackground.Instance.conManager.WriteLog("加载完毕");
            if (isCutModelGO)
            {
                modelGameObject model = transform.parent.GetComponent<modelGameObject>();
                if (model != null)
                {
                    modelFrameManager mf = model.vtkAllFrame.listFrame[vtkframe.selfFrameIndex % GlobalVariableBackground.Instance.modelCacheCount].go.GetComponent<modelFrameManager>();
                    mf.goHaveCutMode = true;
                    mf.ToEnable();
                }
            }

            vtkframe.isAllFinish = true;

        }

    }
    
    private void ToEnable()
    {
        //min = modelState.MinValue;
        //max = modelState.MaxValue;

       //现阶段 是否还需要父物体碰撞盒？？？？？ 

        //碰撞体规则，拾取则为子物体的网格碰撞，其他状态为单帧模型的Cube碰撞体
        bool isChild = GlobalVariableBackground.Instance.conManager.isUsePackUp;
        //if (GlobalVariableBackground.Instance.isUseVtk)
        //{
        //单帧父物体 隐藏碰撞以及模型
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<MeshCollider>().enabled = false;
        if (vtkframe.selfFrameIndex == GlobalVariableBackground.Instance.FrameAllIndex)
        {
            //父物体是否显示碰撞盒 ,该帧不包含切割模型
            if (!isChild && !goHaveCutMode && transform.parent.GetComponent<modelGameObject>().vtkAllFrame.loadFile.dataType == Enums.DataType.Default)
            {
                
                transform.GetComponent<MeshCollider>().enabled = true;
            }
            else
            {
                transform.GetComponent<MeshCollider>().enabled = false;
            }

            modelGameObject mgo = transform.parent.GetComponent<modelGameObject>();
            //子物体碰撞盒显示条件
            for (int i = 0; i < transform.childCount; i++)
            {
                if ((mgo.vtkAllFrame.loadFile.dataType == Enums.DataType.Default &&
                    (mgo.showMode == Enums.ShowMode.Cell ||
        mgo.showMode == Enums.ShowMode.CellAndLine) ) || mgo.vtkAllFrame.loadFile.dataType == Enums.DataType.Water)
                {
                    //是否有切割对象
                    if (goHaveCutMode)
                    {
                        if (CutModeHide)
                        {
                            transform.GetChild(i).GetComponent<MeshRenderer>().enabled = true;
                        }
                        else
                        {
                            transform.GetChild(i).GetComponent<MeshRenderer>().enabled = false;
                        }
                    }
                    else
                    {
                        transform.GetChild(i).GetComponent<MeshRenderer>().enabled = true;
                    }
                }
                else
                {
                    transform.GetChild(i).GetComponent<MeshRenderer>().enabled = false;
                }
                //isCollider区分是水还是船对象是否有碰撞功能（貌似逻辑有问题）
                //isChild区分是否子对象能拾取碰撞
                //goHaveCutMode区分对象是否含有切割模式对象
                if (isCollider && isChild&& !goHaveCutMode)
                {
                    //如果是切割体
                    if (isCutModelGO)
                    {
                        //且未隐藏
                        if (!CutModeHide)
                        {
                            transform.GetChild(i).GetComponent<MeshCollider>().enabled = true;
                        }
                        else
                        {
                            transform.GetChild(i).GetComponent<MeshCollider>().enabled = false;
                        }
                    }
                    else
                    {
                        //不是切割体 且 不含有切割对象
                        transform.GetChild(i).GetComponent<MeshCollider>().enabled = true;
                    }
                }
                else
                {
                    //有切割体 且 切割对象隐藏
                    if (CutModeHide)
                    {
                        transform.GetChild(i).GetComponent<MeshCollider>().enabled = true;
                    }
                    else
                    {
                        transform.GetChild(i).GetComponent<MeshCollider>().enabled = false;
                    }
                }

                if (modelState.symmetricmode != Enums.SymmetricMode.NONE)
                {
                    transform.GetChild(i).GetComponent<ChildMeshManager>().stateUpdate();
                }
            }

            if (pick.lastFrameIndex != GlobalVariableBackground.Instance.FrameAllIndex)
            {
                TableAddValue();
            }


            if (pick.isMeshHave)
            {
                PickUpUpdate();
            }
        }
        else
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).GetComponent<MeshRenderer>().enabled = false;
                transform.GetChild(i).GetComponent<MeshCollider>().enabled = false;
            }
        }
        //}
    }

    private void PickUpUpdate()
    {
        if (pick.isPointMode)
        {
            for (int i = 0; i < vtkframe.meshArray.Length; i++)
            {
                for (int k = 0; k < vtkframe.meshArray[i].pointArray.Length; k++)
                {
                    //修正关系 根据number;
                    if (vtkframe.meshArray[i].pointArray[k].number == pick.number)
                    {

                        GlobalVariableBackground.Instance.conManager.WriteLog("meshArray[" + i + "].pointArray[" + k + "].number is " + pick.number); GlobalVariableBackground.Instance.conManager.WriteLog("old MeshIndex is " + pick.MeshIndex + " pointIndex is " + pick.pointIndex);
                        pick.MeshIndex = i;
                        pick.pointIndex = k;
                        GlobalVariableBackground.Instance.conManager.WriteLog("new  MeshIndex is " + pick.MeshIndex + " pointIndex is " + pick.pointIndex);
                    }
                }

            }
        }

        if (pick.isMeshHave &&
            pick.ModelName.Equals(transform.parent.name) &&
            modelState.attIndex >= 0 &&
            pick.isPointMode)
        {
            Mesh m = transform.GetChild(pick.MeshIndex).GetComponent<MeshFilter>().mesh;
            Vector3[] vecArray = m.vertices;
            int[] triArray = m.triangles;
            Vector3 vec = transform.TransformPoint(vecArray[pick.pointIndex]);
            Vector3[] newVecArray =  GlobalVariableBackground.Instance.conManager.GetPointCube(vec, 0.04f);
            //GlobalVariableBackground.Instance.conManager.WriteLog("点坐标："+vec.x+" , "+vec.y+" , "+vec.z);
            //GlobalVariableBackground.Instance.conManager.WriteLog("Number is : "+vtkframe.meshArray[pick.MeshIndex].pointArray[pick.pointIndex].number);

            pick.mesh.vertices = newVecArray;
            pick.mesh.RecalculateNormals();
        }
        else
        if (pick.isMeshHave &&
            pick.ModelName.Equals(transform.parent.name) &&
            modelState.attIndex >= 0 &&
            !pick.isPointMode)
        {
            Mesh m = transform.GetChild(pick.MeshIndex).GetComponent<MeshFilter>().mesh;
            Vector3[] vecArray = m.vertices;
            int[] triArray = m.triangles;
            

            ControlManager cm = GlobalVariableBackground.Instance.conManager;

            List<Vector3> listVec = new List<Vector3>();
            List<int> listTri = new List<int>();
            Vector3 v1 = transform.TransformPoint(vecArray[triArray[pick.TriIndex * 3]]);
            Vector3 v2 = transform.TransformPoint(vecArray[triArray[pick.TriIndex * 3+1]]);
            Vector3 v3 = transform.TransformPoint(vecArray[triArray[pick.TriIndex * 3+2]]);
            cm.AddLineArray3(listVec, listTri, cm.MakeQuad(v1, v2, v3,0.1f));

            if (pick.mesh.vertices.Length > listVec.Count)
            {
                pick.mesh.triangles = listTri.ToArray();
                pick.mesh.SetVertices(listVec);
            }
            else
            {
                pick.mesh.SetVertices(listVec);
                pick.mesh.triangles = listTri.ToArray();
            }
            pick.mesh.RecalculateNormals();
        }
    }

    private IEnumerator ToDrawColor()
    {
        if (modelState.isUsecolor)
        {
            Color[] colorArray;
            Color color;
            float min = vtkframe.point_MaxAndMin[modelState.maxminIndex].Min;
            float max = vtkframe.point_MaxAndMin[modelState.maxminIndex].Max;
            for (int i = 0; i < vtkframe.meshArray.Length; i++)
            {
                Mesh mesh = transform.GetChild(i).GetComponent<MeshFilter>().mesh;
                if (modelState.isPointAtt)
                {
                    CLoadTest.Point_Out[] pointArray = vtkframe.meshArray[i].pointArray;
                    colorArray = new Color[pointArray.Length];
                    for (int k = 0; k < pointArray.Length; k++)
                    {
                        if (k == 25000)
                        {
                            yield return new WaitForEndOfFrame();
                        }
                        color = new Color();
                        try
                        {
                            //modelState.MaxValue = Mathf.Max(modelState.MaxValue, vtkframe.meshArray[i].pointArray[k].attArray[modelState.colorIndex].arrayData[0]);
                            //modelState.MinValue = Mathf.Min(modelState.MinValue, vtkframe.meshArray[i].pointArray[k].attArray[modelState.colorIndex].arrayData[0]);
                            float value = (pointArray[k].attArray[modelState.attIndex].arrayData[0] - min) / (max - min);
                            //Color color = Color.HSVToRGB((1 - ((value - minColorValue) / ColorLength)) * 0.63f, 1f, 1f);
                            color = Color.HSVToRGB((1 - value) * 0.63f, 1f, 1f);
                            colorArray[k] = color;
                        }
                        catch
                        {
                            GlobalVariableBackground.Instance.conManager.WriteLog("Error!!");
                            if (pointArray[k] == null)
                            {
                                GlobalVariableBackground.Instance.conManager.WriteLog("vtkframe.meshArray[" + i + "].pointArray[" + k + "] is null");
                            }
                            else
                            if (pointArray[k].attArray == null)
                            {
                                GlobalVariableBackground.Instance.conManager.WriteLog("vtkframe.meshArray[" + i + "].pointArray[" + k + "].attArray is null");
                            }
                            else
                            if (pointArray[k].attArray[modelState.attIndex] == null)
                            {
                                GlobalVariableBackground.Instance.conManager.WriteLog("vtkframe.meshArray[" + i + "].pointArray[" + k + "].attArray[modelState.colorIndex] is null");
                            }
                            else if (pointArray[k].attArray[modelState.attIndex].arrayData == null)
                            {
                                GlobalVariableBackground.Instance.conManager.WriteLog("vtkframe.meshArray[" + i + "].pointArray[" + k + "].attArray[modelState.colorIndex].arrayData[0] is null");
                            }
                        }


                    }
                    mesh.colors = colorArray;
                    colorArray = null;
                    yield return new WaitForEndOfFrame();
                }
                else
                {
                    //网格颜色处理
                    Dictionary<int, List<float>> dic = new Dictionary<int, List<float>>();
                    int[] tri = mesh.triangles;
                    int indexTri,pointIndex;
                    float value;
                    List<float> listf;
                    for (int j = 0; j < tri.Length;j++)
                    {
                        indexTri = j / 3;
                        pointIndex = tri[j];
                        value =vtkframe.meshArray[i].cellArray[indexTri].attArray[modelState.attIndex].arrayData[0];

                        if (!dic.ContainsKey(pointIndex))
                        {
                            listf = new List<float>();
                            dic.Add(pointIndex, listf);
                        }
                        else
                        {
                            listf = dic[pointIndex];
                        }
                        listf.Add(value);
                    }
                    Color[] cArray = new Color[dic.Count];
                    Color c;
                    float v;
                    float sum;
                    float colorValue;
                    for (int j = 0; j < cArray.Length; j++)
                    {
                        sum = 0;
                        for (int k = 0; k <dic[j].Count; k++)
                        {
                            sum += dic[j][k];
                        }
                        v = sum / dic[j].Count;
                        dic[j].Clear();
                        dic[j] = null;
                        colorValue = (v - min) / (max - min);
                        c = Color.HSVToRGB((1 - colorValue) * 0.63f, 1f, 1f);
                        cArray[j] = c;
                    }
                    dic.Clear();
                    dic = null;
                    mesh.colors = cArray;
                    cArray = null;
                    yield return new WaitForEndOfFrame();
                }
            }


        }
        isLoadColorFininsh = true;
    }

    private void TableAddValue()
    {
        //Others.PackUpObject pick = GlobalVariableBackground.Instance.conManager.pickClick;
        //表格 点
        if (pick.isMeshHave &&
            pick.ModelName.Equals(transform.parent.name) &&
            modelState.attIndex >= 0 &&
            GlobalVariableBackground.Instance.TableObject.gameObject.activeSelf &&
            pick.isPointMode)
        {
            //确认为 点模式的数据
            float value = vtkframe.meshArray[pick.MeshIndex].pointArray[pick.pointIndex].attArray[pick.AttIndex].arrayData[0];
            GlobalVariableBackground.Instance.TableObject.plotData(value);
            pick.lastFrameIndex = GlobalVariableBackground.Instance.FrameAllIndex;
        }
        if (pick.isMeshHave &&
    pick.ModelName.Equals(transform.parent.name) &&
    modelState.attIndex >= 0 &&
    GlobalVariableBackground.Instance.TableObject.gameObject.activeSelf &&
    !pick.isPointMode)
        {
            float value = vtkframe.meshArray[pick.MeshIndex].cellArray[pick.TriIndex].attArray[pick.AttIndex].arrayData[0];
            GlobalVariableBackground.Instance.TableObject.plotData(value);
            pick.lastFrameIndex = GlobalVariableBackground.Instance.FrameAllIndex;
        }

    }


    private IEnumerator vtkLoadModel()
    {
        GameObject goChild;
        bool isboat = transform.parent.GetComponent<modelGameObject>().vtkAllFrame.loadFile.dataType == Enums.DataType.Default;
        for (int i = 0; i < vtkframe.meshArray.Length; i++)
        {
            if (transform.childCount <= i)
            {
                if (isboat)
                {
                    goChild = GameObject.Instantiate(GlobalVariableBackground.Instance.modelPerfab);
                }
                else
                {
                    goChild = GameObject.Instantiate(GlobalVariableBackground.Instance.waterPerfab);
                }
                goChild.name = name + "|" + i;
                goChild.transform.parent = transform;

                goChild.AddComponent<ClickGameObjectEvent>();
                ChildMeshManager cmm = goChild.AddComponent<ChildMeshManager>();
                cmm.modelState = modelState;
            }
            else
            {
                goChild = transform.GetChild(i).gameObject;
                goChild.name = name + "|" + i;
            }
            Vector3[] NewVec = new Vector3[vtkframe.meshArray[i].vec3Array.Length];
            Vector2[] newUV = new Vector2[vtkframe.meshArray[i].vec3Array.Length];
            float UVlengthX = vtkframe.maxX - vtkframe.minX;
            float UVlengthY = vtkframe.maxY - vtkframe.minY;
            for (int j = 0; j < vtkframe.meshArray[i].vec3Array.Length; j++)
            {
                NewVec[j] = transform.TransformPoint(vtkframe.meshArray[i].vec3Array[j]);
                newUV[j] = new Vector2((vtkframe.meshArray[i].vec3Array[j].x - vtkframe.minX) / UVlengthX, (vtkframe.meshArray[i].vec3Array[j].y - vtkframe.minY) / UVlengthY);
            }
            MeshFilter mfchild = goChild.GetComponent<MeshFilter>();
            if (mfchild.mesh.vertices.Length <= NewVec.Length)
            {
                mfchild.mesh.vertices = NewVec;
                if (!isboat)
                {
                    mfchild.mesh.uv = newUV;
                }
                mfchild.mesh.triangles = vtkframe.meshArray[i].triArray;
            }
            else
            {
                mfchild.mesh.triangles = vtkframe.meshArray[i].triArray;
                mfchild.mesh.vertices = NewVec;
                if (!isboat)
                {
                    mfchild.mesh.uv = newUV;
                }
            }
            vtkframe.meshArray[i].triArray = null;
            vtkframe.meshArray[i].vec3Array = null;
            NewVec = null;
            mfchild.mesh.uv2 = null;


            //bool ishaveNormals = false;
            //int normalsIndex = 0;
            //for (int j = 0; j < vtkframe.pointAttType.listAttName.Count; j++)
            //{
            //    if (vtkframe.pointAttType.listAttName[j].ToLower().Equals("normals"))
            //    {
            //        normalsIndex = j;
            //        ishaveNormals = true;
            //        break;
            //    }
            //}
            //if (ishaveNormals)
            //{
            //    Vector3[] nomArray = new Vector3[vtkframe.meshArray[i].triArray.Length];
            //    for (int j = 0; j < vtkframe.meshArray[i].pointArray.Length; j++)
            //    {
            //        Vector3 vec = new Vector3();
            //        vec.x = vtkframe.meshArray[i].pointArray[j].attArray[normalsIndex].arrayData[0];
            //        vec.z = vtkframe.meshArray[i].pointArray[j].attArray[normalsIndex].arrayData[1];
            //        vec.y = vtkframe.meshArray[i].pointArray[j].attArray[normalsIndex].arrayData[2];
            //        nomArray[j] = vec;
            //    }
            //    mfchild.mesh.normals = nomArray;
            //    nomArray = null;
            //} else
            //{
            mfchild.mesh.RecalculateNormals();
            mfchild.mesh.RecalculateTangents();
            //}
            mfchild.mesh.name = goChild.name + "Mesh";
            MeshCollider mc = mfchild.GetComponent<MeshCollider>();

            yield return new WaitForEndOfFrame();
        }
        isLoadModelFininsh = true;
    }

    //private IEnumerator LoadModel()
    //{

    //    if (!frame.tooBig)
    //    {
    //        MeshFilter mf = gameObject.GetComponent<MeshFilter>();
    //        if (mf != null)
    //        {
    //            mf.mesh.vertices = frame.mesh.vertices;
    //            mf.mesh.triangles = frame.mesh.triangles;
    //            mf.mesh.RecalculateNormals();
    //        }
    //    }
    //    else
    //    {
    //        //MeshFilter mf = gameObject.GetComponent<MeshFilter>();
    //        //mf.mesh = new Mesh();
    //        //CombineInstance[] combine = new CombineInstance[frame.listChildMesh.Count];
    //        //Mesh childMesh;
    //        //for (int i = 0; i < combine.Length; i++)
    //        //{
    //        //    childMesh = new Mesh();
    //        //    childMesh.vertices = frame.listChildMesh[i].vertices;
    //        //    childMesh.triangles = frame.listChildMesh[i].triangles;
    //        //    combine[i].mesh = childMesh;
    //        //    //矩阵(Matrix)自身空间坐标的点转换成世界空间坐标的点   
    //        //    combine[i].transform = transform.worldToLocalMatrix;
    //        //}
    //        //mf.mesh.CombineMeshes(combine, false);



    //        GameObject goChild;


    //        for (int i = 0; i < frame.listChildMesh.Count; i++)
    //        {
    //            if (transform.childCount <= i)
    //            {
    //                goChild = GameObject.Instantiate(GlobalVariableBackground.Instance.cubePerfab);
    //                goChild.name = name + "|" + i;
    //                goChild.transform.parent = transform;
    //            }
    //            else
    //            {
    //                goChild = transform.GetChild(i).gameObject;
    //            }
    //            if (frame.selfFrameIndex == GlobalVariableBackground.Instance.FrameAllIndex)
    //            {
    //                goChild.GetComponent<MeshRenderer>().enabled = true;
    //                if (isCollider)
    //                {
    //                    goChild.GetComponent<MeshCollider>().enabled = true;
    //                }
    //            }
    //            else
    //            {
    //                goChild.GetComponent<MeshRenderer>().enabled = false;
    //                goChild.GetComponent<MeshCollider>().enabled = false;

    //            }
    //            MeshFilter mfchild = goChild.GetComponent<MeshFilter>();
    //            //Mesh newMesh = new Mesh();
    //            //mfchild.mesh = newMesh;
    //            //newMesh .vertices = frame.listChildMesh[i].vertices;
    //            //newMesh.triangles = frame.listChildMesh[i].triangles;
    //            //newMesh.RecalculateNormals();
    //            //GlobalVariableBackground.Instance.conManager.WriteLog("child name "+ goChild.name + " vertices is "+ frame.listChildMesh[i].vertices.Length);
    //            if (mfchild.mesh.vertices.Length <= frame.listChildMesh[i].vertices.Length)
    //            {
    //                mfchild.mesh.vertices = frame.listChildMesh[i].vertices;
    //                mfchild.mesh.triangles = frame.listChildMesh[i].triangles;
    //            }
    //            else
    //            {
    //                mfchild.mesh.triangles = frame.listChildMesh[i].triangles;
    //                mfchild.mesh.vertices = frame.listChildMesh[i].vertices;
    //            }
    //            mfchild.mesh.RecalculateNormals();
    //            mfchild.mesh.name = goChild.name + "Mesh";
    //            MeshCollider mc = mfchild.GetComponent<MeshCollider>();
    //            mc.sharedMesh = mfchild.mesh;
    //            yield return new WaitForEndOfFrame();

    //        }
    //    }

    //}




}
