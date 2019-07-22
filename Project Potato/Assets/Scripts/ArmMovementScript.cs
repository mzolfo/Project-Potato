using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmMovementScript : MonoBehaviour
{
    private PlayerController myController;

    public Vector2 currentAimAngle;

    private void Start()
    {
        myController = GetComponentInParent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        ArmRotation();
    }

    private void ArmRotation()
    {
        if (myController.joystickType == Joysticks.Keyboard)
        {
            //subtracting player pos from mouse pos 
            Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg; // find the angle in degrees
            transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
        }
        else if (myController.joystickType == Joysticks.Joy2)
        {
            if (Input.GetAxis("Joy2_AimHoriz") != 0 && Input.GetAxis("Joy2_AimVert") != 0)
            { 
                float rotZ = Mathf.Atan2(Input.GetAxis("Joy2_AimHoriz"), Input.GetAxis("Joy2_AimVert")) * Mathf.Rad2Deg; // find the angle in degrees (isn't trig fun)
                Quaternion actualRotation = Quaternion.Euler(0f, 0f, rotZ);
                transform.rotation = Quaternion.Inverse(actualRotation);
            }
        }
    }

}
