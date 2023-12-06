using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHeadCtrl : MonoBehaviour
{
   public GameObject enemy;

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("PlayerFeet"))
        {
            GameCtrl.instance.PlayerStompsEnemy(enemy);
            SFXCtrl.instance.ShowEnemyPoof(enemy.transform.position);
        }
	}
}