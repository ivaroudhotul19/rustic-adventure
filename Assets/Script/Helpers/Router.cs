using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Router : MonoBehaviour
{
    public void ShowPausePanel()
    {
        GameCtrl.instance.ShowPausePanel();
    }

    public void HidePausePanel()
    {
        GameCtrl.instance.HidePausePanel();
    }
}
