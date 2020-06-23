using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxing : MonoBehaviour {

    [SerializeField]
    private Transform[] backgrounds;
    private float[] parallaxScales;        //The proportion of the camera's movement to move the backgrounds by
    public float smoothing = 1f;          // How smooth the parallax is going to be, Make sure to set this above 0.

    private Transform cam;               //Reference to the main cameras transform.
    private Vector3 previousCamPosition; //The position of the camera in the previous frame



    //This is called before Start(). good for refferences
     void Awake()
    {
        //set up camera for refference
        cam = Camera.main.transform;
        
    }
    // Use this for initialization
    void Start ()
    {
        //The previous frame had the urrent frame's camera position
        previousCamPosition = cam.position;

        //Assigning coresponding prallaxScales;
        parallaxScales = new float[backgrounds.Length];

        for (int i = 0; i < backgrounds.Length; i++)
        {
            parallaxScales[i] = backgrounds[i].position.z * -1;
        }

	}
	
	// Update is called once per frame
	void Update ()
    {
        //foreach background
        for(int i = 0; i < backgrounds.Length; i++)
        {
            //the parallax is the opposite of the camera movement because of the previous frame multiplied by the scale;
            float parallax = (previousCamPosition.x - cam.position.x) * parallaxScales[i];

            //Set a  target x position whic is the current possition plus the parallax
            float backgroundTargetPosX = backgrounds[i].position.x + parallax;

            //create a target position is the background's current position with its target x position
            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

            //fade between current position and the target position using lerp
            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
        }

        //set the previousCamPosition to the camera's position at the end of the frame;
        previousCamPosition = cam.position;
		
	}
}
