
public class Health
{
   private int health;
   private int healthMax;
   public Health(int healthMax){
        this.healthMax = healthMax;
    }
    public int GetHealth(){
        return health;
    }
    public void Damage(int damageAmount){
		health -= damageAmount;
        if(health<0){
            health = 0;
        }
	}
}
