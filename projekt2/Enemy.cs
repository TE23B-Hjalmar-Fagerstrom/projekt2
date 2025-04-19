namespace EnemyAndCharacter
{
    public class Enemy
    {
        public int EnemyHealth = 50;
        public int EnemyDamage = 1;
        public int EnemyGold = 15;
        public int EnemyGoldSpent = 0;
    }

    public class EnemyCharacter
    {
        public int CharacterHealth = 100;
        public int CharacterMaxHealth = 100;
        public int CharacterDamage = 5;
        public int CharacterArmor = 0;
        public int CharacterEvadeProbability => Random.Shared.Next(1,101);
        public int CharacterEvadeChance = 5;
    }
}