using System;
using System.ComponentModel.DataAnnotations;

namespace friendManager.Models
{
    public class Friend
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime FirstMeetDate { get; set; }
        
        public string BiggestSecret { get; set; }
        public decimal Likeability { get; set; }
    }
}
