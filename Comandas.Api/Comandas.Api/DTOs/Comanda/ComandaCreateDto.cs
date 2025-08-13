namespace Comandas.Api.DTOs.Comanda;

public class ComandaCreateDto
{
    public int NumeroMesa { get; set; }

    public string NomeCliente { get; set; }

    public int[] CardapioItens { get; set; }
}
