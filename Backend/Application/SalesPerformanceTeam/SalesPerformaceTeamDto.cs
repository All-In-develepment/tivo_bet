using Domain;

namespace Application.SalesPerformanceTeam
{
    public class SalesPerformaceTeamDto
    {
        public int SPTId { get; set; } // Id do registro
        public DateTime SPTDate { get; set; } // Data do Relatório
        public int SPTTotalLeads { get; set; } // Total de Leads Chegados

        // Vendas
        public int SPTTotalSales { get; set; } // Total de Vendas Realizadas
        public float SPTTotalSalesAmont { get; set; } // Soma total de vendas em R$
        public float SPTAVGSales { get; set; } // Média de vendas por lead
        public float SPTAVGSalesAmont { get; set; } // Média de vendas por lead em R$

        // Cadastros
        public int SPTTotalRegister { get; set; } // Quantidade de cadastros realizados
        public float SPTTotalRegisterAmont { get; set; } // Soma total de cadastros em R$
        public float SPTAVGRegister { get; set; } // Média de cadastros por lead
        public float SPTAVGRegisterAmont { get; set; } // Média de cadastros por lead em R$

        // Redepositos
        public int SPTTotalRedeposit { get; set; } // Quantidade de cadastros realizados
        public float SPTTotalRedepositAmont { get; set; } // Soma total de cadastros em R$
        public float SPTAVGRedeposit { get; set; } // Média de cadastros por lead
        public float SPTAVGRedepositAmont { get; set; } // Média de cadastros por lead em R$

        public float SPTAVGConvertion { get; set; } // Média total de conversão


        // Foreign Keys
        // Casa de aposta
        public Guid SPTBookmakerId { get; set; }
        public string SPTBookmakerName { get; set; }

        // Vendedor
        public Guid SPTSellerId { get; set; }
        public string SPTSellerName { get; set; }

        // Projeto
        public Guid SPTProjectId { get; set; }
        public string SPTProjectName { get; set; }

        // Evento
        public Guid SPTEventId { get; set; }
        public string SPTEventName { get; set; }

        public DateTime SPTCreatedAt { get; set; }
        public DateTime SPTUpdatedAt { get; set; }
    }
}