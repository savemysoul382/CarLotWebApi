using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using AutoLotDAL.Models;

namespace AutoLotDAL.EF
{
    public class MyDataInitializer : DropCreateDatabaseAlways<AutoLotEntities>
    {
        protected override void Seed(AutoLotEntities context)
        {
            var customers = new List<Customer>
            {
                new Customer {FirstName = "Dave", LastName = "Brenner"},
                new Customer {FirstName = "Matt", LastName = "Walton"},
                new Customer {FirstName = "Steve", LastName = "Hagen"},
                new Customer {FirstName = "Pat", LastName = "Walton"},
                new Customer {FirstName = "Bad", LastName = "Customer"}
            };
            customers.ForEach(
                x => context.Customers.AddOrUpdate(
                    c => new {c.FirstName, c.LastName},
                    x));


            var cars = new List<inventory>
            {
                new inventory {Make = "VW", Color = "Black", PetName = "Zippy"},
                new inventory {Make = "Ford", Color = "Rust", PetName = "Rusty"},
                new inventory {Make = "Saab", Color = "Black", PetName = "Mel"},
                new inventory {Make = "Yugo", Color = "Yellow ", PetName = "Clunker"},
                new inventory {Make = "BMW", Color = "Black", PetName = "Bimmer"},
                new inventory {Make = "BMW", Color = "Green", PetName = "Hank"},
                new inventory {Make = "BMW", Color = "Pink", PetName = "Pinky"},
                new inventory {Make = "Pinto", Color = "Black ", PetName = "Pete"},
            };
            context.Cars.AddOrUpdate(x => new {x.Make, x.Color}, cars.ToArray());
            var orders = new List<Order>
            {
                new Order() {Car = cars[0], Customer = customers[0]},
                new Order() {Car = cars[1], Customer = customers[1]},
                new Order() {Car = cars[2], Customer = customers[2]},
                new Order() {Car = cars[3], Customer = customers[3]}
            };
            orders.ForEach(x => context.Orders.AddOrUpdate(c => new {c.Carld, c.Custld}, x));
            context.CreditRisks.AddOrUpdate(
                x => new {x.FirstName, x.LastName},
                new CreditRisk
                {
                    Id = customers[4].Id,
                    FirstName = customers[4].FirstName,
                    LastName = customers[4].LastName,
                });
        }
    }
}