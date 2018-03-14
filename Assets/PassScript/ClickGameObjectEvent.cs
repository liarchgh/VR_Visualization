//using RTEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickGameObjectEvent : MonoBehaviour
{
//    /// <summary>
//    /// 代表当前物体的身份
//    /// </summary>
//    public DataType ThisID;
//    /// <summary>
//    /// 自身的transform 组件
//    /// </summary>
//    public Vector3 thisPosition;
//    public Quaternion thisRotation;
//    public Vector3 thisScale;

//    /// <summary>
//    /// 是否点击
//    /// </summary>
//    private bool isClickObj = false;

//    private void Awake()
//    {
//        //点击场景中的物体调用
//        //EditorObjectSelection.Instance.SelectionChanged += OnSelectionChanged;
//        //thisPosition = transform.position;
//        //thisRotation = transform.rotation;
//        //thisScale = transform.localScale;
//    }

//    public void OnMouseDown()
//    {
//        //if (DetectionMouseDown()) return;
//        //isClickObj = true;
//        //GloableVariables.PitchOnGameObject = gameObject;

//        //VRGizmo_Mangers.Instance.SwitchObj(gameObject);
//    }
//    private void Update()
//    {
//        //if (EditorObjectSelection.Instance.LastSelectedGameObject != null)
//        //{
//        //    //EditorObjectSelection.Instance.LastSelectedGameObject.SetActive(false);
//        //}
//        //if (Input.GetMouseButtonUp(1) && GloableVariables.PitchOnGameObject == this.gameObject)
//        //{
//        //    Debug.Log("显示");
//        //    OverallEvent_Manger.Instance.PitchOnObj(gameObject);
//        //}
//    }

//    private void OnEnable()
//    {
//        //GloableVariables.PitchOnGameObject = gameObject;
//    }

//    /// <summary>
//    /// 隐藏对象时 把静态变量置空
//    /// </summary>
//    private void OnDisable()
//    {
//        if (isClickObj == true)
//        {
//            OverallEvent_Manger.Instance.PitchOnObj(null);
//            isClickObj = false;
//        }
//        //如果这个对象储存的是自身 则删除
//        if (GloableVariables.PitchOnGameObject == gameObject)
//        {
//            GloableVariables.PitchOnGameObject = null;
//        }
//        //if (EditorGizmoSystem.Instance != null)
//        //EditorGizmoSystem.Instance.ActiveGizmoType = GizmoType.Rotation;
//    }

//    /// <summary>
//    /// 场景中选中物体会调用此方法
//    /// </summary>
//    /// <param name="selChangedEventArgs">结构体 包含 类型 和列表</param>
//    private void OnSelectionChanged(ObjectSelectionChangedEventArgs selChangedEventArgs)
//    {
//        if (selChangedEventArgs.SelectedObjects.Count > 0)
//        {
//        }
//        else if (isClickObj == true)
//        {
//            OverallEvent_Manger.Instance.PitchOnObj(null);
//            isClickObj = false;

//            //如果这个对象储存的是自身 则删除
//            if (GloableVariables.PitchOnGameObject == gameObject)
//            {
//                GloableVariables.PitchOnGameObject = null;
//            }
//        }
//        //调用更新UI的方法
//        OverallEvent_Manger.Instance.UpdateTest();
//        //点击物体切换图标 （主要用于 HTC）
//        VRGizmo_Mangers.Instance.RemoveGizmo(gameObject);
//    }

//    /// <summary>
//    /// 检测鼠标按下是否能点击到物体（物体容易被UI覆盖 点击会有穿透ui效果）
//    /// </summary>
//    /// <returns></returns>
//    public bool DetectionMouseDown()
//    {
//#if IPHONE || ANDROID
//			if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
//#else
//        if (EventSystem.current.IsPointerOverGameObject())
//#endif
//        {
//            return true;
//        }
//        else
//        {
//            return false;
//        }
//    }

//    /// <summary>
//    /// 重置当前物体
//    /// </summary>
//    public void Resst()
//    {

//        this.transform.position = thisPosition;
//        this.transform.rotation = thisRotation;
//        this.transform.localScale = thisScale;
//    }

}