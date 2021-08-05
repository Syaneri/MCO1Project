using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventBroadcaster
{
    private static EventBroadcaster sharedInstance;
    private Dictionary<string, ObserverList> eventObservers;

    public static EventBroadcaster Instance {
        get {
            if (sharedInstance == null) {
                sharedInstance = new EventBroadcaster();
            }
            return sharedInstance;
        }
    }

    private EventBroadcaster() {
        this.eventObservers = new Dictionary<string, ObserverList>();
    }

    public void PrintObservers() {

		int totalEvents = 0;

		foreach (ObserverList observer in this.eventObservers.Values) {
			totalEvents += observer.GetListenerLength();
		}

		Debug.LogWarning("TOTAL OBSERVER LENGTH: " +totalEvents);

		foreach (KeyValuePair<string, ObserverList> keyValue in this.eventObservers) {
			Debug.LogWarning(keyValue.Key + " length: " + keyValue.Value.GetListenerLength());
		}
	}

    public void AddObserver(string notificationName, System.Action<Parameters> action) {
		if (this.eventObservers.ContainsKey(notificationName)) {
			ObserverList eventObserver = this.eventObservers[notificationName];
			eventObserver.AddObserver(action);
		}
		else {
			ObserverList eventObserver = new ObserverList();
			eventObserver.AddObserver(action);
			this.eventObservers.Add(notificationName,eventObserver);
		}
	}

    public void AddObserver(string notificationName, System.Action action) {

		//if there is already an existing key, add the listener to the observer list
		if(this.eventObservers.ContainsKey(notificationName)) {
			ObserverList eventObserver = this.eventObservers[notificationName];
			eventObserver.AddObserver(action);
		}
		//create a new instance of an observer list
		else {
			ObserverList eventObserver = new ObserverList();
			eventObserver.AddObserver(action);
			this.eventObservers.Add(notificationName,eventObserver);
		}
	}

    public void RemoveObserver(string notificationName) {
		if(this.eventObservers.ContainsKey(notificationName)) {
			ObserverList eventObserver = this.eventObservers[notificationName];
			eventObserver.RemoveAllObservers();
			this.eventObservers.Remove(notificationName);
		}
	}

    public void RemoveActionAtObserver(string notificationName, System.Action action) {
		if(this.eventObservers.ContainsKey(notificationName)) {
			ObserverList eventObserver = this.eventObservers[notificationName];
			eventObserver.RemoveObserver(action);
		}
	}

    public void RemoveActionAtObserver(string notificationName, System.Action<Parameters> action) {
		if(this.eventObservers.ContainsKey(notificationName)) {
			ObserverList eventObserver = this.eventObservers[notificationName];
			eventObserver.RemoveObserver(action);
		}
	}

    public void RemoveAllObservers() {
		foreach(ObserverList eventObserver in this.eventObservers.Values) {
			eventObserver.RemoveAllObservers();
		}

		this.eventObservers.Clear();
	}

    public void PostEvent(string notificationName) {
		if(this.eventObservers.ContainsKey(notificationName)) {
			ObserverList eventObserver = this.eventObservers[notificationName];
			eventObserver.NotifyObservers();
		}
	}

    public void PostEvent(string notificationName, Parameters parameters) {
		if(this.eventObservers.ContainsKey(notificationName)) {
			ObserverList eventObserver = this.eventObservers[notificationName];
			eventObserver.NotifyObservers(parameters);
		}

	}
}
