using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineBehaviour : MonoBehaviour
{
    private bool isActive = false;
    public Material activateMaterial;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ActivateMine(2.0f));
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!isActive) {
            return;
        }
        //can also use layers, using tag here since it looks better than layer = 6
        if (other.collider.CompareTag("Enemy") || other.collider.CompareTag("Strong Enemy"))
        {
            Destroy(this.gameObject);
        }
    }

    //wait time seconds till mine is active
    IEnumerator ActivateMine(float time) {
        yield return new WaitForSeconds(time);
        isActive = true;
        GetComponent<Renderer>().material = activateMaterial;
    }

}
