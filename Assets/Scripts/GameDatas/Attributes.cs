using System;

namespace Assets.Scripts.GameDatas
{
    [Serializable]
    public class Attributes
    {
        public enum AttributeType
        {
            None,
            Strength = 1,
            Dexterity = 2,
            Constitution = 3,
            Intelligence = 4,
            Willpower = 5,
            Speed = 6
        }

        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Constitution { get; set; }
        public int Intelligence { get; set; }
        public int Willpower { get; set; }
        public int Speed { get; set; }

        public void SetAttribute(AttributeType attributeType, int value)
        {
            switch (attributeType)
            {
                case AttributeType.Strength:
                    Strength = value;
                    break;
                case AttributeType.Dexterity:
                    Dexterity = value;
                    break;
                case AttributeType.Constitution:
                    Constitution = value;
                    break;
                case AttributeType.Intelligence:
                    Intelligence = value;
                    break;
                case AttributeType.Willpower:
                    Willpower = value;
                    break;
                case AttributeType.Speed:
                    Speed = value;
                    break;
            }
        }

        public int GetAttribute(AttributeType attributeType)
        {
            int value = 0;

            switch (attributeType)
            {
                case AttributeType.Strength:
                    value = Strength;
                    break;
                case AttributeType.Dexterity:
                    value = Dexterity;
                    break;
                case AttributeType.Constitution:
                    value = Constitution;
                    break;
                case AttributeType.Intelligence:
                    value = Intelligence;
                    break;
                case AttributeType.Willpower:
                    value = Willpower;
                    break;
                case AttributeType.Speed:
                    value = Speed;
                    break;
            }

            return value;
        }
    }
}

