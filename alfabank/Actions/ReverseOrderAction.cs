using System;
using System.Collections.Generic;
using System.Linq;
using alfabank.ComponentModel;

namespace alfabank.Actions
{
    public class ReverseOrderAction : AlfabankAction
    {
        public override string Action { get; set; } = AlfabankRestActions.ReverseOrder;
    }
}
