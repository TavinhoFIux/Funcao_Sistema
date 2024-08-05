using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebAtividadeEntrevista.Models;

namespace FI.WebAtividadeEntrevista.Models
{
    public class ClienteModelIncluir 
    {
        [Required]
        public ClienteModel Cliente { get; set; }

        public List<BeneficiarioModel> Beneficiarios { get; set; }
    }
}