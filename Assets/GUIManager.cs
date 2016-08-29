using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUIManager : MonoBehaviour {

	public Button preview;
	public Button record;
	public Button render;
	public Text processing;
	public Slider framerate;

	private RailManager rm;

	void Start(){
		rm = Object.FindObjectOfType<RailManager>();
		processing.gameObject.SetActive(false);
	}

	public void OnPreview(){
		record.interactable = false;
		render.interactable = false;
	}

	public void OnRecord(){
		
	}

	public void OnRender(){
		preview.interactable = false;
		record.interactable = false;
		render.interactable = false;
		processing.gameObject.SetActive(true);
	}

	public void OnPreviewEnd(){
		record.interactable = true;
		render.interactable = true;
	}

	public void OnRenderEnd(){
		Debug.Log("on render end");
		preview.interactable = true;
		record.interactable = true;
		render.interactable = true;
		processing.gameObject.SetActive(false);
	}
}
