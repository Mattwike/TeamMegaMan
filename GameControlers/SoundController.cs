using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using Project1.GameObjects;
using Microsoft.Xna.Framework.Audio;

namespace Project1.GameControllers
{
    public class soundController
    {
        private ContentManager _content;
        private Song song; // Background music
        private Song shootingSound; // Shooting sound effect
        private bool isShootingSoundPlaying;

        public soundController(ContentManager content)
        {
            _content = content;
            isShootingSoundPlaying = false;
        }

        public void Initialize()
        {
            // Load background music and play it in a loop
            song = _content.Load<Song>("06 Bombman Stage");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(song);
            //shootingSound = _content.Load<SoundEffect>("shooting-101soundboards");
        }

        public void LoadContent()
        {
            // Load the shooting sound effect here
            //shootingSound = _content.Load<SoundEffect>("shooting-101soundboards");

        }

        public void Update(Megaman megaman, bool isPaused)
        {
            // Check if the game is not paused
            if (!isPaused)
            {
                if (MediaPlayer.State == MediaState.Paused)
                {
                    MediaPlayer.Resume();
                }

                //Play shooting sound if MegaMan is shooting and the sound isn't already playing
                //if (megaman.is_shooting && !isShootingSoundPlaying)
                //{
                //    MediaPlayer.Play(song)
                //    isShootingSoundPlaying = true;  // Mark that shooting sound is playing
                //}
                //else if (!megaman.is_shooting && isShootingSoundPlaying)
                //{
                //    isShootingSoundPlaying = false;  // Stop playing sound when not shooting
                //}
            }
            else
            {
                if (MediaPlayer.State == MediaState.Playing)
                {
                    MediaPlayer.Pause();
                }
            }
        }
    }
}