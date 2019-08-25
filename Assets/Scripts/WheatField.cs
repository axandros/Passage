using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheatField : MonoBehaviour
{
    WheatTile[] Plots = null;

    [SerializeField]
    public int NumberOfStates = 4;
    
    private int _growthState = 0;
    public int GrowthState { get { return _growthState; } }
    public int NextGrowthState { get
        {
            if (GrowthState >= NumberOfStates-1)
            { return 0; }
            else
            { return GrowthState + 1; }
        }
     }

    [SerializeField]
    float FullGrowthTime = 20.0f;
    float _currentGrowthTime = 0.0f;
    public float GrowthTime { get { return FullGrowthTime; } }
    public float Growth { get { return _currentGrowthTime; } 
    set { _currentGrowthTime = value; } 
    }

    void Start()
    {
        Plots = GetComponentsInChildren<WheatTile>();
        SetState(_growthState);
    }

    void Update()
    {
        _currentGrowthTime += Time.deltaTime;
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

    public bool AdvanceState()
    {
        bool ret = false;
        if(CanAdvance)
        {
            for (int index = 0; index < Plots.Length; index++)
            {
                Plots[index].UpdateMesh(NextGrowthState-1);
            }
            _growthState = NextGrowthState;
            _currentGrowthTime = 0.0f;
        }
        return ret;
    }

    public bool CanAdvance
    {
        get
        {
            return _currentGrowthTime >= FullGrowthTime;
        }
    }
}
