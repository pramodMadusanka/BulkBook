using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace BulkBookWeb.Models;

public class Category
{
    [Key]
    public int Id{ get; set; }
    [Required(ErrorMessage ="Name must be not null")]
    public string Name { get; set; }
    [DisplayName("Display Order")]
    [Range(1,100, ErrorMessage ="Display Order must be in between 1 and 100")]
    [Required(ErrorMessage ="Display Order must be not null")]
    public int DisplayOrder { get; set; }
    public DateTime CreatedDateTime { get; set; } = DateTime.Now;
}