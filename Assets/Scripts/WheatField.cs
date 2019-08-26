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

    [SerializeField]
    float DryingTime = 10.0f;
    float _currentDryTime = 0.0f;
    public bool _isBeingWatered = false;

    //[SerializeField]
    float WaterAbsorptionFactor = 5.0f;

    BoxCollider _bc = null;


    void Start()
    {
        Plots = GetComponentsInChildren<WheatTile>();
        SetState(_growthState);
        _bc = GetComponent<BoxCollider>();

        _currentGrowthTime = GrowthTime * 0.8f;
    }

    void Update()
    {
        _currentGrowthTime += Time.deltaTime;
       

        if (!_isBeingWatered)
        {
            //_currentDryTime += Time.deltaTime;
            _currentDryTime = Mathf.Min(_currentDryTime + Time.deltaTime,DryingTime * 2);

            if (_currentDryTime > DryingTime )
            {
                SoilState(1);
                //Debug.Log(name + " is  parched");
            }
            if (_currentDryTime > DryingTime*2)
            {
                SetState(0);
                //Debug.Log(name + " has died");
            }
        } else
        {
            _currentDryTime = Mathf.Max(_currentDryTime - Time.deltaTime *WaterAbsorptionFactor, 0.0f);
            if (_currentDryTime < DryingTime/2)
            {
                SoilState(0);
                //Debug.Log(name + " is quenched.");
            }
        }
        if(name== "WheatField")
        {
          // Debug.Log("Dryness: " + _currentDryTime);
        }
    }
    private void LateUpdate()
    {
        _isBeingWatered = false;
    }

    public void SetState(int state)
    {
        if (NumberOfStates > state)
        {
            //Debug.Log(name + " is set to growth state: " + state);
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


    public void SoilState(int state)
    {
        //Debug.Log(name + " is set to soil state: " + state);
        for (int index = 0; index < Plots.Length; index++)
        {
            Plots[index].UpdateSoil(state);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Collision etected");
        if (collision.gameObject.layer == 10)
        {
            Debug.Log(name + ":  Now Being Watered.");
            _isBeingWatered = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 10)
        {
            Debug.Log(name + ": No longer Being Watered.");
            _isBeingWatered = false;
        }
    }
}
