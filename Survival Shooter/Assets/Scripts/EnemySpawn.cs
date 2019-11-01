using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnemySpawn : NetworkBehaviour {

	// Use this for initialization
    public float time = 1;
    public  float creatTime = 5;
    public GameObject enemy;
	void Start () {
        InvokeRepeating("ACC",2,10);
	}
    void ACC()
    {
        creatTime -= 0.01f;
    }
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        if (time >= creatTime)
        {
            time =  time-creatTime;
            CmdCreat();
        }
	}
    [Command]
    public void CmdCreat()
    {
        GameObject Enemy=GameObject.Instantiate(enemy, transform.position, transform.rotation);
        NetworkServer.Spawn(Enemy);
    }
}
