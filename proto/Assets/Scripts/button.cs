using UnityEngine;
using System.Collections;

public class button : MonoBehaviour {
	public int buttonno;
	private Ray ray;
	private RaycastHit hit;
	battlemain bmain;
	// Use this for initialization
	void Start () {
		
		bmain=GameObject.Find("MainCamera").GetComponent<battlemain>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)){
			ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if(Physics.Raycast(ray, out hit, Mathf.Infinity)){
				if(hit.transform.gameObject == gameObject){
					bmain.summontest(buttonno);
				}
			}
		}
	}
}
