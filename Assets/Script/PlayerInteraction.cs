using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public GameObject player;
    public Transform holdPos;
    public Camera playerCamera; // Reference to the player camera

    public float throwForce = 500f;
    public float pickUpRange = 3f; // Adjusted for better accuracy
    public float rotationSpeed = 5f;

    private GameObject heldObj;
    private Rigidbody heldObjRb;
    private bool isRotating = false;
    private int holdLayer;
    private Vector3 defaultPosition;

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
                TryPickUpObject();
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
                isRotating = !isRotating;
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

    void TryPickUpObject()
    {
        RaycastHit[] hits = Physics.RaycastAll(playerCamera.transform.position, playerCamera.transform.forward, pickUpRange);
        System.Array.Sort(hits, (x, y) => x.distance.CompareTo(y.distance)); // Sort by closest first

        foreach (RaycastHit hit in hits)
        {
            if (hit.transform.CompareTag("pickup"))
            {
                PickUpObject(hit.transform.gameObject);
                break;
            }
        }
    }

    void PickUpObject(GameObject pickUpObj)
    {
        if (pickUpObj.GetComponent<Rigidbody>())
        {
            heldObj = pickUpObj;
            heldObjRb = pickUpObj.GetComponent<Rigidbody>();
            defaultPosition = heldObj.transform.position;

            heldObjRb.isKinematic = true;
            heldObj.transform.SetParent(holdPos, true); // Set parent with world position intact
            heldObj.transform.localPosition = Vector3.zero; // Ensure it's centered in holdPos
            heldObj.transform.localRotation = Quaternion.identity; // Reset rotation

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
