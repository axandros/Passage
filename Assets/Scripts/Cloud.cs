using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    [SerializeField]
    float TravelTime = 10.0f;
    [SerializeField]
    float EndXPos;
    [SerializeField]
    float MinZ, MaxZ, MinY, MaxY = 0.0f;

    float StartPosition;
    float timeSinceStart = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        StartPosition = transform.position.x;
        timeSinceStart = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceStart += Time.deltaTime;
        transform.position = new Vector3(Mathf.Lerp(StartPosition, EndXPos, timeSinceStart/TravelTime),5.0f,-3.0f);
    }
}
