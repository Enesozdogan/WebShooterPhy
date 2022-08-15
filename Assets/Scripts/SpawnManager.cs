using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    public int obstaclesToSpawn=1;
    public GameObject  swatPrefab;
    
    public int swatNumber,swatNumToSpawn=5;
    public Transform[] bulletPositions;
    public GameObject bulletPrefab;
    private GameManager gameManager;
    private float boundx=11;
    
    // Start is called before the first frame update
    void Start()
    {
        gameManager=GameObject.Find("Game Manager").GetComponent<GameManager>();
        StartCoroutine(SpawnRoutine());
       //InvokeRepeating("SpawnObstacles",2f,2f);
       //InvokeRepeating("SpawnEnemyBullets",3f,1f);
    }

    // Update is called once per frame
    void Update()
    {
        swatNumber=FindObjectsOfType<SwatEmptyColl>().Length;
        if(swatNumber<50 && !gameManager.isGameOver){
            SpawnSwat(swatNumToSpawn);
        }
        
    }
    void SpawnObstacles(){
        
        int randomObstacleIndex;
        for(int i=0;i<2;i++){
            randomObstacleIndex=Random.Range(0,obstaclePrefabs.Length);
            Instantiate(obstaclePrefabs[randomObstacleIndex],GenerateRandomPosition(4,10),obstaclePrefabs[randomObstacleIndex].transform.rotation);

        }
        
    }
    void SpawnEnemyBullets(){
        
        for(int i=0; i<3;i++){
            int randomBulletIndex=Random.Range(0,bulletPositions.Length);
            Instantiate(bulletPrefab,bulletPositions[randomBulletIndex].position,bulletPositions[randomBulletIndex].rotation);
        }
       
    }
    public Vector3 GenerateRandomPosition(int boundz1, int boundz2){
        Vector3 randomPos=new Vector3(Random.Range(boundx,-boundx),0.55f,Random.Range(boundz1,boundz2));
        return randomPos;
    }

    IEnumerator SpawnRoutine(){
        while(!gameManager.isGameOver){

            yield return new WaitForSeconds(2f);
            SpawnObstacles();
        }
    }

    void SpawnSwat(int spawnNumber){
        
        GameObject swatObj=ObjectPool.SharedInstance.GetPooledObject();
        if(swatObj!=null){
            swatObj.transform.position=GenerateRandomPosition(12,15);
            swatObj.transform.rotation=Quaternion.identity;
            swatObj.SetActive(true);
            swatObj.GetComponentInChildren<Enemy>().DeactivateRagdoll();
            if(transform.childCount>0)
            transform.GetChild(0).gameObject.SetActive(true);    
        }
        /*
        for(int i=0;i<spawnNumber;i++){
            Instantiate(swatPrefab,GenerateRandomPosition(12,15),Quaternion.identity);
        }
        */
    }
}
