using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlManager : MonoBehaviour {

    public delegate void CustomEventHandler();

    public delegate void CustomEventArgs<T>(T args);

    /// <summary>
    /// 帧数据自检事件
    /// </summary>
    public event CustomEventHandler FrameChange;

    /// <summary>
    /// 帧显示自检事件
    /// </summary>
    public event CustomEventHandler FrameEnable;

    /// <summary>
    /// 帧初始化事件
    /// </summary>
    public event CustomEventHandler FrameInitValue;

    public event CustomEventHandler ToPointMode;

    public event CustomEventHandler ToCellMode;

    public event CustomEventHandler ToLineMode;

    public event CustomEventHandler ToLineAndCellMode;

    public event CustomEventHandler ToCutModel;

    public event CustomEventHandler ToCanPickUp;

    public event CustomEventHandler ToSmooth;

    public event CustomEventArgs<Enums.SymmetricMode> ToSymmetric;

    public event CustomEventHandler ToArrowMode;

    public float timeFloat;

    //public float[] timeArray = new float[5] {1f,1f,1f,1f,1f};

    //public int timeIndex =1;

    public GameObject senceModel;

    public Text text;

    private List<string> listStr = new List<string>();
    private Material line;
    private Material selectLine;

    public bool isUsePackUp = false;

    public Others.PackUpObject pickUpObj;

    public Others.PackUpObject pickClick;

    private Enums.PickMode pickmode;

    private RaycastHit hit;//用来保存射线碰撞信息  

    int[] pointTriTemp;

    Color[] colorPointArrayTemp;


    List<Vector3> listVec = new List<Vector3>();
    List<int> listTri = new List<int>();

    private void Awake()
    {
        GlobalVariableBackground.Instance.conManager = this;
        senceModel = GameObject.Find("SenceModel");
        line = GlobalVariableBackground.Instance.MaterialLine;
        selectLine = GlobalVariableBackground.Instance.MaterialSelectLine;
        pickUpObj = new Others.PackUpObject();
        pickClick = new Others.PackUpObject();

        //pointPickObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
        //pointPickObj.GetComponent<MeshRenderer>().enabled = false;
        //Destroy(pointPickObj.GetComponent<BoxCollider>());
        //pointPickObj.GetComponent<MeshFilter>().mesh = GlobalVariableBackground.Instance.miniCubeMesh;
        //pointPickObj.transform.localScale = pointPickObj.transform.localScale * 1.01f;
        pickmode = Enums.PickMode.Point;
        colorPointArrayTemp = new Color[24];
        for (int i = 0; i < 24; i++)
        {
            //基佬紫
            colorPointArrayTemp[i] = Color.HSVToRGB(304 / 360f, 1f, 1f);
        }
        pointTriTemp = GlobalVariableBackground.Instance.miniCubeMesh.triangles;
    }

    private void Start()
    {
        OverallEvent_Manger.Instance.AnimationState += Instance_AnimationState;
        OverallEvent_Manger.Instance.StartFPS += Instance_StartFPS;
        OverallEvent_Manger.Instance.ShowState += Instance_ShowState;
        OverallEvent_Manger.Instance.SetPickup += Instance_SetPickup;
    }

    private void Instance_SetPickup(Pickup args)
    {
        switch (args)
        {
            case Pickup.PickupMesh:
                isUsePackUp = true;
                pickmode = Enums.PickMode.cell;
                FrameEnable();
                break;
            case Pickup.PickupPoint:
                isUsePackUp = true;
                pickmode = Enums.PickMode.Point;
                FrameEnable();
                break;
            default:
                break;
        }

    }

    private void Instance_ShowState(ShowPointLinePlaneState args)
    {
        switch (args)
        {
            case ShowPointLinePlaneState.Point:
                ChangePointMode();
                break;
            case ShowPointLinePlaneState.Line:
                ChangeLineMode();
                break;
            case ShowPointLinePlaneState.Plane:
                ChangeCellMode();
                break;
            default:
                break;
        }
    }

    private void Instance_StartFPS(int args)
    {

    }

    private void Instance_AnimationState(AnimationFrame args)
    {
        switch (args)
        {
            case AnimationFrame.PreviousFrame:
                GlobalVariableBackground.Instance.playRun = false;
                LastFrame();
                break;
            case AnimationFrame.Play:
                GlobalVariableBackground.Instance.playRun = true;
                StartCoroutine("Run", timeFloat);
                break;
            case AnimationFrame.Stop:
                GlobalVariableBackground.Instance.playRun = false;
                break;
            case AnimationFrame.NextFrame:
                GlobalVariableBackground.Instance.playRun = false;
                NextFrame();
                break;
            default:
                break;
        }

    }


    // Update is called once per frame
    void Update()
    {
        //下一帧
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            GlobalVariableBackground.Instance.playRun = false;
            NextFrame();
        }
        //上一帧
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            GlobalVariableBackground.Instance.playRun = false;
            LastFrame();
        }

        //自动播放
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            GlobalVariableBackground.Instance.playRun = true;
            StartCoroutine("Run", timeFloat);
        }

        //初始化帧
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            FrameInit();
        }

        //显示点模式
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            ChangePointMode();
        }
        //显示网格模式
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            ChangeCellMode();
        }

        //显示线模式
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            ChangeLineMode();
        }

        //显示网格+线模式
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            ChangeLineAndCellMode();
        }

        //切割模型
        if (Input.GetKeyDown(KeyCode.C))
        {
            CutModel();
        }

        ////垃圾回收 测试使用
        //if (Input.GetKeyDown(KeyCode.G))
        //{
        //    GC.Collect();
        //    GlobalVariableBackground.Instance.conManager.WriteLog("GC.Collect()");
        //}

        //网格线拾取
        if (Input.GetKeyDown(KeyCode.P))
        {
            isUsePackUp = true;
            pickmode = Enums.PickMode.cell;
            FrameEnable();
            GlobalVariableBackground.Instance.conManager.WriteLog("cell isUsePackUp is " + isUsePackUp);
        }

        //点拾取
        if (Input.GetKeyDown(KeyCode.O))
        {
            isUsePackUp = true;
            pickmode = Enums.PickMode.Point;
            FrameEnable();
            GlobalVariableBackground.Instance.conManager.WriteLog("point isUsePackUp is " + isUsePackUp);
        }
        //关闭拾取
        if (Input.GetKeyDown(KeyCode.U))
        {
            isUsePackUp = false;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            if (ToArrowMode != null)
            {
                ToArrowMode();
            }
        }

        //拾取实时 效果显示
        if (isUsePackUp)
        {
            PickUpObject(pickUpObj);
            if (pickClick.isMeshHave)
            {
                Graphics.DrawMesh(pickClick.mesh, senceModel.transform.localToWorldMatrix, selectLine, 0);
            }
            if (pickUpObj.isMeshHave)
            {
                Graphics.DrawMesh(pickUpObj.mesh, senceModel.transform.localToWorldMatrix, line, 0);
            }
        }

        //确认拾取
        if (Input.GetMouseButtonDown(0))
        {
            PickUpClick();
        }

        //根据Z轴对称
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (ToSymmetric != null)
            {
                ToSymmetric(Enums.SymmetricMode.Z);
            }
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            if (ToSmooth != null)
            {
                ToSmooth();
            }
        }
    }

    public void WriteLog(string str)
    {
        listStr.Add(str);
        while (listStr.Count > 20)
        {
            listStr.RemoveAt(0);
        }
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        for (int i = 0; i < listStr.Count; i++)
        {
            sb.AppendLine(listStr[i]);
        }
        text.text = sb.ToString();
        sb = null;
    }

    /// <summary>
    /// 检测帧是否加载完毕
    /// </summary>
    /// <returns></returns>o
    private bool ChickFrameFinish()
    {
        for (int i = 0; i < senceModel.transform.childCount; i++)
        {
            VTK_AllFrame allFrame = senceModel.transform.GetChild(i).GetComponent<modelGameObject>().vtkAllFrame;
            if (!allFrame.listFrame[(GlobalVariableBackground.Instance.FrameAllIndex + 1) % GlobalVariableBackground.Instance.modelCacheCount].isAllFinish)
            {
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// 点击选中
    /// </summary>
    private void PickUpClick()
    {
        if (pickUpObj.isMeshHave)
        {
            pickClick.lastFrameIndex = GlobalVariableBackground.Instance.FrameAllIndex;
            pickClick.isMeshHave = pickUpObj.isMeshHave;
            pickClick.pickUpIndex = pickUpObj.pickUpIndex;
            pickClick.pickUpName = pickUpObj.pickUpName;
            Vector3[] vec = pickUpObj.mesh.vertices;
            int[] tri = pickUpObj.mesh.triangles;
            if (pickClick.mesh.vertices.Length > vec.Length)
            {
                pickClick.mesh.triangles = tri;
                pickClick.mesh.vertices = vec;
            }
            else
            {
                pickClick.mesh.vertices = vec;
                pickClick.mesh.triangles = tri;
            }

            int colorLength = pickUpObj.mesh.colors.Length;
            pickClick.isPointMode = pickUpObj.isPointMode;
            pickClick.AttIndex = pickUpObj.AttIndex;
            pickClick.pointIndex = pickUpObj.pointIndex;
            Color[] color = new Color[colorLength];
            for (int i = 0; i < color.Length; i++)
            {
                color[i] = Color.yellow;
            }

            pickClick.mesh.colors = color;

            pickClick.mesh.RecalculateNormals();
            pickClick.mesh.bounds = new Bounds(Vector3.zero, Vector3.one * 100000f);

            modelFrameManager mf = hit.transform.parent.GetComponent<modelFrameManager>();
            if (mf != null)
            {
                if (mf.vtkframe.CellAttType.listAttCount.Count > 0 && pickmode == Enums.PickMode.cell)
                {
                    if (!GlobalVariableBackground.Instance.TableObject.gameObject.activeSelf)
                    {
                        GlobalVariableBackground.Instance.TableObject.gameObject.SetActive(true);
                    }
                }
                else if (mf.vtkframe.pointAttType.listAttCount.Count > 0 && pickmode == Enums.PickMode.Point)
                {
                    if (!GlobalVariableBackground.Instance.TableObject.gameObject.activeSelf)
                    {
                        GlobalVariableBackground.Instance.TableObject.gameObject.SetActive(true);
                    }
                }
                else
                {
                    GlobalVariableBackground.Instance.TableObject.gameObject.SetActive(false);
                }
                GlobalVariableBackground.Instance.TableObject.ClearValue();
                if (pickClick.MeshIndex >= 0 && pickClick.pointIndex >= 0)
                {
                    pickClick.number = mf.vtkframe.meshArray[pickClick.MeshIndex].pointArray[pickClick.pointIndex].number;
                }
            }

        }

    }

    /// <summary>
    /// 跟随显示选中网格或点
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    private Others.PackUpObject PickUpObject(Others.PackUpObject obj)
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //射线投射  
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform == null)//如果射线没有碰撞到Transform,return 
            {
                obj.isMeshHave = false;
                return obj;
            }

            if (obj == null)
            {
                obj = new Others.PackUpObject();
            }

            if (obj.pickUpName.Equals(hit.transform.name) && obj.pickUpIndex == hit.triangleIndex && pickmode == Enums.PickMode.cell)
            {
                return obj;
            }

            //获取hit.transform的MeshFilter组件  
            MeshFilter meshFilter = hit.transform.GetComponent<MeshFilter>();
            if (meshFilter == null)
            {
                obj.isMeshHave = false;
                return obj;
            }

            if (hit.transform.parent == null)
            {
                obj.isMeshHave = false;
                return obj;
            }

            modelFrameManager modelChild = hit.transform.parent.GetComponent<modelFrameManager>();
            if (modelChild == null)
            {
                obj.isMeshHave = false;
                return obj;
            }

            if (!hit.transform.name.Contains("|"))
            {
                obj.isMeshHave = false;
                return obj;
            }

            Mesh mesh = meshFilter.mesh;
            int[] triArray = mesh.triangles;
            Vector3[] vecArray = mesh.vertices;
            //CLoadTest.Cell_Out cell = modelChild.vtkframe.meshArray[index].cellArray[hit.triangleIndex];
            //int number = cell.number;
            int hitIndex = hit.triangleIndex * 3;
            //AddLineArray(listVec, listTri, MakeQuad(vecArray[triArray[hitIndex]], vecArray[triArray[hitIndex + 1]], 0.001f));
            //AddLineArray(listVec, listTri, MakeQuad(vecArray[triArray[hitIndex + 1]], vecArray[triArray[hitIndex + 2]], 0.001f));
            //AddLineArray(listVec, listTri, MakeQuad(vecArray[triArray[hitIndex + 2]], vecArray[triArray[hitIndex]], 0.001f));
            obj.AttIndex = modelChild.modelState.attIndex;
            switch (pickmode)
            {
                case Enums.PickMode.Point:
                    Vector3 v1 = transform.TransformPoint(vecArray[triArray[hitIndex]]);
                    Vector3 v2 = transform.TransformPoint(vecArray[triArray[hitIndex+1]]);
                    Vector3 v3 = transform.TransformPoint(vecArray[triArray[hitIndex+2]]);

                    float f1 = Vector3.Distance(v1, hit.point);
                    float f2 = Vector3.Distance(v2, hit.point);
                    float f3 = Vector3.Distance(v3, hit.point);
                    Vector3 vec;
                    if (f1 < f2 && f1 < f3)
                    {
                        vec = v1;
                        obj.pointIndex = triArray[hitIndex];

                    }
                    else if (f2 < f1 && f2 < f3)
                    {
                        vec = v2;
                        obj.pointIndex = triArray[hitIndex+1];
                    }
                    else
                    {
                        vec = v3;
                        obj.pointIndex = triArray[hitIndex+2];
                    }

                    Vector3[] vecTemp = GetPointCube(vec, 0.04f);
                    obj.mesh.vertices = vecTemp;
                    obj.mesh.triangles = pointTriTemp;
                    obj.mesh.colors = colorPointArrayTemp;
                    obj.mesh.RecalculateNormals();
                    obj.pickUpIndex = hit.triangleIndex;
                    obj.pickUpName = hit.transform.name;
                    obj.isMeshHave = true;
                    obj.isPointMode = true;
                    //GlobalVariableBackground.Instance.conManager.WriteLog("obj.pickUpName is " + obj.pickUpName + " obj.pickUpIndex is " + obj.pickUpIndex);
                    int index = Convert.ToInt32(obj.pickUpName.Split('|')[1]);
                    break;
                case Enums.PickMode.cell:
                    //TODO：追加线颜色处理
                    AddLineArray3(listVec, listTri, MakeQuad(vecArray[triArray[hitIndex]], vecArray[triArray[hitIndex + 1]], vecArray[triArray[hitIndex + 2]], GlobalVariableBackground.Instance.Linewight*1.5f));
                    Color[] colorArray = new Color[listVec.Count];

                    for (int i = 0; i < listVec.Count; i++)
                    {
                        //基佬紫
                        colorArray[i] = Color.HSVToRGB(304 / 360f, 1f, 1f);
                    }

                    //Mesh ms = new Mesh();
                    if (obj.mesh.vertices.Length > listVec.Count)
                    {
                        obj.mesh.triangles = listTri.ToArray();
                        obj.mesh.SetVertices(listVec);
                    }
                    else
                    {
                        obj.mesh.SetVertices(listVec);
                        obj.mesh.triangles = listTri.ToArray();
                    }
                    obj.mesh.colors = colorArray;
                    obj.mesh.RecalculateNormals();
                    listVec.Clear();
                    listTri.Clear();
                    colorArray = null;
                    obj.pickUpIndex = hit.triangleIndex;
                    obj.pickUpName = hit.transform.name;
                    obj.isMeshHave = true;
                    obj.isPointMode = false;

                    //GlobalVariableBackground.Instance.conManager.WriteLog("obj.pickUpName is " + obj.pickUpName + " obj.pickUpIndex is " + obj.pickUpIndex);
                    break;
                default:
                    break;
            }
            return obj;
        }
        obj.isMeshHave = false;
        return obj;
    }

    public Vector3[] GetPointCube(Vector3 vec ,float w)
    {
        w = w / 2;

        float maxX = vec.x + w;
        float minX = vec.x - w;

        float maxY = vec.y + w;
        float minY = vec.y - w;

        float maxZ = vec.z + w;
        float minZ = vec.z - w;
        Vector3[] vecTemp = new Vector3[24];
        vecTemp[0] = new Vector3(maxX, minY, maxZ);
        vecTemp[1] = new Vector3(minX, minY, maxZ);
        vecTemp[2] = new Vector3(maxX, maxY, maxZ);
        vecTemp[3] = new Vector3(minX, maxY, maxZ);
        vecTemp[4] = new Vector3(maxX, maxY, minZ);
        vecTemp[5] = new Vector3(minX, maxY, minZ);
        vecTemp[6] = new Vector3(maxX, minY, minZ);
        vecTemp[7] = new Vector3(minX, minY, minZ);
        vecTemp[8] = new Vector3(maxX, maxY, maxZ);
        vecTemp[9] = new Vector3(minX, maxY, maxZ);
        vecTemp[10] = new Vector3(maxX, maxY, minZ);
        vecTemp[11] = new Vector3(minX, maxY, minZ);
        vecTemp[12] = new Vector3(maxX, minY, minZ);
        vecTemp[13] = new Vector3(maxX, minY, maxZ);
        vecTemp[14] = new Vector3(minX, minY, maxZ);
        vecTemp[15] = new Vector3(minX, minY, minZ);
        vecTemp[16] = new Vector3(minX, minY, maxZ);
        vecTemp[17] = new Vector3(minX, maxY, maxZ);
        vecTemp[18] = new Vector3(minX, maxY, minZ);
        vecTemp[19] = new Vector3(minX, minY, minZ);
        vecTemp[20] = new Vector3(maxX, minY, minZ);
        vecTemp[21] = new Vector3(maxX, maxY, minZ);
        vecTemp[22] = new Vector3(maxX, maxY, maxZ);
        vecTemp[23] = new Vector3(maxX, minY, maxZ);
        return vecTemp;
    }

    public Vector3[] MakeQuad(Vector3 p1, Vector3 p2, Vector3 p3, float w)
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


    /// <summary>
    /// 播放下一帧
    /// </summary>
    private void NextFrame()
    {
        GlobalVariableBackground.Instance.playRun = false;
        if (GlobalVariableBackground.Instance.FrameAllIndex == GlobalVariableBackground.Instance.FrameALLCount - 1)
        {
            return;
        }

        if (!ChickFrameFinish())
        {
            GlobalVariableBackground.Instance.conManager.WriteLog("数据未加载完毕");
            return;
        }

        GlobalVariableBackground.Instance.FrameAllIndex++;

        GlobalVariableBackground.Instance.conManager.WriteLog("FrameAllIndex is " + GlobalVariableBackground.Instance.FrameAllIndex);
        if (FrameChange != null)
        {
            FrameChange();//帧自检内容
            FrameEnable();//帧自检显示
        }
    }

    /// <summary>
    /// 播放上一帧
    /// </summary>
    private void LastFrame()
    {
        GlobalVariableBackground.Instance.playRun = false;
        if (GlobalVariableBackground.Instance.FrameAllIndex == 0)
        {
            return;
        }
        if (!ChickFrameFinish())
        {
            GlobalVariableBackground.Instance.conManager.WriteLog("数据未加载完毕");
            return;
        }
        GlobalVariableBackground.Instance.FrameAllIndex--;
        GlobalVariableBackground.Instance.conManager.WriteLog("FrameAllIndex is " + GlobalVariableBackground.Instance.FrameAllIndex);
        if (FrameChange != null)
        {
            FrameChange();
            FrameEnable();
        }
    }

    /// <summary>
    /// 自动播放
    /// </summary>
    /// <returns></returns>
    IEnumerator  Run()
    {
        if (GlobalVariableBackground.Instance.playRun)
        {
            if (GlobalVariableBackground.Instance.FrameAllIndex != GlobalVariableBackground.Instance.FrameALLCount - 1)
            {

                NextFrame();
                GlobalVariableBackground.Instance.playRun = true;
                yield return new WaitForSeconds(timeFloat);
                StartCoroutine("Run", timeFloat);
            }
            else
            {
                GlobalVariableBackground.Instance.playRun = false;
                GlobalVariableBackground.Instance.conManager.WriteLog("运行到头");
            }
        }


    }

    /// <summary>
    /// 帧初始化
    /// </summary>
    private void FrameInit()
    {
        GlobalVariableBackground.Instance.playRun = false;
        if (FrameInitValue != null)
        {
            FrameInitValue();
        }
    }

    private void ChangePointMode()
    {
        if (ToPointMode != null)
        {
            ToPointMode();
            FrameEnable();

        }
    }

    private void ChangeCellMode()
    {
        if (ToCellMode != null)
        {
            ToCellMode();
            FrameEnable();
        }
    }

    private void ChangeLineMode()
    {
        if (ToLineMode != null)
        {
            ToLineMode();
            FrameEnable();
        }
    }

    private void ChangeLineAndCellMode()
    {
        if (ToLineAndCellMode != null)
        {
            ToLineAndCellMode();
            FrameEnable();
        }
    }

    private void CutModel()
    {
        if (ToCutModel != null)
        {
            if (GlobalVariableBackground.Instance.CutLoadingCount == 0)
            {
                ToCutModel();
            }
            else
            {
                GlobalVariableBackground.Instance.conManager.WriteLog("正在切割中，请稍后");
            }
        }
    }

}
