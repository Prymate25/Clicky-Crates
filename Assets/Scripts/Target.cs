using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{   public float minSpeed=12;
    private float maxSpeed=16;
    private float maxTorque=10;
    private float xRange=4;
    public float ySpawnPos=-2;
    private Rigidbody targetRb;
    public GameManager gameManager;
    public int pointValue;
    public ParticleSystem explosionParticle;

    // Start is called before the first frame update
    void Start()
    {
        targetRb=GetComponent<Rigidbody>();
        targetRb.AddForce(RandomForce(),ForceMode.Impulse);
        targetRb.AddTorque(RandomTorque(),RandomTorque(),RandomTorque(),ForceMode.Impulse);
        transform.position=RandomSpawnPos();
        gameManager=GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //private void OnMouseDown(){
        //if(gameManager.isGameActive){
            //Destroy(gameObject);
            //Instantiate(explosionParticle,transform.position,explosionParticle.transform.rotation);
            //gameManager.UpdateScore(pointValue);
        //}
        
    //}
    private void OnTriggerEnter(Collider other){
        Destroy(gameObject);
        if(!gameObject.CompareTag("Bad") && gameManager.isGameActive){
            gameManager.UpdateLives(-1);
            //gameManager.GameOver();
        }
    }
    public void DestroyTarget(){
        if(gameManager.isGameActive){
            Destroy(gameObject);
            Instantiate(explosionParticle,transform.position,explosionParticle.transform.rotation);
            gameManager.UpdateScore(pointValue);
        }
    }
    Vector3 RandomForce(){
        return Vector3.up*Random.Range(minSpeed,maxSpeed);
    }
    float RandomTorque(){
        return Random.Range(-maxTorque,maxTorque);
    }
    Vector3 RandomSpawnPos(){
        return new Vector3(Random.Range(-xRange,xRange),ySpawnPos);
    }
}
