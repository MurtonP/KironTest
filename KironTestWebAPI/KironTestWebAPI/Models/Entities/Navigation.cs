using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KironTestWebAPI.Models.Entities
{
    public class Navigations
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public required string Text { get; set; }
        public int ParentID { get; set; }
    }

    public class NavigationRecursive
    {
        public int ID { get; set; }

        public string Text { get; set; } = string.Empty;
        public int ParentID { get; set; }
        public List<NavigationRecursive> Children { get; set; } = new();
    }
}
