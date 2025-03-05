using System;
using UnityEngine;

public class PlayerPickUpItemControl : MonoBehaviour
{
    [SerializeField]
    private Transform positionGetItem;
    [SerializeField]
    private Vector3 directionDropItemOffset;
    [SerializeField]
    private Transform positionDirectionDropItem;
    [SerializeField]
    private float forceDropItem;

    private Transform currentItemPicked;

    private void Update()
    {
        if (GameInput.Instance.GetPlayerDropItem())
        {
            DropItem();
        }
    }

    public void SetPickedItem(Transform item)
    {
        if (currentItemPicked != null)
        {
            DropItem();
        }

        currentItemPicked = item;
        currentItemPicked.GetComponent<Rigidbody>().isKinematic = true;
        currentItemPicked.GetComponent<Collider>().enabled = false;
        currentItemPicked.transform.parent = positionGetItem;
        currentItemPicked.transform.localPosition = Vector3.zero;
        currentItemPicked.transform.localRotation = Quaternion.identity;
    }

    public void DropItem()
    {
        if (currentItemPicked == null)
        {
            return;
        }

        currentItemPicked.transform.parent = null;

        currentItemPicked.GetComponent<Collider>().enabled = true;

        Vector3 direction = (positionDirectionDropItem.position - currentItemPicked.position + directionDropItemOffset).normalized;

        Rigidbody itemRigidBody = currentItemPicked.GetComponent<Rigidbody>();
        itemRigidBody.isKinematic = false;
        itemRigidBody.AddForce(direction * forceDropItem, ForceMode.Impulse);

        currentItemPicked = null;
    }
}
