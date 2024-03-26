using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractRange : MonoBehaviour
{

    public float radius;
    [SerializeField] SphereCollider collider;

    List<Interactable> interactablesInRange = new List<Interactable>();
    Interactable selectedInteractable;

    Transform followTarget;

    public void setTarget(Transform followTarget)
    {
        this.followTarget = followTarget;

        start();        
    }

    void start()
    {
        collider.radius = radius;

        collider.enabled = true;

        this.enabled = true;

    }

    // Update is called once per frame
    void Update()
    {

        if(followTarget == null)
        {
            this.enabled = false;
            return;
        }
            
        moveToTarget();

        if(Input.GetKeyDown(KeyCode.E))
            tryInteract();

        
    }

    
    void moveToTarget()
    {
        transform.position = followTarget.position;
    }

    void tryInteract()
    {
        if(selectedInteractable == null)
            return;
        
        selectedInteractable.interact();
    }










    ////////////
    //CONTROL
    ////////////

    public void enable()
    {
        collider.enabled = true;
    }

    public void disable()
    {
        collider.enabled = false;
    }

    public void refresh()
    {
        unselectLastInteractable();
        interactablesInRange.Clear();

        disable();
        Invoke("enable", 0.5f);
    }






    ////////////
    //COLLISIONS
    ////////////

    void OnTriggerEnter(Collider hitObj)
    {
        if(hitObj.tag != "Interactable")
            return;

        addToRange(hitObj);

        //Debug.Log($"{hitObj.name} added to range, time: {Time.time}"); //debug
    }

    void OnTriggerExit(Collider hitObj)
    {
        if(hitObj.tag != "Interactable")
            return;

        removeFromRange(hitObj);

        //Debug.Log($"{hitObj.name} removed from range, time: {Time.time}"); //debug
    }










    ////////////
    //FIND INTERACTABLES IN RANGE
    ////////////

    void addToRange(Collider hitObj)
    {
        var interactable = hitObj.GetComponent<Interactable>();

        interactablesInRange.Add(interactable);

        findClosestInteractable();

        //Debug.Log($"{hitObj.name} added to range"); //debug

    }

    void removeFromRange(Collider hitObj)
    {
        var interactable = hitObj.GetComponent<Interactable>();

        removeFromRange(interactable);
        
    }

    public void removeFromRange(Interactable interactable)
    {
        interactablesInRange.Remove(interactable);

        if(interactable == selectedInteractable)
        {
            selectedInteractable = null;
            interactable.hideInteractPrompt();
        }

        findClosestInteractable();
    }

    void findClosestInteractable()
    {
        var interactablesInRangeCopy = new List<Interactable>(interactablesInRange); //to avoid enumeration modified error if new interactables are added/removed in between loop
    
        float closestDist = 100f;

        foreach(var interactable in interactablesInRangeCopy)
        {
            if(distFrom(interactable) < closestDist)
            {
                unselectLastInteractable();

                closestDist = distFrom(interactable);

                selectedInteractable = interactable;

                selectedInteractable.showInteractPrompt();
            }
        }
    }

    public void unselectLastInteractable()
    {
        if(selectedInteractable != null)
        {
            selectedInteractable.hideInteractPrompt();
            selectedInteractable = null;
        }
            
    }

    float distFrom(Interactable interactable)
    {
       return Vector3.Distance(this.transform.position, interactable.transform.position);
    }



}
