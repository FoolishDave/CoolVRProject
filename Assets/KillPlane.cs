using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlane : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision) {
        collision.transform.position = new Vector3(-1f, 1.25f, 0f);
        collision.transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
}
