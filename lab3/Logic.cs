using System.Runtime.InteropServices.ComTypes;
using System.Runtime.InteropServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public class Logic {
    public List<Tweet>? tweets { get; set; }
    string date_format = "MMMM d, yyyy 'at' hh:mmtt";
    
    public Logic() {
        tweets = new List<Tweet>();
    }

    public void GetFromJson() {
        var file = File.ReadAllLines("data.jsonl");
        // List<Tweet> tweets = new List<Tweet>();

        foreach (var line in file) {
            var tweet = JsonSerializer.Deserialize<Tweet>(line);
            tweets.Add(tweet);
        }
    }

    public void saveToXML() {
        System.Xml.Serialization.XmlSerializer data = new System.Xml.Serialization.XmlSerializer(typeof(List<Tweet>));
        TextWriter writer = new StreamWriter("data_xml.xml");
        data.Serialize(writer, tweets);
        writer.Close();
    }

    public void readFromXML() {
        StreamReader reader = new StreamReader("data_xml.xml");
        System.Xml.Serialization.XmlSerializer data = new System.Xml.Serialization.XmlSerializer(typeof(List<Tweet>));
        
        tweets = (List<Tweet>)data.Deserialize(reader);
    }

    public void sortByUserName() {
        if (tweets == null) return;
        tweets.Sort((prev, next) => prev.UserName.CompareTo(next.UserName));
    }

    public void sortByDate() {
        if (tweets == null) return;
        tweets.Sort((prev, next) => DateTime.ParseExact(prev.CreatedAt, date_format, null).CompareTo(DateTime.ParseExact(next.CreatedAt, date_format, null)));
    }

    public void sortByNameAndDate() {
        if (tweets == null) return;
        tweets.OrderBy(tweet => tweet.UserName)
            .ThenBy(tweet => DateTime.ParseExact(tweet.CreatedAt, date_format, null));
    }

    public Tweet findOldestTweet() {
        if (tweets == null) throw new Exception("tweets are empty");
        var temp = tweets.OrderBy(tweet => DateTime.ParseExact(tweet.CreatedAt, date_format, null));
        return temp.First();
    }

    public Tweet findNewestTweet() {
        if (tweets == null) throw new Exception("tweets are empty");
        var temp = tweets.OrderBy(tweet => DateTime.ParseExact(tweet.CreatedAt, date_format, null));
        return temp.Last();
    }


    public Dictionary<string, List<Tweet>> dictionaryOfTweetsByUser() {
        Dictionary<string, List<Tweet>> dict = new Dictionary<string, List<Tweet>>();
        
        foreach (var i in tweets) {
            if (dict.ContainsKey(i.UserName))
                dict[i.UserName].Add(i);
            else
                dict.Add(i.UserName, new List<Tweet> { i });
        }

        dict = dict.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
        return dict;
    }


    public Dictionary<string, int> frequencyOfWords() {
            if (tweets == null) throw new Exception("tweets are empty");
            Dictionary<string, int> dict = new Dictionary<string, int>();

            foreach (var tweet in tweets) {
                // string [] words = Tweet.reg.Split(i.Text);
                string[] words = tweet.Text.Split(' ', '.', ',', '!', '?', ':', ';', '-', '(', ')', '[', ']', '{', '}', '/', '\\', '"', '\'', '\t', '\n', '\r');
                
                foreach (var word in words) {
                    String word_low = word.ToLower();
                    if (dict.ContainsKey(word_low))
                        dict[word_low]++;
                    else
                        dict.Add(word_low, 1);
                }
            }

            return dict;
        }
    
    public Dictionary<string, int> Top10Words() {
        Dictionary<string, int> dict = frequencyOfWords();
        Dictionary<string, int> result = new Dictionary<string, int>();
        int counter = 0;
        for (int i = 0; i < dict.Count; i++)
        {
            if (dict.ElementAt(i).Key.Length >= 5)
            {
                result.Add(dict.ElementAt(i).Key, dict.ElementAt(i).Value);
                counter++;
            }
            if (counter == 10)
                break;
        }
        return result;
    }

    public Dictionary<string, double> idf() {
        if (tweets == null) throw new Exception("tweets are empty")3
        ;
        Dictionary<string, int> dict = new Dictionary<string, int>();

        foreach (var tweet in tweets) {
            
        }
    }
}