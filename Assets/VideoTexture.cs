using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;


public class VideoTexture : MonoBehaviour {

	private RawImage videoInput;
	private WebCamTexture webcamTexture;
	private int resWidth = 3840;
	private int resHeight = 2160;

	private Color32[] data;
	private Texture2D snap;
	private RailManager railmanager;

	private EffectsManager effectsmanager;

	void Awake(){
		videoInput = GetComponent<RawImage>();
		effectsmanager = FindObjectOfType<EffectsManager>();
	}

	// Use this for initialization
	void Start () {		
		webcamTexture = new WebCamTexture();
		videoInput.texture = webcamTexture;
		videoInput.material.mainTexture = webcamTexture;
		webcamTexture.Play ();
	}

	public void Pause(){
		webcamTexture.Pause();
	}

	public void Play(){
		webcamTexture.Play();
	}

	public void Stop(){
		webcamTexture.Stop();
	}

	public Texture2D RecordFrame(){
		snap = new Texture2D(webcamTexture.width, webcamTexture.height);

		snap.SetPixels(webcamTexture.GetPixels());
//		Debug.Log(snap.GetPixel(100,11));
		string tmpcol = effectsmanager.eColor;
		for(int y = 0; y < snap.height; y ++){
			for(int x = 0; x < snap.width; x++){
				Color tc = snap.GetPixel(x,y);

				if( tmpcol == "blue"){
					tc.b *= 0.1f;
				} else if(tmpcol == "red"){
					tc.r *= 0.1f;
				} else if(tmpcol == "yellow"){
					tc.g *= 0.1f;
				} else if(tmpcol == "green"){
					tc.g *= 0.2f;
				} else {
					
				}
				tc.a = 0.5f;
				snap.SetPixel(x,y,tc);
			}
		}

		snap.Apply();
		return snap;
	}


	public void ApplyEffect(string name){
		Color color = Color.white;
		switch(name){
			case "red":
			color = Color.red;
				break;
			case "blue":
			color = Color.blue;
				break;
			case "green":
			color = Color.green;
				break;
			case "yellow":
			color = Color.yellow;
				break;
			default:
			color = Color.white;
				break;
		}
		GetComponent<RawImage>().color = color;
	}


//	public void SavePicture(){
//		string name = emailAddressInput.text;
//		System.IO.File.WriteAllText(SaveImagePath+name+".txt",messageInput.text);
//		System.IO.File.WriteAllBytes(SaveImagePath + name + ".png", snap.EncodeToPNG());
//	}
}
