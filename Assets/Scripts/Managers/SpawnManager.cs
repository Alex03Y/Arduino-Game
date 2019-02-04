using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject Skeleton;
    public Transform Target;
    public List<Transform> SpawnPoints;
    
    [Space]
    public int WaveSizePerPoint = 5;
    public float InWaveDelay = 0.2f;
    public float WavesDelay = 5f;

    [Space]
    public int MaxEnemiesCount = 30;
    public List<GameObject> SpawnedObject = new List<GameObject>();

    private void Awake()
    {
        StartCoroutine(SpawnWaves());
    }
    
    IEnumerator SpawnWaves()
    {
        IEnumerator SpawnWave(Transform point)
        {
            for (int i = 0; i < WaveSizePerPoint; i++)
            {
                if (SpawnedObject.Count >= MaxEnemiesCount) break;
                var skeleton           = Instantiate(Skeleton, point.position, Quaternion.identity, transform);
                var skeletonController = skeleton.GetComponentInChildren<SkeletonController>();
                skeletonController.Target = Target;
                skeletonController.Health.OnDeath.AddListener(() =>
                {
                    var index = SpawnedObject.FindIndex(x => x.GetInstanceID().Equals(skeleton.GetInstanceID()));
                    if (index == -1) return;
                    SpawnedObject.RemoveAt(index);
                    Destroy(skeleton, 10f);
                });

                SpawnedObject.Add(skeleton);
                yield return new WaitForSecondsRealtime(InWaveDelay);
            }
        }

        while (true)
        {
            SpawnPoints.ForEach(x => StartCoroutine(SpawnWave(x)));
            yield return new WaitForSecondsRealtime(WavesDelay);
        }
    }
}
