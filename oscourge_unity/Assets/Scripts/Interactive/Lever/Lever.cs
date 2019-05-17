using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour

{
	Animator anim;
	public Trigger[] triggers;

	int hashActivation = Animator.StringToHash("lever_activation");
	int hashDeactivation = Animator.StringToHash("lever_deactivation");

	private enum AnimEvent { Activate, Deactivate, NoEvent };
	private enum State { active, inactive };
	private AnimEvent animEvent;
	private State state;

	// Start is called before the first frame update
	void Start()
	{
		anim = GetComponent<Animator>();
		animEvent = AnimEvent.NoEvent;
		state = State.inactive;
	}

	// Update is called once per frame
	void Update()
	{
		if(this.animEvent == AnimEvent.Activate){
			anim.Play(hashActivation);
			this.animEvent = AnimEvent.NoEvent;
			this.state = State.active;

			foreach(Trigger trigger in triggers){
				trigger.activate();
			}
		}

		else if(this.animEvent == AnimEvent.Deactivate){
			anim.Play(hashDeactivation);
			this.animEvent = AnimEvent.NoEvent;
			this.state = State.inactive;
		}
	}

	public void trigger(){
		if(this.state == State.inactive){
			this.activate();
		}
		else{
			this.deactivate();
		}
	}

	private void activate(){
		this.animEvent = AnimEvent.Activate;
	}

	private void deactivate(){
		this.animEvent = AnimEvent.Deactivate;
	}
}
