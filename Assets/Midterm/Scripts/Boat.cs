public class Boat {
    private string name;
    private float currentSpeed;
    private bool inMotion;
    private float currentRotation;

    public Boat() { // Empty Constructor for Boat Class
        this.name = null;
        currentSpeed = 0;
        inMotion = false;
        currentRotation = 0;
    }
    
    public Boat(string name) { // In case I wanted to Name a Boat (Ended up Unused)
        this.name = name;
        currentSpeed = 0;
        inMotion = false;
        currentRotation = 0;
    }

    // Getters and Setters
    public float GetCurrentSpeed() {
        return currentSpeed;
    }

    public void SetCurrentSpeed(float speed) {
        currentSpeed = speed;
    }

    public bool GetInMotion() {
        return inMotion;
    }

    public void SetInMotion(bool motion) {
        inMotion = motion;
    }

    public float GetCurrentRotation() {
        return currentRotation;
    }

    public void SetCurrentRotation(float speed) {
        currentRotation = speed;
    }
}
