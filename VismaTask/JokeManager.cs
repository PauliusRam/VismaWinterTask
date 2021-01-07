using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace VismaTask
{
    class JokeManager
    {
        private List<JokeTemplate> jokeList = new List<JokeTemplate>();                     // Contains all dad jokes
        private int tryCounter=0;                                                           // Counter of checking how many attempts were made at menu input
        public void LoadJokes()                                                             // Preloads jokes to list
        {
              jokeList.Add(new JokeTemplate(jokeList.Count + 1, "Did you hear about the restaurant on the moon?", "Great food, no atmosphere!"));
              jokeList.Add(new JokeTemplate(jokeList.Count + 1, "Did you hear the rumor about butter?", "Well, I'm not going to spread it!"));
              jokeList.Add(new JokeTemplate(jokeList.Count + 1, "Do you know the last thing my grandfather said to me before he kicked the bucket?", "Grandson, watch how far I can kick this bucket."));
              jokeList.Add(new JokeTemplate(jokeList.Count + 1, "Want to hear a joke about construction?", "I'm still working on it!"));
              jokeList.Add(new JokeTemplate(jokeList.Count + 1, "Dad, did you get a haircut ?", "No, I got them all cut!"));
              jokeList.Add(new JokeTemplate(jokeList.Count + 1, "How do you get a squirrel to like you? ", "Act like a nut."));
              jokeList.Add(new JokeTemplate(jokeList.Count + 1, "Why don't eggs tell jokes?", "They'd crack each other up."));
              jokeList.Add(new JokeTemplate(jokeList.Count + 1, "What do you call someone with no body and no nose ?", "Nobody knows."));
              jokeList.Add(new JokeTemplate(jokeList.Count + 1, "Why couldn't the bicycle stand up by itself?", "It was two tired."));
        }
        private void GenerateMenu()                                                         //Generates menu
        {
            Console.WriteLine("{0} Menu {1}", new string('~', 47), new string('~', 47));
            Console.WriteLine("Press 1 to print all dad jokes ");
            Console.WriteLine("Press 2 to print random dad joke");
            Console.WriteLine("Press 3 to print chosen dad joke");
            Console.WriteLine("Press 4 to get current number of jokes in the database");
            Console.WriteLine("Press 5 to get random joke and random punchline");
            Console.WriteLine("Press 6 to add new dad jokes ");
            Console.WriteLine("Press 9 to exit");
            Console.WriteLine(new string('~', 100));
        }
        public void HandleRequest()                                          //Handles menu choices 
        {
            Console.Clear();
            GenerateMenu();
            string request = Console.ReadLine();
            bool exit = false;
            while (!exit)
            {
                if (tryCounter >= 2)                                                        // resets window after 2 incorrect input atemps
                {
                    tryCounter=0;
                    Console.Clear();
                    GenerateMenu();
                }
                if (Int32.TryParse(request, out int requestNumber))                         // checks if input is integer number
                {
                    Console.WriteLine(new string('~', 100));
                    switch (requestNumber)
                    {
                        case 1:                                                             //handles printing whole joke list
                            if (jokeList.Count > 0)
                            {
                                PrintAllDadJokes();
                            }
                            else { Console.WriteLine("There are currently no jokes"); }
                            break;
                        case 2:                                                             //handles printing random joke
                            if (jokeList.Count > 0) 
                            { 
                                PrintRandomJoke(); 
                            }
                            else 
                            { 
                                Console.WriteLine("There are currently no jokes"); 
                            }
                            break;
                        case 3:                                                             //handles printing specific joke
                            if (jokeList.Count > 0)
                            {
                                Console.WriteLine("Please write number between: 1 and {0}", jokeList.Count);
                                string consoleRead = Console.ReadLine();
                                bool isNumber = Int32.TryParse(consoleRead, out int chosenJokeId);
                                if (!isNumber || chosenJokeId > jokeList.Count || chosenJokeId < 1)
                                {
                                    Console.WriteLine(new string('~', 100));
                                    Console.WriteLine(" ERROR : Incorrect input or inserted number is out of range");
                                }
                                else
                                {
                                    PrintChosenJoke((chosenJokeId - 1));
                                }
                            }
                            else { Console.WriteLine("There are currently no jokes"); }
                            break;
                        case 4:                                                             //Handles printing joke counter
                            Console.WriteLine("Current joke count is: {0}.", jokeList.Count);
                            break;
                        case 5:                                                             //Handles printing mixed joke
                            if (jokeList.Count > 0) 
                            { 
                                PrintMixedJoke(); 
                            }
                            else 
                            {
                                Console.WriteLine("There are currently no jokes"); 
                            }
                            break;
                        case 6:                                                             //Handles adding new jokes
                            Console.WriteLine("Enter dad joke question: ");
                            AddNewJoke();
                            break;
                        case 9:                                                            //Handles exit condition
                            exit = true;
                            Console.WriteLine("Program will now exit");
                            break;
                        default:                                                          //handles invalid number input
                                Console.WriteLine("Incorrect number from the input!");
                                Console.WriteLine("Please enter new number: ");
                                tryCounter++;
                                break;
                    }
                    if (requestNumber != 9)
                    {
                        OnTaskFinish();
                    }
                }
                else
                {
                        //Handles invalid character that are not numbers
                        Console.WriteLine(new string('~', 100));
                        Console.WriteLine("Incorrect input, input should be number and should be chosen from the menu");
                        Console.WriteLine("Please enter new number: ");
                        tryCounter++;                
                }
                if (requestNumber != 9) 
                { 
                    request = Console.ReadLine(); 
                }
            }
        }
        //prints tthe list for all dad jokes
        private void PrintAllDadJokes()
        {
            Console.WriteLine("List of all jokes: ");
            foreach (var joke in jokeList)
            {
                Console.WriteLine("{0}. Question: {1} \n    Punchline: {2}", joke.id, joke.question, joke.punchline);
            }
        }
        // prints random joke by using same random function on both question and statement
        private void PrintRandomJoke()
        {
            int max = jokeList.Count - 1;
            Random random = new Random();
            int randomInt = random.Next(0, max);
            Console.WriteLine("Your random joke is: ");
            Console.WriteLine("{0}. Question: {1} \n    Punchline: {2}", jokeList[randomInt].id, jokeList[randomInt].question, jokeList[randomInt].punchline);
        }
        //Prints chosen joke by id
        private void PrintChosenJoke( int id)
        {
            Console.WriteLine(new string('~', 100));
            Console.WriteLine("Your chosen joke is: ");
            Console.WriteLine("{0}. Question: {1} \n    Punchline: {2}", jokeList[id].id, jokeList[id].question, jokeList[id].punchline);
        }
        // Prints out mixed joke by using new random functions for question and statement
        private void PrintMixedJoke()
        {
            int max = jokeList.Count - 1;
            Random random = new Random();
            int randomQuestionInt = random.Next(0, max);
            int randomPunchLineInt = random.Next(0, max);
            Console.WriteLine("Your mixed random joke is: ");
            Console.WriteLine("Question: {0} \n    Punchline: {1}", jokeList[randomQuestionInt].question, jokeList[randomPunchLineInt].punchline);
        }
        // Method for reloading screen on task finish
        private void OnTaskFinish()
        {
            Console.WriteLine(new string('~', 100));
            Console.WriteLine("Press any key, to go to menu");
            Console.WriteLine("BEWARE: Previous information will be cleared");
            Console.ReadKey();
            Console.Clear();
            GenerateMenu();
        }
        // Method for adding new jokes. 
        private void AddNewJoke()
        {
            string dadJokeQuestion = Console.ReadLine();
            if (JokeFilter(dadJokeQuestion))                                            //filters if joke question has inapproriate words
            {
                Console.WriteLine(new string('~', 100));
                Console.WriteLine("Your joke question contains inappropriate words");
                Console.WriteLine("Please enter new joke: ");
                AddNewJoke();
            }
            else
            {
                Console.WriteLine("Enter dad joke punchline: ");
                string dadJokePunchline = Console.ReadLine();
                if (JokeFilter(dadJokePunchline) )                                      //filters if dad joke statement has inappropriate words
                {
                    Console.WriteLine(new string('~', 100));
                    Console.WriteLine("Your joke punchline contains inappropriate words");
                    Console.WriteLine("Please enter new joke: ");
                    AddNewJoke();
                }
                else
                {
                    if (dadJokePunchline.Length == 0 || dadJokeQuestion.Length == 0)    // checks if either of two are empty
                    {
                        Console.WriteLine("Incorrect input: question or statement is empty");
                        Console.WriteLine("Please enter new joke: ");
                        AddNewJoke();
                    }
                    else
                    {
                        if (!dadJokeQuestion.Contains("?") ) // checks if question has proper punctuation
                        {
                            Console.WriteLine("Incorrect punctuation in question");
                            Console.WriteLine("Joke should end with '?' mark");
                            Console.WriteLine("Please enter new joke: ");
                            AddNewJoke();
                        }
                        else
                        {
                            jokeList.Add(new JokeTemplate(jokeList.Count + 1, dadJokeQuestion, dadJokePunchline)); // adds new joke
                            Console.WriteLine(new string('~', 100));
                            Console.WriteLine("Your dad joke was added succefully");
                        }
                    }
                }
            }
        }
        // Returns true if joke contains bad words
        // Returns false if joke does not contain bad words
        private bool JokeFilter(string joke)
        {
            var regexFilter = new Regex(@"\b(shit|shitface|dickbutt)\b", RegexOptions.IgnoreCase);
            if (regexFilter.IsMatch(joke))
            {
                return true;
            }
            else { return false; }
        }
    }
}
