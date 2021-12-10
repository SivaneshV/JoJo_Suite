using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.ComponentModel;

namespace JoJoSuite.Activities.Common
{

    public sealed class CommentOut : NativeActivity
    {
        public string ImageIcon => "images.comment16.png";
        public string Title => "Comment out";

        [DefaultValue(null)]
        public Activity Body { get; set; }

        protected override void Execute(NativeActivityContext context)
        {
            // Obtain the runtime value of the Text input argument
           
        }
    }
}
