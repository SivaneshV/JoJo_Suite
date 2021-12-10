using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Activities;
using System.ComponentModel;
using Microsoft.Exchange.WebServices.Data;

namespace JoJoSuite.Activities.Email
{
    public sealed class GetMail : NativeActivity<object>    
    {
        public GetMail()
        {
            this.DisplayName = "Get Mail";
        }
        [Category("Input")]
        [Description("Please provide exchangeService")]
        [DefaultValue(null)]
        public OutArgument<ExchangeService> exchangeService { get; set; }
        protected override void Execute(NativeActivityContext context)
        {
            dynamic driver = null;
            this.Result.Set(context, driver);

        }
    }
}
