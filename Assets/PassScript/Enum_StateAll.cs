/// <summary>
/// 状态栏可操作枚举
/// </summary>
public enum statusBar
{
    /// <summary>
    /// 选中
    /// </summary>
    OptFor = 1,
    /// <summary>
    /// 移动
    /// </summary>
    Translation = 2,
    /// <summary>
    /// 重置
    /// </summary>
    Reset = 4,
    /// <summary>
    /// 缩放
    /// </summary>
    Scale = 8,
    /// <summary>
    /// 显示 
    /// </summary>
    Show = 16,
    /// <summary>
    /// 旋转
    /// </summary>
    Rotation = 32,
    /// <summary>
    /// 全局缩放
    /// </summary>
    OverallZoom = 64,
}

/// <summary>
/// 动画帧枚举
/// </summary>
public enum AnimationFrame
{
    /// <summary>
    /// 上一帧
    /// </summary>
    PreviousFrame = 1,
    /// <summary>
    /// 播放
    /// </summary>
    Play = 2,
    /// <summary>
    /// 停止
    /// </summary>
    Stop = 3,
    /// <summary>
    /// 下一帧
    /// </summary>
    NextFrame = 4,
}

/// <summary>
/// 数据类型包括水 船等
/// </summary>
public enum DataType
{
    /// <summary>
    /// 水
    /// </summary>
    Water = 1,
    /// <summary>
    /// 船
    /// </summary>
    Ship = 2,
    /// <summary>
    /// 锚链
    /// </summary>
    AnchorChain = 4,
}

/// <summary>
/// 包括几何数据和数值数据
/// </summary>
public enum Data
{
    /// <summary>
    /// 几何数据
    /// </summary>
    GeometryData = 1,
    /// <summary>
    /// 数值数据
    /// </summary>
    NumericalData = 2,
}

/// <summary>
/// UI按钮类型
/// </summary>
public enum ButtonType
{
    /// <summary>
    /// 普通按钮
    /// </summary>
    None = 0,
    /// <summary>
    /// 菜单类按钮
    /// </summary>
    Menu = 1,
    /// <summary>
    /// 快捷按钮
    /// </summary>
    TSpeedButton = 2,
}

/// <summary>
/// 点线面显示
/// </summary>
public enum ShowPointLinePlaneState
{
    /// <summary>
    /// 点
    /// </summary>
    Point = 1,
    /// <summary>
    /// 线
    /// </summary>
    Line = 2,
    /// <summary>
    /// 面
    /// </summary>
    Plane = 4,
}

/// <summary>
/// 数据操作类型
/// </summary>
public enum DataOperationType
{
    /// <summary>
    /// 缩放
    /// </summary>
    Zoom = 1,
    /// <summary>
    /// 重置
    /// </summary>
    Reset = 2,
    /// <summary>
    /// 切割
    /// </summary>
    Incision = 3,
    /// <summary>
    /// 对称
    /// </summary>
    Symmetry = 4,
    /// <summary>
    /// 隐藏
    /// </summary>
    Conceal = 5,
    /// <summary>
    /// 拾取网格
    /// </summary>
    MashPickup = 6,
    /// <summary>
    /// 矢量
    /// </summary>
    Vector = 7,
    /// <summary>
    /// 网格平滑
    /// </summary>
    MashSmoothing = 8,
    /// <summary>
    /// 显示
    /// </summary>
    Show = 9,
}

/// <summary>
/// 轴向
/// </summary>
public enum Axis
{
    None = 0,
    X = 1,
    Y = 2,
    Z = 3,
}

/// <summary>
/// 拾取方式
/// </summary>
public enum Pickup
{
    /// <summary>
    /// 拾取网格
    /// </summary>
    PickupMesh = 1,
    /// <summary>
    /// 拾取点
    /// </summary>
    PickupPoint = 2,
}