using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class CharacterController : MonoBehaviour
{
    public float speed = 10.0f;
    public float rotationSpeed = 100.0f;
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // Get the horizontal and vertical axis.
        // By default they are mapped to the arrow keys.
        // The value is in the range -1 to 1
        float translation = CrossPlatformInputManager.GetAxis("Vertical") * speed;
        float rotation = -CrossPlatformInputManager.GetAxis("Horizontal") * rotationSpeed;

        // Make it move 10 meters per second instead of 10 meters per frame...
        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;

        if (translation > 0)
        {
            anim.SetBool("isRunning", true);
            anim.SetFloat("speed", 1.0f);
        }
        else if (translation < 0)
        {
            anim.SetBool("isRunning", true);
            anim.SetFloat("speed", -1.0f);
        }
        else
            anim.SetBool("isRunning", false);

        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            anim.SetTrigger("jump");
        }

        // Move translation along the object's z-axis
        transform.Translate(0, 0, translation);

        // Rotate around our y-axis
        transform.Rotate(0, rotation, 0);
    }
}
