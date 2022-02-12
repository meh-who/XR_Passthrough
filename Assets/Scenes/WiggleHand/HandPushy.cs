using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HandPushy : MonoBehaviour
{
    [SerializeField] private Transform _palm;
    private Transform _cube;
    private bool _isFacing;
    // Start is called before the first frame update
    void Start()
    {
        _cube = transform.Find("Square").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.name != _palm.name) return;

            float dist = Vector3.Distance(transform.position, other.transform.position);
            //Debug.Log(dist);
        _cube.DOScaleZ(.2f/dist, .1f);

    }


    private void CheckFacing()
    {
        Vector3 forward = _palm.TransformDirection(Vector3.forward); //check palm facing
        Vector3 toOther = (transform.position - _palm.position).normalized; //check relative orientation bt cube and palm

        // check if hand palm is facing towards head
        _isFacing = Vector3.Dot(forward, toOther) > 0.1f;
    }
}
