using System;
using Game.Player;

namespace Core.Services.GameFactory
{
    public interface IGameFactory : IDisposable
    {
        PlayerInputMap PlayerActions { get; }
        IGameCharacter GameCharacter { get; }
        IGameCharacter CreatePlayer();
        PlayerInputMap CreatePlayerActions();
    }
}