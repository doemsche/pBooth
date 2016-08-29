using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;


public class AddToSnapper : MonoBehaviour {

	private HorizontalScrollSnap scrollview;

	public RectTransform rtPrefab;
	public RectTransform list;
	// Use this for initialization
	void Start () {
		scrollview = GetComponentInChildren<HorizontalScrollSnap>();
	}

	public void AddItem(){
		RectTransform child = Instantiate(rtPrefab,list.transform.position,Quaternion.identity,list.transform) as RectTransform;
//		scrollview.GetComponentInChildren<HorizontalLayoutGroup>()
//		child.transform.SetParent(list.transform);
	}
}
