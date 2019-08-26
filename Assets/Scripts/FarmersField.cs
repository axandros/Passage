using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmersField : MonoBehaviour
{
    WheatField[] Fields;

    [SerializeField]
    Farmer _farmer = null;

    // Start is called before the first frame update
    void Start()
    {
        Fields = GetComponentsInChildren<WheatField>(); 
    }

    // Update is called once per frame
    void Update()
    {
        for(int index = 0; index < Fields.Length; index++)
        {
            if(Fields[index].CanAdvance)
            {
                _farmer.AddFieldToList(Fields[index]);
            }
        }
        //TestGrowthHalt();
        //TestDrying();
    }

    void IncreaseFieldState(int index)
    {
        if (Fields[index].Growth >= 1.0f)
        {
            Fields[index].SetState(Fields[index].NextGrowthState);
            Fields[index].Growth = 0.0f;
        }
    }

    void TestGrowthHalt()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            IncreaseFieldState(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            IncreaseFieldState(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            IncreaseFieldState(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            IncreaseFieldState(3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            IncreaseFieldState(4);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            IncreaseFieldState(5);
        }
    }

    void TestDrying()
    {
       
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetSoilState(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetSoilState(1);
        }
      
    }
    void SetSoilState(int state)
    {
        for (int index = 0; index < Fields.Length; index++)
        {
            Fields[index].SoilState(state);
        }
    }
}

