using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    
    public float MoveSpeed = 10f;
    public GameObject SkeletonPiecesPrefab;


    private Player player;
    private Rigidbody rigidBody;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    public void Init(Player player)
    {
        this.player = player;
    }

    void Update()
    {
        if (player != null)
        {
            Vector3 direction = (player.transform.position - transform.position).normalized;
       
            transform.position += direction * MoveSpeed * Time.deltaTime;
            transform.rotation = Quaternion.LookRotation(direction);
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            rigidBody.velocity = Vector3.zero;

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Player player = collision.collider.GetComponent<Player>();
        if (player)
        {
            Kill();
        }
    }

    public void Kill()
    {
        Instantiate(SkeletonPiecesPrefab, transform.position, transform.rotation);
        GameObject.Destroy(gameObject);
    }

}
