using System.Collections.Generic;
using Newtonsoft.Json;
using Oxide.Core;
using Oxide.Core.Plugins;
using Oxide.Core.Libraries.Covalence;

namespace Oxide.Plugins
{
    [Info("PersonalWelcomeMessage", "PapaRust", "1.4.9")]
    [Description("Sends a private message to players when they join the server.")]

    class PersonalWelcomeMessage : CovalencePlugin
    {
        #region Configuration

        private Configuration config;

        public class Configuration
        {
            [JsonProperty("Welcome Message")]
            public string WelcomeMessage { get; set; }

            [JsonProperty("First Time Player Message")]
            public string FirstTimePlayerMessage { get; set; }

            [JsonProperty("Delay Before Sending Message (seconds)")]
            public float Delay { get; set; }
        }

        protected override void LoadDefaultConfig()
        {
            Config.WriteObject(new Configuration
            {
                WelcomeMessage = "Hi {player_name}! Welcome back to {server_name}.",
                FirstTimePlayerMessage = "Hi {player_name}! Welcome to {server_name}. Our ruleset is heavily enforced and bans are issued daily. Ignorance is not an excuse, please read the rules and understand them BEFORE you begin playing: https://wolfrust.gg/#rules or Discord.GG/lonewolf | THANK YOU FOR PLAYING!",
                Delay = 5.0f
            }, true);
        }

        private void Init()
        {
            config = Config.ReadObject<Configuration>();
        }

        #endregion

        #region Data Management

        private StoredData storedData;

        private class StoredData
        {
            public HashSet<string> ConnectedPlayers = new HashSet<string>();
        }

        private void LoadData()
        {
            storedData = Interface.Oxide.DataFileSystem.ReadObject<StoredData>("WelcomeMessage_Data");
        }

        private void SaveData()
        {
            Interface.Oxide.DataFileSystem.WriteObject("WelcomeMessage_Data", storedData);
        }

        private void OnServerInitialized()
        {
            LoadData();
        }

        #endregion

        private void OnUserConnected(IPlayer player)
        {
            if (player.IsConnected)
            {
                bool isFirstTime = !storedData.ConnectedPlayers.Contains(player.Id);
                if (isFirstTime)
                {
                    storedData.ConnectedPlayers.Add(player.Id);
                    SaveData();
                }

                string messageToSend = isFirstTime ? config.FirstTimePlayerMessage : config.WelcomeMessage;
                messageToSend = messageToSend.Replace("{player_name}", player.Name).Replace("{server_name}", ConVar.Server.hostname);
                timer.Once(config.Delay, () => player.Message(messageToSend));
            }
        }
    }
}
