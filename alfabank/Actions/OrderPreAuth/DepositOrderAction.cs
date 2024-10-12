using System;
using System.Collections.Generic;
using System.Linq;
using alfabank.ComponentModel;

namespace alfabank.Actions
{
    [LoginAuthorization]
    [RestUrl("rest/deposit.do")]
    public class DepositOrderAction : AlfabankAction
    {
    }
}
