using Comandas.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comandas.Shared.DTOs;

public record ComandasResponseDto(string nomeCliente, int numeroMesa, bool situacaoComanda, IEnumerable<ComandaItem> comandaItens);

