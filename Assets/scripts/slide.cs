using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slide : MonoBehaviour
{
    [SerializeField] int period = 2;
    [SerializeField] Vector3 movementVector = new Vector3(0,0,.05f);
    private int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        const float tau = Mathf.PI * 2f;
        float cycle = Time.time / period;
        float rawSinWave = Mathf.Sin(cycle * tau);
        float movementFactor = rawSinWave / 2f;
        Vector3 offset = movementFactor * movementVector;
        transform.position += offset;
        index++;
    }
}
