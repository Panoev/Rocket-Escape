using UnityEngine.SceneManagement;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    AudioSource audioSource;
    
    [SerializeField] AudioClip mainEngine;
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float mainRotation = 10;
    
    [SerializeField] ParticleSystem mainEngineParticle ;
    [SerializeField] ParticleSystem leftThruster; 
    [SerializeField] ParticleSystem rightThruster;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
    ProcessThrust();
    ProcessRotation();  
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
           StartThrusting();   
        }
       else 
        {
           StopThrusting();
        }    
    }
    
    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A)) 
          {
            RotateToRight();
          }
        else if (Input.GetKey(KeyCode.D)) 
          {
            RotateToLeft();
          }
        else
          {
            StopRotating();
          }
    }

    
     void StartThrusting ()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if(!audioSource.isPlaying) 
        {
           audioSource.PlayOneShot(mainEngine);
        }
        if (!mainEngineParticle.isPlaying) 
        {    
           mainEngineParticle.Play();
        }
    }


    void StopThrusting()
    {
        audioSource.Stop();
        mainEngineParticle.Stop();
    }
    
    void RotateToRight () 
    {
       ApplyRotation(mainRotation);
       if(!leftThruster.isPlaying)
       {
         leftThruster.Play();
       }
    }

   
   
    void RotateToLeft ()
    {
       ApplyRotation(-mainRotation);
       if(!rightThruster.isPlaying)
       {
         rightThruster.Play();
       }
    }

   
   
    void StopRotating()
    {
      leftThruster.Stop();
      rightThruster.Stop(); 
    }


   
    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true ;
        transform.Rotate(Vector3.forward  * rotationThisFrame * Time.deltaTime );
        rb.freezeRotation = false ; 
    }


}