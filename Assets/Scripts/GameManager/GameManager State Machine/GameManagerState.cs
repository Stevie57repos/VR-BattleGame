using UnityEngine;

public abstract class GameManagerState 
{
    // Start is called before the first frame update
    public abstract void Start(GameManager_BS gameManager );

    public abstract void EnterState(GameManager_BS gameManager);

    public abstract void Update(GameManager_BS gameManager);

    //public abstract void ButtonActivation(GameManager_BS gameManager);
    
    
    

}
