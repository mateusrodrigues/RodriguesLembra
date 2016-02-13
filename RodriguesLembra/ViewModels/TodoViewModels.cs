using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RodriguesLembra.ViewModels
{
    public class CreateTodoViewModel
    {
        [Required(ErrorMessage = "Título é obrigatório")]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "Data é obrigatória")]
        [DataType(DataType.Date, ErrorMessage = "Data não é válida")]
        public DateTime DueDate { get; set; }

        public Guid RealmID { get; set; }
    }
}