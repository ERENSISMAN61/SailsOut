using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class Country
{
    public string Name { get; private set; }
    public List<Country> allies = new List<Country>();
    public List<Country> enemies = new List<Country>();

    public Country(string name)
    {
        Name = name;
    }

    public void AddAlly(Country country)
    {
        if (!allies.Contains(country))
        {
            allies.Add(country);
        }
    }

    public void RemoveAlly(Country country)
    {
        allies.Remove(country);
    }

    public void AddEnemy(Country country)
    {
        if (!enemies.Contains(country))
        {
            enemies.Add(country);
        }
    }

    public void RemoveEnemy(Country country)
    {
        enemies.Remove(country);
    }

    public bool IsAllyWith(Country country)
    {
        return allies.Contains(country);
    }

    public bool IsAtWarWith(Country country)
    {
        return enemies.Contains(country);
    }

    public List<string> GetAllies()
    {
        return allies.Select(a => a.Name).ToList();
    }

    public List<string> GetEnemies()
    {
        return enemies.Select(e => e.Name).ToList();
    }
}
