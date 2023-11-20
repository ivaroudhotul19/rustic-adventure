using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;    //untuk bekerja dengan file
using System.Runtime.Serialization.Formatters.Binary; //untuk menyimpan data dalam format biner

public class GameCtrl : MonoBehaviour
{
    public static GameCtrl instance;
    public float restartDelay;
    public GameData data;
    public UI ui;
    public int coinValue;
    public float maxTime;
    public Text textTimer;
    public GameObject shinningCoin;
    public int shinningCoinValue;
    public int enemyValue;
    public enum Item {
        Coin, ShinningCoin, Enemy
    }

    string dataFilePath;
    BinaryFormatter bf;
    float timeLeft;
    private GameObject player;
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        bf = new BinaryFormatter();
        dataFilePath = Application.persistentDataPath + "/game.dat";

        Debug.Log(dataFilePath);
    }

    void Start()
    {
        // ResetData();
        timeLeft = maxTime;
        HandleFirstBoot();
        UpdateHearts();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            ResetData();
        }

        if(timeLeft > 0) {
            UpdateTimer();
        }
    }

    public void SaveData(){
        FileStream fs = new FileStream(dataFilePath, FileMode.Create);
        bf.Serialize(fs,data);
        fs.Close();
    }

    public void LoadData(){
        if(File.Exists(dataFilePath)){
            FileStream fs = new FileStream(dataFilePath, FileMode.Open);
            data = (GameData)bf.Deserialize(fs);
            //Debug.Log("Number of Coins = " + data.coinCount);
            ui.txtCoinCount.text = " x " + data.coinCount;
            ui.txtScore.text = "Score : " + data.score;
            fs.Close();
        }
    }

    void OnEnable() {
        Debug.Log("Data Loaded");
        LoadData();
    }

    void OnDisable() {
        Debug.Log("Data Saved");
        SaveData();
    }

    void ResetData(){
        FileStream fs = new FileStream(dataFilePath, FileMode.Create);
       
        data.coinCount = 0;
        ui.txtCoinCount.text = " x 0";
        data.score = 0;
        ui.txtScore.text = "Score : " + data.score;
        data.lives = 5;
        UpdateHearts();
        bf.Serialize(fs,data);
        fs.Close();
        Debug.Log("Data Reset");
    }
    public void PlayerDiedFall(GameObject player) {
        player.SetActive(false);
        StartCoroutine(RestartLevel());
        CheckLives();
        //Invoke("RestartLevel", restartDelay);
    }
    public void PlayerDied(GameObject player)
    {
        Debug.Log("jatuh");
        player.SetActive(false);
        StartCoroutine(RestartLevel());
    }


    public void PlayerDiedAnimationFall(GameObject player){
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        // Tambahkan gaya fisika untuk menggerakkan pemain ke atas dan ke belakang
        rb.AddForce(new Vector2(-100f, 200f));
        
        // Memutar pemain
        player.transform.Rotate(new Vector3(0, 0, 45f));
        
        // Menonaktifkan skrip PlayerCtrl pada pemain
        player.GetComponent<PlayerCtrl>().enabled = false;

        // Menonaktifkan semua Collider2D pada pemain
        Collider2D[] colliders = player.GetComponents<Collider2D>();
        foreach (Collider2D c2d in colliders)
        {
            c2d.enabled = false;
        }

        // Menonaktifkan semua GameObject anak pada pemain
        foreach (Transform child in player.transform) {
            child.gameObject.SetActive(false);
        }

       Camera.main.GetComponent<CameraCtrl>().enabled = false;
       StartCoroutine("PauseBeforeReloadFall", player);
    }

    public void PlayerDiedAnimation(GameObject player){
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        // Tambahkan gaya fisika untuk menggerakkan pemain ke atas dan ke belakang
        rb.AddForce(new Vector2(-100f, 200f));
        
        // Memutar pemain
        player.transform.Rotate(new Vector3(0, 0, 45f));
        
        // Menonaktifkan skrip PlayerCtrl pada pemain
        player.GetComponent<PlayerCtrl>().enabled = false;

        // Menonaktifkan semua Collider2D pada pemain
        Collider2D[] colliders = player.GetComponents<Collider2D>();
        foreach (Collider2D c2d in colliders)
        {
            c2d.enabled = false;
        }

        // Menonaktifkan semua GameObject anak pada pemain
        foreach (Transform child in player.transform) {
            child.gameObject.SetActive(false);
        }

       Camera.main.GetComponent<CameraCtrl>().enabled = false;
       StartCoroutine("PauseBeforeReload", player);

    }

    IEnumerator PauseBeforeReloadFall(GameObject player){
        yield return new WaitForSeconds(0.5f);
        //CheckLives();
        PlayerDiedFall(player);
    }

    IEnumerator PauseBeforeReload(GameObject player){
        yield return new WaitForSeconds(0.5f);
        PlayerDied(player);
    }

    public void PlayerDrowned(GameObject player)
    {
        Invoke("RestartLevel", restartDelay);
    }

    public void updateCoinCount(){

        data.coinCount += 1;
        ui.txtCoinCount.text = " x " + data.coinCount;
        //updateScore(coinValue);
    }

    public void updateCoinCount1(){

        data.coinCount -= 1;
        ui.txtCoinCount.text = " x " + data.coinCount;
        //updateScore(coinValue);
    }
    public void updateScore1(){

        data.score -= 1;
        ui.txtScore.text = "Score : " + data.score;
    }

    public void updateScore(Item item){
        int itemValue = 0;


        switch (item) {
            case Item.ShinningCoin:
                itemValue = shinningCoinValue;
                break;
            case Item.Coin:
                itemValue = coinValue;
                break;
            case Item.Enemy:
                itemValue = enemyValue;
                break;
            default:
                break;
        }
        //data.score += value;
        data.score += itemValue;
        ui.txtScore.text = "Score : " + data.score;
    }

    public void BulletHitEnemy(Transform enemy){
        Vector3 pos = enemy.position;
        pos.z = 20f;
        SFXCtrl.instance.EnemyExplosion(pos);

        //menampilkan koin
        Instantiate(shinningCoin, pos, Quaternion.identity);
        Destroy(enemy.gameObject);

        AudioCtrl.instance.EnemyExplosion(pos);
    }

    IEnumerator RestartLevel(){
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("Level1");
    }

    void UpdateTimer(){
        timeLeft -= Time.deltaTime;
        textTimer.text = "Timer : " + (int)timeLeft;
        if(timeLeft <= 0) {
            textTimer.text = "Timer: 0";
            SceneManager.LoadScene("Level1");
        } 
          
    }

    void HandleFirstBoot(){
        if(data.isFirstBoot) {
            data.lives = 5;
            data.coinCount = 0;
            data.score = 0;
            data.isFirstBoot = false;
        }
    }
   
    void UpdateHearts()
    {
        int fullHearts = (int)data.lives;
        float remaining = (float)data.lives - fullHearts;

        for (int i = 0; i < 5; i++)
        {
            if (i < fullHearts)
            {
                ui.heartImages[i].sprite = ui.heartFull;
            }
            else if (remaining >= 0.75f)
            {
                ui.heartImages[i].sprite = ui.heartThreeQuarter;
                remaining -= 0.75f;
            }
            else if (remaining >= 0.5f)
            {
                ui.heartImages[i].sprite = ui.heartHalf;
                remaining -= 0.5f;
            }
            
            else if (remaining >= 0.25f)
            {
                ui.heartImages[i].sprite = ui.heartQuarter;
                remaining -= 0.25f;
            }
            else
            {
                ui.heartImages[i].sprite = ui.heartEmpty;
            }
        }
    }

    public void CheckLives(){
        double updatedLives = data.lives;
        updatedLives -=1;
        data.lives = updatedLives;

        if(data.lives == 0) {
            Invoke("GameOver", restartDelay);
        } else {
            SaveData();
            StartCoroutine(RestartLevel());
        }
    }

    public void ReducePlayerHealthMushmaw()
    {
        double updatedLives = data.lives;
        updatedLives -=0.25;
        data.lives = updatedLives;

        Debug.Log("musuh");

        if(data.lives == 0) {
            Invoke("GameOver", restartDelay);
        } else {
            SaveData();
            StartCoroutine(RestartLevel());
        }
    }
    void GameOver(){

    }
}