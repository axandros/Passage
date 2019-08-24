using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheatTile : MonoBehaviour
{
    [SerializeField]
    MeshFilter SoilMesh = null;
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
}
