using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class PlayerHeath : NetworkBehaviour {

    public  int playerHp = 100;
    private Animator Ani;//玩家动画状态机
    private Animator CanvasAni;
    private PlayerMove PlayerMove;
    public float flashSpeed = 5f;                              
    public bool damaged;
    private SkinnedMeshRenderer bodyRenderer;//皮肤渲染器
    private playerShoot playerShoot;
    private test test;
    public Slider Hpslider;
    //public int currentHP=100;
    public AudioClip playerHurt;
	// Use this for initialization
	void Start () {
        Ani = this.GetComponent<Animator>();
        this.PlayerMove = this.GetComponent<PlayerMove>();
        this.bodyRenderer = transform.Find("Player").GetComponent<SkinnedMeshRenderer>();
        CanvasAni = GameObject.Find("Canvas").GetComponent<Animator>();
        //this.bodyRenderer = transform.Find("Player").renderer as SkinnedMeshRenderer;4.6版本使用方法
        //playerShoot = this.GetComponentInChildren<playerShoot>();
        test = this.GetComponent<test>();
        if (isLocalPlayer)
        {
            Hpslider = GameObject.Find("HPSlider").GetComponent<Slider>();
        }
	}
	
	// Update is called once per frame
	void Update () {
        //slider.value = currentHP;
        if (isLocalPlayer)
        {
            Hpslider.value = playerHp;
        }
        bodyRenderer.material.color = Color.Lerp(bodyRenderer.material.color, Color.white, flashSpeed * Time.deltaTime);
	}
    [ClientRpc]
    void RpcHp(int serverHp)
    {
        playerHp = serverHp;
    }
    public void TakeDamage(int Demage)//玩家受到伤害
    {
        this.playerHp -= Demage;
        RpcHp(playerHp);
        bodyRenderer.material.color = Color.red;
        AudioSource.PlayClipAtPoint(playerHurt, this.transform.position, 2f);
        if (this.playerHp <= 0)
        {
            Ani.SetTrigger("Die");
            //Dead();
            if (isLocalPlayer)
            {
                Invoke("CanvasAnimation", 1);
                CmdDeath();
               // this.gameObject.SetActive(false);
                 //Destroy(this.gameObject, 2);
                //Invoke("ReStart", 4);
            }
        }
    }
    /*private void Dead()//玩家死亡后的处理
    {
        this.test.enabled=false;
        this.PlayerMove.enabled = false;
        this.playerShoot.enabled = false;
        GetComponent<PlayerHeath>().enabled = false;
    }*/
    [ClientRpc]
    void RpcDeath()
    {
        Destroy(this.gameObject, 2);
    }
    [Command]
    void CmdDeath()
    {
        Destroy(this.gameObject, 2);
        RpcDeath();
    }
    private void ReStart()//重新加载游戏
    {
        SceneManager.LoadScene("Game");
    }
    private void CanvasAnimation()
    {   
            CanvasAni.SetTrigger("GameOver");
    }
}
