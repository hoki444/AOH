using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class battlemain : MonoBehaviour {

	public GameObject field2;
	public GameObject mcard;
	public GameObject notice1;
	public GameObject notice2;
	public GameObject notice3;
	Object tobj;
	List<card> pdeck=new List<card>();
	List<card> edeck=new List<card>();
	List<card> phand=new List<card>();
	List<card> ehand=new List<card>();
	card tcard;
	bool ecard=false;
	string tstr;
	int ind=1;
	int gtime=0;

	// Use this for initialization
	void Start () {
		StreamReader sw= new StreamReader("battleinfo\\battle.txt");
		while (ind<100) {
			try{tstr=sw.ReadLine();
				if (tstr=="e")
					ecard=true;
				if (!ecard){
					tobj=Instantiate (mcard,
					                   mcard.transform.position,
					                   this.gameObject.transform.localRotation);
					tobj.name="tcard"+ind.ToString();
					tcard = GameObject.Find("tcard"+ind.ToString()).GetComponent<card>();
					tcard.assigncard(tstr);
					pdeck.Add(tcard);
				}
				ind++;
			}
			catch{break;}
			}
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonUp(0)){
			Vector3 point=Input.mousePosition;
			Debug.Log(point);
			if(point.x>39&&point.x<114&&point.y>23&&point.y<41)
			{
				pdeck[0].movetohand();
				phand.Add(pdeck[0]);
				pdeck.RemoveAt (0);
			}
		}
	}
	void attackevent(){
		}
	void OnGUI()
	{

	}
}
