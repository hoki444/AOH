using UnityEngine;
using System.Collections;

public class test : MonoBehaviour {
	private Ray ray;
	private RaycastHit hit;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)){
			ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			Debug.Log(ray);
			if(Physics.Raycast(ray, out hit, Mathf.Infinity)){
				Debug.Log(hit.transform.gameObject);
				if(hit.transform.gameObject == gameObject){
					Debug.Log ("success!");
				}
			}
		}
	}
}
