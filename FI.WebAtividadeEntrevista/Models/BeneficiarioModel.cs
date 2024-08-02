using System.ComponentModel.DataAnnotations;

namespace FI.WebAtividadeEntrevista.Models
{
    /// <summary>
    /// Classe de Modelo de Cliente
    /// </summary>
    public class BeneficiarioModel
    {
        public long Id { get; set; }

        /// <summary>
        /// BeneficiarioNome
        /// </summary>
        [Required]
        public string BeneficiarioNome { get; set; }

        /// <summary>
        /// BeneficiarioCPF
        /// </summary>
        [Required]
        [RegularExpression(@"^\d{3}\.\d{3}\.\d{3}\-\d{2}$", ErrorMessage = "Digite um CPF válido")]
        public string BeneficiarioCPF { get; set; }


        /// <summary>
        /// clienteCPF
        /// </summary>
        [Required]
        [RegularExpression(@"^\d{3}\.\d{3}\.\d{3}\-\d{2}$", ErrorMessage = "Digite um CPF válido")]
        public string ClienteCPF { get; set; }
    }
}