using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof (TankCharacter))]
public class TankControl : MonoBehaviour
{
    private TankCharacter m_Character; // A reference to the TankCharacter on the object
    private Vector3 m_Move;

	public GameObject turret;
	public GameObject barrelPivot;
	public GameObject barrel;
	public GameObject camera;

	private GameObject grapple;
	private bool grappleActive = false;
	private HingeJoint grabHinge;
	public int speed;

	MouseLook mouseLook;
    
    private void Start()
    {
        // get the third person character ( this should never be null due to require component )
        m_Character = GetComponent<TankCharacter>();

		mouseLook = new MouseLook ();
		mouseLook.Init (turret.transform, barrelPivot.transform, camera.transform);
    }

	private void Update()
	{
		// update mouse camera movement
		if (!grappleActive) {
			mouseLook.LookRotation (turret.transform, barrelPivot.transform, camera.transform);
		}

		// mouse click
		if (Input.GetMouseButtonDown (0)) {
			// spawn a firework
			ShootMissile ();
		}

		if (Input.GetMouseButtonDown (1)) {
			ShootGrapple ();
		}
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

	private void ShootMissile()
	{
		// instantiate fireworks at end of barrel
		GameObject fireworks = Instantiate( Resources.Load ("Fireworks", typeof(GameObject))) as GameObject;

		fireworks.transform.localPosition = barrel.transform.localPosition;
		//fireworks.transform.Translate(new Vector3(0, 0, -barrel.transform.lossyScale.z));
		fireworks.transform.localRotation = barrel.transform.rotation;
		fireworks.transform.localPosition = barrel.transform.TransformPoint (fireworks.transform.localPosition);
	}

	private void ShootGrapple() 
	{
		grappleActive = true;

		GameObject grapple = Instantiate( Resources.Load ("Grapple", typeof(GameObject))) as GameObject;

		grapple.transform.SetParent (barrel.transform);
		grapple.GetComponent<Rigidbody> ().AddForce (grapple.transform.forward * speed, ForceMode.Impulse);
	}

//
//	//To shoot your hook, call this method:
//	void GrapplingShot(){
//		rigidbody.velocity = new Vector3 (x, y, z) * speed;
//		inAir = true;
//		//This is the direction your hook moves multiplied by speed.
//	}
//	void OnCollisionEnter (Collision col) {
//		if (inAir = true) {
//			rigidbody.velocity = 0;
//			inAir = false;
//			grabHinge = gameObject.AddComponent <HingeJoint>();
//			grabHinge.connectedBody = col.rigidbody;
//			//This stops the hook once it collides with something, and creates a HingeJoint to the object it collided with.
//		}
//	}
}
