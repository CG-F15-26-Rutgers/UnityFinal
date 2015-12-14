using UnityEngine;
using System.Collections;
using Character;

public class PersonController : MonoBehaviour
{
    void Start()
    {
    }
    void Update()
    {
        KeyboardController.Move(transform);
    }
}