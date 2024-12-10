using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Player : MonoBehaviour
{
    public Fireball FireballPrefab;

    public float Speed = 3f;
    public float RotationSpeed = 5f;

    private Rigidbody _rigidbody;
    private Plane groundPlane;
    private Animator animator;

    private bool isAttackDown = false;
    private Vector3 attackPos = Vector3.zero;
    private Quaternion shootRotation = Quaternion.identity;
    private float shootTime = 0;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        groundPlane = new Plane(Vector3.up, Vector3.zero);
        animator = GetComponentInChildren<Animator>();
        animator.speed = 2;
    }

    void Update()
    {
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Vector3 dir = new Vector3(input.x, 0, input.y);
        transform.position += dir * Speed * Time.deltaTime;


        animator.SetFloat("MoveSpeed", input.magnitude);

        float angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
        Quaternion newRotation = Quaternion.Euler(0, angle, 0);
        float difference = Quaternion.Angle(transform.rotation, newRotation) / 90f;

        if (shootTime > 0)
        {
            newRotation = shootRotation;
        }

        transform.rotation = Quaternion.RotateTowards(transform.rotation, newRotation, Time.deltaTime * RotationSpeed * 500f * Mathf.Max(dir.magnitude, shootTime*6.0f));
        shootTime -= Time.deltaTime;

        animator.SetFloat("Strafe", Mathf.Clamp(difference, -0.5f, 0.5f) + 0.5f);


        //Attack
        if (Input.GetAxis("Fire1") > 0.01f) {
            if (!isAttackDown)
            {
                isAttackDown = true;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                float hitDist = 0.0f;
                if (groundPlane.Raycast(ray, out hitDist))
                {
                    attackPos = ray.GetPoint(hitDist);
                    Vector3 lookDir = (attackPos - transform.position).normalized;
                    Quaternion lookRotation = Quaternion.LookRotation(lookDir);

                    shootRotation = lookRotation;
                    shootTime = 0.2f;

                    Fireball fireball = Instantiate<Fireball>(FireballPrefab, transform.position + Vector3.up * 0.5f, Quaternion.identity);
                    fireball.MoveDirection = lookDir;

                    animator.SetTrigger("Shoot");
                }
            } 
        }
        else
        {
            isAttackDown = false;
        }



    }
}
