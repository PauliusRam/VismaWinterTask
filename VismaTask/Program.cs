using System;
using System.Collections.Generic;

namespace VismaTask
{
    class Program
    {
        static void Main(string[] args)
        {
            JokeManager jokeManager = new JokeManager();
            jokeManager.LoadJokes();
            jokeManager.HandleRequest();
        }
       
    }
}
