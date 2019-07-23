using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmMovementScript : MonoBehaviour
{
    private PlayerMoveScript myMovementScript;

    public Vector2 currentAimAngle;

    private void Start()
    {
        myMovementScript = GetComponentInParent<PlayerMoveScript>();
    }

    // Update is called once per frame
    void Update()
    {
        ArmRotation();
    }

    private void ArmRotation()
    {
        if (myMovementScript.joystickType == PlayerMoveScript.Joysticks.Keyboard)
        {
            //subtracting player pos from mouse pos 
            Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg; // find the angle in degrees
            transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
        }
        else if (myMovementScript.joystickType == PlayerMoveScript.Joysticks.Joy2)
        {
            float rotZ = Mathf.Atan2(Input.GetAxis("Joy2_AimHoriz"), Input.GetAxis("Joy2_AimVert")) * Mathf.Rad2Deg; // find the angle in degrees (isnt trig fun)
            Quaternion actualRotation = Quaternion.Euler(0f, 0f, rotZ);
            transform.rotation = Quaternion.Inverse(actualRotation);
        }
    }

}
