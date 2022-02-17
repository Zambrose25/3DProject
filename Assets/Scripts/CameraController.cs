using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject player;

    public Transform Target, Player;
    public Transform Obstruction;

    //float mouseX, mouseY;
    float rotationSpeed = 1;

    private Vector3 offset;

    public float xRotation = 0f;


    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.transform.position;
        Obstruction = Target;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
        //CameraControl();
        ViewObstructed();
    }

    void Update()
    {
        CameraControlFixed();
    }

    //void CameraControl()
    //{
        //mouseX += mouse.current.delta.X * rotationSpeed;
        //mouseY -= mouse.current.delta.Y * rotationSpeed;
        //mouseY = Mathf.Clamp(mouseY, -35, 60);

        //transform.LookAt(Target);

        //if(Input.GetKey(KeyCode.LeftShift))
        //{
        //    Target.rotation = Quaternion.Euler(mouseY, mouseX, 0);
        //}
       // else
        //{
        //    Target.rotation = Quaternion.Euler(mouseY, mouseX, 0);
       //     Player.rotation = Quaternion.Euler(0, mouseX, 0);
       // }

    //}

    void CameraControlFixed()
    {
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        Player.Rotate(Vector3.up * mouseX);
    }

    void ViewObstructed()
    {
        RaycastHit hit;

        if(Physics.Raycast(transform.position, Target.position - transform.position, out hit, 4.5f))
        {
            if (hit.collider.gameObject.tag != "Player")
            {
                Obstruction = hit.transform;
                Obstruction.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;

                if (Vector3.Distance(Obstruction.position, transform.position) >= 3f && Vector3.Distance(transform.position, Target.position) >= 1.5f)
                {
                    transform.Translate(Vector3.forward * 2f * Time.deltaTime);
                }
            }
            else
            {
                Obstruction.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
                if(Vector3.Distance(transform.position, Target.position) < 4.5f)
                {
                    transform.Translate(Vector3.back * 2f * Time.deltaTime);
                }
            } 
        }
    }

}
