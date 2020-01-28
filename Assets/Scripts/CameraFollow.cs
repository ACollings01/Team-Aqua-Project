using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    //private float maxZoom = 8.0f; // This should be less than the camera's distance from the player model to ensure no clipping occurs
	//private float zoom = 0.0f;

    private float yOffset = 18; // Determines the max height above the player
    private float zOffset = -1;  // Determines how far back from the player the camera is

    private void Start()
    {
        // Automatically finds the object tagged as "Player"
        player = GameObject.FindGameObjectWithTag("Player");

		transform.position = new Vector3(player.transform.position.x, player.transform.position.y + yOffset, player.transform.position.z - zOffset);
		transform.LookAt(player.transform);
	}

    // Update is called once per frame
    void Update()
    {
        // Should automatically look at the player regardless of zoom amount or angle
        //AdjustZoom(Input.mouseScrollDelta.y);
        // Ensures that player doesn't zoom in or out further than allowed
		//ConstrainZoom();

        // Comment out the two lines above to remove zoom functionality

        transform.localPosition = new Vector3(player.transform.position.x, player.transform.position.y + yOffset - 1.8f, player.transform.position.z - zOffset);
		transform.LookAt(player.transform);
	}

    // Changes the zoom based on amount (negative will zoom out, positive will zoom in)
	/*void AdjustZoom(float amount)
	{
		if ((zoom < maxZoom && amount > 0) || (zoom > 0 && amount < 0))
		{
			zoom += amount;
		}
	}

    // Keeps zoom within 0 and maxZoom
	void ConstrainZoom()
	{
		zoom = zoom > maxZoom ? maxZoom : zoom;
		zoom = zoom < 0 ? 0 : zoom;
	}
    */
}