using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    void OnTriggerEnter(Collider col)
    {
        GameObject hit = col.gameObject;
        EnemyHeath enemyHeath = hit.GetComponent<EnemyHeath>();
        if (enemyHeath != null)
        {
            enemyHeath.TakeDamage(20,this.transform.position) ;
        }
        Destroy(this.gameObject);
    }
}
