using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NetworkSet : NetworkBehaviour {
	void Start () {
        OfflineUISet();
	}
    void OfflineUISet()
    {
        GameObject.Find("BtnConnection").GetComponent<Button>().onClick.AddListener(StartClient);
        GameObject.Find("BtnCreat").GetComponent<Button>().onClick.AddListener(StartHost);
    }
    void OnlineUISet()
    {
        GameObject.Find("BtnBreakOff").GetComponent<Button>().onClick.AddListener(StopHost);
    }
    public void StartHost()//创建主机
    {
        NetworkManager.singleton.StartHost();
    }
    public void StartClient()//创建客户端
    {
        NetworkManager.singleton.networkAddress = GameObject.Find("IFIP").GetComponent<InputField>().text;
        NetworkManager.singleton.StartClient();
    }
    public void StopHost()//停止客户端 
    {
        NetworkManager.singleton.StopHost();
    }
    void OnLevelWasLoaded(int level)//场景切换时自动调用
    {
        if (level == 0)
        {
            OfflineUISet();
        }
        else
        {
            OnlineUISet();
        }
    }
}
