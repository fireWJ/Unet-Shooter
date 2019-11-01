using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class test : NetworkBehaviour {

    public float shootRate = 6;//射击速率
    private Light light;//枪口灯光
    private ParticleSystem particleSystem;//粒子效果
    private AudioSource audio;
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    private float Timer = 0;//计时器
	void Start () {
        light = this.GetComponentInChildren<Light>();
        particleSystem = this.GetComponentInChildren<ParticleSystem>();
        audio = this.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if (isLocalPlayer==false)
        {
            return;
        }
        Timer += Time.deltaTime;
        if (Input.GetMouseButton(0) && Timer >= 1 / shootRate)
        {
            Timer = 0;
            CmdShoot();
        }
	}
    [Command]
    void CmdShoot()
    {
        light.enabled = true;
        //particleSystem.Play();
        GameObject bullet=GameObject.Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 20;
        Destroy(bullet, 3);
        NetworkServer.Spawn(bullet);//在指定的客户端生成指定的物体
        audio.Play();
        Invoke("ClearEffect", 0.03f);
    }
    void ClearEffect()
    {
        light.enabled = false;
    }
}
