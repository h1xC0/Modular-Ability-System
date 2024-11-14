using System;
using Core.Services.GameFactory;
using Core.Systems.CommandSystem;
using Game.Level.Environment.Spawpoints;

namespace Core.CommandBindings.Payloads
{
    public class GameplayPayload : ICommandPayload
    {
        public Action<IGameFactory> Action;
        public SpawnPoint PlayerSpawn;

        public GameplayPayload(SpawnPoint playerSpawn, Action<IGameFactory> action)
        {
            Action = action;
            PlayerSpawn = playerSpawn;
        }
    }
}