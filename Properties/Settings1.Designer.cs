// Decompiled with JetBrains decompiler
// Type: Ascon.Pilot.SDK.GraphicLayerSample.Properties.Settings
// Assembly: Ascon.Pilot.SDK.GraphicLayerSample.ext2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 187B3BB9-3768-4B7C-861E-6A56C03BF53E
// Assembly location: D:\Projects\Pilot-ICE\SDK\b396a650-48de-48bb-bf68-8ed251a97fbe\Ascon.Pilot.SDK.GraphicLayerSample.ext2.dll

using System;
using System.CodeDom.Compiler;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Ascon.Pilot.SDK.GraphicLayerSample.Properties
{
    [CompilerGenerated]
    [GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "15.9.0.0")]
    internal sealed class Settings : ApplicationSettingsBase
    {
        private static Settings defaultInstance = (Settings)SettingsBase.Synchronized((SettingsBase)new Settings());

        public static Settings Default
        {
            get
            {
                Settings defaultInstance = Settings.defaultInstance;
                return defaultInstance;
            }
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("")]
        public string Path
        {
            get => (string)this[nameof(Path)];
            set => this[nameof(Path)] = (object)value;
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("0")]
        public string X
        {
            get => (string)this[nameof(X)];
            set => this[nameof(X)] = (object)value;
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("0")]
        public string Y
        {
            get => (string)this[nameof(Y)];
            set => this[nameof(Y)] = (object)value;
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("Top")]
        public string VerticalAligment
        {
            get => (string)this[nameof(VerticalAligment)];
            set => this[nameof(VerticalAligment)] = (object)value;
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("Left")]
        public string HorizontalAligment
        {
            get => (string)this[nameof(HorizontalAligment)];
            set => this[nameof(HorizontalAligment)] = (object)value;
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("1")]
        public string Scale
        {
            get => (string)this[nameof(Scale)];
            set => this[nameof(Scale)] = (object)value;
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("0")]
        public string Angle
        {
            get => (string)this[nameof(Angle)];
            set => this[nameof(Angle)] = (object)value;
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("False")]
        public bool IncludeStamp
        {
            get => (bool)this[nameof(IncludeStamp)];
            set => this[nameof(IncludeStamp)] = (object)value;
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("00000000-0000-0000-0000-000000000000")]
        public Guid UniqId
        {
            get => (Guid)this[nameof(UniqId)];
            set => this[nameof(UniqId)] = (object)value;
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("1")]
        public string PageNumber
        {
            get => (string)this[nameof(PageNumber)];
            set => this[nameof(PageNumber)] = (object)value;
        }
    }
}
