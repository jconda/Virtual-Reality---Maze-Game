using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Key : MonoBehaviour 
{
	//Create a reference to the KeyPoofPrefab and Door
	public static bool hasKey;
	public GameObject PoofPrefab;
	public GameObject Door;

	void Update()
	{
		//Bonus: Key Animation
		transform.Rotate(Vector3.up, 60.0f * Time.deltaTime);
	}

	public void OnKeyClicked()
	{
		// Instatiate the KeyPoof Prefab where this key is located
		// Make sure the poof animates vertically
		// Call the Unlock() method on the Door
		// Destroy the key. Check the Unity documentation on how to use Destroy
		Quaternion rotation = Quaternion.Euler(270,0,0);
		GameObject poof = (GameObject)Instantiate(PoofPrefab, transform.position, rotation);
		Door doorScript = (Door) Door.GetComponent(typeof(Door));
		doorScript.Unlock();
		//Destroy (gameObject);
		StartCoroutine(PlayAudio());
		hasKey = true;
	}

	IEnumerator PlayAudio() {
		// Hide the key
		GetComponent<Renderer>().enabled = false;
		AudioSource audio = GetComponent<AudioSource>();
		audio.Play();
		// Hidden key cant be clicked
		Destroy(GetComponent<EventTrigger>());
		yield return new WaitForSeconds(audio.clip.length);
		Destroy (gameObject);
	}
}