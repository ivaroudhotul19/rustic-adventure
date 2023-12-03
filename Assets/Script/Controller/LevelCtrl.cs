using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LevelCtrl : MonoBehaviour
{

    void Start()
    {
        // 
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Invoke("ShowLevelCompleteMenu", 1f);
        }
    }

    void ShowLevelCompleteMenu()
    {
        GameCtrl.instance.LevelComplete();
    }
}
