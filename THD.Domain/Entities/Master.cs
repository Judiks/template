using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace THD.Domain.Entities
{<ROW SYMBOL = "DFR" CO_NAME="DFR CORP." EXCHANGE="    NYSE" CUR_PRICE="19.5" YRL_HIGH="21" YRL_LOW="5.125" P_E_RATIO="24.4" BETA="1.4" PROJ_GRTH="67" INDUSTRY="3573" PRICE_CHG="58" SAFETY="4" RATING="B" RANK="1" OUTLOOK="1" RCMNDATION="HOLD" RISK="HIGH"/>
    public class Master: BaseEntity
    {
        public string Symbol { get; set; }
    }
}
