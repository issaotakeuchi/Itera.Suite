namespace Itera.Suite.Domain.Enums;

public enum StatusProjeto
{
    Estimado = 1, //Antes mesmo de calcular (pré-cotação informal)
    Orcado = 2, //Quando já fizemos o orçamento e enviamos ao cliente
    AprovadoPeloCliente = 3,  //Cliente deu o "go", podemos seguir
    EmPlanejamento = 4,  //Rooming list, logística, restrições, planilhas
    EmExecucao = 5,  //Viagem está acontecendo
    Concluido = 6,  //Projeto finalizado
    Cancelado = 7  //Cancelado por qualquer motivo
}
