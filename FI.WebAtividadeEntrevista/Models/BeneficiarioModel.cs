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
        public string Nome { get; set; }

        /// <summary>
        /// BeneficiarioCPF
        /// </summary>
        [Required]
        [RegularExpression(@"^\d{3}\.\d{3}\.\d{3}\-\d{2}$|^\d{11}$", ErrorMessage = "Digite um CPF válido.")]

        public string CPF { get; set; }


        /// <summary>
        /// clienteCPF
        /// </summary>
        [Required]
        [RegularExpression(@"^\d{3}\.\d{3}\.\d{3}\-\d{2}$|^\d{11}$", ErrorMessage = "Digite um CPF válido.")]
        public string ClienteCPF { get; set; }
    }
}