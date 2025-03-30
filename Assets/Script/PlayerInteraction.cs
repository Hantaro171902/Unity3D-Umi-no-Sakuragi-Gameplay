using Unity.VisualScripting;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject player;
    public Transform holdPos;

    public float throwForce = 500f;  // force at which the object is thrown at
    public float pickUpRange = 100f;  // how far the player can pick up objects 

    private float rotationSpeed = 1f;   // how fast/slow the object rotates
    private GameObject heldObj; // the object that the player is pickup
    private Rigidbody heldObjRb;
    private bool canDrop = true;
    private bool isRotating = false;

    private int LayerNumber;    // layer index

    void Start()
    {
        LayerNumber = LayerMask.NameToLayer("holdLayer");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (heldObj == null)
            {
                RaycastHit[] hits = Physics.RaycastAll(transform.position, transform.forward, pickUpRange);

                foreach (RaycastHit hit in hits)
                {
                    Debug.Log("Hit object: " + hit.transform.gameObject.name);

                    if (hit.transform.gameObject.tag == "pickup")
                    {
                        PickUpObject(hit.transform.gameObject);
                        Debug.Log("Can Pick Up: " + hit.transform.gameObject.name);
                        break; // Stop after finding the first valid object
                    }
                }
            }
            else
            {
                if (canDrop == true)
                {
                    StopClipping();  //prevent object from clipping through walls
                    DropObject();
                }
            }
        }
        if (heldObj != null)
        {
            MoveObject();   // keep object position at holdPos
            if (isRotating && Input.GetMouseButton(0)) // Hold left-click to rotate
            {
                RotateObject();
            }
            if (Input.GetKeyDown(KeyCode.Mouse0) && canDrop == true)    // Mouse0 is left click
            {
                StopClipping();
                ThrowObject();
            }
        }
    }

    // Pickup 
    void PickUpObject(GameObject pickUpObj)
    {
        if (pickUpObj.GetComponent<Rigidbody>())
        {
            heldObj = pickUpObj;    //assign heldObj to the object that was hit by the raycast (no longer == null)
            heldObjRb = pickUpObj.GetComponent<Rigidbody>(); //assign Rigidbody
            heldObjRb.isKinematic = true;
            heldObjRb.transform.parent = holdPos.transform; //parent object to holdposition
            heldObj.layer = LayerNumber;    // change the layer of the object to holdLayer

            // make sure the object doesn't collide with the player
            Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), true);
        }
        Debug.Log("Picking up: " + pickUpObj.name);

    }

    void DropObject()
    {

        // re-enable collision between the player and the object
        Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), true);

        heldObj.transform.parent = null;
        heldObjRb.isKinematic = false;
        heldObj.layer = 0;  // change the layer of the object to default
        heldObj = null;
    }

    void MoveObject()
    {
        // keep object position at the same position as holdPos
        heldObj.transform.position = holdPos.position;
    }

    void RotateObject()
    {
        if (Input.GetKeyDown(KeyCode.R)) // hold R to rotate object
        {
            isRotating = !isRotating;
            canDrop = false;

            float XaxisRotation = Input.GetAxis("Mouse X") * rotationSpeed;
            float YaxisRotation = Input.GetAxis("Mouse Y") * rotationSpeed;
            //rotate the object depending on mouse X-Y Axis
            heldObj.transform.Rotate(Vector3.down, XaxisRotation, Space.World);
            heldObj.transform.Rotate(Vector3.right, YaxisRotation, Space.World);
        }
        else
        {
            canDrop = true;
        }
    }

    void ThrowObject()
    {
        // re-enable collision between the player and the object
        Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), true);
        heldObj.transform.parent = null;
        heldObjRb.isKinematic = false;
        heldObj.layer = 0;  // change the layer of the object to default
        heldObjRb.AddForce(player.transform.forward * throwForce);
        heldObj = null;
    }

    void StopClipping()
    {
        var clipRange = Vector3.Distance(heldObj.transform.position, transform.position);
        RaycastHit[] hits = Physics.RaycastAll(transform.position, transform.TransformDirection(Vector3.forward), clipRange);

        // if the array length is greater than 1, meaning it has hit more than just the object we are carrying
        if (hits.Length > 1)
        {
            // change object position to camera position
            heldObj.transform.position = transform.position + new Vector3(0f, -0.5f, 0f); //offset slightly downward to stop object dropping above player 

        }
    }
}
