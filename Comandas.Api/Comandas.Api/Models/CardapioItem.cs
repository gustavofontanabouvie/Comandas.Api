using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Comandas.Api.Models;

public class CardapioItem
{

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string Titulo { get; set; }

    public string Descricao { get; set; }

    public double Preco { get; set; }

    public bool PossuiPreparo { get; set; }
}
