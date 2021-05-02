using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
	private float horizontalInput;//A,D
	private float verticalInput;//W,S
	private float steerAngle;//current angle being steered

	//referene to respective wheel colliders
	public WheelCollider LFWheel, RFWheel;//left and right front wheel
	public WheelCollider LRWheel, RRWheel;//left and right rear wheel

	//transform of wheels
	public Transform LFTransform, RFTransform;
	public Transform LRTransform, RRTransform;

	public float maxSteerAngle = 40;//limits how fast car can steer
	public float motorForce = 500;//force applied when accelerating

    private void FixedUpdate()
    {
		//get input
		horizontalInput = Input.GetAxis("Horizontal");
		verticalInput = Input.GetAxis("Vertical");

		//update current steer angle
		steerAngle = maxSteerAngle * horizontalInput;
		LFWheel.steerAngle = this.steerAngle;
		RFWheel.steerAngle = this.steerAngle;

		//update acceleration (only powers front wheels)
		LFWheel.motorTorque = motorForce * verticalInput;
		RFWheel.motorTorque = motorForce * verticalInput;

		//update rotation and position of wheels
		updateWheelTransform(LFWheel, LFTransform);
		updateWheelTransform(RFWheel, RFTransform);
		updateWheelTransform(LRWheel, LRTransform);
		updateWheelTransform(RRWheel, RRTransform);


	}

	//update the rotation and transform of the wheel
	private void updateWheelTransform(WheelCollider wheelCollider , Transform wheelTransform)
    {
		Vector3 pos = wheelTransform.position;
		Quaternion rot = wheelTransform.rotation;

		wheelCollider.GetWorldPose(out pos, out rot);

		wheelTransform.position = pos;
		wheelTransform.rotation = rot;

    }
}
