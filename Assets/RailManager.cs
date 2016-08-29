using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Collections;
using System.Threading;
using UnityToolbag;
public class RailManager : MonoBehaviour {

	public Dictionary<string, Texture2D> frames;
	public RectTransform railprefab;
	public RectTransform list;
	public RectTransform preview;

	private GUIManager guimanager;

	void Start(){
		frames = new Dictionary<string,Texture2D>();
		guimanager = Object.FindObjectOfType<GUIManager>();
	}

	public void AddToRail(Texture2D snap){
		//add to memory
		string key = snap.GetInstanceID().ToString();
		frames.Add(key,snap);
		//add to gui
		Sprite sprite = Sprite.Create(snap, new Rect(0,0,snap.width,snap.height), new Vector2(0,0));
		RectTransform thumb = Instantiate(railprefab,list.transform.position,Quaternion.identity,list.transform) as RectTransform;
		thumb.GetComponent<Image>().sprite = sprite;
		thumb.GetComponent<Image>().preserveAspect = true;
		thumb.GetComponentInChildren<Button>().GetComponent<RemoveAtKey>().key = key;
	}

	public void RemoveAtIndex(string key){
		//remove from memory
		frames.Remove(key);
		//gui object is removed by button (self-destroy)
	}

	public void RenderSequence(string session){
		//UnityEngine.Debug.Log(frames.Count);
		System.IO.Directory.CreateDirectory("/Users/dschlaepfer/tmp/"+session);
		var list = frames.Keys.ToList(); 
		list.Sort();
		int i = 1;
		foreach( var key in list ){
			//UnityEngine.Debug.Log(frames[key]);
			System.IO.File.WriteAllBytes("/Users/dschlaepfer/tmp/" + session +"/f_"+ i.ToString()+ ".png", frames[key].EncodeToPNG());
			i++;
		}

		Process proc = new Process();
		proc.StartInfo.FileName = "ruby";
		proc.StartInfo.WorkingDirectory = "/Users/dschlaepfer/tmp/";
		proc.StartInfo.Arguments = "agif.rb "+session;
		proc.EnableRaisingEvents = true;
		proc.Exited += new System.EventHandler(this.ProcessFinished);
		proc.Start();
	}

	private void ProcessFinished(object sender,System.EventArgs e){
		//Use UnityToolbag for executing code in the main thread
		Dispatcher.Invoke(()=>{
			guimanager.OnRenderEnd();	
		});
	}

	public void Preview(){
		StartCoroutine(PlayMoviePreview());
	}

	private IEnumerator PlayMoviePreview(){
		WaitForSeconds wait = new WaitForSeconds(guimanager.framerate.value);
		Image tmpPreview = preview.GetComponent<Image>();
		int count = frames.Count;
		int counter = 0;
		foreach( Texture2D t in frames.Values){
			//UnityEngine.Debug.Log("yield");
			Sprite tmpSprite = Sprite.Create(t, new Rect(0,0,t.width,t.height), new Vector2(0,0));
			tmpPreview.sprite = tmpSprite;
			yield return wait;
			counter ++;
			if(counter == count){
				//UnityEngine.Debug.Log("alles fertig");
				guimanager.OnPreviewEnd();
			}
		}
	}
}
