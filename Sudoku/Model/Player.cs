using System;
using System.IO;

namespace Sudoku.Model
{
    /// <summary>
    ///     A player.
    /// </summary>
    public class Player
    {
        /// <summary>
        ///     Gets the identifier.
        /// </summary>
        /// <value>
        ///     The identifier.
        /// </value>
        public Guid ID { get; }
        /// <summary>
        ///     Gets the name.
        /// </summary>
        /// <value>
        ///     The name.
        /// </value>
        public string Name { get; }
        /// <summary>
        ///     Gets or sets the score.
        /// </summary>
        /// <value>
        ///     The score.
        /// </value>
        public int Score { get; set; }
        /// <summary>
        ///     Gets or sets the number of games.
        /// </summary>
        /// <value>
        ///     The number of games.
        /// </value>
        public int GameCount { get; set; }
        /// <summary>
        ///     Gets or sets the playtime.
        /// </summary>
        /// <value>
        ///     The playtime.
        /// </value>
        public int Playtime { get; set; }
        /// <summary>
        ///     Gets the Date/Time of the created.
        /// </summary>
        /// <value>
        ///     The created.
        /// </value>
        public DateTime Created { get; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Player"/> class.
        /// </summary>
        /// <param name="name"> The name. </param>
        public Player(string name)
        {
            ID = Guid.NewGuid();
            Name = name;
            Score = 0;
            GameCount = 0;
            Playtime = 0;
            Created = DateTime.Now;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Player"/> class.
        /// </summary>
        /// <param name="reader"> The reader. </param>
        public Player(BinaryReader reader)
        {
            ID = new Guid(reader.ReadBytes(16));
            Name = reader.ReadString();
            GameCount = reader.ReadInt32();
            Score = reader.ReadInt32();
            Playtime = reader.ReadInt32();
            Created = new DateTime(reader.ReadInt64());
        }

        /// <summary>
        ///     Saves the given writer.
        /// </summary>
        /// <param name="writer"> The writer to save. </param>
        public void Save(BinaryWriter writer)
        {
            writer.Write(ID.ToByteArray());
            writer.Write(Name);
            writer.Write(GameCount);
            writer.Write(Score);
            writer.Write(Playtime);
            writer.Write(Created.Ticks);
        }
    }
}
