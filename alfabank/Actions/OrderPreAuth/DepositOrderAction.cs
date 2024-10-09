using System;
using System.Collections.Generic;
using System.Linq;
using alfabank.ComponentModel;

namespace alfabank.Actions.OrderPreAuth
{
    public class DepositOrderAction : AlfabankAction
    {
        public override string Action { get; set; } = AlfabankActions.PreAuth.DepositOrder;
    }
}
