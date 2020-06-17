using KellermanSoftware.CompareNetObjects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuditProject.Models
{
    /*public class AuditChange
    {
        public string DateTimeStamp { get; set; }
        public AuditActionType AuditActionType { get; set; }
        public string AuditActionTypeName { get; set; }
        public List<AuditDelta> Changes { get; set; }
        public AuditChange()
        {
            Changes = new List<AuditDelta>();
        }
    }*/

    public class AuditDelta
    {
        //[BsonId]
        public string FieldName { get; set; }
        public string ValueBefore { get; set; }
        public string ValueAfter { get; set; }
    }

    public enum AuditActionType
    {
        Create = 1,
        Update,
        Delete
    }


    //Audit Repository

    public class AuditTrail
    {
        public void CreateAuditTrail<T>(AuditActionType Action, int KeyFieldID, T OldObject, T NewObject)
        {
            // get the differance
            CompareLogic compObjects = new CompareLogic();
            compObjects.Config.MaxDifferences = 99;
            ComparisonResult compResult = compObjects.Compare(OldObject, NewObject);
            List<AuditDelta> DeltaList = new List<AuditDelta>();
            foreach (var change in compResult.Differences)
            {
                AuditDelta delta = new AuditDelta();
                delta.FieldName = change.PropertyName;
                delta.ValueBefore = change.Object1Value;
                delta.ValueAfter = change.Object2Value;
                DeltaList.Add(delta);
            }
            AuditLog audit = new AuditLog();
            audit.AuditActionTypeENUM = (int)Action;
            audit.DataModel = this.GetType().Name;
            audit.DateTimeStamp = DateTime.Now;
            audit.KeyFieldID = KeyFieldID;
            audit.ValueBefore = JsonConvert.SerializeObject(OldObject);
            audit.ValueAfter = JsonConvert.SerializeObject(NewObject);
            audit.Changes = JsonConvert.SerializeObject(DeltaList);
            /*AuditTestEntities ent = new AuditTestEntities();
            ent.AuditTable.Add(audit);
            ent.SaveChanges();*/

        }
    }
}
