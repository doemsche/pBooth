using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EffectsManager : MonoBehaviour {

	public Button red;
	public Button blue;
	public Button green;
	public Button yellow;
	private MainManager mm;

	private string effectColor;

	public string eColor {
		get{
			return effectColor;
		}
		set{
			effectColor = value;
		}
	}

	void Start () {
		
		mm = Object.FindObjectOfType<MainManager>();
		red = GetComponentsInChildren<Button>()[0];
		green = GetComponentsInChildren<Button>()[1];
		blue = GetComponentsInChildren<Button>()[2];
		yellow = GetComponentsInChildren<Button>()[3];
		red.onClick.AddListener(() => ApplyEffect("red"));
		green.onClick.AddListener(() => ApplyEffect("blue"));
		blue.onClick.AddListener(() => ApplyEffect("green"));
		yellow.onClick.AddListener(() => ApplyEffect("yellow"));
	}


	private void ApplyEffect(string colorname){
		mm.ApplyEffect(colorname);
		eColor = colorname;
	}
		
}
