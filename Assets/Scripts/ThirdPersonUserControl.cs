using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof (ThirdPersonCharacter))]
public class ThirdPersonUserControl : MonoBehaviour
{
    private ThirdPersonCharacter m_Character; // A reference to the ThirdPersonCharacter on the object
    private Vector3 m_Move;

	public GameObject turret;
	public GameObject barrel;
	public GameObject camera;

	MouseLook mouseLook;
    
    private void Start()
    {
        // get the third person character ( this should never be null due to require component )
        m_Character = GetComponent<ThirdPersonCharacter>();

		mouseLook = new MouseLook ();
		mouseLook.Init (turret.transform, barrel.transform, camera.transform);
    }

	private void Update()
	{
		// update mouse camera movement
		mouseLook.LookRotation (turret.transform, barrel.transform, camera.transform);
	}

    // Fixed update is called in sync with physics
    private void FixedUpdate()
    {
        // read inputs
        float h = CrossPlatformInputManager.GetAxis("Horizontal");
        float v = CrossPlatformInputManager.GetAxis("Vertical");

        // we use world-relative directions in the case of no main camera
		m_Move = v*Vector3.forward + h*Vector3.right;

        // pass all parameters to the character control script
        m_Character.Move(m_Move);


    }
}
