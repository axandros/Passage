using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheatFieldGrowth : MonoBehaviour
{
    GameObject[] Plots;

    [SerializeField]
    MeshFilter Filter;

    [SerializeField]
    Mesh[] States;

    public const int StatesNumber = 4;
    private int _growthState = 0;

    void Start()
    {
        SetState(_growthState);
    }

    void Update()
    {
        //For Testing, set state with number keys
    }

    public void SetState(int state)
    {
        if (States.Length > state)
        {
            for (int index = 0; index < Plots.Length; index++) {
                Plots[index].GetComponent<MeshFilter>().mesh = States[state];
            }
        }
    }
}
