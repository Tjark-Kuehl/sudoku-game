using System;

public class Class1
{
	public Class1()
	{

    public abstract class GameDifficulty
    {
        protected GameDifficulty()
        {

        }

        public abstract int EmptyFieldCount { get; }

        public sealed class Easy : GameDifficulty
        {
            public override int EmptyFieldCount
            {
                get { return 40; }
            }
            public static readonly Easy Default = new Easy();
        }

        public sealed class Medium : GameDifficulty
        {
            public override int EmptyFieldCount
            {
                get { return 50; }
            }
            public static readonly Medium Default = new Medium();
        }

        public sealed class Hard : GameDifficulty
        {
            public override int EmptyFieldCount
            {
                get { return 60; }
            }
            public static readonly Hard Default = new Hard();
        }
    }
}
