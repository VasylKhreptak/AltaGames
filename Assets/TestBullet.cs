using System;
using GamePlay.Entity.Interfaces;
using UnityEngine;

public class TestBullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out IInfectable infectable))
        {
            infectable.Infect();
        }
    }
}
