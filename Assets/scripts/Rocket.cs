using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    #region classFields
    [Header("M" +
        "ovement Config")]
    [Range(200f, 350f)]
    [SerializeField] float rot_speed = 200f;

    private Rigidbody rocketRB;
    private AudioSource rocketAS;
    private float maxRocketVolume;
    #endregion

    // Start is called before the first frame update
    void Awake()
    {
        rocketRB = GetComponent<Rigidbody>();
        rocketAS = GetComponent<AudioSource>();
        maxRocketVolume = rocketAS.volume;
    }

    // Update is called once per frame
    void Update()
    {
        Thrust();
        Rotate();
    }

    #region movement
    void Rotate() {
        rocketRB.freezeRotation = true;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
            transform.Rotate(Vector3.forward * Time.deltaTime * rot_speed);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(-Vector3.forward * Time.deltaTime * rot_speed);
        }
        rocketRB.freezeRotation = false;
    }

    void Thrust() {
        if (Input.GetKey(KeyCode.Space)) {
            rocketRB.AddRelativeForce(Vector3.up);
            if (rocketAS.volume < maxRocketVolume) //so it doesn't layer
            {
                //Stop the audio if it's not fully faded out yet
                rocketAS.Stop();
                if (!rocketAS.isPlaying)
                {
                    //Set volume back to max and play the sound
                    rocketAS.volume = maxRocketVolume;
                    rocketAS.Play();
                }

            }

        }
        else
        {
            //When the spacebar isn't being pressed, this is a check for if the sound is above 0
            if (rocketAS.volume > 0)
            {
                //If above 0, then multiply it down by the gametime - this will fade it out.
                //-0.9 seems to be about the perfect multiplier. Too large and it will always clip out. Too small and you'll always clip
                rocketAS.volume -= 0.9F * Time.deltaTime;
            }
            //Stop the sound completely if the sound has reduced enough
            else if (rocketAS.volume == 0)
            {
                rocketAS.Stop();
            }
        }
    }
    #endregion

    void ResetLevel() {
        SceneManager.LoadScene(0);
    }

    void NextLevel() {
        // TODO: add more levels
        SceneManager.LoadScene(1);
    }


    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag) {
            case "friendly":break;
            case "end":
                NextLevel();
                break;
            default:
                ResetLevel();
                break;
        }
    }
}
