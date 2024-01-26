using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;    //untuk bekerja dengan file
using System.Runtime.Serialization.Formatters.Binary; //untuk menyimpan data dalam format biner
using DG.Tweening; // untuk animasi dengan DOTween

public class GameCtrl : MonoBehaviour
{
    public static GameCtrl instance;
    public float restartDelay;
    [HideInInspector]
    public GameData data;
    public UI ui;
    public int coinValue;
    public float maxTime;
    public Text textTimer;
    public GameObject shinningCoin;
    public GameObject harmonyKey;
    public GameObject levelCompleteMenu;
    public int shinningCoinValue;
    public int enemyValue;
    bool isPaused;     
    public enum Item {
        Coin, ShinningCoin, Enemy
    }
    public GameObject mobileUI;
    string dataFilePath;
    BinaryFormatter bf;
    float timeLeft;
    private GameObject player;
    private Vector3 initialPlayerPosition;
    public float levelCompletionTime;

    
    void Awake() // Fungsi Awake dijalankan saat objek pertama kali diinisialisasi
    {
        if (instance == null) {
            instance = this;
        }

        // Inisialisasi objek BinaryFormatter untuk menyimpan dan membaca data dalam format biner
        bf = new BinaryFormatter();
        dataFilePath = Application.persistentDataPath + "/game.dat";

        Debug.Log(dataFilePath);
        // Inisialisasi pemain dan posisi awal pemain
        player = GameObject.FindGameObjectWithTag("Player"); // Inisialisasi player
        initialPlayerPosition = player.transform.position;
    }

    void Start() {
        // Mengatur ulang data permainan dan inisialisasi timer
        ResetData();
        timeLeft = maxTime;
        HandleFirstBoot();
        UpdateHearts();
        if(PlayerPrefs.HasKey("CPX")){
            PlayerPrefs.DeleteKey("CPX");
        }
        isPaused = false;
    }

    void Update() {
        // Mengembalikan pemain ke checkpoint jika checkpoint disimpan
        if(PlayerPrefs.HasKey("CPX")){
            float x = PlayerPrefs.GetFloat("CPX");
            float y = PlayerPrefs.GetFloat("CPY");
            player.transform.position = new Vector3(x,y,transform.position.z);
            initialPlayerPosition = player.transform.position;

            PlayerPrefs.DeleteKey("CPX");
        }
        if (isPaused) // Mengatur waktu berhenti jika permainan dijeda
        {
            Time.timeScale = 0;
        } else {
            Time.timeScale = 1;
        }

        if(Input.GetKeyDown(KeyCode.Escape)){  // Mengatur ulang data jika tombol Escape ditekan
            ResetData();
        }

        if(timeLeft > 0 ) { // Mengupdate timer permainan
            UpdateTimer();
        }
    }

    public void SaveData(){ // Fungsi SaveData untuk menyimpan data permainan ke file
        FileStream fs = new FileStream(dataFilePath, FileMode.Create);
        bf.Serialize(fs,data);
        fs.Close();
    }

    public void LoadData(){ //untuk membaca data permainan dari file
        if(File.Exists(dataFilePath)){
            FileStream fs = new FileStream(dataFilePath, FileMode.Open);
            data = (GameData)bf.Deserialize(fs);
            //Debug.Log("Number of Coins = " + data.coinCount);
            ui.txtCoinCount.text = " x " + data.coinCount;
            ui.txtScore.text = "Score : " + data.score;
            fs.Close();
        }
    }

    //untuk memuat dan menyimpan data saat objek diaktifkan dan dinonaktifkan
    void OnEnable() {
        //Debug.Log("Data Loaded");
        LoadData();
    }

    void OnDisable() {
       // Debug.Log("Data Saved");
        SaveData();
        Time.timeScale = 1;
    }

    void ResetData(){
        FileStream fs = new FileStream(dataFilePath, FileMode.Create); //file akan dihapus jika sudah ada atai dibuat jika belum ada
        data.coinCount = 0; //data menjadi 0
        ui.txtCoinCount.text = " x 0";
        data.score = 0; //nilai score dalam data = 0
        ui.txtScore.text = "Score : " + data.score;
        data.lives = 5; 
        data.keyFound = false;
        data.harmonyKeyFound = false;
        UpdateHearts(); 
        bf.Serialize(fs,data); //untuk menulis objek data ke dalam file yang telah dibuka dan akan menyimpan status permainan ke file ini
        fs.Close();
        Debug.Log("Data Reset");
    }
    public void PlayerDiedFall(GameObject player) {
        player.SetActive(false);
        StartCoroutine(RespawnPlayer());
        CheckLives();
        //Invoke("RestartLevel", restartDelay);
    }
    public void PlayerDied(GameObject player)
    {
        Debug.Log("jatuh");
        player.SetActive(false);
        StartCoroutine(RespawnPlayer());
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
        Invoke("RespawnPlayer", restartDelay);
    }
    public void PlayerStompsEnemy(GameObject enemy) {
        enemy.tag = "Untagged";
        Destroy(enemy);
    }

