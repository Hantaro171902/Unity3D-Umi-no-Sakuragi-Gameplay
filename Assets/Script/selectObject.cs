using UnityEngine;

public class selectObject : MonoBehaviour
{
    void SelectAllExceptOne (GameObject excludeObject)
    {
        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        foreach (GameObject obj in allObjects)
        {
            if (obj != excludeObject)
            {
                // Perform your action here
                Debug.Log("Selected: " + obj.name);
            }
        }
    }
}
