using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerShoot :MonoBehaviour {

    public float shootRate=8;//射击速率
    private Light light;//枪口灯光
    private ParticleSystem particleSystem;//粒子效果
    private LineRenderer line;
    private AudioSource audio;
    private float Timer = 0;//计时器
    //public AudioClip ac;
	// Use this for initialization
	void Start () {
        light = this.GetComponent<Light>();
        particleSystem=this.GetComponentInChildren<ParticleSystem>();
        line = this.GetComponent<LineRenderer>();
        audio = this.GetComponent<AudioSource>();
	}
	// Update is called once per frame
	void Update () {
        /*timer += time.deltatime;
        if (input.getmousebutton(0) && timer >= 1 / shootrate)
        {
            timer = 0;
            cmdfire();
        }*/
	}
    void Shoot()
    {
        light.enabled = true;
        particleSystem.Play();
        this.line.enabled = true;
        line.SetPosition(0, transform.position);
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit Hit;
        if (Physics.Raycast(ray, out Hit))
        {
            line.SetPosition(1, Hit.point);
            //判断当前射击有没有碰到敌人
            if (Hit.collider.tag == "Enemy")
            {
                Hit.collider.GetComponent<EnemyHeath>().TakeDamage(40, Hit.point);
            }
        }
        else
        {
            line.SetPosition(1, transform.position + transform.forward * 100);
        }
        audio.Play();
        Invoke("ClearEffect", 0.03f);
    }
    public void Fire()
    {
        //otherPrefab.GetComponent<NetworkIdentity>().AssignClientAuthority(connectionToClient);
        Shoot();
        Debug.Log(123);
    }
    void ClearEffect()
    {
        light.enabled = false;
        line.enabled = false;
    }
}
