using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WizLib_Model.Models
{
    [Table("tb_SampleAnnotations")]
    public class SampleAnnotation
    {
        //public int Id { get; set; }
        [Key]
        public int SampleAnnotation_Id { get; set; }
        [Column("Name")]
        public string SampleName { get; set; }
    }
}