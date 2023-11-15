using UnityEngine;

public class DragObject : MonoBehaviour
{
    public float distanceFromCamera = 2.0f; // Fixed distance from the camera
    private Rigidbody selectedRigidbody;
    private Camera mainCamera;
    private Vector3 originalScreenTargetPosition;
    private float selectionDistance;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.GetComponent<Rigidbody>())
                {
                    selectedRigidbody = hit.collider.gameObject.GetComponent<Rigidbody>();

                    // Constrain Rigidbody rotation
                    selectedRigidbody.constraints = RigidbodyConstraints.FreezeRotation;

                    selectionDistance = distanceFromCamera; // Use the fixed distance
                    originalScreenTargetPosition = mainCamera.WorldToScreenPoint(selectedRigidbody.position);
                }
            }
        }

        if (Input.GetMouseButton(0) && selectedRigidbody)
        {
            Vector3 mousePositionOffset = Input.mousePosition - originalScreenTargetPosition;
            Vector3 newScreenTargetPosition = originalScreenTargetPosition + mousePositionOffset;
            Vector3 newWorldTargetPosition = mainCamera.ScreenToWorldPoint(new Vector3(newScreenTargetPosition.x, newScreenTargetPosition.y, selectionDistance));

            // Prevent clipping and maintain distance from camera
            Vector3 directionToTarget = newWorldTargetPosition - mainCamera.transform.position;
            float distanceToTarget = directionToTarget.magnitude;
            RaycastHit[] hits = Physics.RaycastAll(mainCamera.transform.position, directionToTarget.normalized, distanceToTarget);
            bool isBlocked = false;
            foreach (var hit in hits)
            {
                if (hit.collider.gameObject != selectedRigidbody.gameObject)
                {
                    isBlocked = true;
                    break;
                }
            }

            if (!isBlocked)
            {
                // Move using Rigidbody.MovePosition
                selectedRigidbody.MovePosition(mainCamera.transform.position + mainCamera.transform.forward * distanceFromCamera);
            }
        }

        if (Input.GetMouseButtonUp(0) && selectedRigidbody)
        {
            // Release constraints when not dragging
            selectedRigidbody.constraints = RigidbodyConstraints.None;
            selectedRigidbody = null;
        }
    }
}
