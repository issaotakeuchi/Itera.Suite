﻿using Itera.Suite.Domain.Common;
using Itera.Suite.Shared.Enums;

namespace Itera.Suite.Domain.Entities
{
    public class ProjetoDeViagem : AuditableEntity, IAggregateRoot
    {
        // 📌 Identidade e núcleo
        public string NomeInterno { get; private set; }
        public string Origem { get; private set; }
        public string Destino { get; private set; }
        public string Objetivo { get; private set; }
        public TipoProjeto Tipo { get; private set; }
        public DateOnly DataSaida { get; private set; }
        public DateOnly DataRetorno { get; private set; }
        public StatusProjeto Status { get; private set; }
        public Guid ClienteId { get; private set; }
        public Cliente Cliente { get; private set; }

        // 📌 Itens de Custo
        private readonly List<ItemDeCusto> _itensDeCusto = new();
        public IReadOnlyCollection<ItemDeCusto> ItensDeCusto => _itensDeCusto.AsReadOnly();

        // 📌 Observações
        private readonly List<ObservacaoProjeto> _observacoes = new();
        public IReadOnlyCollection<ObservacaoProjeto> Observacoes => _observacoes.AsReadOnly();

        // ⚙️ Construtor protegido para EF Core
        protected ProjetoDeViagem() { }

        // ⚙️ Construtor rico
        public ProjetoDeViagem(
        Guid clienteId,
        string nomeInterno,
        string origem,
        string destino,
        string objetivo,
        DateOnly dataSaida,
        DateOnly dataRetorno,
        TipoProjeto tipo,
        string criadoPor)
        {
            // validações
            ClienteId = clienteId;
            NomeInterno = nomeInterno ?? throw new ArgumentNullException(nameof(nomeInterno));
            Origem = origem ?? throw new ArgumentNullException(nameof(origem));
            Destino = destino ?? throw new ArgumentNullException(nameof(destino));
            Objetivo = objetivo ?? throw new ArgumentNullException(nameof(objetivo));
            DataSaida = dataSaida;
            DataRetorno = dataRetorno;
            Tipo = tipo;

            if (dataRetorno < dataSaida)
                throw new ArgumentException("Data de retorno não pode ser anterior à saída.");

            Status = StatusProjeto.Estimado;

            CriadoPor = criadoPor ?? "Sistema";
            DataCriacao = DateTime.UtcNow;
        }

        // ⚙️ Métodos do agregado
        public void AdicionarItemDeCusto(ItemDeCusto item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            if (item.ValorUnitario <= 0) throw new ArgumentException("Valor do item deve ser maior que zero.");
            _itensDeCusto.Add(item);
        }

        public void AdicionarObservacao(string texto, Guid autorId)
        {
            if (string.IsNullOrWhiteSpace(texto))
                throw new ArgumentException("Texto da observação não pode ser vazio.");
            if (autorId == Guid.Empty)
                throw new ArgumentException("AutorId não pode ser Guid.Empty.");

            _observacoes.Add(new ObservacaoProjeto(texto, autorId));
        }

        public decimal CalcularValorTotalProvisionado()
        {
            return _itensDeCusto.Sum(i => i.Total);
        }

        public void MarcarComoConcluido()
        {
            if (_itensDeCusto.Any(i => i.StatusAtual != StatusItemDeCusto.PagoIntegralmente))
                throw new InvalidOperationException("Não é possível concluir: existem itens não pagos.");

            Status = StatusProjeto.Concluido;
        }
    }
}