    public void updateCoinCount(){
        data.coinCount += 1;
        ui.txtCoinCount.text = " x " + data.coinCount;
    }

    public void updateCoinCount1(){
        data.coinCount -= 1;
        ui.txtCoinCount.text = " x " + data.coinCount;
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
                Debug.Log("ShinningCoinValue: " + itemValue);
                break;
            case Item.Coin:
                itemValue = coinValue;
                Debug.Log("CoinValue: " + itemValue);
                break;
            case Item.Enemy:
                itemValue = enemyValue;
                Debug.Log("EnemyValue: " + itemValue);
                break;
            default:
                break;
        }
        data.score += itemValue;
        ui.txtScore.text = "Score : " + data.score;
    }

    public int getItemValue(Item item){
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
        return itemValue;
    }

    public void BulletHitEnemy(Transform enemy){
        Vector3 pos = enemy.position;
        pos.z = 20f;
        SFXCtrl.instance.EnemyExplosion(pos);

        //menampilkan shinning  koin
        Instantiate(shinningCoin, pos, Quaternion.identity);
        Destroy(enemy.gameObject);

        AudioCtrl.instance.EnemyExplosion(pos);
        updateScore(Item.Enemy);
        //updateScore(Item.ShinningCoin);
    }

    public void ShowHarmonyKey(Transform enemy){
        Vector3 pos = enemy.position;
        pos.z = 20f;
        SFXCtrl.instance.EnemyExplosion(pos);

        Instantiate(harmonyKey, pos, Quaternion.identity);
        Destroy(enemy.gameObject);

        AudioCtrl.instance.EnemyExplosion(pos);
    }

    IEnumerator RestartLevel(){
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("Level1");
    }

    IEnumerator RespawnPlayer()
    {
        yield return new WaitForSeconds(1.0f);

        // Reset posisi pemain ke posisi awal
        player.transform.position = initialPlayerPosition;

        // Reset rotasi pemain
        player.transform.rotation = Quaternion.identity;

        // Menyalakan kembali pemain dan komponen yang diperlukan
        player.SetActive(true);
        player.GetComponent<PlayerCtrl>().enabled = true;

        Collider2D[] colliders = player.GetComponents<Collider2D>();
        foreach (Collider2D c2d in colliders)
        {
            c2d.enabled = true;
        }

        foreach (Transform child in player.transform)
        {
            child.gameObject.SetActive(true);
        }

        // Menyalakan kembali kontrol kamera
        Camera.main.GetComponent<CameraCtrl>().enabled = true;

        UpdateHearts();
    }

    void UpdateTimer(){
        timeLeft -= Time.deltaTime;
        textTimer.text = "Timer : " + (int)timeLeft;
        if(timeLeft <= 0) {
            textTimer.text = "Timer: 0";
            string sceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(sceneName);
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

    public void UpdateHearts()
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


    public void DecreaseLivesFireTrap()
    {
        double updatedLives = data.lives;
        updatedLives -= 0.25;
        data.lives = updatedLives;
        UpdateHearts();

        if(data.lives <= 0) {
            data.lives = 5;
            SaveData();
            Invoke("GameOver", restartDelay);
        } 
    }

    public void CheckLives(){
        double updatedLives = data.lives;
        updatedLives -=1;
        data.lives = updatedLives;

        if(data.lives <= 0) {
            Invoke("GameOver", restartDelay);
        } else {
            SaveData();
            StartCoroutine(RespawnPlayer());
        }
    }

    public void ReducePlayerHealthJamur()
    {
        double updatedLives = data.lives;
        updatedLives -=0.25;
        data.lives = updatedLives;

        Debug.Log("musuh");

        if(data.lives <= 0) {
            data.lives = 0;
            SaveData();
            Invoke("GameOver", restartDelay);
        } else {
            SaveData();
            StartCoroutine(RespawnPlayer());
        }
    }

    public void ReducePlayerHealthTikus()
    {
        double updatedLives = data.lives;
        updatedLives -=0.5;
        data.lives = updatedLives;

        Debug.Log("musuh");

        if(data.lives <= 0) {
            data.lives = 0;
            SaveData();
            Invoke("GameOver", restartDelay);
            //playerCtrl.DeactivatePlayer();
        } else {
            SaveData();
            StartCoroutine(RespawnPlayer());
        }
    }

    public void ReducePlayerHealthLandak()
    {
        double updatedLives = data.lives;
        updatedLives -=1;
        data.lives = updatedLives;

        Debug.Log("musuh");

        if(data.lives <= 0) {
            data.lives = 0;
            SaveData();
            Invoke("GameOver", restartDelay);
        } else {
            SaveData();
            StartCoroutine(RespawnPlayer());
        }
    }
    public void ReducePlayerHealthDino()
    {
        double updatedLives = data.lives;
        updatedLives -=1.5;
        data.lives = updatedLives;

        if(data.lives <= 0) {
            data.lives = 0;
            SaveData();
            Invoke("GameOver", restartDelay);
        } else {
            SaveData();
            StartCoroutine(RespawnPlayer());
        }
    }

    public void UpdateKeyCount(){
        data.keyFound = true;

        ui.keyImage.sprite = ui.keySprite;
        if(data.keyFound) {
            GameObject[] finishObjectsArray = GameObject.FindGameObjectsWithTag("Finish");

            foreach (GameObject finish in finishObjectsArray) {
                Destroy(finish);
            }
        }
    }

    public int GetScore(){
        return data.score;
    }

    public void SetStarsAwarded(int levelNumber, int numOfStars)
    {
        data.levelData[levelNumber].starsAwarded = numOfStars;
        Debug.Log("Number of Stars Awarded = " + data.levelData[levelNumber].starsAwarded);
    }

    public void UnlockLevel(int levelNumber)
    {
        if((levelNumber+1) <= (data.levelData.Length-1))
            data.levelData[(levelNumber+1)].isUnlocked = true;
    }


    void GameOver(){
        ui.panelGameOver.SetActive(true);
        string formattedCompletionTime = string.Format("{0}:{1:00}", (int)levelCompletionTime / 60, (int)levelCompletionTime % 60);
        ui.panelGameOver.GetComponentInChildren<Text>().text = "Waktu: " + formattedCompletionTime;

        mobileUI.SetActive(false);

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
        
    }
   

    public void DecreaseLivesDuri(float amount)
    {
        double updatedLives = data.lives;
        updatedLives -= 0.25;
        data.lives = updatedLives;
        UpdateHearts();

        if(data.lives <= 0) {
            data.lives = 5;
            SaveData();
            Invoke("GameOver", restartDelay);
        } 
    }


    public void LevelComplete()
    {
        levelCompleteMenu.SetActive(true);
        mobileUI.SetActive(false);
        float completionTime = maxTime - timeLeft;
        string formattedTime = string.Format("{0}:{1:00}", (int)completionTime / 60, (int)completionTime % 60);
        levelCompleteMenu.GetComponentInChildren<Text>().text = "Waktu: " + formattedTime;
        SetLevelCompletionTime(completionTime);

    }

    public void ShowPausePanel()
    {
        if (mobileUI.activeInHierarchy)
            mobileUI.SetActive(false);
        
        ui.panelPause.SetActive(true);
        ui.panelPause.gameObject.GetComponent<RectTransform>().DOAnchorPosY(0, 0.7f, false);

        //isPaused = true;
        Invoke("SetPause", 1.1f);
    }

    void SetPause()
    {
        // set the bool
        isPaused = true;
    }

    public void HidePausePanel()
    {
        isPaused = false;

        if (!mobileUI.activeInHierarchy)
            mobileUI.SetActive(true);

        ui.panelPause.SetActive(false);
        // animate the pause panel
        ui.panelPause.gameObject.GetComponent<RectTransform>().DOAnchorPosY(600, 0.7f, false);
    }

    public void OpenEnding(){
        data.harmonyKeyFound = true;
        Debug.Log("harmony key found");

        if(data.harmonyKeyFound) {
            GameObject[] gateEndingObjectsArray = GameObject.FindGameObjectsWithTag("GateEnding");

            foreach (GameObject gateEnding in gateEndingObjectsArray) {
                Destroy(gateEnding);
            }

            mobileUI.SetActive(false);
        }
    }

    public bool checkHarmonyKey(){
        return data.harmonyKeyFound;
    }

    public void SetLevelCompletionTime(float time)
    {
        levelCompletionTime = time;
    }
}