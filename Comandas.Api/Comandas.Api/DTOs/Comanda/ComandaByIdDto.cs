namespace Comandas.Api.DTOs.Comanda;

public record ComandaByIdDto(int numeroMesa, string nomeCliente, bool situacaoComanda, List<ComandaItemByIdDto> itens);

public record ComandaItemByIdDto(int idComandaItem, int cardapioItemId, string titulo);

public record ComandaDto(int numeroMesa, string nomeCliente, bool situacaoComanda);
