using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmMovementScript : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        //subtracting player pos from mouse pos 
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        difference.Normalize();     //normalizing the vector, sum of vector will be equal to 1

        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg; // find the angle in degrees
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
    }
}
