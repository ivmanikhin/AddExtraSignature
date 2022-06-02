// Decompiled with JetBrains decompiler
// Type: Microsoft.Practices.Prism.Commands.DelegateCommandBase
// Assembly: Ascon.Pilot.SDK.GraphicLayerSample.ext2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 187B3BB9-3768-4B7C-861E-6A56C03BF53E
// Assembly location: D:\Projects\Pilot-ICE\SDK\b396a650-48de-48bb-bf68-8ed251a97fbe\Ascon.Pilot.SDK.GraphicLayerSample.ext2.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Media;

namespace Microsoft.Practices.Prism.Commands
{
  public abstract class DelegateCommandBase : 
    INotifyPropertyChanged,
    IRaiseCanExecuteChangedCommand,
    ICommand/*,
    IActiveAware*/
  {
    private readonly Action<object> _executeMethod;
    private readonly Func<object, bool> _canExecuteMethod;
    private bool _isActive;
    private List<WeakReference> _canExecuteChangedHandlers;
    private string _caption;
    private string _hint;
    private string _inputGestureText;
    private ImageSource _icon;
    private bool _isBeta;

    protected DelegateCommandBase(Action<object> executeMethod, Func<object, bool> canExecuteMethod)
    {
      this._executeMethod = executeMethod != null && canExecuteMethod != null ? executeMethod : throw new ArgumentNullException(nameof (executeMethod));
      this._canExecuteMethod = canExecuteMethod;
    }

    public bool IsActive
    {
      get => this._isActive;
      set
      {
        if (this._isActive == value)
          return;
        this._isActive = value;
        this.OnIsActiveChanged();
      }
    }

    public string Caption
    {
      get => this._caption;
      set
      {
        this._caption = value;
        this.OnPropertyChanged(nameof (Caption));
      }
    }

    public string Hint
    {
      get => this._hint;
      set
      {
        this._hint = value;
        this.OnPropertyChanged(nameof (Hint));
      }
    }

    public string InputGestureText
    {
      get => this._inputGestureText;
      set
      {
        this._inputGestureText = value;
        this.OnPropertyChanged(nameof (InputGestureText));
      }
    }

    public ImageSource Icon
    {
      get => this._icon;
      set
      {
        this._icon = value;
        this.OnPropertyChanged(nameof (Icon));
      }
    }

    protected virtual void OnCanExecuteChanged() => WeakEventHandlerManager.CallWeakReferenceHandlers((object) this, this._canExecuteChangedHandlers);

    public void RaiseCanExecuteChanged() => this.OnCanExecuteChanged();

    public virtual event EventHandler IsActiveChanged;

    protected virtual void OnIsActiveChanged()
    {
      EventHandler isActiveChanged = this.IsActiveChanged;
      if (isActiveChanged == null)
        return;
      isActiveChanged((object) this, EventArgs.Empty);
    }

    void ICommand.Execute(object parameter) => this.Execute(parameter);

    bool ICommand.CanExecute(object parameter) => this.CanExecute(parameter);

    protected void Execute(object parameter) => this._executeMethod(parameter);

    protected bool CanExecute(object parameter) => this._canExecuteMethod == null || this._canExecuteMethod(parameter);

    public event EventHandler CanExecuteChanged
    {
      add => WeakEventHandlerManager.AddWeakReferenceHandler(ref this._canExecuteChangedHandlers, value, 2);
      remove => WeakEventHandlerManager.RemoveWeakReferenceHandler(this._canExecuteChangedHandlers, value);
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
      PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
      if (propertyChanged == null)
        return;
      propertyChanged((object) this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
