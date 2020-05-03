using System;

namespace EventSourcingCQRS.ReadModel.Models
{
    [Serializable]
    public class Product : IReadEntity
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
