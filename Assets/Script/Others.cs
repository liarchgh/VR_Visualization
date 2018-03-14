using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Others {

    /// <summary>
    /// 最大值与最小值
    /// </summary>
    public class MaxAndMin
    {
        /// <summary>
        /// 属性名
        /// </summary>
        public string Attname;

        /// <summary>
        /// 属性维度
        /// </summary>
        public int Dimension;

        /// <summary>
        /// 是否属于点的属性
        /// </summary>
        public bool isPointAtt;

        /// <summary>
        /// 最大值
        /// </summary>
        public float Max = -99999f;

        /// <summary>`
        /// 最小值
        /// </summary>
        public float Min = 99999f;
    }

    public class ThreadGetMaxAndMinPack
    {
        public object objLock;
        public VTK_AllFrame vtkAllFrame;
        public Structure_LoadFile structure;
        public System.IO.FileInfo fileAddres;

    }

    public class ThreadLoadFilePack
    {
        public object objLock;
        public VTK_Frame vtkFrame;
        public VTK_AllFrame vtkAllFrame;
        public System.IO.FileInfo fileAddres;
        public string strAboutArrMAxMin;
    }

    public class ThreadCutModelPack
    {
        public object objLock;
        public VTK_Frame vtkFrame;
        public VTK_AllFrame vtkAllFrame;
        public System.IO.FileInfo fileAddres;
        public float[] model16;
        public float[] plane16;
    }

    public class ThreadSmoothModePack
    {
        public object objLock;
        public VTK_Frame vtkFrame;
        public VTK_AllFrame vtkAllFrame;
        public System.IO.FileInfo fileAddres;
        public int smoothCount;

    }

    public class SameMesh
    {
        public Vector3[] vertices;
        public int[] triangles;
    }

    public class MaterialPerfab
    {
        List<Vector4> listPositionBuffer;
        List<Vector4> listColorBuffer;
    }

    public class ModelState
    {
        public bool canPickUp = false;
        public bool isUsecolor = false;
        public int attIndex = -1;
        public int maxminIndex = -1;
        public bool isPointAtt = true;
        public Enums.SymmetricMode symmetricmode = Enums.SymmetricMode.NONE;
        public bool isSmooth = false;
        public bool isArrowMode = false;

    }

    public class PackUpObject
    {
        /// <summary>
        /// 选中的MeshObject名称
        /// </summary>
        public string pickUpName="";
        /// <summary>
        /// 当前Mesh的三角行索引值TriangleIndex
        /// </summary>
        public int pickUpIndex=-1;
        /// <summary>
        /// 判断Mesh是否已有数据
        /// </summary>
        public bool isMeshHave = false;
        public Mesh mesh = new Mesh();
        /// <summary>
        /// 点集合的索引值
        /// </summary>
        public int pointIndex =-1;
        /// <summary>
        /// 点或网格的原始序号
        /// </summary>
        public int number = -1;
        /// <summary>
        /// 是否为点模式
        /// </summary>
        public bool isPointMode = false;
        /// <summary>
        /// 选择的属性值索引
        /// </summary>
        public int AttIndex = -1;

        /// <summary>
        /// 刷新数据使用的上一帧索引
        /// </summary>
        public int lastFrameIndex = -1;

        private string frameName;
        private string modelName;
        private int meshIndex=-1;
        private int triIndex=-1;


        private void getValue()
        {

            // ship_#0|0
            string[] strTemp = pickUpName.Split('|');
            ModelName = strTemp[0].Split('#')[0];
            frameName = strTemp[0];
            meshIndex = System.Convert.ToInt32(strTemp[1]);
            triIndex = pickUpIndex;
        }



        public string FrameName
        {
            get
            {
                if (frameName == null|| triIndex != pickUpIndex)
                {
                    getValue();
                }
                return frameName;
            }
        }

        public int MeshIndex
        {
            get
            {
                if (meshIndex <0 || triIndex != pickUpIndex)
                {
                    getValue();
                }
                return meshIndex;
            }
            set
            {
                meshIndex = value;
            }
        }

        public int TriIndex
        {

            get
            {
                if (triIndex < 0 || triIndex != pickUpIndex)
                {
                    getValue();
                }
                return triIndex;
            }

        }

        public string ModelName
        {
            get
            {
                if (modelName == null || triIndex != pickUpIndex)
                {
                    getValue();
                }
                return modelName;
            }

            set
            {
                modelName = value;
            }
        }
    }

}
