using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheatFieldGrowth : MonoBehaviour
{
    WheatTile[] Plots = null;

    [SerializeField]
    MeshFilter Filter;

    [SerializeField]
    Mesh[] States = null;

    public int NumberOfStates { get { return States.Length; } }
    private int _growthState = 0;

    void Start()
    {
        Plots = GetComponentsInChildren<WheatTile>();
        SetState(_growthState);
    }

    void Update()
    {
        //For Testing, set state with number keys
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetState(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetState(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetState(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SetState(3);
        }
    }

    public void SetState(int state)
    {
        if (NumberOfStates > state)
        {
            _growthState = state;
            for (int index = 0; index < Plots.Length; index++) {
                Plots[index].UpdateMesh(state-1);
            }
        }
    }
}
