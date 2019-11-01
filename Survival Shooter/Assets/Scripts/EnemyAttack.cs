using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

    public int attack = 10;
    private float timer = 0;
    public bool damage;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            timer += Time.deltaTime;
            if (timer >= 0.5f)
            {
                damage = true;
                timer = 0;
                other.GetComponent<PlayerHeath>().TakeDamage(attack);
            }
        }
    }
}
