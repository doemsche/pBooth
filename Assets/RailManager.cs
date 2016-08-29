using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;
using System.Collections.Generic;
using System.Diagnostics;
using System.Collections;

public class RailManager : MonoBehaviour {

	public Hashtable frames;
	public RectTransform railprefab;
	public RectTransform list;

	void Start(){
		frames = new Hashtable();
	}

	public void AddToRail(Texture2D snap){
		//add to memory
		string key = snap.GetInstanceID().ToString();
		UnityEngine.Debug.Log(key);
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
		int i = 1;
		foreach( Texture2D t in frames.Values ){
			//UnityEngine.Debug.Log(t);
			System.IO.File.WriteAllBytes("/Users/dschlaepfer/tmp/" + session + i.ToString()+ ".png", t.EncodeToPNG());
			i++;
		}

		ProcessStartInfo proc = new ProcessStartInfo();
		proc.FileName = "ruby";
		proc.WorkingDirectory = "/Users/dschlaepfer/tmp/";
		proc.Arguments = "agif.rb";
		Process.Start(proc);

	}
}
