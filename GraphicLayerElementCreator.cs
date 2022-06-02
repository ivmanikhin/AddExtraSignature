// Decompiled with JetBrains decompiler
// Type: Ascon.Pilot.SDK.GraphicLayerSample.GraphicLayerElementCreator
// Assembly: Ascon.Pilot.SDK.GraphicLayerSample.ext2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 187B3BB9-3768-4B7C-861E-6A56C03BF53E
// Assembly location: D:\Projects\Pilot-ICE\SDK\b396a650-48de-48bb-bf68-8ed251a97fbe\Ascon.Pilot.SDK.GraphicLayerSample.ext2.dll

using System;
using System.Globalization;
using System.Windows;
using System.Xml.Linq;

namespace Ascon.Pilot.SDK.GraphicLayerSample
{
  public class GraphicLayerElementCreator
  {
    public const string TextBlockSample1 = "<TextBlock Foreground=\"Blue\" FontSize=\"20\" TextAlignment=\"Center\">АО ССМО<LineBreak />«ГорСпецСму»<LineBreak /><LineBreak /><LineBreak />В ПРОИЗВОДСТВО РАБОТ</TextBlock>";
    public const string TextBlockSample2 = "<TextBlock Foreground=\"Red\" FontSize=\"20\">{0}</TextBlock>";
    public const string PathSample = "<Path Data=\"M 77.95,18.45 L 77.95,312.15 L 378.25,112.15 L 578.25,18.45 Z \"  Stroke=\"#FF087000\" StrokeThickness=\"8\" StrokeStartLineCap=\"Round\"  StrokeEndLineCap=\"Round\" StrokeDashCap=\"Round\" StrokeMiterLimit=\"8\"></Path>";

    public static GraphicLayerElement Create(
      double xOffsetFromSettings,
      double yOffsetFromSettings,
      Point scale,
      double angle,
      int position,
      VerticalAlignment verticalAlignment,
      HorizontalAlignment horizontalAlignment,
      string contentType,
      Guid elementId,
      int pageNumber,
      bool isFloating)
    {
      int dpi = 96;
      double offsetX = xOffsetFromSettings / 25.4 * (double) dpi;
      double offsetY = yOffsetFromSettings / 25.4 * (double) dpi;
      Guid contentId = Guid.NewGuid();
      return new GraphicLayerElement(elementId, contentId, offsetX, offsetY, position, scale, angle, verticalAlignment, horizontalAlignment, contentType, pageNumber - 1, isFloating);
    }

    public static XElement CreateStamp1() => XElement.Parse(string.Format("<TextBlock Foreground=\"Blue\" FontSize=\"20\" TextAlignment=\"Center\">АО ССМО<LineBreak />«ГорСпецСму»<LineBreak /><LineBreak /><LineBreak />В ПРОИЗВОДСТВО РАБОТ</TextBlock>"));

    public static XElement CreateStamp2() => XElement.Parse(string.Format("<TextBlock Foreground=\"Red\" FontSize=\"20\">{0}</TextBlock>", (object) string.Format((IFormatProvider) CultureInfo.CurrentUICulture, "{0:dd MMM yyyy}", (object) DateTime.Now)));

    public static XElement CreateStampWithDateTime() => XElement.Parse(string.Format("<TextBlock Foreground=\"Red\" FontSize=\"20\">{0}</TextBlock>", (object) DateTime.Now.ToString((IFormatProvider) CultureInfo.CurrentCulture)));
  }
}
