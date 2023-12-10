using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnResetCtrl: MonoBehaviour {
    public void ResetGame() {
        DataCtrl.instance.ResetData();
        SceneManager.LoadScene("Home");
    }
}