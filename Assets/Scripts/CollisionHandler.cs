
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    
    [SerializeField] float invokeTime = 1.5f ;
    [SerializeField] AudioClip crash ;
    [SerializeField] AudioClip finish ;

    [SerializeField] ParticleSystem crashParticle ;
    [SerializeField] ParticleSystem finishParticle ;
    
    
    
    
    Movement movement ;
    AudioSource audioSource;

    bool isTransitioning = false ; 
    bool collisionDisabled = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
    }

     void Update() 
    {
      DisableCollision();   
    }

    
    void OnCollisionEnter(Collision other)
    {
        if(isTransitioning || collisionDisabled){return ;}
        
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("You have bumped into a friendly object");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartCrashSequence();
                 
                break;
        }
    }
    
    void StartCrashSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        crashParticle.Play();
        GetComponent<Movement>().enabled = false ;
        Invoke("ReloadLevel", invokeTime);
        audioSource.PlayOneShot(crash);
        
    }

    void StartSuccessSequence()
    {
       isTransitioning = true ;
       audioSource.Stop();
       finishParticle.Play();
       GetComponent<Movement>().enabled = false ;
       Invoke("NextLevel", invokeTime);
       audioSource.PlayOneShot(finish);
      
    }
    
    
    
    void NextLevel() 
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1 ;
        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }            


    void DisableCollision()
    {
        if(Input.GetKey(KeyCode.L))
        {
            NextLevel();
        }
        else if(Input.GetKey(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled;
        }
    }


}





