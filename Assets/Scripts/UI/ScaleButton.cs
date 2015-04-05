using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScaleButton : MonoBehaviour {


	GameObject player;
	ScalesOBJ scales;
	ScalesOBJ.Scale scaleToActivate;
	
	public Button scaleButton;
	public string name;
	
	// Use this for initialization
	void Awake ()
	{
		scaleButton = this.GetComponent<Button>();
		player = GameObject.FindGameObjectWithTag ("Player"); //Initialize player references.
		scales = player.GetComponent<ScalesOBJ>();
	}
	
	
	void Start () 
	{
		for (int i = 0; i < scales.scaleList.Count; i++)
		{ 
			if (scales.scaleList[i].name.Equals(name))
				scaleToActivate = scales.scaleList[i];	
		}	
	
	
		scaleButton.onClick.AddListener(() => 
		{ 
			scales.ChangeKey (scaleToActivate);
		});
		

	}

}
