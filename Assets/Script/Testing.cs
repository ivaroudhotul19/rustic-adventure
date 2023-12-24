using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using.CodeMonkey.Utils;

public class Testing : MonoBehaviour
{

    void Start()
    {
        //PoinPopup.Create(Vector3.zero, 300);
    }

    private void Update(){
        if(Input.GetMouseButtonDown(0)) {
            PoinPopup.Create(UtilsClass.GetMouseWorldPosition(), 100);
        }
    }

}
