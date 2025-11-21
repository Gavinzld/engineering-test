using System.Collections.Generic;
using System.Runtime;

namespace GildedRose.Console;

class Program
{
    IList<Item> Items = new List<Item>();
    
    static void Main(string[] args)
    {
        System.Console.WriteLine("OMGHAI!");

        var app = new Program()
                      {
                          Items = new List<Item>
                                      {
                                          new Item {Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20},
                                          new Item {Name = "Aged Brie", SellIn = 2, Quality = 0},
                                          new Item {Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7},
                                          new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80},
                                          new Item
                                              {
                                                  Name = "Backstage passes to a TAFKAL80ETC concert",
                                                  SellIn = 15,
                                                  Quality = 20
                                              },
                                          new Item {Name = "Conjured Mana Cake", SellIn = 3, Quality = 6}
                                      }

                      };

        app.UpdateQuality();

        System.Console.ReadKey();
    }

    public void UpdateQuality()
    {
        foreach (var item in Items)
        {
            if (IsSulfuras(item)) 
            {
                continue;
            }

            if (IsAgedBrie(item) || IsBackstagePass(item))
            { 
                IncreaseQuality(item, 1); 

                if (IsBackstagePass(item))
                {
                    if (item.SellIn <= 10) IncreaseQuality(item, 1);
                    if (item.SellIn <= 5) IncreaseQuality(item, 1);
                }
            } 
            else 
            {
                DecreaseQuality(item, IsConjured(item) ? 2 : 1);
            }

            item.SellIn--;

            if (item.SellIn < 0)
            {
                if (IsBackstagePass(item)) 
                {
                    item.Quality = 0;
                }
                else if (IsAgedBrie(item))
                {
                    IncreaseQuality(item, 1);
                }
                else
                {
                    DecreaseQuality(item, IsConjured(item) ? 2 : 1);
                }
            }
        }
    }

    private static bool IsAgedBrie(Item item) => item.Name.Contains("Aged Brie");
    private static bool IsSulfuras(Item item) => item.Name.Contains("Sulfuras");
    private static bool IsBackstagePass(Item item) => item.Name.Contains("Backstage passes");
    private static bool IsConjured(Item item) => item.Name.Contains("Conjured");

    private static void IncreaseQuality(Item item, int amount)
    {
        item.Quality = System.Math.Min(item.Quality + amount, 50);
    }

    private static void DecreaseQuality(Item item, int amount)
    {
        item.Quality = System.Math.Max(item.Quality - amount, 0);
    }
}

public class Item
{
    public string Name { get; set; } = "";

    public int SellIn { get; set; }

    public int Quality { get; set; }
}
