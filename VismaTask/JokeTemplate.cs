using System;
using System.Collections.Generic;
using System.Text;

namespace VismaTask
{
    class JokeTemplate
    {
        public int id { get; private set; }
        public string question { get; private set; }
        public string punchline { get; private set; }
        public JokeTemplate(int id, string question,string punchline) {
            this.id = id;
            this.question = question;
            this.punchline = punchline;
        }
    }
}
