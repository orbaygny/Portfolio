// Extension method to start a coroutine on a MonoBehaviour instance.
public static void StartCoroutine(this IEnumerator coroutine, MonoBehaviour mono)
{
    mono.StartCoroutine(coroutine);
}

// Extension method to start a coroutine after a delay asynchronously.
public static async void InvokeCoroutine(this IEnumerator coroutine, float delay, MonoBehaviour mono)
{
    delay *= 1000; // convert to milliseconds
    int _delay = (int)delay;
    await Task.Delay(_delay);
    mono.StartCoroutine(coroutine);
}

// Removes specified axes (X, Y, Z) from a Vector3 by zeroing them out.
// Useful for restricting movement or calculations to specific axes.
public static Vector3 RemoveAxis(this Vector3 vector,
    bool removeY = false,
    bool removeX = false,
    bool removeZ = false)
{
    if (removeX) vector.x = 0;
    if (removeY) vector.y = 0;
    if (removeZ) vector.z = 0;
    return vector;
}

// Checks distance between two vectors with options for inclusive/exclusive bounds.
// Useful for range checks or proximity triggers.
public static bool CheckDistance(this Vector3 thisVector,
    Vector3 targetVector,
    float desiredDistance,
    bool lowerThanDesired = true,
    bool desiredInclude = true)
{
    var distance = Vector3.Distance(thisVector, targetVector);

    if (lowerThanDesired)
    {
        return desiredInclude ? distance <= desiredDistance : distance < desiredDistance;
    }
    else
    {
        return desiredInclude ? distance >= desiredDistance : distance > desiredDistance;
    }
}
