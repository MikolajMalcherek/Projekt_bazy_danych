using bazy.Models;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace bazy.Models
{
    // prawdopodobnie zle mapowane sa te zmienne do bazy danych.

    public class Zawodnik
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idzawodnicy { get; set; }
        public string imie_zawodnika { get; set; }

        public string nazwisko_zawodnika { get; set; }

        public string kraj_pochodzenia { get; set; }


    }
}
