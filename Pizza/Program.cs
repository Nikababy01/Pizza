using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FavoritePizzaToppings
{
    class Program
    {
        static void Main(string[] args)
        {
            var json = File.ReadAllText("pizzas.json");

            var pizzas = JsonSerializer.Deserialize<List<Pizza>>(json);

                // order the pizza toppings alphabetically then convert to string to make easy to compare
                var toppingsStrings = pizzas.Select(pizza => String.Join(", ", pizza.toppings.OrderBy(topping => topping)));
            //foreach (var topping in toppingsStrings)
            //{
            //    Console.WriteLine(topping);
            //}

            // will store in Dictionary so we can track toppings count
            var toppingsCount = new Dictionary<string, int>();

            // loop through toppings strings and add to dictionary. increment count value if not a unique entry
            foreach (var topping in toppingsStrings)
            {
                if (!toppingsCount.ContainsKey(topping))
                {
                    toppingsCount.Add(topping, 1);
                }
                else
                {
                    toppingsCount[topping]++;
                }
            }

            // sort the dictionary by highest count value and turn it into a list so can return the top 20
            var topTwentyPizzas = toppingsCount.OrderByDescending(key => key.Value).ToList().GetRange(0, 20);

            // loop to 20 and print the toppings and order counts
            foreach (var pizza in topTwentyPizzas)
            {
                Console.WriteLine($"{topTwentyPizzas.IndexOf(pizza) + 1}) Pizza Topping(s): {pizza.Key}\nOrder Count: {pizza.Value}\n");
            }
            // this is the no issues found line
        }
    }

    public class Pizza
        {
            public List<string> toppings { get; set; }
        }
    }