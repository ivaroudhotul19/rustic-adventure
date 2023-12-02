using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class RefreshData : MonoBehaviour
{
    void Start()
    {
        DataCtrl.instance.RefreshData();
    }
}
