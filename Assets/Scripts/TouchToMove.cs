using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchToMove : MonoBehaviour
{
    public GameObject foodPrefab;
    GameObject food;
    Vector3 goalPos;

    float walkSpeed = 0.5f;
    float rotSpeed = 2f;
    float buffer = 0.25f;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && food == null)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 1000.0f))
            {
                if (hit.collider.gameObject.tag == "ground")
                {
                    goalPos = hit.point;
                    food = Instantiate(foodPrefab, goalPos, Quaternion.identity);
                    Invoke("RemoveFood", 4.0f);
                }
            }
        }
    }

    private void LateUpdate()
    {
        if (food)
        {
            var lookGoal = new Vector3(goalPos.x, transform.position.y, goalPos.z);
            var lookDir = lookGoal - transform.position;
            if (lookDir.magnitude > buffer)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookDir), rotSpeed * Time.deltaTime);
                transform.Translate(0, 0, walkSpeed * Time.deltaTime);
                anim.SetBool("isRunning", true);
                anim.SetFloat("speed", 1.0f);
            }
            else
            {
                anim.SetBool("isRunning", false);
                if (food != null)
                {
                    anim.SetBool("isEating", true);
                }
            }
        }
    }

    void RemoveFood()
    {
        Destroy(food);
    }
}
