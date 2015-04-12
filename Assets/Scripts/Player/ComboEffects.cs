using UnityEngine;
using System.Collections;

public class ComboEffects : MonoBehaviour
{
	GameObject player;
	ComboTiming comboTiming;
	CombatControllerIII combat;
	Combos combos;
		
	Rigidbody2D effectInstance;
	
	
	
	// Define all prefab'd effects.
	public Rigidbody2D med;

		
						
	void Start ()
	{
		player = GameObject.FindGameObjectWithTag ("Player");	// Finds player
		comboTiming = player.GetComponent<ComboTiming>();
		combat = player.GetComponent<CombatControllerIII>();
		combos = player.GetComponent<Combos>();		
	}
	
	public void Invoke (SongsOBJ.Combo invoked)
	{
		// All effects go here.
		if (invoked.effect.Equals("Med"))
		{
			effectInstance = Instantiate(med, combat.noteOrigin.position, combat.noteOrigin.rotation) as Rigidbody2D;	
			CustomProjectile note = effectInstance.gameObject.GetComponent<CustomProjectile>();
			note.setDirection (combat.heWhoShoots.localScale.x);
			note.setDamage (combos.Damage);
			note.setSpeed (4);
		}
		if (invoked.effect.Equals("SlowMed"))
		{
			effectInstance = Instantiate(med, combat.noteOrigin.position, combat.noteOrigin.rotation) as Rigidbody2D;
			effectInstance.transform.localScale *= 2;	
			CustomProjectile note = effectInstance.gameObject.GetComponent<CustomProjectile>();
			note.setDirection (combat.heWhoShoots.localScale.x);
			note.setDamage (combos.Damage);
			note.setSpeed (2);
			CameraLogic.ShakeItUp(0.25f, 0.2f, 1.0f);
		}
	}
}
