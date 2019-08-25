using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Farmer : MonoBehaviour
{
    enum FarmerStates { IDLE, MOVING, FARMING, HOME };

    FarmerStates _farmerState = FarmerStates.IDLE;

    List<WheatField> _fieldsNeedingMaintenance = null;
    WheatField _fieldMaintaining = null;

    // AI
    Rigidbody _rb = null;
    NavMeshAgent _agent = null;
    [SerializeField]
    Vector3 Home = new Vector3(0.0f, 0.0f, 0.0f);

    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _rb = GetComponent<Rigidbody>();
        _fieldsNeedingMaintenance = new List<WheatField>();
        if(Home == Vector3.zero)
        {
            Home = transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (_farmerState)
        {
            case FarmerStates.FARMING:
                FarmingState();
                break;
            case FarmerStates.MOVING:
                MovingState();
                break;
            case FarmerStates.IDLE:
                IdleState();
                break;
            case FarmerStates.HOME:
                HomeState();
                break;
        }
    }

    float timeFarming = 0.0f;
    void FarmingState()
    {
        timeFarming += Time.deltaTime;
        Debug.Log("Time Farming: " + timeFarming);
        if(timeFarming > 2.0)
        {
            _fieldMaintaining.AdvanceState();
            _farmerState = FarmerStates.IDLE;
            timeFarming = 0.0f;
        }
    }
    void MovingState()
    {
        //----------------------
        // Are we still moving?

        // 
        if (!_agent.pathPending)
        {
            // Are we in stopping distance?
            if (_agent.remainingDistance <= _agent.stoppingDistance)
            {
                // Is there a path to the destination?  Is the speed 0?
                if (!_agent.hasPath || _agent.velocity.sqrMagnitude == 0f)
                {
                  
                    if (_fieldMaintaining != null)
                    {
                        Debug.Log("Arrived at Field");
                        _farmerState = FarmerStates.FARMING;
                    }
                    else{
                        float distanceHome = (transform.position - Home).magnitude;
                        if (distanceHome <= _agent.stoppingDistance)
                        {
                            Debug.Log("Arrived Home.");
                            _farmerState = FarmerStates.HOME;
                        }
                        else
                        {
                            _agent.destination = Home;
                            Debug.Log("Stopped with no known desitination.  Going Home");
                        }
                    }

                }
            }
        }
    }
    void MoveTo(Vector3 dest)
    {
        _agent.destination = Home;
        _farmerState = FarmerStates.MOVING;
    }
    void IdleState()
    {
        MaintainFields();
        if (_fieldsNeedingMaintenance.Count == 0)
        {
            MoveTo(Home);
        }
    }

    void HomeState()
    {
        MaintainFields();
    }

    void MaintainFields()
    {
        if (_fieldsNeedingMaintenance.Count > 0)
        {
            _fieldMaintaining = _fieldsNeedingMaintenance[0];
            _fieldsNeedingMaintenance.RemoveAt(0);
            _farmerState = FarmerStates.MOVING;
            _agent.SetDestination(_fieldMaintaining.transform.position);
        }
    }

    public void AddFieldToList(WheatField wf)
    {
        if (!_fieldsNeedingMaintenance.Contains(wf))
        {
            _fieldsNeedingMaintenance.Add(wf);
            Debug.Log("Field Added to the List");
        }
    }
}

