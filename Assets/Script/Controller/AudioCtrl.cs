using System.Collections;
using UnityEngine;

public class AudioCtrl : MonoBehaviour
{
    public static AudioCtrl instance;
    public PlayerAudio playerAudio;
    public AudioFX audioFX;
    public Transform player;
    public GameObject BGMusic;
    public bool bgMusicOn;

    public bool soundOn;
    void Start()
    {
        if(instance == null) {
            instance = this;
        }

        if(bgMusicOn) {
            BGMusic.SetActive(true);
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
}
