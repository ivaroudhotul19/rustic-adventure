using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// untuk menghandle partikel effect pada game
public class SFXCtrl : MonoBehaviour
{
    public static SFXCtrl instance;
    public SFX sfx;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void ShowCoinSparkle(Vector3 pos) {
        Instantiate(sfx.sfx_coin_pickup, pos, Quaternion.identity);
    }
    public void EnemyExplosion(Vector3 pos) {
        Instantiate(sfx.sfx_enemy_explosion, pos, Quaternion.identity);
    }

    public void ShowBulletSparkle(Vector3 pos){
        Instantiate(sfx.sfx_bullet_pickup, pos, Quaternion.identity);
    }

    public void ShowPalyerLanding(Vector3 pos){
        Instantiate(sfx.sfx_playerLands, pos, Quaternion.identity);
    }

    public void ShowSplash(Vector3 pos){
        Instantiate(sfx.sfx_splash, pos, Quaternion.identity);
    }

    public void HandleBoxBreaking(Vector3 pos) {
        Vector3 pos1 = pos;
        pos1.x -= 0.3f;

        Vector3 pos2 = pos;
        pos2.x += 0.3f;

        Vector3 pos3 = pos;
        pos3.x -= 0.3f;
        pos3.y -= 0.3f;

        Vector3 pos4 = pos;
        pos4.x += 0.3f;
        pos4.y += 0.3f;

        Instantiate(sfx.sfx_fragment_1, pos1, Quaternion.identity);
        Instantiate(sfx.sfx_fragment_2, pos2, Quaternion.identity);
        Instantiate(sfx.sfx_fragment_2, pos3, Quaternion.identity);
        Instantiate(sfx.sfx_fragment_1, pos4, Quaternion.identity);

        AudioCtrl.instance.BreakableCrates(pos);
    }

}
