using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;

public class EnemyHeath : NetworkBehaviour {
      [SyncVar]
    public float enemyHp = 100;
    private Animator ani;
    private AudioSource audio;//敌人被打中的声音
    public AudioClip death;//敌人被打死的声音
    public int reward;// 打死敌人后获得的分数
    private ParticleSystem particleSystem;
	// Use this for initialization
    void Awake()
    {
        ani = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
        particleSystem = this.GetComponentInChildren<ParticleSystem>();
    }
    public void TakeDamage(float damage,Vector3 hit)
    {
        if (this.enemyHp <= 0) return;
        audio.Play();
        particleSystem.transform.position = hit;
        particleSystem.Play();
        this.enemyHp -= damage;
        if (this.enemyHp <= 0)
        {
            CmdEnemyDeadAni();
            Dead();
        }
    }
    void Dead()//处理敌人死亡后的操作
    {
        Score.instance.AddScore(reward);
       // ani.SetTrigger("Dead");
        AudioSource.PlayClipAtPoint(death, transform.position,1f);
        Destroy(this.gameObject, 0.9f);
    }
    [ClientRpc]
    void RpcEnemyDeadAni()
    {
        ani.SetTrigger("Dead");
    }
	[Command]
    void CmdEnemyDeadAni()
    {
        RpcEnemyDeadAni();
        ani.SetTrigger("Dead");
    }
}
