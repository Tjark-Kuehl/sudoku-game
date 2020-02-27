using System;
using System.IO;

namespace Sudoku.Model
{
    public class Player
    {
        public Guid ID { get; }
        public string Name { get; }
        public int GameCount { get; set; }
        public int Score { get; set; }
        public int Playtime { get; set; }
        public DateTime Created { get; }

        public Player(string name)
        {
            ID = Guid.NewGuid();
            Name = name;
            Score = 0;
            GameCount = 0;
            Playtime = 0;
            Created = DateTime.Now;
        }

        public Player(BinaryReader reader)
        {
            ID = new Guid(reader.ReadBytes(16));
            Name = reader.ReadString();
            GameCount = reader.ReadInt32();
            Score = reader.ReadInt32();
            Playtime = reader.ReadInt32();
            Created = new DateTime(reader.ReadInt64());
        }

        public void Save(BinaryWriter writer)
        {
            writer.Write(ID.ToByteArray());
            writer.Write(Name);
            writer.Write(GameCount);
            writer.Write(Score);
            writer.Write(Playtime);
            writer.Write(Created.Ticks);
        }

        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is Player other)
            {
                return ID == other.ID;
            }
            return obj?.Equals(this) ?? false;
        }
    }
}
