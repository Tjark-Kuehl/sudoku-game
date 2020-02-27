using System.Collections.Generic;
using System.IO;
using Sudoku.Model;

namespace Sudoku.Control
{
    public class PlayerManager
    {
        private const string SAVE_FILE = "players.dat";

        public HashSet<Player> Players { get; } = new HashSet<Player>();

        public PlayerManager()
        {
            if (!File.Exists(SAVE_FILE)) return;

            using var fs = new FileStream(SAVE_FILE, FileMode.Open);
            using var reader = new BinaryReader(fs);
            int pc = reader.ReadInt32();
            for (int i = 0; i < pc; i++)
            {
                Players.Add(new Player(reader));
            }
        }

        public void SavePlayer()
        {
            using var fs = new FileStream(SAVE_FILE, FileMode.Create);
            using var writer = new BinaryWriter(fs);

            writer.Write(Players.Count);
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
