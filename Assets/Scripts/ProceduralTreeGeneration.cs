using UnityEngine;

public class ProceduralTreeGeneration : MonoBehaviour
{
    public GameObject Tree1;
    public GameObject Tree2;
    public GameObject Tree3;  // make sure this is palm tree (so it can be spcifically made taller)
    public float mapLength = 50.0f;
    public float mapwidth = 50.0f;
    public int treesPerQuarter = 3;
    public int clustersPerQuarter = 1;
    public int minTreesPerCluster = 1;
    public int maxTreesPerCluster = 2;
    public int clusterRadius = 3;
    public float midTreeMinSize = 1.1f;
    public float midTreeMaxSize = 1.3f;
    public float surroundingTreeMinSize = 0.75f;
    public float surroundingTreeMaxSize = 1.0f;
    public float playerProtectionSquareSize = 2;

    // Start is called before the first frame update
    void Start()
    {
        playerProtectionSquareSize = playerProtectionSquareSize/2f;

        for(int quadrant = 0; quadrant < 4; quadrant++){
            for(int i = 0; i < treesPerQuarter; i++) {
                Vector3 randomPos = new Vector3(0,0,0);
                if(quadrant == 0){
                    randomPos.x = Random.Range(-mapwidth/4, mapwidth/4) - mapwidth/4;
                    randomPos.z = Random.Range(-mapLength/4, mapLength/4) - mapLength/4;
                }
                else if (quadrant == 1){
                    randomPos.x = Random.Range(-mapwidth/4, mapwidth/4) - mapwidth/4;
                    randomPos.z = Random.Range(-mapLength/4, mapLength/4) + mapLength/4;
                }
                else if (quadrant == 2){
                    randomPos.x = Random.Range(-mapwidth/4, mapwidth/4) + mapwidth/4;
                    randomPos.z = Random.Range(-mapLength/4, mapLength/4) - mapLength/4;
                }
                else {
                    randomPos.x = Random.Range(-mapwidth/4, mapwidth/4) + mapwidth/4;
                    randomPos.z = Random.Range(-mapLength/4, mapLength/4) + mapLength/4;
                }
                float tree = Random.Range(0.0f, 3.0f);
                if(tree <= 1 && (randomPos.x > playerProtectionSquareSize || randomPos.x < -playerProtectionSquareSize) && (randomPos.z > playerProtectionSquareSize || randomPos.z < -playerProtectionSquareSize)){
                    complexSpawn(Tree1, randomPos, 1.0f, 0.0f);
                }
                else if (tree <= 2 && (randomPos.x > playerProtectionSquareSize || randomPos.x < -playerProtectionSquareSize) && (randomPos.z > playerProtectionSquareSize || randomPos.z < -playerProtectionSquareSize)){
                    complexSpawn(Tree2, randomPos, 1.0f, 0.0f);
                }
                else if ((randomPos.x > playerProtectionSquareSize || randomPos.x < -playerProtectionSquareSize) && (randomPos.z > playerProtectionSquareSize || randomPos.z < -playerProtectionSquareSize)){
                    complexSpawn(Tree3, randomPos, 1.5f, 0.0f);
                }
            }
            for(int i = 0; i < clustersPerQuarter; i++) {
                // find cluster center
                Vector3 randomPos = new Vector3(0,0,0);
                if(quadrant == 0){
                    randomPos.x = Random.Range(-mapwidth/4, mapwidth/4) - mapwidth/4 + clusterRadius;
                    randomPos.z = Random.Range(-mapLength/4, mapLength/4) - mapLength/4 + clusterRadius;
                }
                else if (quadrant == 1){
                    randomPos.x = Random.Range(-mapwidth/4, mapwidth/4) - mapwidth/4 + clusterRadius;
                    randomPos.z = Random.Range(-mapLength/4, mapLength/4) + mapLength/4 - clusterRadius;
                }
                else if (quadrant == 2){
                    randomPos.x = Random.Range(-mapwidth/4, mapwidth/4) + mapwidth/4 - clusterRadius;
                    randomPos.z = Random.Range(-mapLength/4, mapLength/4) - mapLength/4 + clusterRadius;
                }
                else {
                    randomPos.x = Random.Range(-mapwidth/4, mapwidth/4) + mapwidth/4 - clusterRadius;
                    randomPos.z = Random.Range(-mapLength/4, mapLength/4) + mapLength/4 - clusterRadius;
                }
                // spawn the mid largest tree
                float tree = Random.Range(0.0f, 3.0f);
                float size = Random.Range(midTreeMinSize, midTreeMaxSize);
                float rotation = Random.Range(0,360);
                if(tree <= 1 && (randomPos.x > playerProtectionSquareSize || randomPos.x < -playerProtectionSquareSize) && (randomPos.z > playerProtectionSquareSize || randomPos.z < -playerProtectionSquareSize)){
                    complexSpawn(Tree1, randomPos, size, rotation);
                }
                else if (tree <= 2 && (randomPos.x > playerProtectionSquareSize || randomPos.x < -playerProtectionSquareSize) && (randomPos.z > playerProtectionSquareSize || randomPos.z < -playerProtectionSquareSize)){
                    complexSpawn(Tree2, randomPos, size, rotation);
                }
                else if((randomPos.x > playerProtectionSquareSize || randomPos.x < -playerProtectionSquareSize) && (randomPos.z > playerProtectionSquareSize || randomPos.z < -playerProtectionSquareSize)){
                    complexSpawn(Tree3, randomPos, size, rotation);
                }
                int treesPerCluster = Random.Range(minTreesPerCluster, maxTreesPerCluster + 1);
                // spawns the surrounding trees (all the same)
                for(int j = 0; j < treesPerCluster; j++) {
                    Vector2 localChildPos = Random.insideUnitCircle * clusterRadius;
                    Vector3 globalChildPos = new Vector3(0,0,0);
                    globalChildPos.x = localChildPos.x + randomPos.x;
                    globalChildPos.z = localChildPos.y + randomPos.z;
                    size = Random.Range(surroundingTreeMinSize, surroundingTreeMaxSize);
                    rotation = Random.Range(0,360);
                    if(tree <= 1 && (globalChildPos.x > playerProtectionSquareSize || globalChildPos.x < -playerProtectionSquareSize) && (globalChildPos.z > playerProtectionSquareSize || globalChildPos.z < -playerProtectionSquareSize)){
                        complexSpawn(Tree1, globalChildPos, size, rotation);
                    }
                    else if (tree <= 2 && (globalChildPos.x > playerProtectionSquareSize || globalChildPos.x < -playerProtectionSquareSize) && (globalChildPos.z > playerProtectionSquareSize || globalChildPos.z < -playerProtectionSquareSize)){
                        complexSpawn(Tree2, globalChildPos, size, rotation);
                    }
                    else if((globalChildPos.x > playerProtectionSquareSize || globalChildPos.x < -playerProtectionSquareSize) && (globalChildPos.z > playerProtectionSquareSize || globalChildPos.z < -playerProtectionSquareSize)){
                        complexSpawn(Tree3, globalChildPos, size, rotation);
                    }
                }
            }
        }
    }

    private void complexSpawn(GameObject tree, Vector3 position, float size, float rotation) {
        GameObject newTree = Instantiate(tree, position, Quaternion.identity);
        newTree.transform.localScale = new Vector3(size,size,size);
        newTree.transform.Rotate(0,rotation,0);
    }
}
