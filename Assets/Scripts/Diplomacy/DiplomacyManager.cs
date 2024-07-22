using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class DiplomacyManager : MonoBehaviour
{
    [SerializeField] private List<Country> countries = new List<Country>();

    void Start()
    {
        AddCountry("1");//Norvheim
        AddCountry("2");//Astenia
        AddCountry("3");//Rastovia
        AddCountry("4");//Zephyran
        AddCountry("5");//Nyumbani
        AddCountry("6");//Jingraya
        AddCountry("7");//Saharazia
        AddCountry("8");//Skylovia

        SetWar("3", "5", true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            DisplayAllRelations();
        }
    }
    public void AddCountry(string name)
    {
        countries.Add(new Country(name));
    }

    public void SetAlliance(string country1, string country2, bool areAllies)
    {
        var c1 = countries.Find(c => c.Name == country1);
        var c2 = countries.Find(c => c.Name == country2);

        if (areAllies)
        {
            c1.AddAlly(c2);
            c2.AddAlly(c1);
        }
        else
        {
            c1.RemoveAlly(c2);
            c2.RemoveAlly(c1);
        }
    }

    public void SetWar(string country1, string country2, bool atWar)
    {
        var c1 = countries.Find(c => c.Name == country1);
        var c2 = countries.Find(c => c.Name == country2);

        if (atWar)
        {
            c1.AddEnemy(c2);
            c2.AddEnemy(c1);
        }
        else
        {
            c1.RemoveEnemy(c2);
            c2.RemoveEnemy(c1);
        }
    }

    public bool AreAllies(string country1, string country2)
    {
        var c1 = countries.Find(c => c.Name == country1);
        var c2 = countries.Find(c => c.Name == country2);

        return c1.IsAllyWith(c2);
    }

    public bool AreAtWar(string country1, string country2)
    {
        var c1 = countries.Find(c => c.Name == country1);
        var c2 = countries.Find(c => c.Name == country2);

        return c1.IsAtWarWith(c2);
    }

    public void DisplayAllRelations()
    {
        StringBuilder sb = new StringBuilder();
        foreach (var country in countries)
        {
            var allies = country.GetAllies();
            var enemies = country.GetEnemies();

            sb.AppendLine($"Country: {country.Name} - Allies: {string.Join(", ", allies)}, Enemies: {string.Join(", ", enemies)}");
        }
        Debug.Log(sb.ToString());
    }
}
