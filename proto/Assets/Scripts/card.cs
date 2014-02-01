using UnityEngine;
using System.Collections;
using System.IO;

public class card : MonoBehaviour {
	public int position0=0;
	public int position1=0;
	public int life=0;
	public int maxhp=0;
	public int hp=0;
	public int atk=0;
	public int speed=0;
	public int cardno=0;
	public string cardname="t";
	public int cost=0;
	public int level=0;
	public string q="F";
	int[,] attackrange = new int[4, 6];
	public bool onfield=true;
	public bool islive=true;
	public Sprite s1;
	public Sprite s2;
	public string state;
	public Texture back;
	bool up=true;
	battlemain bmain;

	// Use this for initialization
	void Start () {
		state = "deck";
	}
	
	// Update is called once per frame
	void Update () {
	}
	public void movetohand(){
		state = "hand";
	}
	public void assigncard(string acard){
		char[] info = acard.ToCharArray ();
		StreamReader sw= new StreamReader(makepath(info));
		cardname=sw.ReadLine();
		q=sw.ReadLine();
		level=int.Parse(sw.ReadLine());
		cardno=int.Parse(sw.ReadLine());
		cost=int.Parse(sw.ReadLine());
		life=int.Parse(sw.ReadLine());
		hp=int.Parse(sw.ReadLine());
		atk=int.Parse(sw.ReadLine());
		speed=int.Parse(sw.ReadLine());
		sw.Close();
		}
	string makepath(char[] info){
		string path="";
		if (info [0] == 'H') {path="cardinfo\\HS\\";
		} else {path="cardinfo\\"+info [0].ToString()+"\\"+info [1].ToString();
				}
		for (int i=2; i<info.Length; i++) {
			path=path+info[i].ToString();
				}
		return (path+".txt");
	}
	public void hit(card ocard){
		hp = hp - ocard.atk;
		if (hp <= 0) {
			life--;
			hp=maxhp;
			if(life<0)
				islive=false;
				}
	}
	
}
