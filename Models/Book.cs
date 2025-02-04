using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestFisrtProjectNonCore.Models
{
    [Table("tbl_Book")]
    public class Book
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(256)]
        public string Title { get; set; }
        [Required]
        [MaxLength(128)]
        public string Auther { get; set; }

        [Required]
        [MaxLength(2000)]
        public string Description { get; set; }

        [ForeignKey("Category")]
        public byte CategoryId { get; set; }
        public Category Category { get; set; }
        public DateTime AddedOn { get; set; }

        public Book()
        {
            AddedOn = DateTime.Now;
        }

    }
}