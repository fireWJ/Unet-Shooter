using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    [SerializeField]
    private Text textScore;
    public int NowScore = 0;//当前分数
    public static Score instance = null;//尽量少用单例模式，因为耦合度较高
    void Awake()
    {
        instance = this;
    }
	// Use this for initialization
	void Start () {
        textScore = this.GetComponent<Text>();
	}
    public void AddScore(int reward)
    {
        NowScore += reward;
        textScore.text = NowScore.ToString();
    }
    public void RemoveScore(int reward)
    {
        NowScore -= reward;
        textScore.text = NowScore.ToString();
    }
}
