using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioCtrl : MonoBehaviour
{
    public static AudioCtrl instance;
    public PlayerAudio playerAudio;
    public AudioFX audioFX;
    public Transform player;
    public GameObject BGMusic;
    public bool bgMusicOn;
    public GameObject btnSound, btnMusic; 
    public Sprite imgSoundOn, imgSoundOff;
    public Sprite imgMusicOn, imgMusicOff; 

    public bool soundOn;
    void Start()
    {
        if(instance == null) {
            instance = this;
        }

        // if(bgMusicOn) {
        //     BGMusic.SetActive(true);
        // }

        if(DataCtrl.instance.data.playMusic)
        {
            BGMusic.SetActive(true);
            btnMusic.GetComponent<Image>().sprite = imgMusicOn;
        }
        else
        {
            BGMusic.SetActive(false);
            btnMusic.GetComponent<Image>().sprite = imgMusicOff;
        }

        if (DataCtrl.instance.data.playSound)
        {
            soundOn = true;
            btnSound.GetComponent<Image>().sprite = imgSoundOn;
        }
        else
        {
            soundOn = false;
            btnSound.GetComponent<Image>().sprite = imgSoundOff;
        }
    }

    // Update is called once per frame
    public void PlayerJump(Vector3 playerPos){
        if(soundOn) {
            AudioSource.PlayClipAtPoint(playerAudio.playerJump, playerPos);
        }
    }

    public void CoinPickup(Vector3 playerPos){
        if(soundOn) {
            AudioSource.PlayClipAtPoint(playerAudio.coinPickup, playerPos);
        }
    }

    public void FireBullets(Vector3 playerPos){
        if(soundOn){
            AudioSource.PlayClipAtPoint(playerAudio.fireBullets, playerPos);
        }
    }

    public void EnemyExplosion(Vector3 playerPos){
        if(soundOn) {
            AudioSource.PlayClipAtPoint(playerAudio.enemyExplosion, playerPos);
        }
    }
    public void BreakableCrates(Vector3 playerPos){
        if(soundOn) {
            AudioSource.PlayClipAtPoint(playerAudio.breakCrates, playerPos);
        }
    }

    public void WaterSplash(Vector3 playerPos){
        if(soundOn) {
            AudioSource.PlayClipAtPoint(playerAudio.waterSplash, playerPos);
        }
    }

    public void PowerUp(Vector3 playerPos){
        if(soundOn) {
            AudioSource.PlayClipAtPoint(playerAudio.powerUp, playerPos);
        }
    }

    public void KeyFound(Vector3 playerPos){
        if(soundOn) {
            AudioSource.PlayClipAtPoint(playerAudio.keyFound, playerPos);
        }
    }

    public void PlayerDied(Vector3 playerPos){
        if(soundOn) {
            AudioSource.PlayClipAtPoint(playerAudio.playerDied, playerPos);
        }
    }
    public void ToggleSound()
    {
        if (DataCtrl.instance.data.playSound)
        {
            soundOn = false;

            // set the music off image to the music button
            btnSound.GetComponent<Image>().sprite = imgSoundOff;

            // save the change to the database
            DataCtrl.instance.data.playSound = false;
        }
        else
        {
            soundOn = true;

            // set the music off image to the music button
            btnSound.GetComponent<Image>().sprite = imgSoundOn;

            // save the change to the database
            DataCtrl.instance.data.playSound = true;
        }
    }

     public void ToggleMusic()
    {
        if(DataCtrl.instance.data.playMusic)
        {
            // stop the BG Music
            BGMusic.SetActive(false);

            // set the music off image to the music button
            btnMusic.GetComponent<Image>().sprite = imgMusicOff;

            // save the change to the database
            DataCtrl.instance.data.playMusic = false;
        }
        else
        {
            // stop the BG Music
            BGMusic.SetActive(true);

            // set the music off image to the music button
            btnMusic.GetComponent<Image>().sprite = imgMusicOn;

            // save the change to the database
            DataCtrl.instance.data.playMusic = true;
        }
    }
}
