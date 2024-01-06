using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBulletScript : MonoBehaviour
{
    private BossAI bossAI;

    // Set reference to BossAI
    public void SetBossAIReference(BossAI bossAI)
    {
        this.bossAI = bossAI;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameCtrl.instance.ReducePlayerHealthTikus();
        }
    }
}
