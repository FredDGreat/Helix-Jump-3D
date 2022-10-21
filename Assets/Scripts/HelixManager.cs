using UnityEngine;

public class HelixManager : MonoBehaviour
{
    public GameObject[] helixRings;
    public float ySpawn = 0;
    public float ringDistance = 5;
    // Start is called before the first frame update

    public float numberOfRings;
    //public GameObject lastRing;
    void Start()
    {
        numberOfRings = GameManager.currentLevelIndex + 5;
        //spwan rings
        for (int i = 0; i < numberOfRings; i++)
        {
            //add -1 to helixRings length to make sure it doesn't include last ring while spawning 
            if (i == 0) SpawnRing(0);
            else SpawnRing(Random.Range(1, helixRings.Length-1));
        }
        //spawn the last ring
        SpawnRing(helixRings.Length - 1);
    }

    public void SpawnRing(int ringIndex)
    {
        GameObject go = Instantiate(helixRings[ringIndex], transform.up * ySpawn, Quaternion.identity);
        go.transform.parent = transform;
        ySpawn -= ringDistance;
    }
}
