using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessNumberMiniGame
{
    public class GameSettings
    {
        public int minNumber {  get; set; }
        public int maxNumber { get; set; }
        public int maxAttempts { get; set; }

        public GameSettings(int min, int max, int attempts)
        {
            minNumber = min;
            maxNumber = max;
            maxAttempts = attempts;
        }
    }
}
