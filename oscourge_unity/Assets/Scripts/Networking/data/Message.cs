using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Message {
    public const int DATA = 1;
    public const int WAIT = 2;
    public const int ROLE = 3;
    public const int ERROR = 4;
    public const int CONNECTION = 5;
    public const int CONNECTION_ERROR = 6;
    public const int PAUSE = 7;
    public const int HANDSHAKE = 8;
    public const int RESUME = 9;
    public const int RECONNECTION = 10;
    public const int ENDGAME = 11;
    public const int RESTART = 12;

    public string name;
    public string msg;
    public int type;
    public InputValues inputValues;
    public Vector3 position;

    [JsonConstructor]
    public Message (string name, string msg, int type, InputValues inputValues, Vector3 position) {
        this.name = name;
        this.msg = msg;
        this.type = type;
        this.inputValues = inputValues;
        this.position = position;
    }

    public Message (int type) {
        this.type = type;
    }
}
