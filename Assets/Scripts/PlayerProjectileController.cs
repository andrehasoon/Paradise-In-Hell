using UnityEngine;

public class PlayerProjectileController : MonoBehaviour {

    public Vector3 velocity;
    public float lifeTimer = 5.0f;

    public void Setup(Vector3 location, Vector3 direction){
        this.transform.position = location;
        velocity = direction;
    }
    // Update is called once per frame
    void Update () {
        this.transform.Translate(velocity * Time.deltaTime);

        // kills the bullet after 5 seconds
        lifeTimer -= Time.deltaTime;
        if(lifeTimer <= 0){
            Destroy(gameObject);
        }
	}

    private void OnTriggerEnter(Collider collider){
        if(collider.gameObject.tag == "Tree"){
            Destroy(gameObject);
        }
        else if (collider.gameObject.tag == "Enemy"){
            Destroy(gameObject);
        }
    }
    
}
