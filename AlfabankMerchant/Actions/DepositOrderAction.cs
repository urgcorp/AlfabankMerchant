﻿using System;
using System.Collections.Generic;
using System.Linq;
using AlfabankMerchant.ComponentModel;

namespace AlfabankMerchant.Actions
{
    /// <summary>
    /// Запрос списания суммы предавторизации
    /// <para>Данную операцию можно осуществлять, если есть соответствующие права в системе</para>
    /// </summary>
    [LoginAuthorization]
    [RestUrl("rest/deposit.do")]
    public class DepositOrderAction : AlfabankAction
    {
    }
}
