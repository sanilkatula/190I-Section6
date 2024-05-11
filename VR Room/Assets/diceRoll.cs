using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class diceRoll : MonoBehaviour
{
    private Vector3 lastVelocity;
    private bool isRolling = false;
    public int topFaceValue; 

    public delegate void OnTopFaceValueChange(int newTopFaceValue);
    public event OnTopFaceValueChange TopFaceValueChanged;

    void Start()
    {

    }

    void Update()
    {
        if (lastVelocity != Vector3.zero && GetComponent<Rigidbody>().velocity == Vector3.zero)
        {
            if (isRolling)
            {
                CheckTopFace();
                isRolling = false;
            }
        }
        else if (GetComponent<Rigidbody>().velocity != Vector3.zero)
        {
            isRolling = true;
        }
        lastVelocity = GetComponent<Rigidbody>().velocity;
    }

    void CheckTopFace()
    {
        RaycastHit hit;
        Vector3[] directions = { Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back };
        int[] faceValues = { 5, 2, 4, 3, 6, 1 };
        int layerMask = 1 << LayerMask.NameToLayer("Ground");
        float maxDistance = 100f;

        for (int i = 0; i < directions.Length; i++)
        {
            Vector3 dir = transform.TransformDirection(directions[i]);
            if (Physics.Raycast(transform.position, dir, out hit, maxDistance, layerMask))
            {
                int newTopFaceValue = faceValues[i];
                if (newTopFaceValue != topFaceValue)
                {
                    topFaceValue = newTopFaceValue;
                    TopFaceValueChanged?.Invoke(topFaceValue); 
                    Debug.Log("The number facing up is: " + topFaceValue);
                }
                break;
            }
        }
    }
}
