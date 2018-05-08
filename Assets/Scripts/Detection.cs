//////////////////////////////////////
//Script created by Alexander Ameye //
//Version: 1.0.2 (PRO)  					  //
//////////////////////////////////////

using System.Collections;
using UnityEngine;
//using UnityEngine.UI;

public class Detection : MonoBehaviour
{
	public float Reach = 4.0F;
	[HideInInspector] public bool InReach;

	public Color DebugRayColor = Color.green;
	[Range(0.0F, 1.0F)]
	public float DebugRayColorAlpha = 1.0F; //Opacity.

	void Start ()
	{
		gameObject.name = "Player";
		gameObject.tag = "Player";
	}

	void Update ()
	{
		//Set origin of ray to 'center of screen' and direction of ray to 'cameraview'.
		Ray ray = Camera.main.ViewportPointToRay(new Vector3 (0.5F, 0.5F, 0F));

		RaycastHit hit; //Variable reading information about the collider hit.

		//Cast ray from center of the screen towards where the player is looking.
		if (Physics.Raycast(ray, out hit, Reach))
		{

			if (hit.collider.tag == "Door")
			{
				InReach = true;

				if (Input.GetKey(KeyCode.E))
				{
					//Give the object that was hit the name 'Door'.
					GameObject Door = hit.transform.gameObject;

					//Get access to the 'Door' script attached to the object that was hit.
					Door dooropening = Door.GetComponent<Door>();

					//Open/close the door by running the 'Open' function found in the 'Door' script.
					if (dooropening.Running == false) StartCoroutine (hit.collider.GetComponent<Door>().Open());

				}
			}

			else InReach = false;
		}

		else InReach = false;

		//Draw the ray as a colored line for debugging purposes.
		Debug.DrawRay (ray.origin, ray.direction * Reach, DebugRayColor);

	}

}
