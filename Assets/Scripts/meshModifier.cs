using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent (typeof(MeshFilter),typeof(MeshRenderer))]
public class meshModifier : MonoBehaviour
{
    private Mesh mesh;
    public float ang = 30f;
    public float scale_rad = 1f;
    public float roc = 3.0f;//radius of curvature
    Vector3[] origmesh;
    // Start is called before the first frame update
    void Start()
    {
        mesh = gameObject.GetComponent<MeshFilter>().mesh;
        origmesh = mesh.vertices;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3[] newv = new Vector3[origmesh.Length];
        for (int i = 0; i < origmesh.Length; i++)
        {
            //float nr = origmesh[i].y / (roc- origmesh[i].x);
            //float xtoroc = roc - origmesh[i].x;
            //float diffx = xtoroc - Mathf.Cos(nr) * xtoroc;
            //newv[i] = new Vector3(origmesh[i].x + diffx, Mathf.Sin(nr) * xtoroc, origmesh[i].z);
            float newy = origmesh[i].y * ang*Mathf.Deg2Rad*roc/2f;
            float newx = origmesh[i].x * scale_rad;
            float newz = origmesh[i].z * scale_rad;
            float nr = newy / roc;
            Vector3 newcenter = new Vector3(roc - Mathf.Cos(nr) * roc, Mathf.Sin(nr) * roc, 0);
            Vector3 diff = Quaternion.AngleAxis(nr*Mathf.Rad2Deg,new Vector3(0,0,-1))*new Vector3(newx, 0, newz);
            newv[i] = newcenter + diff;
        }
        mesh.vertices = newv;
    }
}
