using System;
using System.Activities;
using System.ComponentModel;
using JoJoSuite.Library.IO;

namespace JoJoSuite.Activities.IO
{

    public sealed class CopyPasteFile : NativeActivity<string>
    {
        [Category("Input")]
        [Description("Please provide the full input file path")]
        [DefaultValue(null)]
        public InArgument<string> Filename { get; set; }

        [Category("Paste")]
        [Description("Please specify the output folder path")]
        [DefaultValue(null)]
        public InArgument<string> PasteFolderPath { get; set; }

        [Category("Input")]
        [Description("To overwrite the existing file")]
        [DefaultValue(false)]
        public bool Overwrite { get; set; }

        [Category("Rename")]
        [Description("Please specify the folder path")]
        [DefaultValue(null)]
        public InArgument<string> RenameFolderPath { get; set; }

        [Category("Rename")]
        [Description("Please specify the file name with extension")]
        [DefaultValue(null)]
        public InArgument<string> FileName { get; set; }

        [Category("Rename")]
        [Description("To rename and move the file from source to destination")]
        [DefaultValue(false)]
        public bool Rename { get; set; }


      

        protected override void Execute(NativeActivityContext context)
        {
            r2rCopyPasteFile oLib = new r2rCopyPasteFile();
            oLib.FileName = context.GetValue(this.Filename);
            oLib.Overwrite = this.Overwrite;
            oLib.RenameFolderPath = context.GetValue(this.RenameFolderPath);
            oLib.Rename = this.Rename;
            oLib.PasteFolderPath = context.GetValue(this.PasteFolderPath);
            oLib.NewFileName = context.GetValue(this.FileName);

            bool res = oLib.DoAction();

            if (res)
            {
                this.Result.Set(context, Convert.ToString(res));
            }
            else
            {
                this.Result.Set(context, Convert.ToString(new Exception(oLib.ErrorMessage)));
            }
        }
    }
}
