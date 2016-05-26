using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class GrappleCollisions : MonoBehaviour {

	public bool inAir = false;
	public HingeJoint grabHinge;

	//	void OnCollisionEnter (Collision col) {
	//		if (inAir = true) {
	//			rigidbody.velocity = 0;
	//			inAir = false;
	//			grabHinge = gameObject.AddComponent <HingeJoint>();
	//			grabHinge.connectedBody = col.rigidbody;
	//			//This stops the hook once it collides with something, and creates a HingeJoint to the object it collided with.
	//		}
	//	}

	void OnCollisionEnter (Collision col) 
	{
		if (inAir = true) {
			gameObject.GetComponent<Rigidbody> ().velocity = new Vector3(0,0,0);
			inAir = false;
			grabHinge = gameObject.AddComponent<HingeJoint> ();
			grabHinge.connectedBody = col.rigidbody;
			//This stops the hook once it collides with something, and creates a HingeJoint to the object it collided with.
		}
	}

}
