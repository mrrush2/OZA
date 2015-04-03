using UnityEngine;
using System.Collections;
using System;

public class ReSkinPlayer : MonoBehaviour {

	public string instrument;


	void Update() // VERY TEMPORARY spritesheet changer based on Austen's also temporary sound changer.
	{
		if (Input.GetKeyDown (KeyCode.X))
			instrument = "ViolinViola";
		if (Input.GetKeyDown (KeyCode.Z))
			instrument = "Trumpet";
	}
	





	void LateUpdate() // Must be done in LateUpdate
	{
		var subSprites = Resources.LoadAll<Sprite> ("PlayerSpritesheets/" + instrument); // Load all sprites from an instrument.

		foreach (var renderer in GetComponentsInChildren<SpriteRenderer>())
		{
			string spriteName = renderer.sprite.name;
			var newSprite = Array.Find(subSprites, item => item.name == spriteName);

			if (newSprite)
				renderer.sprite = newSprite; //Set new sprites for aniation
		}
	}


}