using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour {

    private NavMeshAgent agent;//要添加命名空间
    private Transform player;
    private Animator ani;
	// Use this for initialization

    void Awake()
    {
        agent = this.GetComponent<NavMeshAgent>();
        ani = this.GetComponent<Animator>();
    }
	void Start () {
        player = GameObject.FindWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(Vector3.Distance(transform.position,player.position)<1.5f)
        {
            agent.speed=0;
            ani.SetBool("Move", false);
        }
        else
        {
            agent.SetDestination(player.position);
            agent.speed = 2.5f;
            ani.SetBool("Move", true);
        }
	}
}
