using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerMove :NetworkBehaviour {

	// Use this for initialization
    private Vector3 movement;
    public float playerSpeed = 6f;
    private Rigidbody rbody;
    private Animator Ani;
    private int floorLayerIndex = -1;
    private playerShoot playershoot;
    private float Timer = 0;//计时器
    public float shootRate = 8;//射击速率
	void Start () {
        rbody = transform.GetComponent<Rigidbody>();
        Ani=this.GetComponent<Animator>();
        floorLayerIndex = LayerMask.GetMask("Floor");
        playershoot = this.GetComponentInChildren<playerShoot>();
        //获取本机的IP地址 
        var strHostName = System.Net.Dns.GetHostName();//获取当前电脑名
        var ipEntry = System.Net.Dns.GetHostEntry(strHostName);//会返回所有地址，包括IPv4和IPv6
        var addr = ipEntry.AddressList;
        GameObject.Find("IpText").GetComponent<Text>().text = addr[1].ToString();
        if (isLocalPlayer)
        {
            GameObject.Find("Main Camera").GetComponent<CameraFallow>().target=gameObject.transform;
        }
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (isLocalPlayer==false)
        {
            return;
        }
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Move(h, v);
        AniChange(h, v);
        Turing();
        //CmdTuring();
	}
    private void Move(float h, float v)//玩家移动
    {
        movement.Set(h, 0f, v);
        movement = movement.normalized * playerSpeed * Time.deltaTime;//normalized将向量长度统一
        rbody.MovePosition(transform.position + movement);//搭配collider使用
        //transform.Translate(new Vector3(h, 0f, v) * playerSpeed * Time.deltaTime);直接修改position,但是不会有碰撞效果，所以这里不适用
    }
    private void AniChange(float h,float v)//动画切换
    {
        if (h != 0 || v != 0)
        {
            Ani.SetBool("IsWalking", true);
        }
        else
        {
            Ani.SetBool("IsWalking", false);
        }
    }
    private void Turing()//玩家
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit Hit;
        if (Physics.Raycast(ray, out Hit, 200, floorLayerIndex))
        {
            Vector3 target = Hit.point;
            target.y = transform.position.y;
            transform.LookAt(target);
        }
    }
}