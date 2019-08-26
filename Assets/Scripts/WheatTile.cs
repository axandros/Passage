using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheatTile : MonoBehaviour
{
    [SerializeField]
    MeshFilter SoilMesh = null;
    [SerializeField]
    Mesh[] SoilMeshList = null;


    [SerializeField]
    MeshFilter WheatMesh = null;
    [SerializeField]
    Mesh[] WheatMeshList = null;

    public void UpdateMesh(int Stage)
    {
        //Debug.Log("UpdateMesh: " + Stage);
        if(Stage < 0)
        {
            WheatMesh.mesh = null;
        }
        else if( Stage < WheatMeshList.Length)
        {
            //Debug.Log("WheatMesh: " + WheatMesh);
            //Debug.Log("WheatMeshList: " + Stage + " | " + WheatMeshList[Stage].name);
            WheatMesh.mesh = WheatMeshList[Stage];
        }
    }

    public void UpdateSoil(int stage)
    {
       
        if(stage < SoilMeshList.Length && stage >= 0)
        {
            SoilMesh.mesh = SoilMeshList[stage];
        }

        float sc = 0.0f; ;
        switch (stage)
        {
            case 0:
                sc = 0.10285f;
                break;
            case 1:
                sc = 1.1f;
                break;
        }
        SoilMesh.transform.localScale = new Vector3(sc,sc,sc);
        
    }
}
