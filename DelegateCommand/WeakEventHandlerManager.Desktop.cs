// Decompiled with JetBrains decompiler
// Type: Microsoft.Practices.Prism.Commands.WeakEventHandlerManager
// Assembly: Ascon.Pilot.SDK.GraphicLayerSample.ext2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 187B3BB9-3768-4B7C-861E-6A56C03BF53E
// Assembly location: D:\Projects\Pilot-ICE\SDK\b396a650-48de-48bb-bf68-8ed251a97fbe\Ascon.Pilot.SDK.GraphicLayerSample.ext2.dll

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Threading;

namespace Microsoft.Practices.Prism.Commands
{
  public static class WeakEventHandlerManager
  {
    public static void CallWeakReferenceHandlers(object sender, List<WeakReference> handlers)
    {
      if (handlers == null)
        return;
      EventHandler[] callees = new EventHandler[handlers.Count];
      int count = 0;
      int num = WeakEventHandlerManager.CleanupOldHandlers(handlers, callees, count);
      for (int index = 0; index < num; ++index)
        WeakEventHandlerManager.CallHandler(sender, callees[index]);
    }

    private static void CallHandler(object sender, EventHandler eventHandler)
    {
      WeakEventHandlerManager.DispatcherProxy dispatcher = WeakEventHandlerManager.DispatcherProxy.CreateDispatcher();
      if (eventHandler == null)
        return;
      if (dispatcher != null && !dispatcher.CheckAccess())
        dispatcher.BeginInvoke((Delegate) new Action<object, EventHandler>(WeakEventHandlerManager.CallHandler), sender, (object) eventHandler);
      else
        eventHandler(sender, EventArgs.Empty);
    }

    private static int CleanupOldHandlers(
      List<WeakReference> handlers,
      EventHandler[] callees,
      int count)
    {
      for (int index = handlers.Count - 1; index >= 0; --index)
      {
        if (!(handlers[index].Target is EventHandler target))
        {
          handlers.RemoveAt(index);
        }
        else
        {
          callees[count] = target;
          ++count;
        }
      }
      return count;
    }

    public static void AddWeakReferenceHandler(
      ref List<WeakReference> handlers,
      EventHandler handler,
      int defaultListSize)
    {
      if (handlers == null)
        handlers = defaultListSize > 0 ? new List<WeakReference>(defaultListSize) : new List<WeakReference>();
      handlers.Add(new WeakReference((object) handler));
    }

    public static void RemoveWeakReferenceHandler(
      List<WeakReference> handlers,
      EventHandler handler)
    {
      if (handlers == null)
        return;
      for (int index = handlers.Count - 1; index >= 0; --index)
      {
        if (!(handlers[index].Target is EventHandler target) || target == handler)
          handlers.RemoveAt(index);
      }
    }

    private class DispatcherProxy
    {
      private Dispatcher _innerDispatcher;

      private DispatcherProxy(Dispatcher dispatcher) => this._innerDispatcher = dispatcher;

      public static WeakEventHandlerManager.DispatcherProxy CreateDispatcher() => Application.Current == null ? (WeakEventHandlerManager.DispatcherProxy) null : new WeakEventHandlerManager.DispatcherProxy(Application.Current.Dispatcher);

      public bool CheckAccess() => this._innerDispatcher.CheckAccess();

      public DispatcherOperation BeginInvoke(
        Delegate method,
        params object[] args)
      {
        return this._innerDispatcher.BeginInvoke(method, DispatcherPriority.Normal, args);
      }
    }
  }
}
