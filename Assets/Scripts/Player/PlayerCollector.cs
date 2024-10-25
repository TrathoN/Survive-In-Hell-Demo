using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerCollector : MonoBehaviour
{

    private PlayerStats player;
    private CircleCollider2D circleCollider;

    private void Start()
    {
        player = FindAnyObjectByType<PlayerStats>();
        circleCollider = GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        circleCollider.radius = player.CurrentMagnetRadius;
    }

    //There is some issue on triggerEnter. Changed from addforce() to position translate.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent(out ICollectable collectable))
        {
            collectable.Pull();
        }
    }
}
