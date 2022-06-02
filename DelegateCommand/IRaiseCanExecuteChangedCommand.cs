// Decompiled with JetBrains decompiler
// Type: Microsoft.Practices.Prism.Commands.IRaiseCanExecuteChangedCommand
// Assembly: Ascon.Pilot.SDK.GraphicLayerSample.ext2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 187B3BB9-3768-4B7C-861E-6A56C03BF53E
// Assembly location: D:\Projects\Pilot-ICE\SDK\b396a650-48de-48bb-bf68-8ed251a97fbe\Ascon.Pilot.SDK.GraphicLayerSample.ext2.dll

using System.Windows.Input;

namespace Microsoft.Practices.Prism.Commands
{
  public interface IRaiseCanExecuteChangedCommand : ICommand
  {
    void RaiseCanExecuteChanged();
  }
}
