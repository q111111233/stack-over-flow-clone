using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StackOverflowClone.Models
{
    [Table("Answers")]
    public class Answer
    {
        [Key]
        public int Id { set; get; }
        public string Text { set; get; }
        public int QuestionId { set; get; }
        public virtual ApplicationUser User { set; get; }
    }
}
