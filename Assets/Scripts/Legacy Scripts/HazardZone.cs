using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardZone {
    public enum hazardType { whirlpool, storm, shark };
    public hazardType type;
    public Vector3 position;
    public Vector3 scale;

    public HazardZone(hazardType type, Vector3 position, Vector3 scale) {
        this.type = type;
        this.position = position;
        this.scale = scale;
    }
}
