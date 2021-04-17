using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVC_Basics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Basics.Controllers
{
    public class GameController : Controller
    {

        [HttpGet]
        public IActionResult GuessingGame()
        {
            int luckyNr = GuessingGameUtility.GetNewRandom();
            string initialMessage = "Guess a number between 1 and 100!";

            //save luckyNr in Session
            HttpContext.Session.SetInt32("LuckyNumber", luckyNr);

            //save NrofGuesses in Cookie
            CookieOptions option = new CookieOptions();
            option.Expires = DateTime.Now.AddMinutes(1);
            Response.Cookies.Append("NrOfGuesses", "0", option);

            //information to view
            //ViewBag.LuckyNumber = luckyNr;
            ViewBag.NrOfGuesses = 0;

            ViewBag.message = TempData["message"] == null ? initialMessage : TempData["message"];

            return View();
        }

        [HttpPost]
        public IActionResult GuessingGame(int guess)
        {
            string message = "";
            int luckyNumber = 0;
            int nrOfGuesses = 0;


            if (guess != 0) //when choosen a nr between 1 and 100
            {
                //try to get luckynr from session and nrOfGuesses from cookie
                try
                {
                    luckyNumber = (int)HttpContext.Session.GetInt32("LuckyNumber");
                    string cookieNrOfGuesses = Request.Cookies["NrOfGuesses"];
                    nrOfGuesses = Int32.Parse(cookieNrOfGuesses) + 1;

                    //check if correct
                    message = GuessingGameUtility.Guess(guess, luckyNumber, nrOfGuesses);
                }
                catch (Exception)
                {
                    //if exception like timing out session/cookie: restart game
                    message = "Unfortenately the game is restarted, guess a new number!";
                }
            }
            else //when not choosen any nr
            {
                message = "Guess a number before pressing Guess!";
            }
            //if winner or if restart is needed
            if (
                message.Contains("Congrat") ||
                message.Contains("Unfort") ||
                message.Contains("pressing")
                )
            {
                TempData["message"] = message;
                return RedirectToAction("GuessingGame");
                //restart game with new random nr and reset nrOfGuesses
                /*luckyNumber = GuessingGameUtility.GetNewRandom();
                HttpContext.Session.SetInt32("LuckyNumber", luckyNumber);
                nrOfGuesses = 0;*/
            }

            //set cookie with nrOfGuesses
            CookieOptions option = new CookieOptions();
            option.Expires = DateTime.Now.AddMinutes(1);
            Response.Cookies.Append("NrOfGuesses", nrOfGuesses.ToString(), option);

            //information to view
            ViewBag.NrOfGuesses = nrOfGuesses;
            ViewBag.message = message;
            //ViewBag.LuckyNumber = luckyNumber;

            return View();
        }
    }
}
