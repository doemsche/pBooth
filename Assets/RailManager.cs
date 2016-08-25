using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;
using System.Collections.Generic;
using System.Diagnostics;

public class RailManager : MonoBehaviour {

	public List<Texture2D> frames;
	public Image railprefab;

	void Start(){
		frames = new List<Texture2D>();
	}

	public void AddToRail(Texture2D snap){
		//add to memory
		frames.Add(snap);

		//add to gui
		float xOffset = frames.Count-1 * 1.60f;
		Image railObj = Instantiate<Image>(railprefab);
		Sprite sprite = Sprite.Create(snap, new Rect(0,0,snap.width,snap.height), new Vector2(0,0));
		railObj.GetComponent<Image>().sprite = sprite;
		railObj.transform.SetParent(this.transform);
		railObj.transform.localScale = Vector3.one;
		railObj.transform.position = new Vector3(xOffset,0,0);
	}

	public void RemoveFromRail(){
		int cnt = frames.Count;
		if(cnt < 1){
			return;
		}
		//remove from memory
		frames.RemoveAt(cnt-1);

		//remove from gui
		Image delImg = GetComponentsInChildren<Image>()[cnt-1];
		Destroy(delImg);
	}

	public void RenderSequence(string session){
		int i = 1;
		foreach( Texture2D t in frames ){
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
