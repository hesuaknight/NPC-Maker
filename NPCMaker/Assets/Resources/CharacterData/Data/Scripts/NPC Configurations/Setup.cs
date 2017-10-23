using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Setup : ScriptableObject {
    public List<string> config;

    public Setup() {
        config = new List<string>();
        config.Add("none");
    }

}
