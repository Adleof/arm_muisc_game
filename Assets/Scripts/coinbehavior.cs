using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class coinbehavior : MonoBehaviour
{
    public UnityEvent eaten;
    public UnityEvent miss;
    public float movespeed;
    private CoinManager mymanager;
    private ActorController myplayer;
    private float padang;

    //mesh modifier
    private Mesh mesh;
    public float scale_rad;
    public float roc;//radius of curvature
    Vector3[] origmesh;

    private void updateMesh()
    {
        Vector3[] newv = new Vector3[origmesh.Length];
        for (int i = 0; i < origmesh.Length; i++)
        {
            float newy = origmesh[i].y * padang * Mathf.Deg2Rad * roc / 2f;
            float newx = origmesh[i].x * scale_rad;
            float newz = origmesh[i].z * scale_rad;
            float nr = newy / roc;
            Vector3 newcenter = new Vector3(Mathf.Cos(nr) * roc - roc, Mathf.Sin(nr) * roc, 0);
            Vector3 diff = Quaternion.AngleAxis(nr * Mathf.Rad2Deg, new Vector3(0, 0, 1)) * new Vector3(newx, 0, newz);
            newv[i] = newcenter + diff;
        }
        mesh.vertices = newv;
    }


    // Start is called before the first frame update
    void Start()
    {
        mymanager = GameObject.FindGameObjectWithTag("coinManager").GetComponent<CoinManager>();
        //eaten.AddListener(GameObject.FindGameObjectWithTag("Player").GetComponent<ActorController>().addscore);
        myplayer = GameObject.FindGameObjectWithTag("Player").GetComponent<ActorController>();
        eaten.AddListener(mymanager.onEatcoin);
        miss.AddListener(mymanager.onMiss);
        mymanager.updateang.AddListener(this.onupdatepaddle);
        //mesh modifier
        movespeed = mymanager.movespeed;
        padang = mymanager.paddleangle;
        roc = mymanager.roc;
        scale_rad = mymanager.scale_rad;
        mesh = gameObject.GetComponent<MeshFilter>().mesh;
        origmesh = mesh.vertices;
        updateMesh();
    }

    public void onupdatepaddle(float pad,float droc, float scalerad)
    {
        //Debug.Log("padlenupdated:"+pad.ToString());
        padang = pad;
        roc = droc;
        scale_rad = scalerad;
        updateMesh();
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    eaten.Invoke();
    //    Destroy(gameObject);
    //}
    

    private float anglebetween(float playerang_inRad,float myangle_inDeg)
    {
        float playerang_indeg = playerang_inRad*Mathf.Rad2Deg;
        if (playerang_indeg < 0 )
        {
            playerang_indeg += 360f;
        }
        float anglediff = playerang_indeg - myangle_inDeg;
        while (anglediff > 180f)
        {
            anglediff -= 360f;
        }
        while (anglediff < -180f)
        {
            anglediff += 360f;
        }
        //Debug.Log("Player:" + playerang_indeg.ToString() + "My:" + myangle_inDeg.ToString() + "Diff:" + anglediff.ToString());
        return anglediff;
    }

    // Update is called once per frame
    void Update()
    {
        float newposz = transform.position.z + movespeed * Time.deltaTime;
        if (newposz < 3f)
        {
            float anglebetweenpaddle = anglebetween(myplayer.curang, transform.rotation.eulerAngles.z);
            //Debug.Log("Myang:"+transform.rotation.eulerAngles.z.ToString()+"Playerangle:"+ (myplayer.curang * Mathf.Rad2Deg).ToString());
            //Debug.Log("anglebetween:" + anglebetweenpaddle.ToString());
            if (Mathf.Abs(anglebetweenpaddle) < padang/2f + 7f)//consider player radius
            {
                eaten.Invoke();
                Destroy(gameObject);
            }
            else
            {
                Destroy(gameObject);
                miss.Invoke();
            }
            return;
        }
        transform.position = new Vector3(transform.position.x, transform.position.y, newposz);
    }
}
