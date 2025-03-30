using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public GameObject player;
    public Transform holdPos;
    public Vector3 defaultPosition; // Store the original position

    public float throwForce = 500f;
    public float pickUpRange = 100f;
    public float rotationSpeed = 5f;

    private GameObject heldObj;
    private Rigidbody heldObjRb;
    private bool isRotating = false;
    private int holdLayer;

    void Start()
    {
        holdLayer = LayerMask.NameToLayer("holdLayer");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (heldObj == null)
            {
                RaycastHit[] hits = Physics.RaycastAll(transform.position, transform.forward, pickUpRange);

                foreach (RaycastHit hit in hits)
                {
                    if (hit.transform.CompareTag("pickup"))
                    {
                        PickUpObject(hit.transform.gameObject);
                        break;
                    }
                }
            }
            else
            {
                DropObject();
            }
        }

        if (heldObj != null)
        {
            MoveObject();

            if (Input.GetKeyDown(KeyCode.R))
            {
                isRotating = !isRotating; // Toggle rotation mode
            }

            if (isRotating && Input.GetMouseButton(0))
            {
                RotateObject();
            }

            if (Input.GetKeyDown(KeyCode.T))
            {
                ThrowObject();
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                ResetObjectPosition();
            }
        }
    }

    void PickUpObject(GameObject pickUpObj)
    {
        if (pickUpObj.GetComponent<Rigidbody>())
        {
            heldObj = pickUpObj;
            heldObjRb = pickUpObj.GetComponent<Rigidbody>();
            defaultPosition = heldObj.transform.position; // Save default position

            heldObjRb.isKinematic = true;
            heldObjRb.transform.parent = holdPos.transform;
            heldObj.layer = holdLayer;

            Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), true);
        }
    }

    void DropObject()
    {
        if (heldObj != null)
        {
            heldObj.transform.parent = null;
            heldObjRb.isKinematic = false;
            heldObj.layer = 0;
            heldObj = null;
        }
    }

    void MoveObject()
    {
        heldObj.transform.position = holdPos.position;
    }

    void RotateObject()
    {
        float XaxisRotation = Input.GetAxis("Mouse X") * rotationSpeed;
        float YaxisRotation = Input.GetAxis("Mouse Y") * rotationSpeed;

        heldObj.transform.Rotate(Vector3.up, -XaxisRotation, Space.World);
        heldObj.transform.Rotate(Vector3.right, YaxisRotation, Space.World);
    }

    void ThrowObject()
    {
        if (heldObj != null)
        {
            heldObj.transform.parent = null;
            heldObjRb.isKinematic = false;
            heldObj.layer = 0;
            heldObjRb.AddForce(player.transform.forward * throwForce);
            heldObj = null;
        }
    }

    void ResetObjectPosition()
    {
        if (heldObj != null)
        {
            heldObj.transform.position = defaultPosition;
            DropObject();
        }
    }
}
