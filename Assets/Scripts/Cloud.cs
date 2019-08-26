using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Cloud : MonoBehaviour
{
    NavMeshAgent _agent = null;

    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        int layerMask;

        layerMask = 1 << 9;
        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit rayhit = new RaycastHit();
        if (Physics.Raycast(r, out rayhit, Mathf.Infinity, layerMask) && Input.GetMouseButton(0))
        {
            _agent.destination = rayhit.point;
        }
        else
        {
            Wander();
        }

        //Fiel Raiinning on.
        layerMask = 1 << 11;
        Vector3 origin = transform.position + new Vector3(0.0f,4.0f, 0.0f);
        Vector3 direction = new Vector3(0.0f, -5.0f, 0.0f);
        Ray rain = new Ray(origin, direction);
        RaycastHit rainhit = new RaycastHit();
        Debug.DrawRay(origin, direction);
        if (Physics.Raycast(rain, out rainhit, Mathf.Infinity, layerMask))
        {
            try
            {
                GameObject go = rainhit.transform.gameObject;
                WheatField wf = go.GetComponent<WheatField>();
                Debug.Log("Found " + go.name );
                if (wf != null) {
                    wf._isBeingWatered = true; 
                    Debug.Log("wateringField");
                }
            }
            catch
            {
                Debug.Log("Excception Occured");
            }
            
            
        }
        else
        {
            Wander();
        }
    }

    void Wander()
    {
    }
}
