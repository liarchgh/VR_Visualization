using UnityEngine;
using System.Collections.Generic;

public class LinesGR : MonoBehaviour
{
    public Shader shader;
    private Mesh ml;
    private Material lmat;

    //private Mesh ms;
    //private Material smat;

    private Vector3 s;
    private float lineSize = 0.03f;

    private GUIStyle labelStyle;
    private GUIStyle linkStyle;

    //private Point first;

    public int[] tri;

    private float speed = 5.0f;
    void Start()
    {
        labelStyle = new GUIStyle();
        labelStyle.normal.textColor = Color.black;

        linkStyle = new GUIStyle();
        linkStyle.normal.textColor = Color.blue;

        ml = new Mesh();
//lmat = new Material(shader);
        lmat = GlobalVariableBackground.Instance.Materialboat;
        //lmat.color = Color.blue;

        //ms = new Mesh();
        //smat = GlobalVariableBackground.Instance.instanceMaterial;
        ////smat = new Material(shader);
        //smat.color = Color.green;
        

        Vector3 v1 = new Vector3(0, 0, 0);
        Vector3 v2 = new Vector3(1, 0, 0);
        Vector3 v3 = new Vector3(0, 1, 0);

        List<Vector3> listVec = new List<Vector3>();
        List<int> listTri = new List<int>();

        AddLineArray3(listVec,listTri,MakeQuad(v1,v2,v3,0.05f));
        ml.SetVertices(listVec);
        ml.triangles = listTri.ToArray();
        ml.RecalculateNormals();
        Vector3 v = new Vector3(0.7f,0.1f,0);
        float f1 = Vector3.Distance(v1, v);
        float f2 = Vector3.Distance(v2, v);
        float f3 = Vector3.Distance(v3, v);

        Debug.Log("ssss");
        tri =GameObject.CreatePrimitive(PrimitiveType.Cube).GetComponent<MeshFilter>().mesh.triangles;

    }

    void Update1()
    {
        //if (Input.GetMouseButton(0))
        //{

        //    Vector3 e = GetNewPoint();

        //    if (first == null)
        //    {
        //        first = new Point();
        //        first.p = transform.InverseTransformPoint(e);
        //    }

        //    if (s != Vector3.zero)
        //    {
        //        Vector3 ls = transform.TransformPoint(s);
        //        AddLine(ml, MakeQuad(ls, e, lineSize), false);

        //        Point points = first;
        //        while (points.next != null)
        //        {
        //            Vector3 next = transform.TransformPoint(points.p);
        //            float d = Vector3.Distance(next, ls);
        //            if (d < 1 && Random.value > 0.9f)
        //            {
        //                AddLine(ms, MakeQuad(next, ls, lineSize), false);
        //            }
        //            points = points.next;
        //        }

        //        Point np = new Point();
        //        np.p = transform.InverseTransformPoint(e);
        //        points.next = np;
        //    }

        //    s = transform.InverseTransformPoint(e);
        //}
        //else
        //{
        //    s = Vector3.zero;
        //}

        //Draw();
        //processInput();
    }

    void Draw()
    {
        Graphics.DrawMesh(ml, transform.localToWorldMatrix, lmat, 0);
        //Graphics.DrawMesh(ms, transform.localToWorldMatrix, smat, 0);
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

        q[0] = transform.InverseTransformPoint(p1 + n1 * w);
        q[1] = transform.InverseTransformPoint(p1 + n1 * -w);
        q[2] = transform.InverseTransformPoint(p2 + n1 * w);
        q[3] = transform.InverseTransformPoint(p2 + n1 * -w);

        q[4] = transform.InverseTransformPoint(p3 + n2 * w);
        q[5] = transform.InverseTransformPoint(p3 + n2 * -w);
        q[6] = transform.InverseTransformPoint(p1 + n2 * w);
        q[7] = transform.InverseTransformPoint(p1 + n2 * -w);

        q[8] = transform.InverseTransformPoint(p2 + n3 * w);
        q[9] = transform.InverseTransformPoint(p2 + n3 * -w);
        q[10] = transform.InverseTransformPoint(p3 + n3 * w);
        q[11] = transform.InverseTransformPoint(p3 + n3 * -w);

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

    public static void AddLine(Mesh m, Vector3[] quad, bool tmp)
    {
        int vl = m.vertices.Length;

        Vector3[] vs = m.vertices;
        if (!tmp || vl == 0) vs = resizeVertices(vs, 4);
        else vl -= 4;

        vs[vl] = quad[0];
        vs[vl + 1] = quad[1];
        vs[vl + 2] = quad[2];
        vs[vl + 3] = quad[3];

        int tl = m.triangles.Length;

        int[] ts = m.triangles;
        if (!tmp || tl == 0) ts = resizeTraingles(ts, 6);
        else tl -= 6;
        ts[tl] = vl;
        ts[tl + 1] = vl + 1;
        ts[tl + 2] = vl + 2;
        ts[tl + 3] = vl + 1;
        ts[tl + 4] = vl + 3;
        ts[tl + 5] = vl + 2;

        m.vertices = vs;
        m.triangles = ts;
        m.RecalculateBounds();
    }

    void processInput()
    {
        float s = speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.LeftShift)) s = s * 10;
        if (Input.GetKey(KeyCode.UpArrow)) transform.Rotate(-s, 0, 0);
        if (Input.GetKey(KeyCode.DownArrow)) transform.Rotate(s, 0, 0);
        if (Input.GetKey(KeyCode.LeftArrow)) transform.Rotate(0, -s, 0);
        if (Input.GetKey(KeyCode.RightArrow)) transform.Rotate(0, s, 0);

        if (Input.GetKeyDown(KeyCode.C))
        {
            ml = new Mesh();
            //ms = new Mesh();
            transform.rotation = Quaternion.identity;
//            first = null;
        }
    }

    Vector3 GetNewPoint()
    {
        return Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z * -1.0f));
    }

    static Vector3[] resizeVertices(Vector3[] ovs, int ns)
    {
        Vector3[] nvs = new Vector3[ovs.Length + ns];
        for (int i = 0; i < ovs.Length; i++) nvs[i] = ovs[i];
        return nvs;
    }

    static int[] resizeTraingles(int[] ovs, int ns)
    {
        int[] nvs = new int[ovs.Length + ns];
        for (int i = 0; i < ovs.Length; i++) nvs[i] = ovs[i];
        return nvs;
    }
    //void OnGUI()
    //{
    //    GUI.Label(new Rect(10, 10, 300, 24), "Cursor keys to rotate (fast with Shift)", labelStyle);
    //    int vc = ml.vertices.Length + ms.vertices.Length;
    //    GUI.Label(new Rect(10, 26, 300, 24), "Drawing " + vc + " vertices. 'C' to clear", labelStyle);
    //    if (GUI.Button(new Rect(10, Screen.height - 20, 300, 24), "zwwdm.com", linkStyle))
    //    {
    //        Application.OpenURL("http://www.zwwdm.com");
    //    }
    //}

    /** Replace the Update function with this one for a click&drag drawing option */
    void Update()
    {
        //processInput();

        Vector3 e;

        if (Input.GetMouseButtonDown(0))
        {
            s = transform.InverseTransformPoint(GetNewPoint());
        }

        if (Input.GetMouseButton(0))
        {
            e = GetNewPoint();
            AddLine(ml, MakeQuad(transform.TransformPoint(s), e, lineSize), true);
        }
        if (Input.GetMouseButtonUp(0))
        {
            e = GetNewPoint();
            AddLine(ml, MakeQuad(transform.TransformPoint(s), e, lineSize), false);
        }

        Draw();
    }
}