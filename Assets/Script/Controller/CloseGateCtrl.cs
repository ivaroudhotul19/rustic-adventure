using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseGateCtrl : MonoBehaviour
{
    private GameObject[] gateArray;

    // Start is called before the first frame update
    void Start()
    {
        gateArray = GameObject.FindGameObjectsWithTag("Gate");
        DeactivateGates();
    }

    void DeactivateGates()
    {
        foreach (GameObject gate in gateArray)
        {
            gate.SetActive(false);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player masuk");
            ActivateGates();
        }
    }

    void ActivateGates()
    {
        foreach (GameObject gate in gateArray)
        {
            gate.SetActive(true);
        }
    }
}
