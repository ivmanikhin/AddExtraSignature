// Decompiled with JetBrains decompiler
// Type: Microsoft.Practices.Prism.Commands.DelegateCommand`1
// Assembly: Ascon.Pilot.SDK.GraphicLayerSample.ext2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 187B3BB9-3768-4B7C-861E-6A56C03BF53E
// Assembly location: D:\Projects\Pilot-ICE\SDK\b396a650-48de-48bb-bf68-8ed251a97fbe\Ascon.Pilot.SDK.GraphicLayerSample.ext2.dll

using System;

namespace Microsoft.Practices.Prism.Commands
{
  public class DelegateCommand<T> : DelegateCommandBase
  {
    public DelegateCommand(Action<T> executeMethod)
      : this(executeMethod, (Func<T, bool>) (o => true))
    {
    }

    public DelegateCommand(Action<T> executeMethod, Func<T, bool> canExecuteMethod)
      : base((Action<object>) (o => executeMethod((T) o)), (Func<object, bool>) (o => canExecuteMethod((T) o)))
    {
      if (executeMethod == null || canExecuteMethod == null)
        throw new ArgumentNullException(nameof (executeMethod));
      Type type = typeof (T);
      if (type.IsValueType && (!type.IsGenericType || !typeof (Nullable<>).IsAssignableFrom(type.GetGenericTypeDefinition())))
        throw new InvalidCastException();
    }

    public bool CanExecute(T parameter) => this.CanExecute((object) parameter);

    public void Execute(T parameter) => this.Execute((object) parameter);
  }
}
