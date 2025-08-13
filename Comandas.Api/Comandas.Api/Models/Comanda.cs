using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Comandas.Api.Models;

public class Comanda
{

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public int NumeroMesa { get; set; }

    public string NomeCliente { get; set; }

    public bool SituacaoComanda { get; set; }

    public virtual ICollection<ComandaItem> ComandaItens { get; set; }

}
