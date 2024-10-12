using System;
using System.Collections.Generic;
using System.Linq;
using alfabank.ComponentModel;

namespace alfabank.Actions
{
    [LoginAuthorization(true)]
    [RestUrl("rest/registerPreAuth.do")]
    public class RegisterOrderPreAuthAction : AlfabankAction
    {

    }
}
