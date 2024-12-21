using UnityEngine;

public class ItemTransformData : MonoBehaviour
{
    public Vector3 savedPosition;
    public Quaternion savedRotation;

    public void SaveTransform(Transform transform, Vector3 positionOffset)
    {
        savedPosition = positionOffset;
        savedRotation = transform.rotation;
    }

    public void ApplySavedTransform(Transform transform, Vector3 newTablePosition)
    {
        transform.position = new Vector3(newTablePosition.x + savedPosition.x, 1.0f, newTablePosition.z + savedPosition.z);
        transform.rotation = savedRotation;
    }
}