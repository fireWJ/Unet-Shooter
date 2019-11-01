using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour {

    private PlayerHeath playerHeath;
    private Animator ani;
    public GameObject player;
	// Use this for initialization
	void Start () {
        ani = this.GetComponent<Animator>();
        playerHeath = player.GetComponent<PlayerHeath>();
	}
	
	// Update is called once per frame
	void Update () {
        if (playerHeath.playerHp <= 0)
        {
            ani.SetTrigger("GameOver");

        }
	}
}
