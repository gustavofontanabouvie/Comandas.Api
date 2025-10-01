

using Comandas.Api.DTOs.Comanda;
using Comandas.Application.Interfaces;
using Comandas.Data.Repositories.Interface;
using Comandas.Domain;
using Comandas.Shared.DTOs;

namespace Comandas.Application.Services;

public class ComandaService : IComandaService
{
    private readonly IComandaRepository _comandaRepository;
    private readonly IMesaRepository _mesaRepository;
    private readonly IComandaItemRepository _comandaItemRepository;
    private readonly ICardapioItemRepository _cardapioItemRepository;
    private readonly IPedidoCozinhaRepository _pedidoCozinhaRepository;

    public ComandaService(IPedidoCozinhaRepository pedidoCozinhaRepository, IComandaRepository comandaRepository, IMesaRepository mesaRepository, IComandaItemRepository comandaItemRepository, ICardapioItemRepository cardapioItemRepository)
    {
        _comandaRepository = comandaRepository;
        _mesaRepository = mesaRepository;
        _comandaItemRepository = comandaItemRepository;
        _cardapioItemRepository = cardapioItemRepository;
        _pedidoCozinhaRepository = pedidoCozinhaRepository;
    }

    public async Task<ComandaByIdDto> GetComandaById(int id)
    {
        var comanda = await _comandaRepository.GetComandaById(id);

        return comanda;
    }

    public async Task<IEnumerable<ComandasResponseDto>> GetComandas()
    {
        var comandas = await _comandaRepository.GetComandas();

        return comandas;
    }

    public async Task<ComandaCreateResponseDto> CreateComanda(ComandaCreateDto comandaCreateDto, CancellationToken cancellationToken)
    {
        var mesa = await _mesaRepository.GetMesa(comandaCreateDto.NumeroMesa, cancellationToken);


        if (mesa.SituacaoMesa)
            throw new Exception("A mesa selecionada já está ocupada");
        //return UnprocessableEntity("A mesa selecionada já está ocupada");

        mesa.SituacaoMesa = true;


        var comanda = new Comanda
        {
            NumeroMesa = comandaCreateDto.NumeroMesa,
            NomeCliente = comandaCreateDto.NomeCliente,
            SituacaoComanda = true
        };

        await _comandaRepository.CreateComanda(comanda, cancellationToken);


        foreach (int id in comandaCreateDto.CardapioItens)
        {
            var cardapioItem = await _cardapioItemRepository.GetCardapioItemById(id, cancellationToken);
            if (cardapioItem == null)
                throw new Exception("CardapioItem não encontrado");

            var comandaItem = new ComandaItem
            {
                CardapioItemId = id,
                Comanda = comanda
            };

            await _comandaItemRepository.CreateComandaItem(comandaItem, cancellationToken);


            if (cardapioItem.possuiPreparo)
            {
                PedidoCozinha pedidoCozinha = new()
                {
                    Comanda = comanda,
                    Situacao = 1,
                    PedidoCozinhaItens = new List<PedidoCozinhaItem>()
                        {
                           new PedidoCozinhaItem()
                           {
                               ComandaItem = comandaItem
                           }
                        }
                };

                await _pedidoCozinhaRepository.CreatePedidoCozinha(pedidoCozinha, cancellationToken);
            }
        }

        await _mesaRepository.SaveChanges(cancellationToken);

        var comandaResponse = new ComandaCreateResponseDto(comanda.Id, comanda.NumeroMesa, comanda.NomeCliente);

        return comandaResponse;
    }
}
