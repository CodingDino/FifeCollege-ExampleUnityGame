﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public float speed = 5;
	public float health = 100;
	public float invulnerableDuration = 1;
	public float blinkDuration = 0.25f;

	private float invulnerableEndTime = 0;
	private float blinkEndTime = 0;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		Rigidbody2D ourRigidBody = GetComponent<Rigidbody2D> ();

		// Ge the current horixontal input (left/right arrows) - between -1 and 1.
		float horizontal = Input.GetAxis ("Horizontal");

		// Get the current velocity from the physics system
		Vector2 velocity = ourRigidBody.velocity;

		// Set our velocity based on the input and our speed value
		velocity.x = horizontal * speed;

		// Put this velocity back into the physics system
		ourRigidBody.velocity = velocity;



		// Handle blinking while invulnerable:

		// Get our sprite renderer component attached to this object
		SpriteRenderer renderer = GetComponent<SpriteRenderer> ();

		// Are we done being invulnerable?
		if (Time.time >= invulnerableEndTime) {
			// if NOT invulnerable...

			// Set the renderer to enabled.
			renderer.enabled = true;
		} else {
			// If YES invulnerable...

			// If it is time to blink...
			if (Time.time >= blinkEndTime) {
				// set our renderer enabled value to the opposite of what it currently is (toggle it)
				renderer.enabled = !renderer.enabled;
				// Set the next time we should blink to our current time plus the blink duration
				blinkEndTime = Time.time + blinkDuration;
			} // end if (Time.time >= blinkEndTime)
		} // end if (Time.time >= invulnerableEndTime)
	} // end Update()

	public void Damage(float damageToDeal)
	{
		if (Time.time >= invulnerableEndTime) {

			// Reducing health by the damage passed in
			health = health - damageToDeal;

			// TODO: handle death

			// Set us as invulnerable for a set duration
			invulnerableEndTime = Time.time + invulnerableDuration;

			// Log the result of the function
			Debug.Log("Damage was dealt");
			Debug.Log("damageToDeal = "+damageToDeal);
			Debug.Log("health = "+health);
		}
	} // end Damage()
}
