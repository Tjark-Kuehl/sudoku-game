using System.Collections.Generic;
using System.IO;
using Sudoku.Model;

namespace Sudoku.Control
{
    public class PlayerManager
    {
        private const string SAVE_FILE = "players.dat";

        public HashSet<Player> Players { get; private set; } = new HashSet<Player>()
        {
            new Player("a"),
            new Player("b"),
            new Player("c"),
            new Player("d"),
            new Player("e")
        };

        public PlayerManager()
        {
            if (!File.Exists(SAVE_FILE))
            {
                SavePlayer();
                return;
            }

            using BinaryReader reader = new BinaryReader(
                new FileStream(SAVE_FILE, FileMode.OpenOrCreate));

            while (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                Players.Add(new Player(reader));
            }
        }

        public void SavePlayer()
        {
            using BinaryWriter writer = new BinaryWriter(
                new FileStream(SAVE_FILE, FileMode.Create));
            foreach (var player in Players)
            {
                player.Save(writer);
            }
        }

        public void RegisterPlayer(Player player)
        {
            Players.Add(player);
            SavePlayer();
        }
    }
}
