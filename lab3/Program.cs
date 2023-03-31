using System.Runtime.InteropServices.ComTypes;
using System.Runtime.InteropServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;


class Program {
    static public void Main() {
        Logic logic = new Logic();

        logic.GetFromJson();

        foreach (Tweet tweet in logic.tweets) {
            System.Console.WriteLine(tweet.ToString());
        }
    }



}