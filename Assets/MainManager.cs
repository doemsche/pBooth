using UnityEngine;
using System.Collections;

public class MainManager : MonoBehaviour {

	public VideoTexture videotexture;
	private RailManager railmanager;

	void Start(){
		railmanager = GetComponentInChildren<RailManager>();
	}

	void Update(){
		if(Input.GetKeyDown(KeyCode.Space)){
			Record();
		}
	}

	public void Render(){
		railmanager.RenderSequence("sessionX");
	}

	public void Record(){
		Texture2D snap = videotexture.RecordFrame();
		railmanager.AddToRail(snap);
	}

	public void Preview(){
		railmanager.Preview();
	}

	public void ApplyEffect(string name){
		videotexture.ApplyEffect(name);
	}
}
