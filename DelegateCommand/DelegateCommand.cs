// Decompiled with JetBrains decompiler
// Type: Microsoft.Practices.Prism.Commands.DelegateCommand
// Assembly: Ascon.Pilot.SDK.GraphicLayerSample.ext2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 187B3BB9-3768-4B7C-861E-6A56C03BF53E
// Assembly location: D:\Projects\Pilot-ICE\SDK\b396a650-48de-48bb-bf68-8ed251a97fbe\Ascon.Pilot.SDK.GraphicLayerSample.ext2.dll

using System;

namespace Microsoft.Practices.Prism.Commands
{
  public class DelegateCommand : DelegateCommandBase
  {
    public DelegateCommand(Action executeMethod)
      : this(executeMethod, (Func<bool>) (() => true))
    {
    }

    public DelegateCommand(Action executeMethod, Func<bool> canExecuteMethod)
      : base((Action<object>) (o => executeMethod()), (Func<object, bool>) (o => canExecuteMethod()))
    {
      if (executeMethod == null || canExecuteMethod == null)
        throw new ArgumentNullException(nameof (executeMethod));
    }

    public void Execute() => this.Execute((object) null);

    public bool CanExecute() => this.CanExecute((object) null);
  }
}
