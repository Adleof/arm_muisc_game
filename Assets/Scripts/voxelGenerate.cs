using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

public class voxelGenerate : MonoBehaviour
{
    public GameObject cubeprefab;
    public float cellsize;
    public Vector3Int gridsize;
    // Start is called before the first frame update
    void Start()
    {
        for (int x = 0; x < gridsize.x; x++)
        {
            for (int y = 0; y < gridsize.y; y++)
            {
                for (int z = 0; z < gridsize.z; z++)
                {
                    float cdx = (x - gridsize.x / 2f) / gridsize.x;
                    float cdy = (y - gridsize.y / 2f) / gridsize.y;
                    float cdz = (z - gridsize.z / 2f) / gridsize.z;
                    if (cdx * cdx + cdy * cdy + cdz * cdz < 0.1f)
                    {
                        GameObject newObject = Instantiate(cubeprefab, (new UnityEngine.Vector3(x, y, z)) * cellsize, UnityEngine.Quaternion.identity) as GameObject;
                        newObject.transform.localScale = new UnityEngine.Vector3(0.1f, 0.1f, 0.1f);
                    }
                }
            }
        }
    }

    // Update is called once per frame
    //void Update()
    //{

    //}
}
