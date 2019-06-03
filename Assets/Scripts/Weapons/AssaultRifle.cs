public class AssaultRifle : Weapon
{
    //this method overrides base properties and took me week to figure out :'(
    //Also I didn't understant coroutines, then I've though that I did, then that I didn't. After whole Saturday I realized reloadTime was 0 the whole time. lesson: Don't be like me.

    //!!!!reset the component in inspector when changing those values!!!! 
    public void Reset()
    {
        velocity = 10f;
        rateOfFire = 0.2f;
        maxMagazine = 28f;
        currentMagazine = maxMagazine;
        reloadTime = 2.0f;
    }
}