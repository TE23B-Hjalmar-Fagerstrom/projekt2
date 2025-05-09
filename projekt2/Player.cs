namespace PlayerAndCharacter
{
    public class Player // alla spelarens variable 
    {
        public int PlayerHealth = 50;
        public int PlayerDamage = 1;
        public int PlayerGold = 15;
        public int PlayerGoldSpent = 0;
        public int hasSeenTutorial = -1;
    }
    
    public class PlayerCharacter // alla spelarkaraktärens variabler
    {
        public int CharacterHealth = 100;
        public int CharacterMaxHealth = 100;
        public int CharacterDamage = 5;
        public int CharacterArmor = 0;
        public int CharacterEvadeProbability => Random.Shared.Next(1, 101);
        public int CharacterEvadeChance = 5;
    }
}