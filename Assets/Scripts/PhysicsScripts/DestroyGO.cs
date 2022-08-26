using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGO : MonoBehaviour
{
    private void Update()
    {
        Destroy(gameObject, 4);
    }
}
