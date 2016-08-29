using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
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
		var session = System.Guid.NewGuid().ToString().Substring(0,8);
		railmanager.RenderSequence(session);
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

	public void ResetApp(){
		SceneManager.LoadScene("photobooth");
	}
}
