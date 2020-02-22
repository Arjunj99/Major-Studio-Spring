using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PickUpManager {
    public static int Fruits;
    public enum PickUpType { Fruits }

    public static int getFruits() {
        return Fruits;
    }
}
