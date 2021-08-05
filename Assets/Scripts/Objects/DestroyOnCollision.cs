using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("MissingItems")) {
            Debug.Log("A missing item was picked up!");
            Destroy(other.gameObject);
        }
    }
}
