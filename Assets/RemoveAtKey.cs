using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RemoveAtKey : MonoBehaviour {

	public string key;
	private RailManager rm;
	// Use this for initialization
	void Start () {
		rm = Object.FindObjectOfType<RailManager>();
		Button myselfButton = GetComponent<Button>();
		myselfButton.onClick.AddListener(() => doIt());
	}
		
	void doIt(){
		rm.RemoveAtIndex(key);
		Destroy(transform.parent.gameObject);
	}
}
