// Decompiled with JetBrains decompiler
// Type: Ascon.Pilot.SDK.GraphicLayerSample.GraphicLayerElement
// Assembly: Ascon.Pilot.SDK.GraphicLayerSample.ext2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 187B3BB9-3768-4B7C-861E-6A56C03BF53E
// Assembly location: D:\Projects\Pilot-ICE\SDK\b396a650-48de-48bb-bf68-8ed251a97fbe\Ascon.Pilot.SDK.GraphicLayerSample.ext2.dll

using System;
using System.Windows;

namespace Ascon.Pilot.SDK.GraphicLayerSample
{
  [Serializable]
  public class GraphicLayerElement : IGraphicLayerElement
  {
    public Guid ElementId { get; set; }

    public Guid ContentId { get; set; }

    public double OffsetY { get; set; }

    public double OffsetX { get; set; }

    public Point Scale { get; set; }

    public double Angle { get; set; }

    public int PositionId { get; set; }

    public int PageNumber { get; set; }

    public Point CornerPoint { get; set; }

    public VerticalAlignment VerticalAlignment { get; set; }

    public HorizontalAlignment HorizontalAlignment { get; set; }

    public string ContentType { get; set; }

    public bool IsFloating { get; set; }

    public GraphicLayerElement()
    {
    }

    public GraphicLayerElement(
      Guid elementId,
      Guid contentId,
      double offsetX,
      double offsetY,
      int positionId,
      Point scale,
      double angle,
      VerticalAlignment verticalAlignment,
      HorizontalAlignment horizontalAlignment,
      string contentType,
      int pageNumber,
      bool isFloating)
    {
      if (pageNumber < 0)
        throw new ArgumentOutOfRangeException(nameof (pageNumber), (object) pageNumber, "pageNumber must be greater than or equal to 0");
      this.ElementId = elementId;
      this.ContentId = contentId;
      this.OffsetX = offsetX;
      this.OffsetY = offsetY;
      this.Scale = scale;
      this.Angle = angle;
      this.PositionId = positionId;
      this.VerticalAlignment = verticalAlignment;
      this.HorizontalAlignment = horizontalAlignment;
      this.ContentType = contentType;
      this.PageNumber = pageNumber;
      this.IsFloating = isFloating;
    }

    public string GetFileName() => "PILOT_GRAPHIC_LAYER_ELEMENT_" + (object) this.ElementId;

    public string GetContentFileName() => "PILOT_CONTENT_GRAPHIC_LAYER_ELEMENT_" + (object) this.ContentId;
  }
}
