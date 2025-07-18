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

        // Bases de cálculo (substituem os itens diretos)
        private readonly List<BaseDeCalculo> _bases = new();
        public IReadOnlyCollection<BaseDeCalculo> Bases => _bases.AsReadOnly();

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

        public void AdicionarObservacao(string texto, Guid autorId)
        {
            if (string.IsNullOrWhiteSpace(texto))
                throw new ArgumentException("Texto da observação não pode ser vazio.");
            if (autorId == Guid.Empty)
                throw new ArgumentException("AutorId não pode ser Guid.Empty.");

            _observacoes.Add(new ObservacaoProjeto(texto, autorId));
        }

        public void CriarNovaBase(string nome, decimal markup, string criadoPor)
        {
            var baseNova = new BaseDeCalculo(nome, markup, criadoPor);
            _bases.Add(baseNova);
        }

        public void AtualizarDados(
        string? nomeInterno,
        string? origem,
        string? destino,
        string? objetivo,
        DateOnly? dataSaida,
        DateOnly? dataRetorno,
        TipoProjeto? tipo,
        StatusProjeto? status,
        string atualizadoPor)
        {
            if (!string.IsNullOrWhiteSpace(nomeInterno)) NomeInterno = nomeInterno;
            if (!string.IsNullOrWhiteSpace(origem)) Origem = origem;
            if (!string.IsNullOrWhiteSpace(destino)) Destino = destino;
            if (!string.IsNullOrWhiteSpace(objetivo)) Objetivo = objetivo;
            if (dataSaida.HasValue) DataSaida = dataSaida.Value;
            if (dataRetorno.HasValue)
            {
                if (dataRetorno < DataSaida)
                    throw new InvalidOperationException("Data de retorno não pode ser antes da saída.");
                DataRetorno = dataRetorno.Value;
            }
            if (tipo.HasValue) Tipo = tipo.Value;
            if (status.HasValue) Status = status.Value;

            AtualizadoPor = atualizadoPor;
            DataAtualizacao = DateTime.UtcNow;
        }

        public void Inativar(string atualizadoPor)
        {
            AtualizadoPor = atualizadoPor;
            DataAtualizacao = DateTime.UtcNow;
            // Se quiser: IsAtivo = false;
        }
    }
}
