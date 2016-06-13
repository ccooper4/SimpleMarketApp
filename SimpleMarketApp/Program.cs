using System;
using System.Net;
using System.Text.RegularExpressions;
using System.Timers;
using Newtonsoft.Json.Linq;

namespace SimpleMarketApp
{
    class Program
    {
        //The item we are checking the price for
        private static string item;

        /// <summary>
        /// The main method of this price checking program
        /// </summary>
        /// <param name="args"></param>
        private static void Main(string[] args)
        {
            Console.WriteLine("Enter the url of item to check the price for: ");
            item = Console.ReadLine();
            string[] split = Regex.Split(item, "http://steamcommunity.com/market/listings/730/");
            item = split[1];
            OnTimedEvent(null, null);
            System.Timers.Timer aTimer = new System.Timers.Timer();
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            aTimer.Interval = 1000;
            aTimer.Enabled = true;

            Console.WriteLine("Press ESC to stop");
            do
            {
                while (!Console.KeyAvailable)
                {
                   
                }
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }

        /// <summary>
        /// The price check to execute every 'x' seconds
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            WebClient client = new WebClient();
            string itemResult = client.DownloadString("http://steamcommunity.com/market/priceoverview/?country=DE&currency=2&appid=730&market_hash_name=" + item);

            JObject o = JObject.Parse(itemResult);
            Console.WriteLine(o.GetValue("lowest_price"));
        }
    }

    

}
