using UnityEngine;
using System.Collections;

public class PowerUpScript : MonoBehaviour {

    HUDScript HUD;
    GameObject camera;
    void Start()
    {
        camera = GameObject.Find("Main Camera");
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            HUD = camera.GetComponent<HUDScript>();
            HUD.IncreaseScore(10);
            Destroy(this.gameObject);
        }
    }
}
