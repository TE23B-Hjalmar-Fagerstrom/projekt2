namespace EnemyAndCharacter
{
    class Enemy
    {
        public int EnemyHealth = 50;
        public int EnemyDamage = 1;
        public int EnemyGold = 15;
        public int EnemyGoldSpent = 0;
    }

    class EnemyCharacter
    {
        public int characterHealth = 100;
        public int characterDamage = 5;
        public int characterArmor = 0;
        public int characterEvadeProbability = Random.Shared.Next(1,101);
        public int characterEvadeChance = 5;
    }
}