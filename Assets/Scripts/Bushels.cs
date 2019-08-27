using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bushels : MonoBehaviour
{
    [SerializeField]
    Text tx = null;
    [SerializeField]
    Farmer fm = null;

    // Start is called before the first frame update
    void Start()
    {
        tx = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        tx.text = fm.Bushels.ToString();
    }
}
