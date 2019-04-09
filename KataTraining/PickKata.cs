using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KataTraining
{
    class PickKata
    {
        private void Begin()
        {
            while (true)
            {
                System.Threading.Thread.Sleep(random.Next(10, 20) * 1000);
                string word = Attacks[random.Next(0, Attacks.Count)];
                label.Content = word;
            }

        }

        private List<string> Attacks = new List<string>(){
        "left roubdhouse swing",
        "right roubdhouse swing",
        "overhead bottle",
        };

        Random random = new Random();

    }
}
