using UnityEngine;

public class CardSpinner : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 50f;
    [SerializeField] private bool spinning = false;
    [SerializeField] private Vector3 resetRotation = Vector3.zero;

    private void Update()
    {
        if (spinning)
        {
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.Self);
        }
    }

    public void SetSpinning(bool active)
    {
        spinning = active;

        if (!spinning)
        {
            transform.localRotation = Quaternion.Euler(resetRotation);
        }
    }
}
