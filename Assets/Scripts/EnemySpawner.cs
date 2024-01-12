using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    // public EnemyController Enemy1;
    public EnemyController Enemy1;
    public PlayerController target;
    public float mapLength = 50.0f;
    public float mapwidth = 50.0f;
    public float spawnTimer = 10.0f;
    private float insideTimer;
    public LootDropper lootData;

    public StatDisplayer stats;

    void Start()
    {
        insideTimer = spawnTimer;
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer -= Time.deltaTime;
        if(spawnTimer <= 0 && !target.IsDead()){
            Vector3 randomPos = new Vector3(0,2.19f,0);
            randomPos.x = Random.Range(-mapwidth/2, mapwidth/2);
            randomPos.z = Random.Range(-mapLength/2, mapLength/2);
            EnemyController newEnemy = Instantiate<EnemyController>(Enemy1);
            newEnemy.Setup(lootData, randomPos, target.transform, stats.GetProjectileEnemyTakeDamage(), stats.GetSpellEnemyTakeDamage());
            spawnTimer = insideTimer;
        }
    }
}
