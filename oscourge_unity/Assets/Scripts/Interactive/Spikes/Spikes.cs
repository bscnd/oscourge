using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : Mechanism
{
    private Animator myAnim;

    // The number of pressurePlates/levers/... that has to be activated to trigger this gameobject
    public int activatorNumber = 1;
    public GameManager gameManager;

    // Set of currently activated activator's id
    private SortedSet<int> activators;
    private SortedSet<int> deactivators;

    private Collider2D spikesCollider;

    int hashIsTriggered = Animator.StringToHash("isTriggered");


    public Spikes() {
        this.activators = new SortedSet<int>();
        this.deactivators = new SortedSet<int>();
    }

    void Start() {
        this.myAnim = GetComponent<Animator>();
        this.spikesCollider = GetComponent<Collider2D>();
        updateSpikes();
    }

    public void updateSpikes() {
        int currentNumberActivator = activators.Count - deactivators.Count;
        myAnim.SetBool(hashIsTriggered, currentNumberActivator >= activatorNumber);
    }

    public void trigger(bool isActivate, int objectId) {
        if (isActivate) {
            activators.Add(objectId);
            deactivators.Remove(objectId);
        }
        else {
            deactivators.Add(objectId);
            activators.Remove(objectId);
        }

        if (myAnim != null) {
            updateSpikes();
        }
    }

    public void disableCollider() {
        this.spikesCollider.enabled = false;
    }

    public void enableCollider() {
        this.spikesCollider.enabled = true;
    }

    public void changeColliderState() {
        this.spikesCollider.enabled = !this.spikesCollider.enabled;
    }

    public override GameObject initTriggerIndicator() {
        if (this.indicator == null) {
            this.indicator = TriggerIndicator.spawn(this.transform.position, new Vector3(0, -0.25F, 0));
        }
        return this.indicator;
    }

    public void OnTriggerEnter2D(Collider2D col) {

        if (col.gameObject.CompareTag("Player") && spikesCollider.enabled) {
            if (gameManager != null)
                gameManager.GameOver();
            else Debug.LogError("The game manager has not been defined on this spike");
        }

    }
}
