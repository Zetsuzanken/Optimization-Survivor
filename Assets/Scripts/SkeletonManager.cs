using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonManager : MonoBehaviour
{
    public Skeleton SkeletonPrefab;
    public static SkeletonManager Instance;
    public int numberOfSkeletons = 300;

    private Stack<Skeleton> skeletonPool = new Stack<Skeleton>();
    private Player player;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        player = FindObjectOfType<Player>();

        for (int i = 0; i < numberOfSkeletons; i++)
        {
            Skeleton s = GameObject.Instantiate<Skeleton>(SkeletonPrefab, Vector3.zero, Quaternion.identity);
            skeletonPool.Push(s);
            s.gameObject.SetActive(false);
        }
    }

    public void Spawn(Transform spawnTransform)
    {
        if (skeletonPool.Count > 0)
        {
            Skeleton s = skeletonPool.Pop();
            s.transform.position = spawnTransform.position;
            s.Init(player);
            s.gameObject.SetActive(true);
        }
        else
        {
            Skeleton s = GameObject.Instantiate<Skeleton>(SkeletonPrefab, spawnTransform.position, Quaternion.identity);
            s.Init(player);
        }
    }
}
