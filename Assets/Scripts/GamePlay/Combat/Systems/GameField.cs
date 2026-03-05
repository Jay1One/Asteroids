using Core.Configs;
using GamePlay.Combat.Units.Player_mechanics;
using UnityEngine;

namespace GamePlay.Combat.Systems
{
    public class GameField
    {
        public float Width { get; private set; }
        public float Height { get; private set; }
        
        public GameField(GameFieldConfig config)
        {
            Width = config.Width;
            Height = config.Height;
        }

        public void TeleportPlayer(Player player)
        {
            float newX = player.transform.position.x;
            float newY = player.transform.position.y;
            
            if (player.transform.position.x > Width/2)
            {
                newX = -Width + player.transform.position.x;
            }
            else if (player.transform.position.x < -Width/2)
            {
                newX = Width + player.transform.position.x;
            }
            
            if (player.transform.position.y > Height/2)
            {
                newY = -Height + player.transform.position.y;
            }
            else if (player.transform.position.y < -Height/2)
            {
                newY = Height + player.transform.position.y;
            }
            
            player.transform.position = new Vector2(newX, newY);
        }
    }
}