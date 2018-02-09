using SimpleKanban.DB.Abstract;
using System;
using System.ComponentModel.DataAnnotations;

namespace SimpleKanban.DB.Entities
{
    public class Card : IEntity
    {
        public int Id { get; set; }
        
        public string Title { get; set; }

        public string Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime Start { get; set; }

        [DataType(DataType.Date)]
        public DateTime? End { get; set; }
        
        public int? Complete { get; set; }
    }
}
