using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RodriguesLembra.ViewModels
{
    public class CreateTodoViewModel
    {
        [Display(Name = "Título")]
        [Required(ErrorMessage = "Título é obrigatório")]
        public string Title { get; set; }

        [Display(Name = "Descrição")]
        public string Description { get; set; }

        [Display(Name = "Deadline")]
        [Required(ErrorMessage = "Data é obrigatória")]
        [DataType(DataType.Date, ErrorMessage = "Data não é válida")]
        public DateTime DueDate { get; set; }

        public Guid RealmID { get; set; }
    }
}