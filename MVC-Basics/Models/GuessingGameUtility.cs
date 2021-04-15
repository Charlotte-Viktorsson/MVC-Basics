using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Basics.Models
{
    public class GuessingGameUtility
    {
        private static Random rnd = new Random();

        public static string Guess(int guess, int luckyNumber, int nrOfGuesses)
        {

            if (guess < luckyNumber)
            {
                return "Sorry, your guess " + guess + " is too low!";
            }

            if (guess > luckyNumber)
            {
                return "Sorry, your guess " + guess + " is too high!";
            }

            return "Congratulations! It is correct! You guessed " + nrOfGuesses
                + " times! Guess a new number!";
        }

        public static int GetNewRandom()
        {
            return rnd.Next(1, 100);
        }

    }
}
