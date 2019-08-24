using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmersField : MonoBehaviour
{
    WheatFieldGrowth[] Fields;

    // Start is called before the first frame update
    void Start()
    {
        Fields = GetComponentsInChildren<WheatFieldGrowth>(); 
    }

    // Update is called once per frame
    void Update()
    {
        for(int index = 0; index < Fields.Length; index++)
        {
            Fields[index].Growth += Random.Range(0, 0.03f);
            if(index == 0)
            {
                //Debug.Log("Growth: " + Fields[index].Growth);

            }
            if(Fields[index].Growth >= 1.0f)
            {
                if (index == 0)
                {
                    Debug.Log("NextGrowth: " + Fields[index].NextGrowthState);
                }
                Fields[index].SetState(Fields[index].NextGrowthState);
                Fields[index].Growth = 0.0f;
            }
        }
    }
}
