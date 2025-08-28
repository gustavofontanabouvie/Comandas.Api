namespace Comandas.Api.DTOs.PedidoCozinha;

public record PedidoRespondeDto(int comandaId, int situacao);

public record PedidoResponseDto(int pedidoId, int numeroMesa, string nomeCliente, string tituloCardapio);