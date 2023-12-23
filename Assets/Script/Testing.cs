using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    [SerializeField] private Transform FloatingText;
    void Start()
    {
        Transform poinPopupTransform = Instantiate(FloatingText, Vector3.zero, Quaternion.identity);
        PoinPopup poinPopup = poinPopupTransform.GetComponent<PoinPopup>();
        poinPopup.Setup(300);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
