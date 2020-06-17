using AuditProject.Models;
using KellermanSoftware.CompareNetObjects;
using System;
using System.Collections.Generic;

namespace AuditProject
{
    class Program
    {
        static void Main(string[] args)
        {

            Case oldCase = new Case()
            {
                IdCase = "case1",
                DescCase = "Desccase1",
                Requester = new UserInfo()
                {
                    IdUser = "user1",
                    UserName="Nameuser1",
                    Email="user1@gmail.com"
                }
            };

            Case newcase = new Case()
            {
                IdCase = "case1",
                DescCase = "Desccase2",
                Requester = new UserInfo()
                {
                    IdUser = "user1",
                    UserName = "Nameuser2",
                    Email = "user1@gmail.com"
                }
            };


            CompareLogic compObjects = new CompareLogic();
            compObjects.Config.MaxDifferences = 99;
            ComparisonResult compResult = compObjects.Compare(oldCase, newcase);


            List<AuditDelta> DeltaList = new List<AuditDelta>();
            foreach (var change in compResult.Differences)
            {
                AuditDelta delta = new AuditDelta();
                delta.FieldName = change.PropertyName;
                delta.ValueBefore = change.Object1Value;
                delta.ValueAfter = change.Object2Value;
                DeltaList.Add(delta);
            }

            foreach(var change in DeltaList)
            {
                Console.WriteLine("FieldName : " + change.FieldName);
                Console.WriteLine("Old Value : " + change.ValueBefore);
                Console.WriteLine("New Value : " + change.ValueAfter);
            }



            Console.WriteLine("Hello World!");
        }
    }
}
