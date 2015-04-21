using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InstrumentController : MonoBehaviour 
{	
	public GameObject  preview;
	public Text description;
	public GameObject perk1;
	public GameObject perk2;
	public GameObject perk3;
	

	
	
	void Awake ()
	{
	
	}
	
	
	
	public void SetInfo(InstrumentsOBJ.Instrument newInstrument)
	{
		Sprite previewSprite = Resources.Load<Sprite> ("MenuFrameContents/Inst/" + newInstrument.name);
		Sprite perk1Sprite = Resources.Load<Sprite> ("MenuFrameContents/Inst/Perks/" + newInstrument.perk1);
		Sprite perk2Sprite = Resources.Load<Sprite> ("MenuFrameContents/Inst/Perks/" + newInstrument.perk2);
		Sprite perk3Sprite = Resources.Load<Sprite> ("MenuFrameContents/Inst/Perks/" + newInstrument.perk3);
		string newDesc = newInstrument.description;
		
		preview.GetComponent<Image>().sprite = previewSprite;
		perk1.GetComponent<Image>().sprite = perk1Sprite;
		perk2.GetComponent<Image>().sprite = perk2Sprite;
		perk3.GetComponent<Image>().sprite = perk3Sprite;
		description.text = newDesc;
	}
}
