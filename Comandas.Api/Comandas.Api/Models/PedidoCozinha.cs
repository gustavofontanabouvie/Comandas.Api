using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Comandas.Api.Models;

public class PedidoCozinha
{

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public int ComandaId { get; set; }

    public virtual Comanda Comanda { get; set; }
    public int Situacao { get; set; }

    public virtual ICollection<PedidoCozinhaItem> PedidoCozinhaItens { get; set; }
}
