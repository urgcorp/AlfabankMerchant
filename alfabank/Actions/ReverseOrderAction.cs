using System;
using System.Collections.Generic;
using System.Linq;
using alfabank.ComponentModel;

namespace alfabank.Actions
{
    [LoginAuthorization(true)]
    [RestUrl("rest/reverse.do")]
    [WSUrl("soap/merchant-ws")]
    public class ReverseOrderAction : AlfabankAction
    {

    }
}
