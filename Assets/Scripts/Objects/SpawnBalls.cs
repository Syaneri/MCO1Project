using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBalls : MonoBehaviour
{
    [SerializeField] private GameObject template;
    [SerializeField] private List<GameObject> objectList;

    // Start is called before the first frame update
    void Start() {
        this.OnSpawnEvent();
        EventBroadcaster.Instance.AddObserver(EventNames.ON_START_SPAWN_BALLS, this.OnSpawnEvent);    
    }

    private void OnDestroy() {
        EventBroadcaster.Instance.RemoveObserver(EventNames.ON_START_SPAWN_BALLS);    
    }

    // Update is called once per frame
    void Update() {

    }

    private void OnSpawnEvent() {
        for (int index=0; index<35; index++) {
            GameObject copy = GameObject.Instantiate(template, this.transform);
            copy.SetActive(true);
            this.objectList.Add(copy);
        }
    }
}
