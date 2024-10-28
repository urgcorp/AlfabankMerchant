using System;
using System.Collections.Generic;
using System.Linq;
using AlfabankMerchant.ComponentModel;

namespace AlfabankMerchant.Actions
{
    /// <summary>
    /// Запрос регистрации заказа с предавторизацией
    /// <para></para>
    /// Если в запросе на оплату не передаётся корзина с данными фискализации,
    /// оператору фискальных данных передаются значения по умолчанию,
    /// указанные в настройках личного кабинета
    /// </summary>
    [LoginAuthorization(true)]
    [RestUrl("rest/registerPreAuth.do")]
    public class RegisterOrderPreAuthAction : AlfabankAction
    {

    }
}
