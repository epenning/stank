using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ThirdPersonCharacter : MonoBehaviour
{
	[SerializeField] float m_MovingTurnSpeed = 180;
	[SerializeField] float m_StationaryTurnSpeed = 90;
	[Range(1f, 4f)][SerializeField] float m_GravityMultiplier = 2f;
	[SerializeField] float m_MoveSpeedMultiplier = 0.1f;

	Rigidbody m_Rigidbody;
	const float k_Half = 0.5f;
	float m_TurnAmount;
	float m_ForwardAmount;
	Vector3 m_GroundNormal;



	void Start()
	{
		m_Rigidbody = GetComponent<Rigidbody>();
		m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
	}


	public void Move(Vector3 move)
	{

		// convert the world relative moveInput vector into a local-relative
		// turn amount and forward amount required to head in the desired
		// direction.
		if (move.magnitude > 1f) move.Normalize();
		//move = Vector3.ProjectOnPlane(move, m_GroundNormal);
		m_TurnAmount = move.x;
		m_ForwardAmount = move.z;

		ApplyExtraTurnRotation();

		Vector3 v = new Vector3(0,0,m_ForwardAmount) * m_MoveSpeedMultiplier / Time.deltaTime;

		v = transform.TransformDirection(v);

		// maintain current y velocity
		v.y = m_Rigidbody.velocity.y;
		m_Rigidbody.velocity = v;
	}

	void ApplyExtraTurnRotation()
	{
		// help the character turn faster (this is in addition to root rotation in the animation)
		float turnSpeed = Mathf.Lerp(m_StationaryTurnSpeed, m_MovingTurnSpeed, m_ForwardAmount);
		transform.Rotate(0, m_TurnAmount * turnSpeed * Time.deltaTime, 0);
	}
}
