using UnityEngine;

public class EnemyProjectileController : MonoBehaviour
{
    public Vector3 velocity;
    public float lifeTimer = 5.0f;
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
        if(collider.gameObject.tag == "Player"){
            Destroy(gameObject);
        }
        if(collider.gameObject.tag == "Tree"){
            Destroy(gameObject);
        }
    }
}
