using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float Speed = 1.0f;
    public Vector3 MoveDirection = Vector3.zero;
    public float Lifetime = 2.0f;

    public ParticleSystem ExplosionSystem;

    private float startTime = 0;
    private bool hasExploded = false;

    private void Start()
    {
        startTime = Time.time;
        GetComponent<ParticleSystem>().Play();
        ExplosionSystem.Stop();
    }

    void Update()
    {
        print("explosion");
        if (!hasExploded)
        {
            transform.position += MoveDirection * Time.deltaTime * Speed;
            float elapsed = (Time.time - startTime + 0.3f) / (Lifetime + 0.3f);
            transform.position = new Vector3(
                transform.position.x, 
                Mathf.Sin(elapsed * Mathf.PI), 
                transform.position.z);

            if (Time.time > startTime + Lifetime)
            {
                Explode();
            }
        } 
        else
        {
            if (!ExplosionSystem.isPlaying)
            {
                GameObject.Destroy(this.gameObject);
            }
        }
    }

    void Explode()
    {
        GetComponent<ParticleSystem>().Stop();
        ExplosionSystem.Play();
        hasExploded = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasExploded) return;

        Skeleton Skeleton = other.GetComponent<Skeleton>();
        if (Skeleton != null)
        {
            Explode();
            Skeleton.Kill();
        }
    }


}
